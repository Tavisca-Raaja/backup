using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tavisca.LogsVisualizer.ElasticSearch.Models;
using Tavisca.LogsVisualizer.ElasticSearch.Translator;
using Tavisca.LogsVisualizer.Model.Interfaces;
using Tavisca.LogsVisualizer.Model.Models;

namespace Tavisca.LogsVisualizer.ElasticSearch
{
    public class ElasticSearchLogsMetaDataService : IElasticSearchLogsMetaDataService
     {
        ElasticClient client;
        public ElasticSearchLogsMetaDataService()
        {
          this.client = ElasticSearchClient.GetClient("http://logs.qa.oski.io/");
        }
        public MetaData GetMetaData(string CorrelationId)
        {

            if (CorrelationId == null)
                return null;
            ElasticSearchMetaData esMetaData = new ElasticSearchMetaData();
            MetaData metaData = new MetaData();
            List<long> applicationNames = new List<long>();
            var searchResponse = client.Search<ElasticSearchMetaData>(s => s.AllIndices()
                                                               .Type("logging")
                                                               .TypedKeys(null)
                                                               .Source(sf => sf.Includes(i => i.Fields(("json_rq_headers.oski-clientId"),
                                                                                                       ("json_rq_headers.oski-clientProgramGroupId"),
                                                                                                       ("json_rq_headers.oski-programId"),
                                                                                                       ("json_rq_headers.oski-tenantId"))))
                                                               .Aggregations(a => a.Terms("GettingApplicationNames", t => t.Field("pid")))
                                                               .Query(q => q.MatchPhrase(ma => ma.Field("cid")
                                                                                                .Query(CorrelationId)))
                                                              .Size(1));
            if (searchResponse.Documents.Count == 0)
                return null;

            var applicationNameAggregations = searchResponse.Aggregations.Terms("GettingApplicationNames");
            foreach (var key in applicationNameAggregations.Buckets)
            {
              applicationNames.Add(long.Parse(key.Key));
            }
            esMetaData.AppNames = applicationNames;
            esMetaData.RequestHeaders= searchResponse.Documents.ToList()[0].RequestHeaders;
            metaData = esMetaData.ToModel();
            return metaData;
            
        }

        public Log GetById(string id)
        {
           /// LogFormat format = new LogFormat();
            dynamic searchResponse = client.Search<Object>(s => s.TypedKeys(null).
                                                        AllIndices()
                                                       .AllTypes()
                                                       .Query(q => q.MatchPhrase(m => m.Field("_id")
                                                                                         .Query(id))));

            var str = searchResponse.Documents[0].ToString();
           Dictionary<string,object> values = JsonConvert.DeserializeObject<Dictionary<string, object>>(str);
            
            return values.ConvertToLog();

        }
     }
}

//var values2 = new Dictionary<string, object>();
//foreach (KeyValuePair<string, object> d in values)
//{
//    values2.Add(d.Key, d.Value);
//}


//var Dic = JsonConvert.DeserializeObject<KeyValuePair<string, string>>();
//.ConvertAll<Dictionary<string, object>>(x=> JsonConvert.DeserializeObject<Dictionary<string,Object>>(x.ToString()));
//var json = JsonConvert.DeserializeObject<List<Dictionary<string,string>>>(str);
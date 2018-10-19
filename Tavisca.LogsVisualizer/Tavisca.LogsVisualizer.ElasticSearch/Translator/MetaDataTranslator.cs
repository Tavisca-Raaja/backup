using System;
using System.Collections.Generic;
using System.Text;
using Tavisca.LogsVisualizer.ElasticSearch.Models;
using Tavisca.LogsVisualizer.Model.Models;

namespace Tavisca.LogsVisualizer.ElasticSearch.Translator
{
    internal static class MetaDataTranslator
    {
        internal static MetaData ToModel(this ElasticSearchMetaData esMetaData)
        {
            MetaData metaData = new MetaData();
            metaData.AppNames = esMetaData.AppNames;
            if (esMetaData.RequestHeaders != null)
            {
                metaData.ClientId = esMetaData.RequestHeaders.ClientId;
                metaData.ClientProgramId = esMetaData.RequestHeaders.ClientProgramId;
                metaData.ProgramId = esMetaData.RequestHeaders.ProgramId;
                metaData.TenantId = esMetaData.RequestHeaders.TenantId;
            }
            return metaData;
        } 
    }
}

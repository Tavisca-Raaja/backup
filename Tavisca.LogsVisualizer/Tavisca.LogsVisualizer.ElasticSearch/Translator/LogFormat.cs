using System;
using System.Collections.Generic;
using System.Text;
using Tavisca.LogsVisualizer.Model.Models;

namespace Tavisca.LogsVisualizer.ElasticSearch.Translator
{
    public static class LogFormat
    {
        public static Log ConvertToLog(this Dictionary<string,object> check)
        {
            Log details = new Log();
            details.ApplicationName = check["app_name"].ToString();
            details.Id = check["id"].ToString();
            details.Type = check["type"].ToString();
            RemoveDuplicateFields(check,"app_name","id","type");
            details.LogData = check;
            return details;
        }

       private static void RemoveDuplicateFields(Dictionary<string, object> check,params string[] fields)
        {
            foreach (var field in fields)
                check.Remove(field);
            
        }

        
    }
}

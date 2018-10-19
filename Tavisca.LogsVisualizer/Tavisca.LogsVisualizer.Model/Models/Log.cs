using System;
using System.Collections.Generic;
using System.Text;

namespace Tavisca.LogsVisualizer.Model.Models
{
    public class Log
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string ApplicationName { get; set; }
        public Dictionary<string, object> LogData { get; set; }
    }
}

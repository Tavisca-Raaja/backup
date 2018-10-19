﻿using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tavisca.LogsVisualizer.Model.Models
    {
    public class MetaData
        {
        public string ClientId { get; set; }
        public string ClientProgramId { get; set; }
        public string TenantId { get; set; }
        public string ProgramId { get; set; }
        public List<long> AppNames { get; set; }
        }
    }

/*
    ILogDb : ESLogDB

    public IEnumerable<Log> GetLogs(string cid);

    public IEnumerable<Log> GetLogs(string cid, params string[] appNames);

    public IEnumerable<Log> GetLogs(string cid, params KeyValuePair<string, string> filters);
    
    public IEnumerable<Log> GetLogs(params KeyValuePair<string, string> filters);



*/

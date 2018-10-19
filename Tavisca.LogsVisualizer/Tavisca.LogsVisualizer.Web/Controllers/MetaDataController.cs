using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tavisca.LogsVisualizer.ElasticSearch;
using Tavisca.LogsVisualizer.Model.Models;

namespace Tavisca.LogsVisualizer.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetaDataController : ControllerBase
    {

        [HttpGet]
        public IActionResult getMetaData([FromQuery]string correlationId)
        {
            ElasticSearchLogsMetaDataService request = new ElasticSearchLogsMetaDataService(); 
            Log s=request.GetById(correlationId);
            if (s == null)
                return BadRequest();
            return Ok(s);
        }
    }
}
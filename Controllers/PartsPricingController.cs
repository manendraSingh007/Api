using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Api.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using System.Runtime.Serialization.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    
    public class PartsPricingController : ControllerBase
    {
        private readonly ILogger _logger;
        public IConfiguration Configuration { get; }
        private readonly IOptions<DoverConfig> appseting;
        public PartsPricingController(IOptions<DoverConfig> app, IConfiguration _configuration, ILogger<PartsPricingController> logger)
        {
            _logger = logger;
            appseting = app;
            Configuration = _configuration;
        }
        [HttpGet]
        public IActionResult Get() 
        {
            return Ok("APi Is working;");
        }
        [Authorize]
        [Route("{PartList}")]
        [Consumes("application/json")]
        [HttpPost(Name = "PartsPrice")]
        public IActionResult PartsPrice([FromBody]List<PartList> PartList)
        {
            
             var msg = new Message<PartPrice>();
            msg.Data = new List<PartPrice>();
           
            try { 
            if (PartList == null)
            {
                throw new ArgumentNullException();
            } else {
                    msg.IsSuccess = true;
                    msg.StatusCode = "200";
                    msg.ReturnMessage = "Success";
                    foreach (var Parts in PartList)
                {

                        if (Parts != null)
                        {
                            var Connection = Configuration.GetSection("DoverConfig").GetSection("DbConnection");
                            List<PartPrice> result = DbClientFactory<DoverDBClient>.Instance.GetPriceOutput(Parts, Connection.Value.ToString()).ToList();
                            if (result.Count > 0)
                            {
                                foreach (var Output in result) { 
                               
                                    Output.buName = Parts.buName;
                                    Output.invoiceNumber = Parts.invoiceNumber.ToString();
                                    //Output.materialNumber = Parts.materialNumber;
                                    Output.dealerNumber = Parts.dealerNumber;
                                    msg.Data.Add(Output);
                               
                            }
                        }
                    }
                    else
                    {
                        msg.IsSuccess = false;
                        msg.ReturnMessage = "Input Parameter is null";
                        msg.Data = null;
                    }
                    
                }
            } }

            catch (Exception ex)
            {
               
                msg.IsSuccess = false;
                msg.ReturnMessage = "Server Error";
                msg.Data = null;
                msg.StatusCode = "500";
                msg.Error = ex.Message;
                var data = this.HttpContext.Response.StatusCode;
                _logger.LogInformation("=============================================");
                _logger.LogInformation(ex.Message);
                _logger.LogError(ex.Message);
                return new JsonResult(msg);
            }
            return Ok(msg);

        }

    }
}
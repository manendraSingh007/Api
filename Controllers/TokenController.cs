using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using Api.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ILogger _logger;
        public IConfiguration Configuration { get; }
        public readonly IOptions<DoverConfig> appseting;
        public TokenController(IOptions<DoverConfig> app, IConfiguration _configuration, ILogger<TokenController> logger)
        {
            appseting = app;
            Configuration = _configuration;
            _logger = logger;
        }
        [HttpGet(Name = "GetToken")]
        [Route("{GetToken}")]
        [Consumes("application/json")]
        public async Task<JsonResult> Get([FromBody]GetToken token)
        {
            try {
                _logger.LogInformation("Get Token on {0}",DateTime.Now);
                var IdentityURL = Configuration.GetSection("DoverConfig").GetSection("IdentityURL");
            var scops = Configuration.GetSection("DoverConfig").GetSection("Audience");
            HttpClient client = new HttpClient();

            DiscoveryDocumentResponse disco = await client.GetDiscoveryDocumentAsync(IdentityURL != null ? IdentityURL.Value.ToString() : "");
            if (disco.IsError)
            {

                return new JsonResult("Server Not responding..");
            }

            TokenResponse tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = token.ClientId != null ? token.ClientId : "client1",
                ClientSecret = token.ClientSecret != null ? token.ClientSecret : "secret1",

                Scope = scops != null ? scops.Value.ToString() : "api11"
            });

            if (tokenResponse.IsError)
            {

                return new JsonResult("Error, while generation the Token..");
            }
            return new JsonResult(tokenResponse.AccessToken);
        }
            catch(Exception ex) 
            {
                _logger.LogInformation(ex.Message);
                _logger.LogError(ex.Message);
                return new JsonResult("Error");
            }
        }
        
    }
}
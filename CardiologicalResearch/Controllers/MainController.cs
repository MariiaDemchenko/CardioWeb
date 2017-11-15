using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using CardiologicalResearch.Models;
using Newtonsoft.Json;

namespace CardiologicalResearch.Controllers
{
    public class MainController : ApiController
    {
        [HttpGet]
        [Route("api/v1")]
        public string Get()
        {
            return string.Empty;
        }

        [HttpPost]
        [Route("api/v1")]
        public async Task<HttpResponseMessage> Post()
        {
            var content = await Request.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(content))
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { error = new { code = -32700, message = "Parse error" } });
            }

            var methodRequest = JsonConvert.DeserializeObject<MethodRequest<Models.IdParams>>(content);
            if (methodRequest == null || string.IsNullOrEmpty(methodRequest.Method))
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { error = new { code = -32700, message = "Parse error" } });
            }

            HttpClient client = new HttpClient(){ BaseAddress = new Uri("http://localhost:49391/api/v1/") };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var method = methodRequest.Method;
            if (method == "login")
            {
                method = "auth";
            }
            else if (method.StartsWith("measurements"))
            {
                method = "measurements";
            }
            else if (method.StartsWith("medicalRecords"))
            {
                method = "medicalRecords";
            }
            else if (method.StartsWith("examinations"))
            {
                method = "examinations";
            }
            else if (method.StartsWith("examinationResults"))
            {
                method = "examinationResults";
            }
            //else
            //{
            //    var itemSubstringIndex = method.LastIndexOf("Item", StringComparison.Ordinal);
            //    if (itemSubstringIndex > 0)
            //    {
            //        if (methodRequest.Params == null)
            //        {
            //            return Request.CreateResponse(HttpStatusCode.OK, new { error = new { code = -32700, message = "Parse error" } });
            //        }

            //        method = $"{method.Remove(itemSubstringIndex)}/{methodRequest.Params.Id}";
            //    }
            //}

            try
            {
                var postResult = await client.PostAsync(method, Request.Content);
                var response = System.Text.RegularExpressions.Regex.Unescape(await postResult.Content.ReadAsStringAsync());
                if (response.Length > 4)
                {
                    response = response.Substring(2, response.Length - 3);
                }

                return new HttpResponseMessage
                {
                    Content = new StringContent($"{{\"jsonrpc\":\"2.0\",\"id\":1,{response}", Encoding.UTF8, "application/json")
                };
            }
            catch (Exception e)
            {
                return new HttpResponseMessage
                {
                    Content = new StringContent($"{e.Message}{Environment.NewLine}{e.StackTrace}")
                };
            }
        }
    }
}

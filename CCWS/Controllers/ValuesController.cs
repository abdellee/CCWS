using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IBM.WatsonDeveloperCloud.CompareComply.v1;
using IBM.WatsonDeveloperCloud.Util;
using Microsoft.AspNetCore.Mvc;

namespace CCWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private string contract_a = @"CompareComplyV1/CompareComply.IT/CompareComplyTestData/contract_A.pdf";
        private string contract_b = @"CompareComplyV1/CompareComply.IT/CompareComplyTestData/contract_B.pdf";

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{fname}")]
        public ActionResult<string> Get(string fname)
        {
            string versionDate = "2018-11-12";
            CompareComplyService service;

            string endpoint = "https://gateway.watsonplatform.net/compare-comply/api";
            string apikey = "Iiilv-QMhc2BlSbpjSO1buqkrHLjYKMrO_Yy2y0h9LBL";
            TokenOptions tokenOptions = new TokenOptions()
            {
                ServiceUrl = endpoint,
                IamApiKey = apikey
            };

            service = new CompareComplyService(tokenOptions, versionDate);


            using (FileStream fs = System.IO.File.OpenRead(contract_a))
            {
                var elementClassificationResult = service.ClassifyElements(fs);

            }

            return fname;
        }
        //// GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

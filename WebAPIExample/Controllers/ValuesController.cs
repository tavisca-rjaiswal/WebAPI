using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace WebAPIExample.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        static List<string> values = new List<string>();

        // GET api/values
        [HttpGet]
        public ActionResult<List<string>> Get()
        {
            return values;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            if (values.Count - 1 < id)
                return "Index Out of bound";
            return values[id];
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] JObject req)
        {
            if (req.ContainsKey("value"))
                values.Add(req.SelectToken("value").ToString());
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<string> Put(int id, [FromBody]JObject req)
        {
            if (values.Count - 1 < id)
                return "Index Out of bound";
            if (req.ContainsKey("value"))
                values[id] = req.SelectToken("value").ToString();
            return "Success";
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            if (values.Count - 1 < id)
                return "Index Out of bound";
            values.RemoveAt(id);
            return "Success";
        }
    }
}

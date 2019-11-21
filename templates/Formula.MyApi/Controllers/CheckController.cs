using System;
using Microsoft.AspNetCore.Mvc;

namespace Formula.MyApi
{
    [ApiController]
    [Route("[controller]")]
    public class CheckController : Controller
    {
        [HttpGet]
        public String Get()
        {
            return "I'm alive!";
        }
    }
}

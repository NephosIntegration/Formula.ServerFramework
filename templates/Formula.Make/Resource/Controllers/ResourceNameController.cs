using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Formula.SimpleAPI;
using Formula.SimpleRepo;
using Formula.MyApi.Data.Repositories;
using Formula.MyApi.Data.Models;

namespace Formula.MyApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResourceNameController : ResourceControllerBase<ResourceNameController, ResourceName, ResourceNameRepository>
    {
        public ResourceNameController(ILogger<ResourceNameController> logger, ResourceNameRepository repository) : base(logger, repository)
        {
        }
    }
}

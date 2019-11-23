using System;
using Microsoft.Extensions.Configuration;
using Formula.SimpleRepo;
using Formula.MyApi.Data.Models;

namespace Formula.MyApi.Data.Repositories
{
    [Repo]
    public class ResourceNameRepository : RepositoryBase<ResourceName, ResourceName>
    {
        public ResourceNameRepository(IConfiguration config) : base (config)
        {
        }
    }
}
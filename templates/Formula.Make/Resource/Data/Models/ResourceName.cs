using System;
using Dapper;
using Formula.SimpleRepo;

namespace Formula.MyApi.Data.Models
{
    [ConnectionDetails("DefaultConnection")]
    [Table ("ResourceName")]
    public class ResourceName
    {
        [Key]
        public int Id { get; set; }
    }
}

using System.Net.Cache;
using Dapper.Contrib.Extensions;

namespace Models
{
    public class SensorType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
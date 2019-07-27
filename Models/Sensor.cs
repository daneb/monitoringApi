using System.Reflection;
using Dapper.Contrib.Extensions;

namespace Models
{
    public class Sensor
    {
        [Key]
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int SensorTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
using System.Reflection;

namespace Models
{
    public class Sensor
    {
        public int ProjectId { get; set; }
        public int SensorTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
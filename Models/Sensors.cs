using System.Reflection;

namespace Models
{
    public class Sensors
    {
        public int ID { get; set; }
        public int ProjectID { get; set; }
        public int SensorTypeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
using System.Net.Cache;

namespace Models
{
    public class SensorUserProjectPermissions
    {
        public int SensorId { get; set; }
        public int UserId { get; set; }
        public string Permission { get; set; }
    }
}
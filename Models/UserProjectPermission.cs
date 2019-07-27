using Dapper.Contrib.Extensions;

namespace Models
{
    public class UserProjectPermission
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public string PermissionContext { get; set; }
        public string Permission { get; set; }
    }
}
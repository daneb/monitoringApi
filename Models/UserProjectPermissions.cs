namespace Models
{
    public class UserProjectPermissions
    {
        public int  Id { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public string PermissionContext { get; set; }
        public string Permission { get; set; }
    }
}
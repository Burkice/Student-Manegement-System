namespace StudenProject.DomainModels
{
    public class Users
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int RoleId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }

        public Roles Role { get; set; }
    }
}

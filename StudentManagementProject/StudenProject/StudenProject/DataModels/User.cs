﻿namespace StudenProject.DataModels
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int RoleId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }

        public Role Role { get; set; }
    }
}

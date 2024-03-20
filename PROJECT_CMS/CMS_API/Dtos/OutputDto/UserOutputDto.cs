﻿namespace CMS_API.Dtos.OutputDto
{
    public class UserOutputDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public string Email { get; set; } = null!;
        public string? Country { get; set; }
        public string? Description { get; set; }
        public string? Role { get; set; }
    }
}

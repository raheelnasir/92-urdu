using System;

namespace Entities
{
    public class EntUserProfile 
    {
        public int UId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? City { get; set; }
        public string? Area { get; set; }
        public string? Location { get; set; }
        public bool IsActive { get; set; }
    }

    
}

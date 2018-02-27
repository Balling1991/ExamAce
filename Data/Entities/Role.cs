using Microsoft.AspNetCore.Identity;
using System;

namespace ExamAce.Data.Entities
{
    public class Role : IdentityRole<int>
    {
        public Role()
        {
        }

        public Role(string role)
            : base(role)
        {
        }

        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
    }
}

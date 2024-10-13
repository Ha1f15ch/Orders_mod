﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("User", Schema = "dbo")]
    public class User
    {
        public User() { }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

        public UserProfile UserProfile { get; set; }
        public List<Role> Roles { get; set; } = new();
        public List<UserRole> UserRoles { get; set; } = new();
        public List<OrderScores> OrderScores { get; set; } = new();
        public List<AssignersRequests> AssignersRequests { get; set; } = new();
    }
}

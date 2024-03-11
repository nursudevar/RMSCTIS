#nullable disable

using DataAccess_.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool IsActive { get; set; }

        public Statuses Status { get; set; }

        public Role Role { get; set; }

        public int RoleId { get; set; }

        public List<UserResource> UserResources { get; set; }

    }
}

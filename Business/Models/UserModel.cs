#nullable disable

using DataAccess_.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class UserModel 
    {

        #region

        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool isActive { get; set; }

        public Statuses Status { get; set; }

        public int RoleId { get; set; }


        #endregion

    }
}

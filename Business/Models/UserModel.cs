#nullable disable

using DataAccess_.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class UserModel 
    {

        #region

        public int Id { get; set; }

        [Required (ErrorMessage = "{0} is required")]
        [StringLength(40, MinimumLength = 2, ErrorMessage ="{0} must be minimum 2 characters")]
        public string UserName { get; set; }

        public string Password { get; set; }

        public bool isActive { get; set; }

        public Statuses Status { get; set; }

        public int RoleId { get; set; }



        #endregion


        #region Extra properties required for the views
        [DisplayName("Active")]
        public string IsActiveOutput { get; set; }

        [DisplayName("Role")]
        public string RoleNameOutput { get; set; }

        // for hiding password value from the user
        [DisplayName("Password")]
        public string PasswordOutput { get; set; }
        #endregion

    }
}

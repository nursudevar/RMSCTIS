﻿#nullable disable

using DataAccess_.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_.Entities
{
    public class UserResource : Record
    {
   
        public int UserId { get; set; }

        public User User { get; set; }


        public int ResourceId { get; set; }


        public Resource Resource { get; set; }


    }
}

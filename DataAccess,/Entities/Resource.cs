﻿#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_.Entities
{
    public  class Resource
    {


        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public decimal Score { get; set; }

        public DateTime? Date { get; set; }
        

        public List<UserResource> UserResources { get; set; }

    }
}

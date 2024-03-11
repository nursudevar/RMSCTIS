using DataAccess_.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_.Contexts
{
    public class Db : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<UserResource> UserResources { get; set; }


        public Db(DbContextOptions options) : base(options) { }
    }
}

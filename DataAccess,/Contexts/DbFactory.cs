﻿using DataAccess_.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccess.Contexts
{
    // Factory class that creates and enables the use of the Db object,
    // this class should be created for scaffolding operations.
    public class DbFactory : IDesignTimeDbContextFactory<Db>
    {
        public Db CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Db>();
            optionsBuilder.UseMySQL("Server=(localdb)\\mssqllocaldb;Database=RMSDB;Trusted_Connection=True;");
          
            // First, create an object containing the connection string of your database
            // (it's more suitable to use the development database).

            return new Db(optionsBuilder.Options);
            // Then, return an object of type Db using the optionsBuilder object we created above.
        }
    }
}
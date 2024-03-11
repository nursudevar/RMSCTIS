using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_.Results.Bases
{
    public abstract class Result
    {
       

        public bool IsSuccessfull { get; }
        public string? Message { get; }

        protected Result(bool isSuccessfull, string? message)
        {
            IsSuccessfull = isSuccessfull;
            Message = message;
        }

    }
}

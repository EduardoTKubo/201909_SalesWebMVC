using System;

namespace SalesWebMVC.Services.Exceptions
{
    public class DbConcurrencyException : ApplicationException
    {
        // construtor basico
        public DbConcurrencyException(string message): base(message)
        {
        }



    }
}

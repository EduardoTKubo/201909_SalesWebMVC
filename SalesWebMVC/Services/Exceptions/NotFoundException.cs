using System;
namespace SalesWebMVC.Services.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        // construtor basico
        public NotFoundException(string message) : base(message)
        {
            // recebe uma string (message) e repassa para a classe : base
        }



    }
}

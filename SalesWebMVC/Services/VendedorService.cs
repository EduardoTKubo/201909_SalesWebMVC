using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Services
{
    public class VendedorService
    {
        // a nossa classe VendedorService tem que ter uma depencia para o dbcontext ( data >> SalesWebMVCContext )
        // declarando esta dependencia
        private readonly SalesWebMVCContext _context;


        // contrutor para que a injecao de dependencia possa ocorrer
        public VendedorService (SalesWebMVCContext context)
        {
            _context = context;
        }


        // findall = retorna uma lista de todos os vendedores
        public List<Vendedor> FindAll()
        {
            return _context.Vendedor.ToList();
        }


    }
}

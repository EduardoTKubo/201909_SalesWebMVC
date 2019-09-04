using System.Linq;
using System.Collections.Generic;
using SalesWebMVC.Models;

namespace SalesWebMVC.Services
{
    public class DepartamentoService
    {
        // a nossa classe VendedorService tem que ter uma depencia para o dbcontext ( data >> SalesWebMVCContext )
        // declarando esta dependencia
        private readonly SalesWebMVCContext _context;


        // contrutor para que a injecao de dependencia possa ocorrer
        public DepartamentoService(SalesWebMVCContext context)
        {
            _context = context;
        }


        // retorna uma lista de Departamentos ordenado por nome
        public List<Departamento> FindAll()
        {
            return _context.Departamento.OrderBy(x => x.Nome).ToList();
        }


    }
}

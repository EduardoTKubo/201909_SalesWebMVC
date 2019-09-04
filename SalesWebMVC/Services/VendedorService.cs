using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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


        // inserir novo vendedor no banco de dados
        public void Insert(Vendedor obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }


        // busca o nome do vendedor informando o Id do vendedor
        public Vendedor FindByID(int id)
        {
            //return _context.Vendedor.FirstOrDefault(obj => obj.Id == id);

            // fazendo um join com a tabela Departamento ( eager loading )
            return _context.Vendedor.Include(obj => obj.Departamento).FirstOrDefault(obj => obj.Id == id);
        }


        // remover o vendedor ( id )
        public void Remove(int id)
        {
            var obj = _context.Vendedor.Find(id);
            _context.Vendedor.Remove(obj);
            _context.SaveChanges();
        }


    }
}

using SalesWebMVC.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Services.Exceptions;
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
        public VendedorService(SalesWebMVCContext context)
        {
            _context = context;
        }


        // findall = retorna uma lista de todos os vendedores
        public async Task<List<Vendedor>> FindAllAsync()
        {
            return await _context.Vendedor.ToListAsync();
        }


        // inserir novo vendedor no banco de dados
        public async Task InsertAsync(Vendedor obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }


        // busca o nome do vendedor informando o Id do vendedor
        public async Task<Vendedor> FindByIDAsync(int id)
        {
            //return _context.Vendedor.FirstOrDefault(obj => obj.Id == id);

            // fazendo um join com a tabela Departamento ( eager loading )
            //return _context.Vendedor.Include(obj => obj.Departamento).FirstOrDefault(obj => obj.Id == id);
            return await _context.Vendedor.Include(obj => obj.Departamento).FirstOrDefaultAsync(obj => obj.Id == id);
        }


        // remover o vendedor ( id )
        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Vendedor.FindAsync(id);
                _context.Vendedor.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }


        public async Task UpdateAsync(Vendedor obj)
        {
            // verifica se existe o id no banco de dados
            // se nao (!) existe vendedor no banco de dados com x.id  igual ao obj.id
            bool temAlgum = await _context.Vendedor.AnyAsync(x => x.Id == obj.Id);

            if (!temAlgum)
            {
                // lançar uma exceção com a mensagem : ...
                throw new NotFoundException("ID não encontrado");
            }

            try
            {
                // fazendo o update
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }


        }

    }
}

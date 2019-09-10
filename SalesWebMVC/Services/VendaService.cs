using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMVC.Services
{
    public class VendaService
    {
        // a nossa classe VendedorService tem que ter uma depencia para o dbcontext ( data >> SalesWebMVCContext )
        // declarando esta dependencia
        private readonly SalesWebMVCContext _context;


        // contrutor para que a injecao de dependencia possa ocorrer
        public VendaService(SalesWebMVCContext context)
        {
            _context = context;
        }

        public async Task<List<Venda>> FindByDateAsync(DateTime? minDate , DateTime? maxDate)
        {
            var result = from obj in _context.Venda select obj;
            if (minDate.HasValue)
            {
                // se a data minima foi informada 
                result = result.Where(x => x.DtVenda >= minDate.Value);
            }
            if (minDate.HasValue)
            {
                // se a data maxima foi informada 
                result = result.Where(x => x.DtVenda <= maxDate.Value);
            }

            return await result
                .Include(x => x.Vendedor)  // join com tabela Vendedor
                .Include(x => x.Vendedor.Departamento)  // join com tabela Departamento
                .OrderByDescending(x => x.DtVenda)
                .ToListAsync();
        }



        public async Task<List<IGrouping<Departamento,Venda>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.Venda select obj;
            if (minDate.HasValue)
            {
                // se a data minima foi informada 
                result = result.Where(x => x.DtVenda >= minDate.Value);
            }
            if (minDate.HasValue)
            {
                // se a data maxima foi informada 
                result = result.Where(x => x.DtVenda <= maxDate.Value);
            }

            return await result
                .Include(x => x.Vendedor)               // join com tabela Vendedor
                .Include(x => x.Vendedor.Departamento)  // join com tabela Departamento
                .OrderByDescending(x => x.DtVenda)
                .GroupBy(x => x.Vendedor.Departamento)  // agrupando
                .ToListAsync();
        }

    }
}

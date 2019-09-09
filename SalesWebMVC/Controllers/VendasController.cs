using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Services;

namespace SalesWebMVC.Controllers
{
    public class VendasController : Controller
    {
        private readonly VendaService _vendaService;

        //construtor
        public VendasController(VendaService vendaService)
        {
            _vendaService = vendaService;
        }



        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> BuscaSimples( DateTime? minDate ,DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                // se minDate nao foi informada ,minDate será igual ao 1º dia do ano
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                // se maxDate nao foi informada ,maxDate será a data atual
                maxDate = DateTime.Now;
            }

            // passando minDate e MaxDate para a view
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            var result = await _vendaService.FindByDateAsync(minDate, maxDate);
            return View(result);
        }

        public IActionResult BuscaAgrupada()
        {
            return View();
        }
    }
}
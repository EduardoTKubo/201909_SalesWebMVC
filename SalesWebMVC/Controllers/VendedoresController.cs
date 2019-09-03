using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Services;

namespace SalesWebMVC.Controllers
{
    public class VendedoresController : Controller
    {
        // declarando uma dependencia para o VendedorService
        private readonly VendedorService _vendedorService;

        // construtor para injetar a dependencia
        public VendedoresController (VendedorService vendedorService)
        {
            _vendedorService = vendedorService;
        }


        public IActionResult Index()
        {
            var list = _vendedorService.FindAll();
            return View(list);
        }


    }
}
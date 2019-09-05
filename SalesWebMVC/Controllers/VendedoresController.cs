using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;
using SalesWebMVC.Services.Exceptions;


namespace SalesWebMVC.Controllers
{
    public class VendedoresController : Controller
    {
        // declarando uma dependencia para o VendedorService
        private readonly VendedorService _vendedorService;

        // declarando uma dependencia para o DepartamentoService
        private readonly DepartamentoService _departamentoService;


        // construtor para injetar a dependencia
        public VendedoresController(VendedorService vendedorService, DepartamentoService departamentoService)
        {
            _vendedorService = vendedorService;
            _departamentoService = departamentoService;
        }


        public IActionResult Index()
        {
            var list = _vendedorService.FindAll();
            return View(list);
        }


        public IActionResult Create()
        {
            // carregar os todos os departamentos
            var departamentos = _departamentoService.FindAll();
            // instanciar um objeto do nosso viewModel
            var viewModel = new VendedorFormViewModel { Departamentos = departamentos };
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Vendedor vendedor)
        {
            _vendedorService.Insert(vendedor);
            // redirecionar para a pag inicial do crud : Index
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _vendedorService.FindByID(id.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _vendedorService.Remove(id);
            // redirecionar para a pag inicial do crud : Index
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _vendedorService.FindByID(id.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _vendedorService.FindByID(id.Value);
            if (obj == null)
            {
                return NotFound();
            }

            // abrir a tela de edição
            // 1º carregar lista de deparmantos
            List<Departamento> departamentos = _departamentoService.FindAll();

            VendedorFormViewModel viewModel = new VendedorFormViewModel { Vendedor = obj, Departamentos = departamentos };
            return View(viewModel);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Vendedor vendedor)
        {
            if (id != vendedor.Id)
            {
                return BadRequest();
            }
            try
            {
                // update
                _vendedorService.Update(vendedor);
                // redirecionar para a pag inicial do crud : Index
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (DbConcurrencyException)
            {
                return BadRequest();
            }

        }

    }
}
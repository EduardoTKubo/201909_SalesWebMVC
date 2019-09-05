using System;
using System.Collections.Generic;
using System.Diagnostics;
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


        public async Task<IActionResult> Index()
        {
            var list = await _vendedorService.FindAllAsync();
            return View(list);
        }


        public async Task<IActionResult> Create()
        {
            // carregar os todos os departamentos
            var departamentos = await _departamentoService.FindAllAsync();
            // instanciar um objeto do nosso viewModel
            var viewModel = new VendedorFormViewModel { Departamentos = departamentos };
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vendedor vendedor)
        {
            // testa se o modelo foi validado, ie, se o formulario foi preenchido integralmente
            if (!ModelState.IsValid)
            {
                var departamentos = await _departamentoService.FindAllAsync();
                var viewModel = new VendedorFormViewModel { Vendedor = vendedor, Departamentos = departamentos };
                return View(viewModel);
            }

            await _vendedorService.InsertAsync(vendedor);
            // redirecionar para a pag inicial do crud : Index
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error) ,new { message = "Id não fornecido" } );
            }

            var obj = await _vendedorService.FindByIDAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }

            return View(obj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _vendedorService.RemoveAsync(id);
            // redirecionar para a pag inicial do crud : Index
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não informado" });
            }

            var obj = await _vendedorService.FindByIDAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }

            return View(obj);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
            }

            var obj = await _vendedorService.FindByIDAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }

            // abrir a tela de edição
            // 1º carregar lista de deparmantos
            List<Departamento> departamentos = await _departamentoService.FindAllAsync();

            VendedorFormViewModel viewModel = new VendedorFormViewModel { Vendedor = obj, Departamentos = departamentos };
            return View(viewModel);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Vendedor vendedor)
        {
            // testa se o modelo foi validado, ie, se o formulario foi preenchido integralmente
            if (!ModelState.IsValid)
            {
                var departamentos = await _departamentoService.FindAllAsync();
                var viewModel = new VendedorFormViewModel { Vendedor = vendedor, Departamentos = departamentos };
                return View(viewModel);
            }

            if (id != vendedor.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não corresponde" });
            }
            try
            {
                // update
                await _vendedorService.UpdateAsync(vendedor);
                // redirecionar para a pag inicial do crud : Index
                return RedirectToAction(nameof(Index));
            }
            //catch (NotFoundException e)
            //{
            //    return RedirectToAction(nameof(Error), new { message = e.Message });
            //}
            //catch (DbConcurrencyException e)
            //{
            //    return RedirectToAction(nameof(Error), new { message = e.Message });
            //}
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }


        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }

    }
}
using System.Collections.Generic;

namespace SalesWebMVC.Models.ViewModels
{
    public class VendedorFormViewModel
    {
        // classe que contem os dados para o formulario de cadastro de Vendedor
        // Vendedor e uma lista de Departamentos 

        public Vendedor Vendedor { get; set; }
        public ICollection<Departamento> Departamentos { get; set; }
    }
}

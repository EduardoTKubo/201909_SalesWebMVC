using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMVC.Models
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        // associacao de Departamento com Vendedor
        public ICollection<Vendedor> Vendedores { get; set; } = new List<Vendedor>();

        // construtor sem argumento
        public Departamento()
        {
        }

        // construtor com argumento
        public Departamento(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public void IncluirVendedor(Vendedor V)
        {
            Vendedores.Add(V);
        }

        public double TotalDeVendasDoDepartamento(DateTime Ini ,DateTime Fim)
        {
            return Vendedores.Sum(Vendedor => Vendedor.TotalDeVendasDoVendedor(Ini, Fim));
        }
    }
}

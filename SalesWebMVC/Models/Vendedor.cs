using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace SalesWebMVC.Models
{
    public class Vendedor
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Dt Nasc")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DtNasc { get; set; }
        
        [Display(Name = "Salário Base")]
        [DisplayFormat(DataFormatString ="{0:F2}")]
        public double SalarioBase { get; set; }

        // associacao de vendedor com departamento
        public Departamento Departamento { get; set; }

        // chave estrangeira - foreign key 
        public int DepartamentoId { get; set; }

        // associacao de vendedor com vendas
        public ICollection<Venda> Vendas { get; set; } = new List<Venda>();



        // construtor sem argumento
        public Vendedor()
        {
        }

        // construtor com argumento
        public Vendedor(int id, string nome, string email, DateTime dtNasc, double salarioBase, Departamento departamento)
        {
            Id = id;
            Nome = nome;
            Email = email;
            DtNasc = dtNasc;
            SalarioBase = salarioBase;
            Departamento = departamento;
        }

        public void IncluirVenda(Venda Vd)
        {
            Vendas.Add(Vd);
        }

        public void RemoverVenda(Venda Vd)
        {
            Vendas.Remove(Vd);
        }

        // total de vendas de 1 vendedor
        public double TotalDeVendasDoVendedor( DateTime Ini ,DateTime Fim)
        {
            return Vendas.Where(Vd => Vd.DtVenda >= Ini && Vd.DtVenda <= Fim).Sum(Vd => Vd.Valor);                        
        }
    }
}

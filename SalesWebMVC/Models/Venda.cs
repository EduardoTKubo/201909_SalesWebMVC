using System;
using SalesWebMVC.Models.Enums;

namespace SalesWebMVC.Models
{
    public class Venda
    {
        public int Id { get; set; }
        public DateTime DtVenda { get; set; }
        public double Valor { get; set; }
        public StatusDaVenda Status { get; set; }

        // associacao de venda com vendedor
        public Vendedor Vendedor { get; set; }



        // construtor sem argumentos
        public Venda()
        {
        }

        // construtor com argumentos
        public Venda(int id, DateTime dtVenda, double valor, StatusDaVenda status, Vendedor vendedor)
        {
            Id = id;
            DtVenda = dtVenda;
            Valor = valor;
            Status = status;
            Vendedor = vendedor;
        }


    }
}

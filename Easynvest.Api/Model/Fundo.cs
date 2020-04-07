using Easynvest.Api.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easynvest.Api.Model
{
    public class Fundo
    {
        public Dados[] Fundos { get; set; }

        public class Dados : Calculo, ICalculo
        {
            public decimal CapitalInvestido { get; set; }
            public decimal ValorAtual { get; set; }
            public DateTime DataResgate { get; set; }
            public DateTime DataCompra { get; set; }
            public decimal Iof { get; set; }
            public string Nome { get; set; }
            public decimal TotalTaxas { get; set; }
            public decimal Quantity { get; set; }
            public decimal IR { get { return CalculaIR(); } }
            public decimal ValorResgate { get { return CalculaResgate(ValorAtual, CapitalInvestido, DataCompra, DataResgate); } }

            public decimal CalculaIR()
            {
                return CalculaRentabilidade(ValorAtual, CapitalInvestido) * 0.15m;
            }
        }
    }
}

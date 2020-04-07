using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easynvest.Api.Interface;

namespace Easynvest.Api.Model
{
    public class TesouroDireto
    {
        public Dados[] Tds { get; set; }

        public class Dados : Calculo, ICalculo
        {
            public decimal ValorInvestido { get; set; }
            public decimal ValorTotal { get; set; }
            public DateTime Vencimento { get; set; }
            public DateTime DataDeCompra { get; set; }
            public decimal Iof { get; set; }
            public string Indice { get; set; }
            public string Tipo { get; set; }
            public string Nome { get; set; }
            public decimal IR { get { return CalculaIR(); } }
            public decimal ValorResgate { get { return CalculaResgate(ValorTotal, ValorInvestido, DataDeCompra, Vencimento); } }

            public decimal CalculaIR()
            {
                return CalculaRentabilidade(ValorTotal, ValorInvestido) * 0.1m;
            }
        }
    }
}

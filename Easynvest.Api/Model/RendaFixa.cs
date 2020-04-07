using Easynvest.Api.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easynvest.Api.Model
{
    public class RendaFixa
    {
        public Dados[] Lcis { get; set; }

        public class Dados : Calculo, ICalculo
        {
            public decimal CapitalInvestido { get; set; }
            public decimal CapitalAtual { get; set; }
            public decimal Quantidade { get; set; }
            public DateTime Vencimento { get; set; }
            public decimal Iof { get; set; }
            public decimal OutrasTaxas { get; set; }
            public decimal Taxas { get; set; }
            public string Indice { get; set; }
            public string Tipo { get; set; }
            public string Nome { get; set; }
            public bool GuarantidoFGC { get; set; }
            public DateTime DataOperacao { get; set; }
            public decimal PrecoUnitario { get; set; }
            public bool Primario { get; set; }
            public decimal IR { get { return CalculaIR(); } }
            public decimal ValorResgate { get { return CalculaResgate(CapitalAtual, CapitalInvestido, DataOperacao, Vencimento); } }

            public decimal CalculaIR()
            {
                return CalculaRentabilidade(CapitalAtual, CapitalInvestido) * 0.05m;
            }
        }
    }
}

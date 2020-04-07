using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easynvest.Api.Model
{
    public abstract class Calculo
    {
        public decimal CalculaRentabilidade(decimal valorTotal, decimal valorInvestido) 
        {
            return valorTotal - valorInvestido;
        }

        public decimal CalculaResgate(decimal valorTotal, decimal valorInvestido, DateTime dataCompra, DateTime dataVencimento)
        {
            if (DateTime.Now >= dataVencimento.AddMonths(-3))
            {
                return valorTotal - (valorInvestido * 0.06m);
            }
            else if (DateTime.Now > dataCompra + (dataVencimento - dataCompra))
            {
                return valorTotal - (valorInvestido * 0.15m);
            }
            else 
            {
                return valorTotal - (valorInvestido * 0.3m);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easynvest.Api.Model
{
    public class Resultado
    {
        public decimal ValorTotal { get; set; }
        public List<Investimento> Investimentos { get; set; }
    }
}

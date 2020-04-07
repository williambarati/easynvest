using System;

namespace Easynvest.Api.Model
{
    public class Investimento
    {
        public string Nome { get; set; }
        public decimal ValorInvestido { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime Vencimento { get; set; }
        public decimal IR { get; set; }
        public decimal ValorResgate { get; set; }
    }
}

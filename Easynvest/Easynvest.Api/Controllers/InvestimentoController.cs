using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Easynvest.Api.Model;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;

namespace Easynvest.Api.Controllers
{
    /*
    Tesouro Direto
    http://www.mocky.io/v2/5e3428203000006b00d9632a
    Renda Fixa
    http://www.mocky.io/v2/5e3429a33000008c00d96336
    Fundos
    http://www.mocky.io/v2/5e342ab33000008c00d96342
    */

    [ApiController]
    [Route("[controller]")]
    public class InvestimentoController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        public InvestimentoController(IMemoryCache cache)
        {
            _cache = cache;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var cacheEntry = _cache.GetOrCreate("ResultadoCacheKey", entry =>
            {
                entry.AbsoluteExpiration = DateTime.Today.AddDays(1);
                entry.SetPriority(CacheItemPriority.High);

                return Ok(CalculaResultado());
            });

            return cacheEntry;
        }

        private Resultado CalculaResultado()
        {
            var resultado = new Resultado
            {
                Investimentos = new List<Investimento>()
            };

            using (var client = new HttpClient())
            {
                var response = client.GetAsync("http://www.mocky.io/v2/5e3428203000006b00d9632a").Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var tesouroDireto = JsonConvert.DeserializeObject<TesouroDireto>(response.Content.ReadAsStringAsync().Result);

                    resultado.ValorTotal += tesouroDireto.Tds.Sum(q => q.ValorTotal);
                    resultado.Investimentos.AddRange(
                        tesouroDireto.Tds.Select(q => new Investimento
                        {
                            Nome = q.Nome,
                            ValorInvestido = q.ValorInvestido,
                            ValorTotal = q.ValorTotal,
                            Vencimento = q.Vencimento,
                            IR = q.IR,
                            ValorResgate = q.ValorResgate
                        }).ToList());
                }

                response = client.GetAsync("http://www.mocky.io/v2/5e3429a33000008c00d96336").Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var rendaFixa = JsonConvert.DeserializeObject<RendaFixa>(response.Content.ReadAsStringAsync().Result);

                    resultado.ValorTotal += rendaFixa.Lcis.Sum(q => q.CapitalAtual);
                    resultado.Investimentos.AddRange(
                        rendaFixa.Lcis.Select(q => new Investimento
                        {
                            Nome = q.Nome,
                            ValorInvestido = q.CapitalInvestido,
                            ValorTotal = q.CapitalAtual,
                            Vencimento = q.Vencimento,
                            IR = q.IR,
                            ValorResgate = q.ValorResgate
                        }).ToList());
                }

                response = client.GetAsync("http://www.mocky.io/v2/5e342ab33000008c00d96342").Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var fundos = JsonConvert.DeserializeObject<Fundo>(response.Content.ReadAsStringAsync().Result);

                    resultado.ValorTotal += fundos.Fundos.Sum(q => q.ValorAtual);
                    resultado.Investimentos.AddRange(
                        fundos.Fundos.Select(q => new Investimento
                        {
                            Nome = q.Nome,
                            ValorInvestido = q.CapitalInvestido,
                            ValorTotal = q.ValorAtual,
                            Vencimento = q.DataResgate,
                            IR = q.IR,
                            ValorResgate = q.ValorResgate
                        }).ToList());
                }
            }

            return resultado;
        }
    }
}

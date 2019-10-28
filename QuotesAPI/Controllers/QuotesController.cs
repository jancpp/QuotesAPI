using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuotesAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuotesAPI.Controllers
{
    [Route("api/[controller]")]
    public class QuotesController : Controller
    {
        static List<Quote> quotes = new List<Quote>()
        {
            new Quote(){Id=0, Author="Einstein", Description="Imagination is more important than knoleadge", Title="Imagination" },
            new Quote(){Id=1, Author="Tom Hardy", Description="If we still alive in the morninig we'll know we are not dead", Title="Alive in morning" }
        };

        [HttpGet]
        public IEnumerable<Quote> Get()
        {
            return quotes;
        }

        [HttpPost]
        public void Post([FromBody] Quote quote)
        {
            quotes.Add(quote);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Quote quote)
        {
            quotes[id] = quote;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            quotes.RemoveAt(id);
        }
    }
}

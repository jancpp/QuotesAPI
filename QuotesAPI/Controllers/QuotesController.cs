using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuotesAPI.Data;
using QuotesAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuotesAPI.Controllers
{
    [Route("api/[controller]")]
    public class QuotesController : Controller
    {
        private QuotesDbContext _quotesDbContext;

        public QuotesController(QuotesDbContext quotesDbContext)
        {
            _quotesDbContext = quotesDbContext;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Quote> Get()
        {
            return _quotesDbContext.Quotes;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Quote Get(int id)
        {
            return _quotesDbContext.Quotes.Find(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Quote quote)
        {
            _quotesDbContext.Quotes.Add(quote);
            _quotesDbContext.SaveChanges();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Quote quote)
        {
            var entity = _quotesDbContext.Quotes.Find(id);

            entity.Title = quote.Title;
            entity.Author = quote.Author;
            entity.Description = quote.Description;
            _quotesDbContext.SaveChanges();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var quote = _quotesDbContext.Quotes.Find(id);
            _quotesDbContext.Quotes.Remove(quote);
            _quotesDbContext.SaveChanges();
        }
    }
}

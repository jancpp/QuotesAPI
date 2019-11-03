using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuotesAPI.Data;
using QuotesAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuotesAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class QuotesController : Controller
    {
        private QuotesDbContext _quotesDbContext;

        public QuotesController(QuotesDbContext quotesDbContext)
        {
            _quotesDbContext = quotesDbContext;
        }

        // GET: api/values
        [HttpGet]
        [AllowAnonymous]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public IActionResult Get(string sort)
        {
            IQueryable<Quote> quotes;
            switch (sort)
            {
                case "desc":
                    quotes = _quotesDbContext.Quotes.OrderByDescending(q => q.CreatedAt);
                    break;
                case "asc":
                    quotes = _quotesDbContext.Quotes.OrderBy(q => q.CreatedAt);
                    break;
                default:
                    quotes = _quotesDbContext.Quotes;
                    break;
            }
            return Ok(quotes);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var quote = _quotesDbContext.Quotes.Find(id);

            if (quote == null)
            {
                return NotFound("No record found against this id");
            }
            else
            {
                return Ok(quote);
            }
        }

        [HttpGet("[action]")]
        public IActionResult PagingQuote(int? pageNumber, int? pageSize)
        {
            var quotes = _quotesDbContext.Quotes;
            var currentPageSize = pageSize ?? 5;
            var currentPageNumber = pageNumber ?? 1;

            return Ok(quotes.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize));
        }

        [HttpGet("[action]")]
        public IActionResult SearchQuote(string type)
        {
            var quotes = _quotesDbContext.Quotes.Where(q => q.Type.StartsWith(type));
            return Ok(quotes);
        }

        [HttpGet("[action]")]
        public IActionResult MyQuote()
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var quotes = _quotesDbContext.Quotes.Where(q => q.UserId == userId);

            return Ok(quotes);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Quote quote)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            quote.UserId = userId;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _quotesDbContext.Quotes.Add(quote);
            _quotesDbContext.SaveChanges();

            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Quote quote)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = _quotesDbContext.Quotes.Find(id);
            if (entity == null)
            {
                return NotFound("No record found against this id");
            }
            // To allow update to users that created record (with the same token)
            if (entity.UserId != userId)
            {
                return BadRequest("Sorry, you are not allowed to update this record");
            }
            else
            {
                entity.Title = quote.Title;
                entity.Author = quote.Author;
                entity.Description = quote.Description;
                entity.CreatedAt = quote.CreatedAt;
                _quotesDbContext.SaveChanges();

                return Ok("Record updated successfuly.");
            }

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var quote = _quotesDbContext.Quotes.Find(id);

            if (quote == null)
            {
                return NotFound("No record found against this id");
            }
            // To allow delete only to users that created record (with the same token)
            if (quote.UserId != userId)
            {
                return BadRequest("Sorry, you are not allowed to delete this record");
            }
            else
            {
                _quotesDbContext.Quotes.Remove(quote);
                _quotesDbContext.SaveChanges();

                return Ok("Quote deleted");
            }
        }
    }
}

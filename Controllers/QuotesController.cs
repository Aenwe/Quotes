using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuotesApi21.Data;
using QuotesApi21.Models;

namespace QuotesApi21.Controllers
{   [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : Controller
    {


        QuotesDbContext _quotesDbContext;

        public QuotesController(QuotesDbContext quotesDbContext)
        {
            _quotesDbContext = quotesDbContext;
        }
        // GET: Quotes
        [HttpGet]
        public IEnumerable<Quote> Get()
        {
            return _quotesDbContext.Quotes;
            //return StatusCode(StatusCodes.Status200OK);
        }

        // GET: Quote
        [HttpGet("{id}", Name = "Get")]
        public Quote Get(int id)
        {
            var quote = _quotesDbContext.Quotes.Find(id);
            return quote;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Quote quote)
        {
            _quotesDbContext.Quotes.Add(quote);
            _quotesDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Quote quote)
        {
            var entity = _quotesDbContext.Quotes.Find(id);

            if (entity == null) {
                return NotFound("The selected record does not exist.");
            }
            entity.Title = quote.Title;
            entity.Author = quote.Author;
            entity.Description = quote.Description;
            _quotesDbContext.SaveChanges();
            //return StatusCode(StatusCodes.Status200OK);
            return Ok("Update completed");
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entity = _quotesDbContext.Quotes.Find(id);
            _quotesDbContext.Quotes.Remove(entity);
            _quotesDbContext.SaveChanges();
            return Ok("Record Deleted");
        }

    }
}
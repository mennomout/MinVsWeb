using Data;
using Microsoft.AspNetCore.Mvc;
using VolLiefdeWebApi.Filters;

namespace VolLiefdeWebApi.Controllers
{
    // New C# 12 language feature: Primary constructors:
    // Other constructors are required to call the primary constructor using "this(...)" syntax.
    public class BnBQuotesController(VolLiefdeRepository quoteRepository) : ControllerBase
    {
        private const string _baseRoute = "bnb/quotes/";
        private readonly VolLiefdeRepository _quoteRepository = quoteRepository;

        [HttpGet(_baseRoute)]
        public IActionResult Get()
        {
            return Ok(_quoteRepository.GetAll());
        }

        [HttpGet(_baseRoute + "{name}")]
        public IActionResult Get(string name)
        {
            if (_quoteRepository.ContainsQuoteBy(name))
            {
                return Ok(_quoteRepository.Get(name));
            }

            return NotFound();
        }

        [HttpPost(_baseRoute + "add/{name}")]
        public IActionResult Post(string name, QuoteModel quote)
        {
            if (!_quoteRepository.ContainsQuoteBy(name))
            {
                _quoteRepository.Add(name, quote);
                return Ok();
            }

            return BadRequest("Only one quote is allowed per participant.");
        }

        [ValidationHelper]
        [HttpDelete(_baseRoute + "delete/{name}")]
        public IActionResult Delete(string name)
        {
            if (_quoteRepository.ContainsQuoteBy(name))
            {
                _quoteRepository.Delete(name);
                return Ok();
            }

            return NotFound();
        }        
    }
}

using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace rest_api_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GreetingsController : ControllerBase
    {
        private static List<Greeting> _greetings = new List<Greeting>()
        {
            new Greeting() { Id: 1, Reciepent: "Jane" , Message: "Kauneid jõule!", Sender: "Liisa"},
            new Greeting() { Id: 2, Reciepent: "Mirjam Tõniste", Message: "Head uut aastat!", Sender: "Madis"},
            new Greeting() { Id: 3, Reciepent: "Kuno", Message: "Palju õnne sünnipäevaks!", Sender: "Alar Karis"}
        };

        // GET: api/<WidgetsController>
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(_greetings);
        }

        // GET api/<WidgetsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _greetings.Find(x=> x.Id == id);
            if (result == null)
            {
                return NotFound(new {Error="Greeting not found"});
            }
            return new JsonResult(result);
        }

        // POST api/<WidgetsController>
        [HttpPost]
        public IActionResult Post([FromBody] Greeting item)
        {
            var newId = _greetings.Last().Id;
            item.Id = newId;
            _greetings.Add(item);
            return CreatedAtAction(nameof(Get), new {id=newId}, item);
        }

        // PUT api/<WidgetsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Greeting item)
        {
            var result = _greetings.Find(x => x.Id == id);
            if (result == null)
            {
                return NotFound(new { Error = "Greeting not found" });
            }
            result.Reciepent = item.Reciepent;
            result.Message= item.Message;
            result.Sender = item.Sender;
            return AcceptedAtAction(nameof(Get), new { id = result.Id }, result);

        }

        // DELETE api/<WidgetsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _greetings.Find(x => x.Id == id);
            if (result == null)
            {
                return NotFound(new { Error = "Greeting not found" });
            }
            _greetings.Remove(result);
            return NoContent();
        }
    }
}

using Books.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Books.Controllers
{
    // Define the route for the API controller and specify that it is an API controller
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BooksContext _context; //should not be accessible outside the class for ensuring db-integrity

        //jab bhi BookController ka instance create hoga, we need to provide BookContext instance in arg (Dependency Injection)
        public BookController(BooksContext context)
        {
            // BookController will use _context to access and manipulate data
            _context = context;
        }

        // HTTP GET method to retrieve all books
        // GET: api/<BookController>
        [HttpGet]
        // Task<...> is a type represent asynchronous operation(contains the result)
        // ActionResult<...> represents the result of an Action Method in a controller
        //IEnumerable <...> is an Interface that represent collection (or list) of Book objects
        public async Task<ActionResult<IEnumerable<Book>>> Get()
        {
            return await _context.Books.ToListAsync(); // Return the list of books from the database
        }

        // HTTP GET method to retrieve a book by ID
        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> Get(int id)
        {
            var book = await _context.Books.FindAsync(id); // Find the book with the specified ID in the database

            if (book == null)
            {
                return NotFound(); // Return 404 if the book is not found
            }

            return book; // Return the found book
        }

        // HTTP POST method to add a new book
        // POST api/<BookController>
        [HttpPost]
        public async Task<ActionResult<Book>> Post([FromBody] Book value)
        {
            _context.Books.Add(value); // Add the new book to the database
            await _context.SaveChangesAsync(); // Save changes to the database

            return CreatedAtAction(nameof(Get), new { id = value.Id }, value); // Return the created book with a 201 status code
        }

        // HTTP PUT method to update an existing book by ID
        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Book value)
        {
            if (id != value.Id)
            {
                return BadRequest(); // Return 400 if the ID in the URL does not match the ID in the body
            }

            _context.Entry(value).State = EntityState.Modified; // Mark the book entity as modified

            try
            {
                await _context.SaveChangesAsync(); // Save changes to the database
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Books.Any(e => e.Id == id))
                {
                    return NotFound(); // Return 404 if the book does not exist
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // Return 204 No Content if the update is successful
        }

        // HTTP DELETE method to delete a book by ID
        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books.FindAsync(id); // Find the book with the specified ID in the database
            if (book == null)
            {
                return NotFound(); // Return 404 if the book is not found
            }

            _context.Books.Remove(book); // Remove the book from the database
            await _context.SaveChangesAsync(); // Save changes to the database

            return NoContent(); // Return 204 No Content if the delete is successful
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using BookAPI.Models;
using BookAPI.Data;
using BookAPI.Data.Dtos;

namespace BookAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{

   private BookContext _context;
   private IMapper _mapper;

   public BookController(BookContext context, IMapper mapper)
   {
        _context = context;
        _mapper = mapper;
   }

      [HttpPost]
      public IActionResult AddBook([FromBody] CreateBookDto bookDto)
      {
         Book book = _mapper.Map<Book>(bookDto);
         _context.Books.Add(book);
         _context.SaveChanges();
         return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
      }

      [HttpGet]
      public IActionResult ListAllBooks([FromQuery] int page = 1)
      {
         if(page > 0) {
            int skip = 10 * (page - 1);
            const int take = 10;
            return Ok( 
               _mapper.Map<List<ReadBookDto>>(_context.Books.Skip(skip).Take(take))
            );
         } else {
            return BadRequest();
         }
      }

      [HttpGet("{id}")]
      public IActionResult GetBookById(int id)
      {
         var book = _context.Books.FirstOrDefault(book => book.Id == id);
         if(book == null) return NotFound();
         return Ok(book);
      }

      [HttpPut("{id}")]
      public IActionResult UpdateBook(int id, [FromBody] UpdateBookDto bookDot)
      {
         var book = _context.Books.FirstOrDefault(book => book.Id == id);
         if(book == null) return NotFound();
         _mapper.Map(bookDot, book);
         _context.SaveChanges();
         return NoContent();
      }

      [HttpDelete("{id}")]
      public IActionResult DeleteBook(int id)
      {
         var book = _context.Books.FirstOrDefault(book => book.Id == id);
         if(book == null) return NotFound();
         _context.Remove(book);
         _context.SaveChanges();
         return NoContent();
      }
}
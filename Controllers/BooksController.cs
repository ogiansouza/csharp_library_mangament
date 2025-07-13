using System.Net;
using LibraryManagement.Communication;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
  private List<Book> _books = new List<Book>()
  {
    new Book { Id = 1, Title = "1984", Author = "George Orwell", Category = "Dystopian", Price = 9.99m, Stock = 5 },
    new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", Category = "Fiction", Price = 7.99m, Stock = 3 },
  };

  [HttpGet]
  [ProducesResponseType(typeof(IEnumerable<Book>), StatusCodes.Status200OK)]
  public IActionResult GetBooks()
  {
    return Ok(_books);
  }

  [HttpGet("{id}")]
  [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public IActionResult GetBookById(int id)
  {
    var book = _books.FirstOrDefault(b => b.Id == id);
    if (book != null)
    {
      return Ok(book);
    }

    return NotFound(new { Message = "Livro não encontrado" });
  }

  [HttpPost]
  [ProducesResponseType(typeof(Book), StatusCodes.Status201Created)]
  public IActionResult CreateBook([FromBody] RegisterBookJson book)
  {
    var createdBook = new Book
    {
      Id = _books.Count + 1,
      Title = book.Title,
      Author = book.Author,
      Category = book.Category,
      Price = book.Price,
      Stock = book.Stock
    };
    _books.Add(createdBook);

    return Created("", createdBook);
  }

  [HttpPut("{id}")]
  [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public IActionResult UpdateBook(int id, [FromBody] UpdateBookJson book)
  {
    var bookToUpdate = _books.FirstOrDefault(b => b.Id == id);
    if (bookToUpdate != null)
    {
      bookToUpdate.Title = book.Title;
      bookToUpdate.Author = book.Author;
      bookToUpdate.Category = book.Category;
      bookToUpdate.Price = book.Price;
      bookToUpdate.Stock = book.Stock;
      return Ok(bookToUpdate);
    }

    return NotFound(new { Message = "Livro não encontrado" });
  }

  [HttpDelete("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public IActionResult DeleteBook(int id)
  {
    var bookToDelete = _books.FirstOrDefault(b => b.Id == id);
    if (bookToDelete != null)
    {
      _books.Remove(bookToDelete);
      return NoContent();
    }

    return NotFound(new { Message = "Livro não encontrado" });
  }
}
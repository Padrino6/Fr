using APIBibliotheque.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIBibliotheque.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
            private readonly List<Book> _books; // Exemple de liste de livres en mémoire (à remplacer avec une base de données)

            public BookController()
            {
                // Initialisation de la liste de livres (à remplacer avec une initialisation de base de données)
                _books = new List<Book>
            {
                new Book { IdL = 1, Title = "Book 1", Author = "Author 1", Year_Publication = 2021, Number_Pages = 200, Stock = 10 },
                new Book { IdL = 2, Title = "Book 2", Author = "Author 2", Year_Publication = 2022, Number_Pages = 150, Stock = 5 }
            };
            }

            [HttpGet]
            public IActionResult GetAllBooks()
            {
                // Renvoyer tous les livres
                return Ok(_books);
            }

            [HttpGet("{id}")]
            public IActionResult GetBookById(int id)
            {
                // Rechercher le livre par son identifiant
                var book = _books.Find(b => b.IdL == id);

                if (book == null)
                {
                    // Livre non trouvé
                    return NotFound();
                }

                // Renvoyer le livre
                return Ok(book);
            }

            [HttpPost]
            public IActionResult AddBook([FromBody] Book book)
            {
                // Générer un nouvel identifiant pour le livre
                int newId = _books.Count + 1;
                book.IdL = newId;

                // Ajouter le livre à la liste
                _books.Add(book);

                // Renvoyer le livre créé avec le nouvel identifiant
                return CreatedAtAction(nameof(GetBookById), new { id = newId }, book);
            }

            [HttpPut("{id}")]
            public IActionResult UpdateBook(int id, [FromBody] Book book)
            {
                // Rechercher le livre par son identifiant
                var existingBook = _books.Find(b => b.IdL == id);

                if (existingBook == null)
                {
                    // Livre non trouvé
                    return NotFound();
                }

                // Mettre à jour les propriétés du livre
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.Year_Publication = book.Year_Publication;
                existingBook.Number_Pages = book.Number_Pages;
                existingBook.Stock = book.Stock;

                // Renvoyer le livre mis à jour
                return Ok(existingBook);
            }

            [HttpDelete("{id}")]
            public IActionResult DeleteBook(int id)
            {
                // Rechercher le livre par son identifiant
                var book = _books.Find(b => b.IdL == id);

                if (book == null)
                {
                    // Livre non trouvé
                    return NotFound();
                }

                // Supprimer le livre de la liste
                _books.Remove(book);

                // Renvoyer une réponse OK
                return Ok();
            }
        }
}


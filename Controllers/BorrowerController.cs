using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIBibliotheque.Models;

namespace APIBibliotheque.Controllers
{
        [ApiController]
        [Route("api/borrowers")]
        public class BorrowerController : ControllerBase
        {
            private readonly List<Borrower> _borrowers; // Exemple de liste d'emprunteurs en mémoire (à remplacer avec une base de données)

            public BorrowerController()
            {
                // Initialisation de la liste d'emprunteurs (à remplacer avec une initialisation de base de données)
                _borrowers = new List<Borrower>
            {
                new Borrower
                {
                    BorrowerId = 1,
                    FirstName = "Armelle",
                    LastName = "Eliane",
                    address = new Address
                    {
                        Number = "43",
                        Street = "Liege",
                        ZipCode = "4000",
                        Country = "Belguim",
                        Land = "Belge"
                    },
                    PhoneNumber = "123-456-7890",
                    Email = "elie.mel@ecole-it.com",
                    BorrowedBooks = new List<Book>()
                },
                new Borrower
                {
                    BorrowerId = 2,
                    FirstName = "El",
                    LastName = "Padrino",
                    address = new Address
                    {
                        Number = "41",
                        Street = "Mons",
                        ZipCode = "7000",
                        Country = "Belgium",
                        Land = "Belge"
                    },
                    PhoneNumber = "987-654-3210",
                    Email = "el.pradino@ecole-it.com",
                    BorrowedBooks = new List<Book>()
                }
            };
            }

            [HttpGet]
            public IActionResult GetAllBorrowers()
            {
                // Renvoyer tous les emprunteurs
                return Ok(_borrowers);
            }

            [HttpGet("{id}")]
            public IActionResult GetBorrowerById(int id)
            {
                // Rechercher l'emprunteur par son identifiant
                var borrower = _borrowers.Find(b => b.BorrowerId == id);

                if (borrower == null)
                {
                    // Emprunteur non trouvé
                    return NotFound();
                }

                // Renvoyer l'emprunteur
                return Ok(borrower);
            }

            [HttpPost]
            public IActionResult AddBorrower([FromBody] Borrower borrower)
            {
                // Générer un nouvel identifiant pour l'emprunteur
                int newId = _borrowers.Count + 1;
                borrower.BorrowerId = newId;

                // Ajouter l'emprunteur à la liste
                _borrowers.Add(borrower);

                // Renvoyer l'emprunteur créé avec le nouvel identifiant
                return CreatedAtAction(nameof(GetBorrowerById), new { id = newId }, borrower);
            }

            [HttpPut("{id}")]
            public IActionResult UpdateBorrower(int id, [FromBody] Borrower borrower)
            {
                // Rechercher l'emprunteur par son identifiant
                var existingBorrower = _borrowers.Find(b => b.BorrowerId == id);

                if (existingBorrower == null)
                {
                    // Emprunteur non trouvé
                    return NotFound();
                }

                // Mettre à jour les propriétés de l'emprunteur
                existingBorrower.FirstName = borrower.FirstName;
                existingBorrower.LastName = borrower.LastName;
                existingBorrower.address = borrower.address;
                existingBorrower.PhoneNumber = borrower.PhoneNumber;
                existingBorrower.Email = borrower.Email;

                // Renvoyer l'emprunteur mis à jour
                return Ok(existingBorrower);
            }

            [HttpDelete("{id}")]
            public IActionResult DeleteBorrower(int id)
            {
                // Rechercher l'emprunteur par son identifiant
                var borrower = _borrowers.Find(b => b.BorrowerId == id);

                if (borrower == null)
                {
                    // Emprunteur non trouvé
                    return NotFound();
                }

                // Supprimer l'emprunteur de la liste
                _borrowers.Remove(borrower);

                // Renvoyer une réponse OK
                return Ok();
            }

            [HttpGet("search")]
            public IActionResult SearchBorrowers(string keyword)
            {
                // Rechercher les emprunteurs par mot-clé dans les prénoms et noms
                var matchingBorrowers = _borrowers.FindAll(b => b.FirstName.Contains(keyword) || b.LastName.Contains(keyword));

                // Renvoyer les emprunteurs correspondants
                return Ok(matchingBorrowers);
            }

            [HttpGet("address/{address}")]
            public IActionResult GetBorrowersByAddress(string address)
            {
                // Rechercher les emprunteurs par adresse
                var matchingBorrowers = _borrowers.FindAll(b => b.address.Country.Contains(address) || b.address.Land.Contains(address));

                // Renvoyer les emprunteurs correspondants
                return Ok(matchingBorrowers);
            }

            [HttpGet("book/{IdL}")]
            public IActionResult GetBorrowersByBook(int bookId)
            {
                // Rechercher les emprunteurs d'un livre par son identifiant
                var matchingBorrowers = _borrowers.FindAll(b => b.BorrowedBooks.Exists(bb => bb.IdL == bookId));

                // Renvoyer les emprunteurs correspondants
                return Ok(matchingBorrowers);
            }
        }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using APIBibliotheque.Models;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/loans")]
    public class LoanController : ControllerBase
    {
        private readonly List<Loan> _loans; // Exemple de liste de prêts en mémoire (à remplacer avec une base de données)

        public LoanController()
        {
            // Initialisation de la liste de prêts (à remplacer avec une initialisation de base de données)
            _loans = new List<Loan>();
        }

        [HttpPost]
        public IActionResult BorrowBook([FromBody] Loan loan)
        {
            // Vérifier si le livre est disponible
            if (!IsBookAvailable(loan.BorrowedBook.IdL))
            {
                return BadRequest("Le livre n'est pas disponible.");
            }

            // Vérifier si l'emprunteur existe
            if (!IsBorrowerExist(loan.borrower.BorrowerId))
            {
                return BadRequest("L'emprunteur n'existe pas.");
            }

            // Ajouter le prêt à la liste
            _loans.Add(loan);

            // Mettre à jour le statut du livre (non disponible)
            UpdateBookStatus(loan.BorrowedBook.IdL, false);

            // Renvoyer le prêt créé
            return CreatedAtAction(nameof(GetLoanById), new { id = loan.LoanId }, loan);
        }

        [HttpPut("{id}")]
        public IActionResult ReturnBook(int id)
        {
            // Rechercher le prêt par son identifiant
            var loan = _loans.Find(l => l.LoanId == id);

            if (loan == null)
            {
                // Prêt non trouvé
                return NotFound();
            }

            // Mettre à jour le statut du livre (disponible)
            UpdateBookStatus(loan.BorrowedBook.IdL, true);

            // Supprimer le prêt de la liste
            _loans.Remove(loan);

            // Renvoyer une réponse OK
            return Ok();
        }

        [HttpPut("{id}/modify")]
        public IActionResult ModifyLoan(int id, [FromBody] Loan updatedLoan)
        {
            // Rechercher le prêt par son identifiant
            var loan = _loans.Find(l => l.LoanId == id);

            if (loan == null)
            {
                // Prêt non trouvé
                return NotFound();
            }

            // Mettre à jour les informations du prêt
            loan.BorrowedDate = updatedLoan.BorrowedDate;
            loan.ReturnDate = updatedLoan.ReturnDate;
            loan.BorrowedBook = updatedLoan.BorrowedBook;
            loan.borrower = updatedLoan.borrower;

            // Renvoyer le prêt mis à jour
            return Ok(loan);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLoan(int id)
        {
            // Rechercher le prêt par son identifiant
            var loan = _loans.Find(l => l.LoanId == id);

            if (loan == null)
            {
                // Prêt non trouvé
                return NotFound();
            }

            // Supprimer le prêt de la liste
            _loans.Remove(loan);

            // Renvoyer une réponse OK
            return Ok();
        }

        [HttpGet("date/{date}")]
        public IActionResult GetLoansByDate(DateTime date)
        {
            // Rechercher les prêts effectués à une date spécifique
            var matchingLoans = _loans.FindAll(l => l.BorrowedDate.Date == date.Date);

            // Renvoyer les prêts correspondants
            return Ok(matchingLoans);
        }

        [HttpGet("date/{startDate}/{endDate}")]
        public IActionResult GetLoansBetweenDates(DateTime startDate, DateTime endDate)
        {
            // Rechercher les prêts effectués entre une date de début et une date de fin
            var matchingLoans = _loans.FindAll(l => l.BorrowedDate.Date >= startDate.Date && l.BorrowedDate.Date <= endDate.Date);

            // Renvoyer les prêts correspondants
            return Ok(matchingLoans);
        }

        [HttpGet("pastdue")]
        public IActionResult GetPastDueLoans()
        {
            // Rechercher les prêts en retard (date de retour dépassée)
            var today = DateTime.Today;
            var pastDueLoans = _loans.FindAll(l => l.ReturnDate < today);

            // Renvoyer les prêts en retard
            return Ok(pastDueLoans);
        }

        [HttpGet("book/{bookId}")]
        public IActionResult GetLoansByBook(int bookId)
        {
            // Rechercher les prêts d'un livre par son identifiant
            var matchingLoans = _loans.FindAll(l => l.BorrowedBook.IdL == bookId);

            // Renvoyer les prêts correspondants
            return Ok(matchingLoans);
        }

        [HttpGet("borrower/{borrowerId}")]
        public IActionResult GetLoansByBorrower(int borrowerId)
        {
            // Rechercher les prêts d'un emprunteur par son identifiant
            var matchingLoans = _loans.FindAll(l => l.borrower.BorrowerId == borrowerId);

            // Renvoyer les prêts correspondants
            return Ok(matchingLoans);
        }

        private bool IsBookAvailable(int bookId)
        {
            // Vérifier si le livre est disponible (statut = true)
            // Vous devez implémenter la logique de vérification de disponibilité du livre en fonction de votre modèle de données
            return true;
        }

        private bool IsBorrowerExist(int borrowerId)
        {
            // Vérifier si l'emprunteur existe dans la base de données
            // Vous devez implémenter la logique de vérification de l'existence de l'emprunteur en fonction de votre modèle de données
            return true;
        }

        private void UpdateBookStatus(int bookId, bool isAvailable)
        {
            // Mettre à jour le statut du livre (disponible ou non disponible)
            // Vous devez implémenter la logique de mise à jour du statut du livre en fonction de votre modèle de données
        }

        private IActionResult GetLoanById(int id)
        {
            // Rechercher le prêt par son identifiant
            var loan = _loans.Find(l => l.LoanId == id);

            if (loan == null)
            {
                // Prêt non trouvé
                return NotFound();
            }

            // Renvoyer le prêt
            return Ok(loan);
        }
    }
}

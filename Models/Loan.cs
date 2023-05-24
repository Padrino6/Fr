namespace APIBibliotheque.Models
{
        public class Loan
        {
            public int LoanId { get; set; }
            public DateTime BorrowedDate { get; set; }
            public DateTime? ReturnDate { get; set; }
            public Book BorrowedBook { get; set; }
            public Borrower borrower { get; set; }
        }

}


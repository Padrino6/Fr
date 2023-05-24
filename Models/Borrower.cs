namespace APIBibliotheque.Models
{

    public class Address
    {
        public string Number { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string Land { get; set; }
    }

    public class Borrower
    {
        public int BorrowerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<Book> BorrowedBooks { get; set; }
    }

}

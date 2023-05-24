namespace APIBibliotheque.Models
{
    public class Book
    {
        public int IdL { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        public int Year_Publication { get; set; }

        public int Number_Pages { get; set; }

        public int Stock { get; set; }
    }
}

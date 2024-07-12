namespace Books
{
    // The Book class represents a book entity with properties for Id, Title, Author, and Genre.
    public class Book
    {
        // Unique identifier for each book
        public int Id { get; set; }

        // Title of the book
        public string Title { get; set; }

        // Author of the book
        public string Author { get; set; }

        // Genre of the book (e.g., Fiction, Non-fiction, Mystery, etc.)
        public string Genre { get; set; }
    }
}

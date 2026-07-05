using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CampusBookingSystemJS
{
    public class Service1 : IService1
    {
        private static List<Book> books = new List<Book>();

        public void AddBook(Book book)
        {
            if (book == null)
                throw new FaultException("Book cannot be null.");

            if (book.BookID <= 0)
                throw new FaultException("Invalid book ID.");

            if (string.IsNullOrWhiteSpace(book.Title))
                throw new FaultException("Title cannot be empty.");

            if (string.IsNullOrWhiteSpace(book.Author))
                throw new FaultException("Author cannot be empty.");

            if (string.IsNullOrWhiteSpace(book.ISBN))
                throw new FaultException("ISBN cannot be empty.");

            if (books.Any(r => r.BookID == book.BookID))
                throw new FaultException("Book with this ID already exists.");

            book.IsAvailable = true;
            books.Add(book);
        }

        public void UpdateBook(Book book)
        {
            if (book == null)
                throw new FaultException("Book cannot be null.");

            if (book.BookID <= 0)
                throw new FaultException("Invalid book ID.");

            if (string.IsNullOrWhiteSpace(book.Title))
                throw new FaultException("Title cannot be empty.");

            if (string.IsNullOrWhiteSpace(book.Author))
                throw new FaultException("Author cannot be empty.");

            if (string.IsNullOrWhiteSpace(book.ISBN))
                throw new FaultException("ISBN cannot be empty.");

            var selectedBook = books.FirstOrDefault(r => r.BookID == book.BookID);

            if (selectedBook == null)
                throw new FaultException("Book not found.");

            selectedBook.Title = book.Title;
            selectedBook.Author = book.Author;
            selectedBook.ISBN = book.ISBN;

        }

        public void DeleteBook(int bookID)
        {
            var book = books.FirstOrDefault(r => r.BookID == bookID);

            if (book == null)
                throw new FaultException("Book not found.");
            books.Remove(book);
        }

        public Book GetBook(int bookID)
        {
            var book = books.FirstOrDefault(r => r.BookID == bookID);

            if (book == null)
                throw new FaultException("Book not found.");

            return book;
        }

        public List<Book> GetAllBooks()
        {
            return books.ToList();
        }

        public List<Book> SearchByTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new FaultException("Title search cannot be empty.");

            return books
                .Where(r => r.Title.ToLower().Contains(title.ToLower()))
                .ToList();
        }

        public List<Book> SearchByAuthor(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
                throw new FaultException("Author search cannot be empty.");

            return books
                .Where(r => r.Author.ToLower().Contains(author.ToLower()))
                .ToList();
        }

        public Book SearchByISBN(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                throw new FaultException("ISBN search cannot be empty.");

            var book = books.FirstOrDefault(r => r.ISBN == isbn);

            if (book == null)
                throw new FaultException("Book not found.");

            return book;
        }

        public bool CheckBookAvailability(int bookID)
        {
            var book = books.FirstOrDefault(r => r.BookID == bookID);

            if (book == null)
                throw new FaultException("Book not found.");

            return book.IsAvailable;
        }

        public void CheckOutBook(int bookID)
        {
            var book = books.FirstOrDefault(r => r.BookID == bookID);

            if (book == null)
                throw new FaultException("Book not found.");

            if (!book.IsAvailable)
                throw new FaultException("Book is already checked out.");

            book.IsAvailable = false;
        }

        public void ReturnBook(int bookID)
        {
            var book = books.FirstOrDefault(r => r.BookID == bookID);

            if (book == null)
                throw new FaultException("Book not found.");

            if (book.IsAvailable)
                throw new FaultException("Book is already available.");

            book.IsAvailable = true;
        }
    }
}
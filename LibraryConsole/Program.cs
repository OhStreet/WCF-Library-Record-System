using LibraryConsole.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LibraryConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Service1Client client = new Service1Client();
            int choice;

            do
            {
                Console.WriteLine("\n=== Library Record System ===");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. Delete Book");
                Console.WriteLine("3. Update Book");
                Console.WriteLine("4. List All Books");
                Console.WriteLine("5. Search Books by Title");
                Console.WriteLine("6. Search Books by Author");
                Console.WriteLine("7. Search Book by ISBN");
                Console.WriteLine("8. Check Out Book");
                Console.WriteLine("9. Return Book");
                Console.WriteLine("10. Check Book Availability");
                Console.WriteLine("0. Exit");
                Console.Write("Enter your choice: ");

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                try
                {
                    switch (choice)
                    {
                        case 1:
                            AddBook(client);
                            break;

                        case 2:
                            DeleteBook(client);
                            break;

                        case 3:
                            UpdateBook(client);
                            break;

                        case 4:
                            ListAllBooks(client);
                            break;

                        case 5:
                            SearchByTitle(client);
                            break;

                        case 6:
                            SearchByAuthor(client);
                            break;

                        case 7:
                            SearchByISBN(client);
                            break;

                        case 8:
                            CheckOutBook(client);
                            break;

                        case 9:
                            ReturnBook(client);
                            break;

                        case 10:
                            CheckAvailability(client);
                            break;

                        case 0:
                            Console.WriteLine("Goodbye.");
                            break;

                        default:
                            Console.WriteLine("Invalid menu choice.");
                            break;
                    }
                }
                catch (FaultException ex)
                {
                    Console.WriteLine("Service error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unexpected error: " + ex.Message);
                }

            } while (choice != 0);

            client.Close();
        }

        static void AddBook(Service1Client client)
        {
            Console.WriteLine("\n--- Add Book ---");

            int bookID = ReadInt("Enter Book ID: ");

            Console.Write("Enter Title: ");
            string title = Console.ReadLine();

            Console.Write("Enter Author: ");
            string author = Console.ReadLine();

            Console.Write("Enter ISBN: ");
            string isbn = Console.ReadLine();

            Book book = new Book
            {
                BookID = bookID,
                Title = title,
                Author = author,
                ISBN = isbn,
                IsAvailable = true
            };

            client.AddBook(book);

            Console.WriteLine("Book added successfully.");
        }

        static void DeleteBook(Service1Client client)
        {
            Console.WriteLine("\n--- Delete Book ---");

            int bookID = ReadInt("Enter Book ID to delete: ");

            client.DeleteBook(bookID);

            Console.WriteLine("Book deleted successfully.");
        }

        static void UpdateBook(Service1Client client)
        {
            Console.WriteLine("\n--- Update Book ---");

            int bookID = ReadInt("Enter Book ID to update: ");

            Console.Write("Enter New Title: ");
            string title = Console.ReadLine();

            Console.Write("Enter New Author: ");
            string author = Console.ReadLine();

            Console.Write("Enter New ISBN: ");
            string isbn = Console.ReadLine();

            Book book = new Book
            {
                BookID = bookID,
                Title = title,
                Author = author,
                ISBN = isbn
            };

            client.UpdateBook(book);

            Console.WriteLine("Book updated successfully.");
        }

        static void ListAllBooks(Service1Client client)
        {
            Console.WriteLine("\n--- All Books ---");

            Book[] books = client.GetAllBooks();

            if (books == null || books.Length == 0)
            {
                Console.WriteLine("No books found.");
                return;
            }

            foreach (Book book in books)
            {
                DisplayBook(book);
            }
        }

        static void SearchByTitle(Service1Client client)
        {
            Console.WriteLine("\n--- Search by Title ---");

            Console.Write("Enter title search term: ");
            string title = Console.ReadLine();

            Book[] books = client.SearchByTitle(title);

            if (books == null || books.Length == 0)
            {
                Console.WriteLine("No matching books found.");
                return;
            }

            foreach (Book book in books)
            {
                DisplayBook(book);
            }
        }

        static void SearchByAuthor(Service1Client client)
        {
            Console.WriteLine("\n--- Search by Author ---");

            Console.Write("Enter author search term: ");
            string author = Console.ReadLine();

            Book[] books = client.SearchByAuthor(author);

            if (books == null || books.Length == 0)
            {
                Console.WriteLine("No matching books found.");
                return;
            }

            foreach (Book book in books)
            {
                DisplayBook(book);
            }
        }

        static void SearchByISBN(Service1Client client)
        {
            Console.WriteLine("\n--- Search by ISBN ---");

            Console.Write("Enter ISBN: ");
            string isbn = Console.ReadLine();

            Book book = client.SearchByISBN(isbn);

            DisplayBook(book);
        }

        static void CheckOutBook(Service1Client client)
        {
            Console.WriteLine("\n--- Check Out Book ---");

            int bookID = ReadInt("Enter Book ID to check out: ");

            client.CheckOutBook(bookID);

            Console.WriteLine("Book checked out successfully.");
        }

        static void ReturnBook(Service1Client client)
        {
            Console.WriteLine("\n--- Return Book ---");

            int bookID = ReadInt("Enter Book ID to return: ");

            client.ReturnBook(bookID);

            Console.WriteLine("Book returned successfully.");
        }

        static void CheckAvailability(Service1Client client)
        {
            Console.WriteLine("\n--- Check Book Availability ---");

            int bookID = ReadInt("Enter Book ID: ");

            bool isAvailable = client.CheckBookAvailability(bookID);

            if (isAvailable)
            {
                Console.WriteLine("This book is available.");
            }
            else
            {
                Console.WriteLine("This book is currently checked out.");
            }
        }

        // Helpers
        static void DisplayBook(Book book)
        {
            if (book == null)
            {
                Console.WriteLine("Book not found.");
                return;
            }

            Console.WriteLine("------------------------------");
            Console.WriteLine("Book ID: " + book.BookID);
            Console.WriteLine("Title: " + book.Title);
            Console.WriteLine("Author: " + book.Author);
            Console.WriteLine("ISBN: " + book.ISBN);
            Console.WriteLine("Available: " + (book.IsAvailable ? "Yes" : "No"));
            Console.WriteLine("------------------------------");
        }

        static int ReadInt(string prompt)
        {
            int value;

            while (true)
            {
                Console.Write(prompt);

                if (int.TryParse(Console.ReadLine(), out value))
                {
                    return value;
                }

                Console.WriteLine("Invalid number. Please try again.");
            }
        }
    }
}

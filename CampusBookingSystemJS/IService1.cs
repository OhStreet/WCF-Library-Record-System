using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CampusBookingSystemJS
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        void AddBook(Book book);

        [OperationContract]
        void UpdateBook(Book book);

        [OperationContract]
        void DeleteBook(int bookID);

        [OperationContract]
        Book GetBook(int bookID);

        [OperationContract]
        List<Book> GetAllBooks();

        [OperationContract]
        List<Book> SearchByTitle(string title);

        [OperationContract]
        List<Book> SearchByAuthor(string author);

        [OperationContract]
        Book SearchByISBN(string isbn);

        [OperationContract]
        bool CheckBookAvailability(int bookID);

        [OperationContract]
        void CheckOutBook(int bookID);

        [OperationContract]
        void ReturnBook(int bookID);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Book
    {
        [DataMember]
        public int BookID { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Author { get; set; }

        [DataMember]
        public string ISBN { get; set; }

        [DataMember]
        public bool IsAvailable { get; set; }
    }
}
    
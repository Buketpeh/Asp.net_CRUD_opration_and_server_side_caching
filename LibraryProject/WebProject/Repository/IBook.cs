using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Repository
{
    interface IBook
    {
        void InsertBook(Book book);
        IEnumerable<Book> GetBooks();
        Book GetBookByID(int Id);
        void UpdateBook(Book book);
        void DeleteBook(int Id);
        void Save();
        Task GetBookByID(int? id);
    }
}

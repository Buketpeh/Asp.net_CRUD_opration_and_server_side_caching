using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Repository
{
    public class BookRepository : IBook
    {
        private LibraryContext DBcontext;

        public BookRepository(LibraryContext objempcontext)
        {

            this.DBcontext = objempcontext;

        }
        public void DeleteBook(int Id)
        {
            Book book_ = DBcontext.Books.Find(Id);
            DBcontext.Books.Remove(book_);
            DBcontext.SaveChanges();
        }

        public Book GetBookByID(int Id)
        {
            return DBcontext.Books.Find(Id);
        }

        public Task GetBookByID(int? id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetBooks()
        {
            return DBcontext.Books.ToList();
        }

        public void InsertBook(Book book)
        {
            DBcontext.Books.Add(book);

            DBcontext.SaveChanges();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void UpdateBook(Book book)
        {
            DBcontext.Entry(book).State = EntityState.Modified;
            DBcontext.SaveChanges();
        }
        
    }
}

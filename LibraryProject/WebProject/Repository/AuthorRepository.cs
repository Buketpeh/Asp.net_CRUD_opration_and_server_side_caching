using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Repository
{
    public class AuthorRepository : IAuthor
    {
        private LibraryContext DBcontext;
        public AuthorRepository(LibraryContext objempcontext)
        {

            this.DBcontext = objempcontext;

        }
        public void DeleteAuthor(int Id)
        {
            Author author_ = DBcontext.Authors.Find(Id);
            DBcontext.Authors.Remove(author_);
            DBcontext.SaveChanges();
        }

        public Task GetAuhtorByID(int? id)
        {
            throw new NotImplementedException();
        }

        public Author GetAuthorByID(int Id)
        {
            return DBcontext.Authors.Find(Id);
        }
       

        public IEnumerable<Author> GetAuthors()
        {
            return DBcontext.Authors.ToList();
        }

        public void InsertAuthor(Author author)
        {
            DBcontext.Authors.Add(author);

            DBcontext.SaveChanges();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void UpdateAuthor(Author author)
        {
            DBcontext.Entry(author).State = EntityState.Modified;
            DBcontext.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.Models;

namespace WebProject.Repository
{
    interface IAuthor
    {
        void InsertAuthor(Author author);
        IEnumerable<Author> GetAuthors();
        Author GetAuthorByID(int Id);
        void UpdateAuthor(Author author);
        void DeleteAuthor(int Id);
        void Save();
        Task GetAuhtorByID(int? id);
        

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClassLibrary.Models;
using WebProject.Repository;
using Microsoft.Extensions.Caching.Memory;

namespace WebProject.Controllers
{
    public class BooksController : Controller
    {
        private IBook _context;
        private LibraryContext db;
        private readonly IMemoryCache memoryCache;

        public BooksController(IMemoryCache memoryCache)
        {
            this._context =new BookRepository(new LibraryContext()) ;
            db= new LibraryContext();
            this.memoryCache = memoryCache;
        }
        
        // GET: Books
        public async Task<IActionResult> Index()
        {
            var list = _context.GetBooks().ToList();
            return View(list);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookdetail = _context.GetBookByID(id);
            var book = new Book();
            book.Id = id;
            book.BookName = bookdetail.BookName;
            book.ReleaseDate = bookdetail.ReleaseDate;
            book.PublishingHouse = bookdetail.PublishingHouse;
            book.Category = bookdetail.Category;
            book.AuthorId = bookdetail.AuthorId;
            return View(book);

        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View(new Book());
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookName,ReleaseDate,PublishingHouse,Category,AuthorId")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.InsertBook(book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book =  _context.GetBookByID(id); //hata verebilir --await silindi...
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookName,ReleaseDate,PublishingHouse,Category,AuthorId")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.UpdateBook(book);
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await db.Books.FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _context.DeleteBook(id);
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return db.Books.Any(e => e.Id == id);
        }
    }
}

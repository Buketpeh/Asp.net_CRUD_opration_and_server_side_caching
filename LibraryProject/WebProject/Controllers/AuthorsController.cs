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
using System.Diagnostics;

namespace WebProject.Controllers
{
    public class AuthorsController : Controller
    {
        private IAuthor _context;
        private LibraryContext db;
        private readonly IMemoryCache memoryCache;

        public AuthorsController(IMemoryCache memoryCache)
        {
            this._context = new AuthorRepository(new LibraryContext());
            db = new LibraryContext();
            this.memoryCache = memoryCache;
        }

        // GET: Authors
        public ActionResult Index(string p)
        {
            List<Author> authors;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var author = db.Authors.Where(x => x.Fistrname.Contains(p) || x.Lastname.Contains(p) || p == null).ToList();
            if (!memoryCache.TryGetValue("Authors", out authors)) { 
                memoryCache.Set("Authors", author);
                }
            authors = memoryCache.Get("Authors") as List<Author>;
            stopwatch.Stop();
            ViewBag.TotalTime = stopwatch.Elapsed;
            ViewBag.TotalRows = author.Count;
          
            return View(author);
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authordetail = _context.GetAuthorByID(id);
            var author = new Author();
            author.Id = id;
            author.Fistrname = authordetail.Fistrname;
            author.Lastname = authordetail.Lastname;
            author.DateofBirth = authordetail.DateofBirth;
           
            return View(author);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View(new Author());
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fistrname,Lastname,DateofBirth")] Author author)
        {
            if (ModelState.IsValid)
            {
                _context.InsertAuthor(author);
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = _context.GetAuthorByID(id); //hata verebilir --await silindi...
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fistrname,Lastname,DateofBirth")] Author author)
        {
            if (id != author.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.UpdateAuthor(author);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.Id))
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
            return View(author);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await db.Authors.FirstOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _context.DeleteAuthor(id);
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(int id)
        {
            return db.Authors.Any(e => e.Id == id);
        }
    }
}

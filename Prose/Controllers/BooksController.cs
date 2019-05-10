using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Prose.Data;
using Prose.Models;
using Prose.Models.BookViewModels;

namespace Prose.Controllers
{
    public class BooksController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context,
                                UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // Gets books based on club where the book is not marked as currently reading or past read
        public async Task<IActionResult> SuggestedBooksIndex(int? clubId)
        {
            if (clubId == null)
            {
                return NotFound();
            }

            var applicationDbContext = await (
                                                from b in _context.Book
                                                    .Include(b => b.ClubUser)
                                                    .Where(b => b.CurrentlyReading == false
                                                            && b.PastRead == false)
                                                from v in _context.Vote.Where(v => v.BookId == b.BookId 
                                                    && b.ClubUser.ClubId == clubId)
                                                    .DefaultIfEmpty()
                                                group new { b, v } by new {
                                                        b.BookId,
                                                        b.Title,
                                                        b.Author,
                                                        b.ClubUser,
                                                        b.Details,
                                                        b.Image } 
                                                        into grouped
                                                select new BooksIndexViewModel
                                                {
                                                    VoteTotal = grouped.Where(gr => gr.v != null).Count(),
                                                    Book = new Book
                                                    {
                                                        BookId = grouped.Key.BookId,
                                                        Title = grouped.Key.Title,
                                                        Author = grouped.Key.Author,
                                                        ClubUser = grouped.Key.ClubUser,
                                                        Details = grouped.Key.Details,
                                                        Image = grouped.Key.Image
                                                    }
                                                }).OrderByDescending(v => v.VoteTotal).ToListAsync();
                

            if (applicationDbContext == null)
            {
                return NotFound();
            }

            return View("SuggestedBooksIndex", applicationDbContext);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.ClubUser)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET method for the club dashboard which will get the book that is marked as true for currently reading
        public async Task<IActionResult> GetCurrentlyReading(int clubId)
        {
            var book = await _context.Book
                .Include(c => c.ClubUser)
                .Include(c => c.ClubUser.Club)
                .Where(b => b.CurrentlyReading == true && b.ClubUser.ClubId == clubId)
                .FirstOrDefaultAsync();
            if (book == null)
            {
                return NotFound();
            }

            return View("ClubDashboard", book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }



        public async Task<IActionResult> SearchApi(string searchName, string searchAuthor, int clubId)
        {
            var searchData = await GoogleBooksAsync(searchName, searchAuthor);

            return View("SearchIndex", searchData);
        }



        // This post takes the input from the selected book in the SearchIndex view and converts it to a book post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuggestBookPost(string title, string author, string image, string details, int clubId)
        {

            var user = await GetCurrentUserAsync();

            var clubUser = await _context.ClubUser
                                .Where(cu => cu.ClubId == clubId 
                                        && cu.UserId == user.Id)
                                .FirstOrDefaultAsync();


            //setting the details based on the selected book
            Book savedBook = new Book()
            {
                Title = title,
                Author = author,
                Image = image,
                Details = details,
                ClubUserId = clubUser.ClubUserId
                
            };

            if (ModelState.IsValid)
            {
                _context.Add(savedBook);
                await _context.SaveChangesAsync();
                return RedirectToAction("SuggestedBooksIndex", new { clubId });
            }
            ViewData["ClubUserId"] = new SelectList(_context.ClubUser, "ClubUserId", "UserId", savedBook.ClubUserId);
            return View(savedBook);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["ClubUserId"] = new SelectList(_context.ClubUser, "ClubUserId", "UserId", book.ClubUserId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,Title,Author,Image,Details,ClubUserId,CurrentlyReading")] Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("SuggestedBooksIndex");
            }
            ViewData["ClubUserId"] = new SelectList(_context.ClubUser, "ClubUserId", "UserId", book.ClubUserId);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.ClubUser)
                .FirstOrDefaultAsync(m => m.BookId == id);
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
            var book = await _context.Book.FindAsync(id);
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction("SuggestedBooksIndex");
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.BookId == id);
        }


        private static async Task<BookRecord> GoogleBooksAsync(string title, string author)
        {
            string key = "AIzaSyAF3M0uelGbxyrv6QuVqrHcfFxM3a3nelc";

            string queryName = title;

            string queryAuthor = "";

            if (author != null)
            {
                queryAuthor = $"+inauthor:{author}";
            }

            string queries = $"{queryName}{queryAuthor}";

            using (var client = new HttpClient())
            {
                var content = await client.GetStringAsync($"https://www.googleapis.com/books/v1/volumes?q={queries}&key={key}");
                return JsonConvert.DeserializeObject<BookRecord>(content);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prose.Data;
using Prose.Models;

namespace Prose.Controllers
{
    public class VotesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ApplicationDbContext _context;

        public VotesController(ApplicationDbContext context,
                                UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        

        // GET: Votes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vote.ToListAsync());
        }

        // GET: Votes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vote = await _context.Vote
                .FirstOrDefaultAsync(m => m.VoteId == id);
            if (vote == null)
            {
                return NotFound();
            }

            return View(vote);
        }


        // Create an instance of a vote on a book that has been suggested ONLY IF the user does not have an existing vote
        public async Task<IActionResult> Create(int bookId, int clubId)
        {
            
            var user = await GetCurrentUserAsync();
            
            //Get the clubUser Id for the current user and current club
            var clubUser = await _context.ClubUser
                            .Where(cu => cu.ClubId == clubId 
                            && cu.UserId == user.Id).FirstOrDefaultAsync();

            //Get current user votes, and perform a check to see if a club user id already exists for this book Id
            var allClubUserVotes = await _context.Vote
                                            .Where(v => v.ClubUserId == clubUser.ClubUserId 
                                            && v.BookId == bookId).FirstOrDefaultAsync();
            
            if(allClubUserVotes != null)
            {
                return RedirectToAction("Delete", new { id = allClubUserVotes.VoteId, clubId = clubId });
            }

            Vote vote = new Vote
            {
                ClubUserId = clubUser.ClubUserId,
                BookId = bookId
            };

            if (ModelState.IsValid)
            {
                _context.Add(vote);
                await _context.SaveChangesAsync();
                return RedirectToAction("SuggestedBooksIndex", "Books", new { clubId });
            }
            return View("Index", "Books");
        }

        // GET: Votes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vote = await _context.Vote.FindAsync(id);
            if (vote == null)
            {
                return NotFound();
            }
            return View(vote);
        }

        // POST: Votes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VoteId,ClubUserId,BookId")] Vote vote)
        {
            if (id != vote.VoteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoteExists(vote.VoteId))
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
            return View(vote);
        }

        public async Task<IActionResult> Delete(int id, int clubId)
        {
            var vote = await _context.Vote
                                .Where(v => v.VoteId == id)
                                .FirstOrDefaultAsync();
            _context.Vote.Remove(vote);
            await _context.SaveChangesAsync();
            return RedirectToAction("SuggestedBooksIndex", "Books", new { clubId });
        }

        private bool VoteExists(int id)
        {
            return _context.Vote.Any(e => e.VoteId == id);
        }
    }
}

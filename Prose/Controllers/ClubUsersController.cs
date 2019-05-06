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
    public class ClubUsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ApplicationDbContext _context;

        public ClubUsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // Get users based on the club Id. This is the result of clicking on the member's button.
        public async Task<IActionResult> MemberIndex(int id)
        {
            var applicationDbContext = _context.ClubUser.Include(c => c.Club).Where(cu => cu.Club.ClubId == id);
            return View("MemberIndex", await applicationDbContext.ToListAsync());
        }

        // Get all the clubs based on the user Id. This is the result of clicking on the user's "my clubs" button
        public async Task<IActionResult> UserClubIndex()
        {
            var user = await GetCurrentUserAsync();

            var ClubData = _context.ClubUser
                .Include(c => c.Club)
                .Include(c => c.User)
                .Where(cu => cu.UserId == user.Id)
                .ToList();

            List<SelectListItem> UserClubsList = new List<SelectListItem>();

            UserClubsList.Insert(0, new SelectListItem
            {
                Text = "My Clubs",
                Value = ""
            });

            foreach (var c in ClubData)
            {
                SelectListItem li = new SelectListItem
                {
                    Value = c.Club.ClubId.ToString(),
                    Text = c.Club.Name
                };
                UserClubsList.Add(li);
            };

            ClubUser ClubUser = new ClubUser();

            ClubUser.Clubs = UserClubsList;

            return View(ClubUser);
        }

        // GET: ClubUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clubUser = await _context.ClubUser
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.ClubUserId == id);
            if (clubUser == null)
            {
                return NotFound();
            }

            return View(clubUser);
        }


        public async Task<IActionResult> Create(int id)
        {
            var user = await GetCurrentUserAsync();

            ClubUser clubUser = new ClubUser
            {
                ClubId = id,
                UserId = user.Id
            };

         
            _context.Add(clubUser);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Clubs");
        }

        // GET: ClubUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clubUser = await _context.ClubUser.FindAsync(id);
            if (clubUser == null)
            {
                return NotFound();
            }
            ViewData["ClubId"] = new SelectList(_context.Club, "ClubId", "Name", clubUser.ClubId);
            return View(clubUser);
        }

        // POST: ClubUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClubUserId,UserId,ClubId")] ClubUser clubUser)
        {
            if (id != clubUser.ClubUserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clubUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClubUserExists(clubUser.ClubUserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction();
            }
            ViewData["ClubId"] = new SelectList(_context.Club, "ClubId", "Name", clubUser.ClubId);
            return View(clubUser);
        }

        // GET: ClubUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clubUser = await _context.ClubUser
                .Include(c => c.Club)
                .FirstOrDefaultAsync(m => m.ClubUserId == id);
            if (clubUser == null)
            {
                return NotFound();
            }

            return View(clubUser);
        }

        // POST: ClubUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clubUser = await _context.ClubUser.FindAsync(id);
            _context.ClubUser.Remove(clubUser);
            await _context.SaveChangesAsync();
            return RedirectToAction();
        }

        private bool ClubUserExists(int id)
        {
            return _context.ClubUser.Any(e => e.ClubUserId == id);
        }
    }
}

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

        public ClubUsersController(ApplicationDbContext context, 
                                    UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // Get users based on the club Id. This is the result of clicking on the member's button.
        public async Task<IActionResult> MemberIndex(int clubId)
        {
            var applicationDbContext = _context.ClubUser
                                        .Include(c => c.Club)
                                        .Include(c => c.User)
                                        .Where(cu => cu.Club.ClubId == clubId);
            return View("MemberIndex", await applicationDbContext.ToListAsync());
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

        // Removes a user from a club
        public async Task<IActionResult> Delete(int? id)
        {
            var user = await GetCurrentUserAsync();

            if (id == null)
            {
                return NotFound();
            }

            var clubUser = await _context.ClubUser
                                .Include(c => c.Club)
                                .Where(m => m.ClubId == id &&
                                    m.UserId == user.Id)
                                .FirstOrDefaultAsync();

            _context.ClubUser.Remove(clubUser);
            await _context.SaveChangesAsync();
            return RedirectToAction("UserClubsList", "Clubs");
        }

        private bool ClubUserExists(int id)
        {
            return _context.ClubUser.Any(e => e.ClubUserId == id);
        }
    }
}

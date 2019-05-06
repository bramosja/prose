using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Prose.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Prose.Models.ViewComponents
{
    public class ClubUserViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public ClubUserViewComponent(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await GetCurrentUserAsync();

            var ClubData = _context.ClubUser.Include(c => c.Club)
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


            ViewBag.ClubDropDownList = UserClubsList;
            return View();
        }
    }
}

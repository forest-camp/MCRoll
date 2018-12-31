using System;
using System.Linq;
using System.Threading.Tasks;
using MCRoll.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MCRoll.ViewComponents
{
    public class RollList : ViewComponent
    {
        private readonly MCRollDbContext _context;

        public RollList(MCRollDbContext context)
        {
            this._context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(Boolean isOpen)
        {
            var rolls = await this._context.Rolls
                .Include(r => r.Participants)
                .Include(r => r.Winners)
                .Where(r => r.IsOpen == isOpen)
                .OrderByDescending(r => r.RollId)
                .ToListAsync();
            return View(rolls);
        }
    }
}

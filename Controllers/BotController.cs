using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuintrixHomeworkPlayerMVP.Data;
using QuintrixHomeworkPlayerMVP.Models;

namespace QuintrixHomeworkPlayerMVP.Controllers
{
    public class BotController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BotController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bot
        public async Task<IActionResult> Index()
        {
              return _context.Bot != null ? 
                          View(await _context.Bot.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Bot'  is null.");
        }

        // GET: Bot/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Bot == null)
            {
                return NotFound();
            }

            var bot = await _context.Bot
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bot == null)
            {
                return NotFound();
            }

            return View(bot);
        }

        // GET: Bot/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bot/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Bot bot)
        {
            if (ModelState.IsValid)
            {
                bot.Id = Guid.NewGuid();
                _context.Add(bot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bot);
        }

        // GET: Bot/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Bot == null)
            {
                return NotFound();
            }

            var bot = await _context.Bot.FindAsync(id);
            if (bot == null)
            {
                return NotFound();
            }
            return View(bot);
        }

        // POST: Bot/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] Bot bot)
        {
            if (id != bot.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BotExists(bot.Id))
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
            return View(bot);
        }

        // GET: Bot/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Bot == null)
            {
                return NotFound();
            }

            var bot = await _context.Bot
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bot == null)
            {
                return NotFound();
            }

            return View(bot);
        }

        // POST: Bot/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Bot == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Bot'  is null.");
            }
            var bot = await _context.Bot.FindAsync(id);
            if (bot != null)
            {
                _context.Bot.Remove(bot);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BotExists(Guid id)
        {
          return (_context.Bot?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using travel_app.Data;
using travel_app.Models;

namespace travel_app.Controllers
{
    public class StagesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public StagesController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: Stages
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.stages.Include(s => s.Travel);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Stages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stage = await _context.stages
                .Include(s => s.Travel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stage == null)
            {
                return NotFound();
            }

            return View(stage);
        }

        // GET: Stages/Create
        public IActionResult Create(int? travelId)
        {
            if (travelId.HasValue)
            {
                var stage = new Stage
                {
                    TravelId = travelId.Value
                };
                ViewData["TravelId"] = new SelectList(_context.travels, "Id", "Title");
                return View(stage);
            }

            ViewData["TravelId"] = new SelectList(_context.travels, "Id", "Title");
            return View();
        }

        // POST: Stages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Stage stage, IFormFile stagePhoto)
        {
            stage.CretedById = "Admin";
            stage.CretedOn = DateTime.Now;

            /*if (ModelState.IsValid)
            {
                _context.Add(stage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TravelId"] = new SelectList(_context.travels, "Id", "Id", stage.TravelId);
            return View(stage);
            */

            /*if (stagePhoto.Length > 0)
            {
                var fileName = "StagePhoto_" + DateTime.Now.ToString("yyyymmddhhmmss") + "_" + stagePhoto.FileName;

                var path = _configuration["FileSettings:UploadFolder"]!;

                var filePath = Path.Combine(path, fileName);

                var stream = new FileStream(filePath, FileMode.Create);

                await stagePhoto.CopyToAsync(stream);

                stage.Image = fileName;
            }*/

            if (stagePhoto.Length > 0)
            {
                var fileName = "StagePhoto_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + stagePhoto.FileName;

                // Salva l'immagine sotto wwwroot
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "stages");

                // Se non esiste rrea la directory 
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath); 
                }

                var filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await stagePhoto.CopyToAsync(stream);
                }

                stage.Image = fileName; // Salva solo il nome del file nel database
            }

            _context.Add(stage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Stages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stage = await _context.stages.FindAsync(id);
            if (stage == null)
            {
                return NotFound();
            }
            ViewData["TravelId"] = new SelectList(_context.travels, "Id", "Title", stage.TravelId);
            return View(stage);
        }

        // POST: Stages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Stage stage, IFormFile stagePhoto)
        {
            if (id != stage.Id)
            {
                return NotFound();
            }

            /*if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StageExists(stage.Id))
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
            ViewData["TravelId"] = new SelectList(_context.travels, "Id", "Id", stage.TravelId);
            return View(stage);
            */

            // Trova lo stage corrente nel database
            var currentStage = await _context.stages.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);

            if (currentStage == null)
            {
                return NotFound();
            }

            // Gestione del caricamento del file immagine
            if (stagePhoto != null && stagePhoto.Length > 0)
            {
                var fileName = "StagePhoto_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + stagePhoto.FileName;

                // Salva l'immagine sotto wwwroot
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "stages");

                // Se non esiste, crea la directory 
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await stagePhoto.CopyToAsync(stream);
                }

                stage.Image = fileName; // Salva solo il nome del file nel database
            }
            else
            {
                // Mantieni l'immagine esistente
                stage.Image = currentStage.Image;
            }

            try
            {
                stage.ModifiedById = "Admin";
                stage.ModifiedOn = DateTime.Now;
                _context.Update(stage);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StageExists(stage.Id))
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

        // GET: Stages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stage = await _context.stages
                .Include(s => s.Travel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stage == null)
            {
                return NotFound();
            }

            return View(stage);
        }

        // POST: Stages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stage = await _context.stages.FindAsync(id);
            if (stage != null)
            {
                _context.stages.Remove(stage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StageExists(int id)
        {
            return _context.stages.Any(e => e.Id == id);
        }
    }
}

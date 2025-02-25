﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LamborghiniAuto.Data;
using LamborghiniAuto.Models;

namespace LamborghiniAuto.Controllers
{
    public class DipendenteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DipendenteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dipendente
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dipendente.ToListAsync());
        }

        // GET: Dipendente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dipendente = await _context.Dipendente
                .FirstOrDefaultAsync(m => m.id == id);
            if (dipendente == null)
            {
                return NotFound();
            }

            return View(dipendente);
        }

        // GET: Dipendente/Create
        public IActionResult AggiuntaDipendente()
        {
            return View();
        }

        // POST: Dipendente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AggiuntaDipendente([Bind("stipendio,mestiere,id,nome,cognome,dataNascita")] Dipendente dipendente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dipendente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dipendente);
        }

        // GET: Dipendente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dipendente = await _context.Dipendente.FindAsync(id);
            if (dipendente == null)
            {
                return NotFound();
            }
            return View(dipendente);
        }

        // POST: Dipendente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("stipendio,mestiere,id,nome,cognome,dataNascita")] Dipendente dipendente)
        {
            if (id != dipendente.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dipendente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DipendenteExists(dipendente.id))
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
            return View(dipendente);
        }

        // GET: Dipendente/Delete/5
        public async Task<IActionResult> Licenzia(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dipendente = await _context.Dipendente
                .FirstOrDefaultAsync(m => m.id == id);
            if (dipendente == null)
            {
                return NotFound();
            }

            return View(dipendente);
        }

        // POST: Dipendente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dipendente = await _context.Dipendente.FindAsync(id);
            _context.Dipendente.Remove(dipendente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DipendenteExists(int id)
        {
            return _context.Dipendente.Any(e => e.id == id);
        }
    }
}

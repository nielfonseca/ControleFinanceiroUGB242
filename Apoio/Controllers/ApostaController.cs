﻿using Apoio.Data;
using Apoio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Apoio.Controllers
{
    public class ApostaController : Controller
    {

        private readonly WebetContext _context;

        public ApostaController(WebetContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context .Apostas.ToList());
        }

        public IActionResult Create()
        {
            ViewData["ApostadorId"] = new SelectList(_context.Apostadores, "Id", "Nome");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create([Bind("Valor", "PrevisaoDeGanho", "Vencedora", "ApostadorId")] Aposta aposta)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ApostadorId"] = new SelectList(_context.Apostadores, "Id", "Nome", aposta.Id);
                return View();
            }
            if(aposta == null)
            {
                return NotFound();
            }
            _context.Add(aposta);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FlashCards.Models;

namespace FlashCards.Controllers
{
    public class DecksController : Controller
    {
        public static readonly List<Deck> decks = new List<Deck>();
        private readonly ILogger<DecksController> _logger;

        public DecksController(ILogger<DecksController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(decks);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Deck deck)
        {
            if (ModelState.IsValid)
            {
                deck.Id = decks.Count + 1;
                decks.Add(deck);
                return RedirectToAction("Index");
            }
            return View(deck);
        }

    }
}
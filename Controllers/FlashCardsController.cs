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
    public class FlashCardsController : Controller
    {
        private readonly ILogger<FlashCardsController> _logger;

        public FlashCardsController(ILogger<FlashCardsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult ViewDeck(int deckId)
        {
            if (deckId <= 0)
            {
                return BadRequest("Invalid deck ID.");
            }

            var deck = DecksController.decks.FirstOrDefault(d => d.Id == deckId);

            if (deck == null)
            {
                return NotFound("Deck not found.");
            }

            return View(deck);
        }

        [HttpGet]
        public IActionResult Create(int deckId)
        {
            if (deckId <= 0)
            {
                return BadRequest("Invalid deck ID.");
            }

            // Pass the deckId to the view
            ViewBag.DeckId = deckId;
            return View();
        }

        [HttpPost]
        public IActionResult Create(FlashCard flashCard)
        {
            if (ModelState.IsValid)
            {
                var deck = DecksController.decks.FirstOrDefault(d => d.Id == flashCard.DeckId);

                if (deck == null)
                {
                    return NotFound("Deck not found.");
                }

                flashCard.Id = (DecksController.decks.Count > 0) ? DecksController.decks.Max(d => d.Id) + 1 : 1;
                deck.CardList().Add(flashCard);

                return RedirectToAction("ViewDeck", "FlashCards", new { deckId = flashCard.DeckId });
            }

            return View(flashCard);
        }
    }
}
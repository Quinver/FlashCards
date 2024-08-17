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

                deck.CardList().Add(flashCard);

                return RedirectToAction("ViewDeck", "FlashCards", new { deckId = flashCard.DeckId });
            }

            return View(flashCard);
        }

        [HttpGet]
        public IActionResult Edit(Guid Id)
        {
            var flashCard = DecksController.decks.SelectMany(d => d.CardList()).FirstOrDefault(fc => fc.Id == Id);

            if (flashCard == null)
            {
                return NotFound("FlashCard not found.");
            }

            return View(flashCard);
        }

        [HttpPost]
        public IActionResult Edit(FlashCard flashCard)
        {
            if (ModelState.IsValid)
            {
                var deck = DecksController.decks.FirstOrDefault(d => d.CardList().Any(fc => fc.Id == flashCard.Id));

                if (deck == null)
                {
                    return NotFound("Deck not found.");
                }

                var existingFlashCard = deck.CardList().FirstOrDefault(fc => fc.Id == flashCard.Id);
                if (existingFlashCard == null)
                {
                    return NotFound("FlashCard not found.");
                }

                existingFlashCard.Front = flashCard.Front;
                existingFlashCard.Back = flashCard.Back;

                return RedirectToAction("ViewDeck", "FlashCards", new { deckId = deck.Id });
            }

            return View(flashCard);
        }

        [HttpPost]
        public IActionResult Delete(Guid id, int deckId)
        {
            // Find the deck containing the flashcard with the given id
            var deck = DecksController.decks
                .FirstOrDefault(d => d.Id == deckId);

            if (deck == null)
            {
                return NotFound("Deck not found.");
            }

            // Find the flashcard to be deleted
            var flashCard = deck.CardList().FirstOrDefault(fc => fc.Id == id);
            if (flashCard != null)
            {
                // Remove the flashcard from the deck
                deck.CardList().Remove(flashCard);
            }

            // Redirect to the deck view after successful deletion
            return RedirectToAction("ViewDeck", "FlashCards", new { deckId = deck.Id });
        }

    }
}
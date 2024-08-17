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
    }
}
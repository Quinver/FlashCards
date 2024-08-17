using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashCards.Models
{
    public class Deck
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }

        private readonly List<FlashCard> FlashCards = new(); 

        public void AddFlashCard(FlashCard flashCard)
        {
            flashCard.Id = FlashCards.Count + 1;
            flashCard.DeckId = Id;
            FlashCards.Add(flashCard);
        }
        
        public List<FlashCard> CardList()
        {
            return FlashCards;
        }
    }
}
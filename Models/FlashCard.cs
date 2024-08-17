using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashCards.Models
{
    public class FlashCard
    {
        public int Id { get; set; }
        public int DeckId { get; set; }
        public string? Front { get; set; }
        public string? Back { get; set; }
    }
}
﻿using System.Collections.Generic;
using System.Linq;

namespace President.ObjectModel
{
    public class CardGroup
    {
        public CardNumber CardNumber { get; set; }
        public List<Card> Cards { get; set; }

        public int NumberOfCards
        {
            get
            {
                return Cards.Count();
            }
        }

        public CardGroup(CardNumber number, List<Card> cards)
        {
            CardNumber = number;
            Cards = cards;
        }
    }
}

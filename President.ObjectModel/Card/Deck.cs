using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace President.ObjectModel
{
    public class Deck
    {
        public List<Card> Cards { get; set; }

        public const int NUMBER_OF_CARDS = 52;

        public Deck()
        {
            this.Reset();
            this.Shuffle();
        }

        /// <summary>
        /// Resets the deck to its initial states
        /// </summary>
        public void Reset()
        {
            Cards = Enumerable.Range(1, 4)
                .SelectMany(s => Enumerable.Range(1, 13)
                    .Select(c => new Card((CardNumber)c, (CardType)s))).ToList();
        }

        /// <summary>
        /// Shuffles the deck randomly
        /// </summary>
        public void Shuffle()
        {
            Cards = Cards.OrderBy(c => Guid.NewGuid()).ToList();
        }

        /// <summary>
        /// Takes a given number of cards from the deck
        /// </summary>
        /// <param name="CardsToTake">The number of cards to take</param>
        /// <returns>A list of cards</returns>
        public List<Card> TakeCards(int nbCardsToTake)
        {
            var cardsToTake = Cards.Take(nbCardsToTake)
                .OrderBy(p => p.CardNumber)
                .ThenBy(p => p.CardType).ToList();
            
            Cards.RemoveRange(0, nbCardsToTake);

            return cardsToTake;
        }
    }
}

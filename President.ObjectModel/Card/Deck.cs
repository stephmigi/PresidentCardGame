using System;
using System.Collections.Generic;
using System.Linq;

namespace President.ObjectModel
{
    public class Deck
    {
        public List<Card> Cards { get; set; }

        /// <summary>
        /// Defines the number of cards in the deck
        /// </summary>
        public const int NUMBER_OF_CARDS = 52;

        /// <summary>
        /// Fills the deck with shuffled cards
        /// </summary>
        public void FillDeck()
        {
            this.Reset();
            this.Shuffle();
        }

        /// <summary>
        /// Resets the deck to its initial state
        /// </summary>
        private void Reset()
        {
            this.Cards = Enumerable.Range(1, 4)
                .SelectMany(s => Enumerable.Range(1, 13)
                    .Select(c => new Card((CardNumber)c, (CardType)s))).ToList();
        }

        /// <summary>
        /// Shuffles the deck randomly
        /// </summary>
        private void Shuffle()
        {
            this.Cards = this.Cards.OrderBy(c => Guid.NewGuid()).ToList();
        }

        /// <summary>
        /// Takes a given number of cards from the deck
        /// </summary>
        /// <param name="nbCardsToTake">The number of cards to take</param>
        /// <returns>A list of cards</returns>
        public List<Card> TakeCards(int nbCardsToTake)
        {
            var cardsToTake = this.Cards.Take(nbCardsToTake)
                .OrderBy(p => p.CardNumber)
                .ThenBy(p => p.CardType).ToList();

            this.Cards.RemoveRange(0, nbCardsToTake);

            return cardsToTake;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace President.ObjectModel
{
    public class Player
    {
        public string Name { get; set; }
        public List<CardGroup> PlayerCards { get; set; }
        public Order Order { get; set; }

        public bool IsItMyTurn { get; set; }

        public Player(string name, Order order)
        {
            this.Name = name == String.Empty ? "Joueur" : name;
            this.Order = order;
            PlayerCards = new List<CardGroup>();
        }

        /// <summary>
        /// Get a list of cards the player can play
        /// </summary>
        /// <param name="lastCardsPlayed">The last cards played</param>
        /// <returns>The list of playable cards</returns>
        public List<CardGroup> ShowPlayableCards(CardGroup lastCardsPlayed)
        {
            var nbCards = lastCardsPlayed.NumberOfCards;
            var cardPlayed = lastCardsPlayed.CardNumber;

            // deuce is the final card, can't play anything after that
            if (cardPlayed == CardNumber.Two)
                return null;

            var groupedPlayerCards = PlayerCards.GroupBy(p => p.CardNumber).SelectMany(group => group);

            var finalList = new List<CardGroup>();
            foreach (var group in groupedPlayerCards)
            {
                finalList.Add(group);
            }

            return finalList;
        }

        /// <summary>
        /// Tells if a player can play
        /// </summary>
        /// <param name="lastCardsPlayed">The last cards played</param>
        /// <returns>True if the player can play, else false</returns>
        public bool CanPlay(CardGroup lastCardsPlayed)
        {
            return ShowPlayableCards(lastCardsPlayed).Any();
        }

        public List<Card> Play(List<Card> lastCardsPlayed)
        {
            return null;
        }


    }
}

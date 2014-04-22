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

        public bool IsPlayingCurrentTurn { get; set; }
        public bool IsItMyTurn { get; set; }

        public int NumberOfCardsLeft
        {
            get
            {
                return PlayerCards.Sum(p => p.NumberOfCards);
            }
        }

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
        public List<CardGroup> GetPlayableCards(CardGroup lastCardsPlayed)
        {
            var nbCards = lastCardsPlayed.NumberOfCards;
            var cardPlayed = lastCardsPlayed.CardNumber;

            // deuce is the final card, can't play anything after that
            if (cardPlayed == CardNumber.Two)
                return null;

            return PlayerCards.Where(c => c.NumberOfCards >= nbCards && c.CardNumber >= cardPlayed).ToList();
        }

        /// <summary>
        /// Tells if a player can play
        /// He should have playable cards and has to be playing current round
        /// </summary>
        /// <param name="lastCardsPlayed">The last cards played</param>
        /// <returns>True if the player can play, else false</returns>
        public bool CanPlay(CardGroup lastCardsPlayed)
        {
            return GetPlayableCards(lastCardsPlayed).Any() && IsPlayingCurrentTurn;
        }

        /// <summary>
        /// Player plays the selected cards.
        /// This consists of putting the selected cards on top of the stack
        /// and removing them from the player's cards.
        /// </summary>
        /// <param name="playedCards">Cards the player plays</param>
        /// <param name="stack">The stack of cards</param>
        public void Play(CardGroup playedCards, List<CardGroup> stack)
        {
            // put playedCards on top of stack
            stack.Add(playedCards);

            // get the group where the cards come from
            var playerGroup = this.PlayerCards.Where(c => c.CardNumber == playedCards.CardNumber).FirstOrDefault();   

            // remove the played cards from the player's group
            playerGroup.Cards.RemoveAll(p => playedCards.Cards.Contains(p));

            // delete the group from the player's card if there are no more cards in group
            if (playedCards.NumberOfCards == 0)
                PlayerCards.Remove(playerGroup);

        }
    }
}

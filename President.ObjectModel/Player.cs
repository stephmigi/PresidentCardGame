using System;
using System.Collections.Generic;
using System.Linq;

namespace President.ObjectModel
{
    public class Player
    {
        /// <summary>
        /// Gets or sets player name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the list of player cards
        /// </summary>
        public List<CardGroup> PlayerCards { get; set; }

        /// <summary>
        /// Gets or sets the seat of the player (this defines order of playing)
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether it is my turn to play
        /// </summary>
        public bool IsItMyTurn { get; set; }

        /// <summary>
        /// Gets a value indicating whether I have finished playing the round
        /// </summary>
        public bool IsRoundFinishedForMe
        {
            get
            {
                return !this.PlayerCards.Any();
            }
        }

        /// <summary>
        /// Gets the number of cards left
        /// </summary>
        public int NumberOfCardsLeft
        {
            get
            {
                return this.PlayerCards.Sum(p => p.NumberOfCards);
            }
        }

        public Player(string name, Order order)
        {
            this.Name = name == string.Empty ? "Joueur" : name;
            this.Order = order;
            this.PlayerCards = new List<CardGroup>();
        }

        /// <summary>
        /// Get a list of cards the player can play
        /// </summary>
        /// <param name="lastCardsPlayed">The last cards played</param>
        /// <returns>The list of playable cards. Null if no cards can be played</returns>
        public List<CardGroup> GetPlayableCards(CardGroup lastCardsPlayed)
        {
            if (lastCardsPlayed == null) return this.PlayerCards;

            var cardsCount = lastCardsPlayed.NumberOfCards;
            var cardPlayed = lastCardsPlayed.CardNumber;

            // deuce is the final card, can't play anything after that
            if (cardPlayed == CardNumber.Two)
                return null;

            var playable =
                this.PlayerCards.Where(c => c.NumberOfCards >= cardsCount && c.CardNumber >= cardPlayed).ToList();
            return playable.Any() ? playable : null;
        }

        /// <summary>
        /// Tells if a player can play
        /// He should have playable cards and has to be playing current round
        /// </summary>
        /// <param name="lastCardsPlayed">The last cards played</param>
        /// <returns>True if the player can play, else false</returns>
        public bool CanPlayThisTurn(CardGroup lastCardsPlayed)
        {
            return this.GetPlayableCards(lastCardsPlayed) != null && !this.IsRoundFinishedForMe;
        }

        /// <summary>
        /// Player plays the selected cards.
        /// This consists of putting the selected cards on top of the stack
        /// and removing them from the player's cards.
        /// </summary>
        /// <param name="selectedCards">Cards the player wants to play</param>
        /// <param name="stack">The stack of cards</param>
        public void Play(CardGroup selectedCards, List<CardGroup> stack)
        {
            // put playedCards on top of stack
            stack.Add(selectedCards);

            // get the group where the cards come from
            var playerGroup = this.PlayerCards.FirstOrDefault(c => c.CardNumber == selectedCards.CardNumber);   

            // remove the played cards from the player's group
            playerGroup.Cards.RemoveAll(p => selectedCards.Cards.Contains(p));

            // delete the group from the player's card if there are no more cards in group
            if (playerGroup.NumberOfCards == 0)
                PlayerCards.Remove(playerGroup);

        }
    }
}

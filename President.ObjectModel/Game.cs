using System;
using System.Collections.Generic;
using System.Linq;

namespace President.ObjectModel
{
    public class Game
    {
        public List<Player> Players { get; set; }

        /// <summary>
        /// Gets or sets the initial deck
        /// </summary>
        public Deck Deck { get; set; }

        /// <summary>
        /// Gets or sets the stack of cards on which players play
        /// </summary>
        public List<CardGroup> Stack { get; set; }

        /// <summary>
        /// Gets the current player
        /// </summary>
        public Player CurrentPlayer
        {
            get
            {
                return this.Players.FirstOrDefault(p => p.IsItMyTurn);
            }
        }

        /// <summary>
        /// Gets the last cards played
        /// </summary>
        public CardGroup LastCardsOnStack
        {
            get
            {
                if (!Stack.Any()) return null;
                return this.Stack.Last();
            }
        }

        /// <summary>
        /// Gets or sets the score of the game
        /// </summary>
        public GameScore GameScore { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class. 
        /// </summary>
        /// <param name="players"> A list of players</param>
        public Game(List<Player> players)
        {
            Players = players;
            this.NewRound();
            GameScore = new GameScore();
        }

        /// <summary>
        /// Deal same number of cards to each player
        /// </summary>
        public void DealCards()
        {
            int cardsPerPlayer = Deck.NUMBER_OF_CARDS / this.Players.Count;
            foreach (var player in this.Players)
            {
                Deck.TakeCards(cardsPerPlayer)
                    .GroupBy(p => p.CardNumber)
                    .ForEach(p => player.PlayerCards.Add(new CardGroup(p.Key, p.ToList())));
            } 
        }

        /// <summary>
        /// Select randomly the first player to play the first turn of the round
        /// </summary>
        public void SelectFirstPlayerForRound()
        {
            var random = new Random().Next(0, this.Players.Count - 1);
            this.Players[random].IsItMyTurn = true;

            this.SetTurnStart();
        }

        /// <summary>
        /// Set the first player of the turn
        /// If it isn't the first turn, last player to have played previous round,
        /// should start the next one.
        /// </summary>
        private void SetTurnStart()
        {
            this.Players.Where(p => p != this.CurrentPlayer).ForEach(p => p.IsItMyTurn = false);
        }

        /// <summary>
        /// Select the next player to play (in the Order)
        /// Player will not be selected if he can't play
        /// </summary>
        /// <param name="lastCardsPlayed">The last Cards Played.</param>
        /// <returns><see cref="bool"/>True if a next player has been found else false</returns>
        public bool SelectNextPlayer(CardGroup lastCardsPlayed)
        {
            var currentPlayerOrder = this.CurrentPlayer.Order;
            var nextOrder = currentPlayerOrder.GetNextOrder();
            var nextPlayer = this.Players.FirstOrDefault(p => p.Order == nextOrder);

            while ((!nextPlayer.CanPlayThisTurn(lastCardsPlayed)) && currentPlayerOrder != nextOrder)
            {
                nextOrder = nextOrder.GetNextOrder();
                nextPlayer = this.Players.FirstOrDefault(p => p.Order == nextOrder);
            }

            if (currentPlayerOrder == nextOrder)
            {
                this.Players.ForEach(p => p.IsItMyTurn = false);
                return false;
            }
                
            this.Players.ForEach(p => p.IsItMyTurn = p == nextPlayer);
            return true;
        }

        /// <summary>
        /// Restart round
        /// </summary>
        public void NewRound()
        {
            this.Deck = new Deck();
            this.Stack = new List<CardGroup>();
        }

        /// <summary>
        /// New turn
        /// </summary>
        private void NewTurn()
        {
            this.Stack = new List<CardGroup>();
            this.SetTurnStart();
        }
    }
}

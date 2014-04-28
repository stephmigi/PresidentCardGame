using System;
using System.Collections.Generic;
using System.Linq;

namespace President.ObjectModel
{
    public class Game
    {
        #region properties

        /// <summary>
        /// The number of rounds to play
        /// </summary>
        public readonly int RoundsToPlay; 

        /// <summary>
        /// List of players of the game
        /// </summary>
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
                return this.Players.SingleOrDefault(p => p.IsItMyTurn);
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
        /// Gets the number of players that haven't finished playing
        /// </summary>
        public int PlayersStillPlayingRound
        {
            get
            {
                return this.Players.Count(p => p.IsRoundFinishedForMe == false);
            }
        }

        /// <summary>
        /// Gets or sets the score of the game
        /// </summary>
        public GameScore GameScore { get; set; }

        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class. 
        /// </summary>
        /// <param name="players"> A list of players</param>
        /// /// <param name="roundsToPlay"> Number of rounds to play</param>
        public Game(List<Player> players, int roundsToPlay)
        {
            this.Players = players;
            this.Deck = new Deck();
            this.Stack = new List<CardGroup>();
            GameScore = new GameScore();
            this.RoundsToPlay = roundsToPlay;
        }

        #endregion

        #region public methods

        /// <summary>
        /// Initialize a new round.
        /// </summary>
        public void InitializeRound()
        {
            this.Deck.FillDeck();
            this.DealCards();
            this.SelectFirstPlayerForRound();
        }

        /// <summary>
        /// Initialize a new turn.
        /// </summary>
        public void InitializeTurn()
        {
            this.Stack.Clear();
        }

        /// <summary>
        /// Select the next player to play (in the Order)
        /// Player will not be selected if he can't play
        /// </summary>
        /// <param name="lastCardsPlayed">The last Cards Played.</param>
        /// <returns><see cref="bool"/>True if a next player has been found else false</returns>
        public bool SelectNextPlayer(CardGroup lastCardsPlayed)
        {
            var nextOrder = this.CurrentPlayer.Order;
            Player nextPlayer;
            do
            {
                nextOrder = nextOrder.GetNextOrder();
                nextPlayer = this.Players.FirstOrDefault(p => p.Order == nextOrder);
            }
            while ((!nextPlayer.CanPlayThisTurn(lastCardsPlayed)) && this.CurrentPlayer.Order != nextOrder);

            if (this.CurrentPlayer.Order == nextOrder)
            {
                return false;
            }

            this.Players.ForEach(p => p.IsItMyTurn = p == nextPlayer);
            return true;
        }

        #endregion

        #region private methods

        /// <summary>
        /// Deal same number of cards to each player
        /// </summary>
        private void DealCards()
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
        private void SelectFirstPlayerForRound()
        {
            var random = new Random().Next(0, this.Players.Count - 1);
            this.Players.ForEach(p => p.IsItMyTurn = p == this.Players[random]);
        }

        #endregion
    }
}

namespace President.ObjectModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
                if (!this.Stack.Any())
                {
                    return null;
                }

                return this.Stack.Last();
            }
        }

        /// <summary>
        /// Gets a value indicating whether round is finished
        /// </summary>
        public bool IsRoundFinished
        {
            get
            {
                return !(this.Players.Count(p => p.IsRoundFinishedForMe == false) > 1);
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

        /// <summary>
        /// Play a game during a given number of rounds.
        /// </summary>
        /// <param name="roundsToPlay">Number of rounds to play</param>
        public void PlayGame(int roundsToPlay)
        {
            int roundsPlayed = 1;
            for (int i = 0; i < roundsToPlay; i++)
            {
                Console.WriteLine("-------------------");
                Console.WriteLine("Round #" + roundsPlayed);
                Console.WriteLine("-------------------");
                this.PlayRound();
                roundsPlayed++;
            }
        }

        /// <summary>
        /// Play one round
        /// </summary>
        private void PlayRound()
        {
            this.InitializeRound();
            int turnCount = 1;

            while (this.Players.Count(p => p.NumberOfCardsLeft > 0) >= 2)
            {
                Console.WriteLine("-------------------");
                Console.WriteLine("Turn #" + turnCount);
                Console.WriteLine("-------------------");
                this.PlayTurn();
                turnCount++;
            }
        }

        /// <summary>
        /// Play a turn
        /// </summary>
        private void PlayTurn()
        {
            this.InitializeTurn();
            do
            {
                // TODO : Hack to fix a bug when a player finishes and Select player still chooses him as Current..?? Fix it one day...
                if (this.CurrentPlayer.IsRoundFinishedForMe)
                {
                    continue;
                }

                CardGroup choice = null;
                int cardsToTake = 0;
                // Bots select cards stupidly
                if (this.CurrentPlayer.TypeOfPlayer == PlayerType.Bot)
                {
                    var playable = this.CurrentPlayer.GetPlayableCards(this.LastCardsOnStack);
                    choice = playable.First();

                    // if nothing is on stack, play the max number of cards possible
                    cardsToTake = this.Stack.Any() ? this.LastCardsOnStack.NumberOfCards : choice.NumberOfCards;
                }
                else
                {
                    // Here goes the logic for user selecting cards in the UI
                    throw new NotImplementedException();
                }

                CardGroup selectedCards = new CardGroup(
                    choice.CardNumber,
                    choice.Cards.Take(cardsToTake).ToList());

                this.CurrentPlayer.Play(selectedCards, this.Stack);

                Console.WriteLine(
                    this.CurrentPlayer.Name + "(Left : " + this.CurrentPlayer.NumberOfCardsLeft + ")");
                selectedCards.Cards.ForEach(
                    c => Console.WriteLine(c.CardNumber.ToString() + " of suit " + c.CardType));

                if (this.CurrentPlayer.IsRoundFinishedForMe)
                {
                    // do stuff when round is finished for player
                    Console.WriteLine(this.CurrentPlayer.Name + "has no more cards !!********");
                }
            }
            while (this.SelectNextPlayer(this.LastCardsOnStack) && !this.IsRoundFinished);
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

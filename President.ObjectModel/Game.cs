using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace President.ObjectModel
{
    public class Game
    {
        public List<Player> Players { get; set; }

        /// <summary>
        /// The initial deck
        /// </summary>
        public Deck Deck { get; set; }

        public List<CardGroup> Stack { get; set; }

        public Player CurrentPlayer
        {
            get
            {
                return Players.Where(p => p.IsItMyTurn == true).FirstOrDefault();
            }
        }

        public CardGroup LastCardsOnStack
        {
            get
            {
                return Stack.Last();
            }
        }

        public Game(List<Player> players)
        {
            Players = players;
            this.NewRound();
        }

        /// <summary>
        /// Deal same number of cards to each player
        /// </summary>
        public void DealCards()
        {
            int cardsPerPlayer = Deck.NUMBER_OF_CARDS / Players.Count;
            foreach (var player in Players)
            {
                Deck.TakeCards(cardsPerPlayer)
                    .GroupBy(p => p.CardNumber)
                    .ForEach(p => player.PlayerCards.Add(new CardGroup(p.Key, p.ToList())));
            } 
        }

        /// <summary>
        /// Select randomly the first player to play
        /// </summary>
        public void SelectFirstPlayer()
        {
            var first = new Random().Next(0, Players.Count - 1) ;
            Players[first].IsItMyTurn = true;
        }

        /// <summary>
        /// Select the next player to play (in the Order)
        /// </summary>
        public void SelectNextPlayer()
        {
            var currentPlayerOrder = CurrentPlayer.Order;
            var nextOrder = currentPlayerOrder == Order.Right ? Order.Top: ++currentPlayerOrder;
            var nextPlayer = Players.Where(p => p.Order == nextOrder).FirstOrDefault();

            Players.ForEach(p => p.IsItMyTurn = p == nextPlayer ? true : false);
        }

        /// <summary>
        /// Tells if any player at the table can play on top of the stack
        /// </summary>
        /// <returns>True if another player can play, else false</returns>
        public bool IsTurnOver()
        {
            return Players.Where(p => p.CanPlay(Stack.Last())).Any();
        }

        /// <summary>
        /// Restart round
        /// </summary>
        public void NewRound()
        {
            Deck = new Deck();
            Stack = new List<CardGroup>();
        }

        /// <summary>
        /// New turn
        /// </summary>
        public void NewTurn()
        {
            Stack = new List<CardGroup>();
        }
    }
}

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

        public Deck Deck { get; set; }

        public Player CurrentPlayer
        {
            get
            {
                return Players.Where(p => p.IsItMyTurn == true).FirstOrDefault();
            }
        }

        public Game(List<Player> players)
        {
            Players = players;
            Deck = new Deck();
        }

        /// <summary>
        /// Deal same number of cards to each player
        /// </summary>
        public void DealCards()
        {
            int cardsPerPlayer = Deck.NUMBER_OF_CARDS / Players.Count;
            Players.ForEach(p => p.PlayerCards = Deck.TakeCards(cardsPerPlayer));
        }

        /// <summary>
        /// Select the first player of the game to play
        /// </summary>
        public void SelectFirstPlayer()
        {
            var first = new Random().Next(0, Players.Count - 1) ;
            Players[first].IsItMyTurn = true;
        }

        /// <summary>
        /// Select the next player to play
        /// </summary>
        public void SelectNextPlayer()
        {
            var currentPlayerOrder = CurrentPlayer.Order;

            var nextOrder = currentPlayerOrder == Order.Right ? Order.Top: ++currentPlayerOrder;
            var nextPlayer = Players.Where(p => p.Order == nextOrder).FirstOrDefault();

            Players.ForEach(p => p.IsItMyTurn = false);
            nextPlayer.IsItMyTurn = true;
        }
    }
}

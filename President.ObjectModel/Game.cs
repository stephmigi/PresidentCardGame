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
            int cardsPerPlayer = 52 / Players.Count;
            Players.ForEach(p => p.PlayerCards = Deck.TakeCards(cardsPerPlayer));
        }
    }
}

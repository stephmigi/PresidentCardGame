using NUnit.Framework;
using President.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace President.Tests
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void TestingTheDeck()
        {
            // new players
            var playerList = new List<Player>
                            {
                                new Player("Joueur 1", PlayerType.Bot, Order.Top),
                                new Player("Joueur 2", PlayerType.Bot, Order.Left),
                                new Player("Joueur 3", PlayerType.Bot, Order.Bottom), 
                                new Player("Joueur 4", PlayerType.Bot, Order.Right) 
                            };

            int roundsToPlay = 10;

            // Start new game with players
            var game = new Game(playerList, roundsToPlay);

            game.PlayGame(10);
        }
    }
}

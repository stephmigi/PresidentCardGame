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
                                new Bot("Joueur 1", Order.Top),
                                new Bot("Joueur 2", Order.Left),
                                new Bot("Joueur 3", Order.Bottom), 
                                new Bot("Joueur 4", Order.Right) 
                            };

            int roundsToPlay = 10;

            // Start new game with players
            var game = new Game(playerList, roundsToPlay);

            game.PlayGame(10);
        }
    }
}

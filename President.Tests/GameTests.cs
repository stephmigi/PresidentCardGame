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
        public void Deck_HasOnlyDifferentCards()
        {
            var Deck = new Deck();
            Deck.FillDeck();
            Assert.That(Deck.Cards.Distinct().Count() == Deck.Cards.Count());
        }

        [Test]
        public void TestingGame()
        {
            // new bots
            var playerList = new List<Player>
                            {
                                new Bot("Joueur 1", Order.Top),
                                new Bot("Joueur 2", Order.Left),
                                new Bot("Joueur 3", Order.Bottom), 
                                new Bot("Joueur 4", Order.Right) 
                            };

            // Start new game of 10 rounds with four players
            var game = new Game(playerList, 10);

            game.PlayGame();
        }
    }
}

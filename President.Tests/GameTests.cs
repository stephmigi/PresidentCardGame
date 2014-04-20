using NUnit.Framework;
using President.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace President.Tests
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void TestingTheDeck()
        {
            var Player1 = new Player("Joueur 1");
            var Player2 = new Player("Joueur 2");
            var Player3 = new Player("Joueur 3");
            var Player4 = new Player("Joueur 4");

            var playerList = new List<Player>();
            playerList.Add(Player1);
            playerList.Add(Player2);
            playerList.Add(Player3);
            playerList.Add(Player4);

            var game = new Game(playerList);

            game.DealCards();

            Console.WriteLine("Player 1 :");
            game.Players[0].PlayerCards.ForEach(c => Console.WriteLine(c.CardNumber + " " + c.CardType));

            Console.WriteLine("Player 2 :");
            game.Players[1].PlayerCards.ForEach(c => Console.WriteLine(c.CardNumber + " " + c.CardType));

            Console.WriteLine("Player 3 :");
            game.Players[2].PlayerCards.ForEach(c => Console.WriteLine(c.CardNumber + " " + c.CardType));

            Console.WriteLine("Player 4 :");
            game.Players[3].PlayerCards.ForEach(c => Console.WriteLine(c.CardNumber + " " + c.CardType));
        }
    }
}

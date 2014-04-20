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
            // new players
            var Player1 = new Player("Joueur 1", Order.Top);
            var Player2 = new Player("Joueur 2", Order.Left);
            var Player3 = new Player("Joueur 3", Order.Bottom);
            var Player4 = new Player("Joueur 4", Order.Right);

            var playerList = new List<Player>();
            playerList.Add(Player1);
            playerList.Add(Player2);
            playerList.Add(Player3);
            playerList.Add(Player4);

            // Start new game with players
            var game = new Game(playerList);

            // Deal the players some cards
            game.DealCards();

            game.SelectFirstPlayer();
            //Console.WriteLine("First Player : " + game.CurrentPlayer.Name + " at position " + 
            //    game.CurrentPlayer.Order.ToString());
            //game.SelectNextPlayer();
            //Console.WriteLine("Next Player : " + game.CurrentPlayer.Name + " at position " +
            //    game.CurrentPlayer.Order.ToString());
            //game.SelectNextPlayer();
            //Console.WriteLine("Next Player : " + game.CurrentPlayer.Name + " at position " +
            //    game.CurrentPlayer.Order.ToString());
            //game.SelectNextPlayer();
            //Console.WriteLine("Next Player : " + game.CurrentPlayer.Name + " at position " +
            //    game.CurrentPlayer.Order.ToString());
            //game.SelectNextPlayer();
            //Console.WriteLine("Next Player : " + game.CurrentPlayer.Name + " at position " +
                //game.CurrentPlayer.Order.ToString());

            var cardsOnTable = new List<Card>();
            cardsOnTable.Add(new Card(CardNumber.Three, CardType.Club));
            cardsOnTable.Add(new Card(CardNumber.Three, CardType.Heart));

            Console.WriteLine("My Cards : ");
            game.CurrentPlayer.PlayerCards.ForEach(p => Console.WriteLine(p.CardNumber + " of suit " + p.CardType));
            var playable = game.CurrentPlayer.ShowPlayableCards(cardsOnTable);
            if (playable.Any())
            {
                Console.WriteLine("I can play : ");
                playable.ForEach(p => Console.WriteLine(p.CardNumber + " of suit " + p.CardType));
            }
               
        }
    }
}

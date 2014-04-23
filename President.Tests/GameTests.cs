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

            game.SelectFirstPlayerForRound();
            Console.WriteLine("First player to start: " + game.CurrentPlayer.Name);

            //var cardsOnTable = new List<Card>();
            //cardsOnTable.Add(new Card(CardNumber.Three, CardType.Club));
            //cardsOnTable.Add(new Card(CardNumber.Three, CardType.Heart));
            //cardsOnTable.Add(new Card(CardNumber.Three, CardType.Spade));
            //var cardGroup = new CardGroup(CardNumber.Three, cardsOnTable);

            //game.Stack.Add(cardGroup);

            do
            {
                Console.WriteLine(game.CurrentPlayer.Name);

                var playable = game.CurrentPlayer.GetPlayableCards(game.LastCardsOnStack);

                // user will select cards in the UI, for now just take stupidly the first possiblity
                var choice = playable.First();

                // if nothing is on stack, play the max number of cards possible
                int cardsToTake = game.Stack.Any() ? game.LastCardsOnStack.NumberOfCards : choice.NumberOfCards;

                CardGroup selectedCards = new CardGroup(choice.CardNumber, choice.Cards.Take(cardsToTake).ToList());

                selectedCards.Cards.ForEach(c => Console.WriteLine(c.CardNumber.ToString() + " of suit " + c.CardType));

                game.CurrentPlayer.Play(selectedCards, game.Stack);
            }
            while (game.SelectNextPlayer(game.LastCardsOnStack));
        }
    }
}

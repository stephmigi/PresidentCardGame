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
                                new Player("Joueur 1", Order.Top),
                                new Player("Joueur 2", Order.Left),
                                new Player("Joueur 3", Order.Bottom), 
                                new Player("Joueur 4", Order.Right) 
                            };

            // Start new game with players
            var game = new Game(playerList);

            // Init game
            game.InitializeRound();

            Console.WriteLine("First player to start: " + game.CurrentPlayer.Name);

            int turnCount = 1;

            while (game.Players.Count(p => p.NumberOfCardsLeft > 0) >= 2)
            {
                game.InitializeTurn();
                Console.WriteLine("-------------------");
                Console.WriteLine("Turn #" + turnCount);
                Console.WriteLine("-------------------");
                do
                {
                    //TODO : Hack to fix a bug when a player finishes and Select player still chooses him as Current..?? Fix it one day...
                    if (game.CurrentPlayer.IsRoundFinishedForMe) continue;

                    var playable = game.CurrentPlayer.GetPlayableCards(game.LastCardsOnStack);

                    // user will select cards in the UI, for now just take stupidly the first possiblity
                    var choice = playable.First();

                    // if nothing is on stack, play the max number of cards possible
                    int cardsToTake = game.Stack.Any() ? game.LastCardsOnStack.NumberOfCards : choice.NumberOfCards;

                    CardGroup selectedCards = new CardGroup(choice.CardNumber, choice.Cards.Take(cardsToTake).ToList());

                    game.CurrentPlayer.Play(selectedCards, game.Stack);

                    Console.WriteLine(game.CurrentPlayer.Name + "(Left : " + game.CurrentPlayer.NumberOfCardsLeft + ")");
                    selectedCards.Cards.ForEach(c => Console.WriteLine(c.CardNumber.ToString() + " of suit " + c.CardType));

                    if (game.CurrentPlayer.IsRoundFinishedForMe)
                    {
                        Console.WriteLine(game.CurrentPlayer.Name + "has no more cards !!********");
                    }
                }
                while (game.SelectNextPlayer(game.LastCardsOnStack) && game.PlayersStillPlayingRound > 1);
                turnCount++;
            }
        }
    }
}

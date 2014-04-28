using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace President.ObjectModel
{
    public class Bot : Player
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Bot"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="order">The order.</param>
        public Bot(string name, Order order)
            : base(name, order)
        {
        }

        /// <summary>
        /// Return this instance.
        /// </summary>
        public override Bot AsBot
        {
            get
            {
                return this; 
            }  
        }

        /// <summary>
        /// Select cards to play on top of stack when it is my turn.
        /// </summary>
        /// <param name="lastCardsOnStack">The last cards on the stack.</param>
        /// <returns>The cards the bot has chosen</returns>
        public CardGroup SelectCardsToPlay(CardGroup lastCardsOnStack)
        {
            var playable = this.GetPlayableCards(lastCardsOnStack);

            // Stupidly take the first option for now.
            var choice = playable.First();

            // if nothing is on stack, play the max number of cards possible
            int cardsToTake = lastCardsOnStack != null ? lastCardsOnStack.NumberOfCards : choice.NumberOfCards;

            return new CardGroup(
                choice.CardNumber,
                choice.Cards.Take(cardsToTake).ToList());
        }
    }
}

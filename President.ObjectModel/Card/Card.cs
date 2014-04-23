namespace President.ObjectModel
{
    public class Card
    {
        public CardNumber CardNumber { get; set; }
        public CardType CardType { get; set; }

        public Card(CardNumber cardNumber, CardType type)
        {
            CardNumber = cardNumber;
            CardType = type;
        }
    }
}
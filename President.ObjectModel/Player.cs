using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace President.ObjectModel
{
    public class Player
    {
        public string Name { get; set; }
        public List<Card> PlayerCards { get; set; }
        public Order Order { get; set; }

        public bool IsItMyTurn {get;set;}

        public Player(string name, Order order)
        {
            this.Name = name == String.Empty ? "Joueur" : name;
            this.Order = order;
        }
    }
}

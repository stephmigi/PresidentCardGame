using System.Collections.Generic;

namespace President.ObjectModel
{
    public class GameScore
    {
        public List<Dictionary<Player,int >> ScoreByRound { get; set; }
        public int NumberOfRoundsPlayed { get; set; }

        public GameScore()
        {
            this.ScoreByRound = new List<Dictionary<Player, int>>();
            this.NumberOfRoundsPlayed = 0;
        }
    }
}

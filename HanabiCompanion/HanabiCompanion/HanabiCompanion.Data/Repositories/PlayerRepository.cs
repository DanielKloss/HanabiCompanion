using System;
using System.Collections.Generic;
using System.Linq;
using HanabiCompanion.Data.Models;

namespace HanabiCompanion.Data.Repositories
{
    public class PlayerRepository : Repository
    {
        public IEnumerable<Player> GetAllPlayers()
        {
            return connection.Table<Player>();
        }

        public bool PlayerExists(string newPlayerName)
        {
            return !(connection.Table<Player>().SingleOrDefault(p => p.name == newPlayerName) == null);
        }

        public void AddPlayer(Player playerToAdd)
        {
            connection.Insert(playerToAdd);
        }

        public void DeletePlayerById(int id)
        {
            connection.Table<PlayerGame>().Delete(p => p.playerId == id);
            connection.Table<Player>().Delete(p => p.id == id);
        }

        public void EditPlayer(Player newPlayer)
        {
            connection.Update(newPlayer, typeof(Player));
        }

        public int GetNumberOfGamesById(int id)
        {
            return connection.Table<PlayerGame>().Where(pg => pg.playerId == id).Count();
        }

        public int GetNumberOfPerfectGames(int id)
        {
            int numberOfPerfectGames = 0;
            
            foreach (PlayerGame game in connection.Table<PlayerGame>().Where(pg => pg.playerId == id))
            {
                if (game.totalScore == 25 && !game.multicolour)
                {
                    numberOfPerfectGames++;
                }

                if (game.totalScore == 30 && game.multicolour && !game.multiColourIsWild)
                {
                    numberOfPerfectGames++;
                }
            }

            return numberOfPerfectGames;
        }

        public int GetWorstScoreById(int id)
        {
            var scores = from playergame in connection.Table<PlayerGame>()
                         where playergame.playerId == id
                         orderby playergame.totalScore ascending
                         select playergame;

            if (scores.FirstOrDefault() == null)
            {
                return 0;
            }
            else
            {
                return scores.FirstOrDefault().totalScore;
            }
        }

        public int GetBestScoreById(int id)
        {
            var scores = from playergame in connection.Table<PlayerGame>()
                         where playergame.playerId == id
                         orderby playergame.totalScore descending
                         select playergame;

            if (scores.FirstOrDefault() == null)
            {
                return 0;
            }

            else
            {
                return scores.FirstOrDefault().totalScore;
            }
        }

        public double GetAverageScoreById(int id)
        {
            var allScores = connection.Table<PlayerGame>().Where(pg => pg.playerId == id).Select(p => p.totalScore);

            if (allScores.Count() == 0)
            {
                return 0;
            }
            else
            {
                return Math.Round(Convert.ToDouble(allScores.Sum()) / Convert.ToDouble(allScores.Count()), 0);
            }
        }

        public KeyValuePair<string, int> GetHighestScoringColourById(int id)
        {
            Dictionary<string, int> colourScores = new Dictionary<string, int>();

            colourScores.Add("Red", connection.Table<PlayerGame>().Where(pg => pg.playerId == id).Sum(c => c.redScore));
            colourScores.Add("White", connection.Table<PlayerGame>().Where(pg => pg.playerId == id).Sum(c => c.whiteScore));
            colourScores.Add("Blue", connection.Table<PlayerGame>().Where(pg => pg.playerId == id).Sum(c => c.blueScore));
            colourScores.Add("Yellow", connection.Table<PlayerGame>().Where(pg => pg.playerId == id).Sum(c => c.yellowScore));
            colourScores.Add("Green", connection.Table<PlayerGame>().Where(pg => pg.playerId == id).Sum(c => c.greenScore));
            colourScores.Add("Multi", connection.Table<PlayerGame>().Where(pg => pg.playerId == id).Sum(c => c.multiScore));

            KeyValuePair<string, int> maxScore = colourScores.First();

            for (int i = 0; i < colourScores.Count; i++)
            {
                foreach (KeyValuePair<string, int> score in colourScores)
                {
                    if (score.Value > maxScore.Value)
                    {
                        maxScore = score;
                    }
                }
            }

            return maxScore;
        }

        public int GetNumberOfGamesWithNoLivesLost(int id)
        {
            return connection.Table<PlayerGame>().Where(pg => pg.playerId == id && pg.livesLost == 0).Count();
        }

        public int GetNumberOfLivesLost(int id)
        {
            int livesLost = 0;

            foreach (PlayerGame game in connection.Table<PlayerGame>().Where(pg => pg.playerId == id))
            {
                livesLost += game.livesLost;
            }

            return livesLost;
        }
    }
}

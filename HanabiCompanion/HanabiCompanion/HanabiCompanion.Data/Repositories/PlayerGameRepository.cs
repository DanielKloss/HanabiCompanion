using System;
using System.Linq;
using HanabiCompanion.Data.Models;

namespace HanabiCompanion.Data.Repositories
{
    public class PlayerGameRepository : Repository
    {
        public void AddPlayerGame(Player player, Game game)
        {
            connection.Insert(new PlayerGame
            {
                gameId = game.id,
                playerId = player.id,
                totalScore = player.totalScore,
                multicolour = game.multicolour,
                multiColourIsWild = game.multiColourIsWild,
                livesLost = player.livesLost
            });
        }

        public int? GetWorstEverScore()
        {
            var scores = from playergame in connection.Table<PlayerGame>()
                         orderby playergame.totalScore ascending
                         select playergame;

            return scores.FirstOrDefault()?.totalScore;
        }

        public int? GetBestEverScore()
        {
            var scores = from playergame in connection.Table<PlayerGame>()
                         orderby playergame.totalScore descending
                         select playergame;

            return scores.FirstOrDefault()?.totalScore;
        }
    }
}

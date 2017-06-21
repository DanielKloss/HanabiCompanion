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
    }
}

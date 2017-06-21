using System.Collections.Generic;
using HanabiCompanion.Data.Models;

namespace HanabiCompanion.Data.Repositories
{
    public class AchievementRepository : Repository
    {
        public void AddAchievement(Achievement achievement)
        {
            connection.Insert(achievement);
        }

        public IEnumerable<Achievement> GetAchievementsById(int id)
        {
            return connection.Table<Achievement>().Where(a => a.playerId == id);
        }
    }
}

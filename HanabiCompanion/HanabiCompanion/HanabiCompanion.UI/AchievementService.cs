using System;
using HanabiCompanion.Data.Models;
using HanabiCompanion.Data.Repositories;

namespace HanabiCompanion.UI
{
    public class AchievementService
    {
        private Game game;
        private PlayerRepository playerRepo;
        private AchievementRepository achievementRepo;
        private PlayerGameRepository playerGameRepo;

        public AchievementService(Game Game)
        {
            game = Game;
            playerRepo = new PlayerRepository();
            achievementRepo = new AchievementRepository();
            playerGameRepo = new PlayerGameRepository();
        }

        internal void AddAchievements(Game game)
        {
            foreach (Player player in game.players)
            {
                foreach (Achievement achievement in player.achievements)
                {
                    AchievementRepository achievementRepo = new AchievementRepository();

                    achievementRepo.AddAchievement(achievement);
                }
            }
        }

        internal void CheckForFirsts(Game game)
        {
            foreach (Player player in game.players)
            {
                if (playerRepo.GetNumberOfGamesById(player.id) == 1)
                {
                    player.achievements.Add(new Achievement() { playerId = player.id, title = "1st Game", image = "/Assets/Logos/milestoneGame.png", dateTime = DateTime.Now });
                }
            }
        }

        internal void CheckForMileStones(Game game)
        {
            foreach (Player player in game.players)
            {
                int numberOfGames = playerRepo.GetNumberOfGamesById(player.id);

                if (numberOfGames % 10 == 0)
                {
                    player.achievements.Add(new Achievement() { playerId = player.id, title = numberOfGames + "th Game", image = "/Assets/Logos/milestoneGame.png", dateTime = DateTime.Now });
                }
            }
        }
    }
}

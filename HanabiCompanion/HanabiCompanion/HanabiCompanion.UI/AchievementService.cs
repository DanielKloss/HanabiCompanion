using System;
using HanabiCompanion.Data.Models;
using HanabiCompanion.Data.Repositories;

namespace HanabiCompanion.UI
{
    public class AchievementService
    {
        private PlayerRepository playerRepo;
        private AchievementRepository achievementRepo;
        private PlayerGameRepository playerGameRepo;
        private Game game;

        public AchievementService(Game Game)
        {
            game = Game;
            playerRepo = new PlayerRepository();
            achievementRepo = new AchievementRepository();
            playerGameRepo = new PlayerGameRepository();
        }

        internal void AddAchievements()
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

        internal void CheckForFirsts()
        {
            foreach (Player player in game.players)
            {
                if (playerRepo.GetNumberOfGamesById(player.id) == 1)
                {
                    player.achievements.Add(new Achievement() { playerId = player.id, title = "1st Game", image = "/Assets/Logos/milestoneGame.png", dateTime = DateTime.Now });
                }

                if (player.totalScore == 25 && !game.multicolour && playerRepo.GetNumberOfPerfectGames(player.id) == 1)
                {
                    player.achievements.Add(new Achievement() { playerId = player.id, title = "Perfect Game", image = "/Assets/Logos/perfectGame.png", dateTime = DateTime.Now });
                }

                if (player.totalScore == 30 && game.multicolour && !game.multiColourIsWild && playerRepo.GetNumberOfPerfectGames(player.id) == 1)
                {
                    player.achievements.Add(new Achievement() { playerId = player.id, title = "Perfect Game", image = "/Assets/Logos/perfectGame.png", dateTime = DateTime.Now });
                }

                if (player.livesLost == 0 && playerRepo.GetNumberOfGamesWithNoLivesLost(player.id) == 1)
                {
                    player.achievements.Add(new Achievement() { playerId = player.id, title = "No Lives Lost", image = "/Assets/Logos/noLivesLost.png", dateTime = DateTime.Now });
                }
            }
        }

        internal void CheckForMileStones()
        {
            foreach (Player player in game.players)
            {
                int numberOfGames = playerRepo.GetNumberOfGamesById(player.id);

                if (numberOfGames % 10 == 0)
                {
                    player.achievements.Add(new Achievement() { playerId = player.id, title = numberOfGames + "th Game", image = "/Assets/Logos/milestoneGame.png", dateTime = DateTime.Now });
                }

                int numberOfPerfectGames = playerRepo.GetNumberOfPerfectGames(player.id);

                if (player.totalScore == 25 && !game.multicolour && numberOfPerfectGames % 10 == 0)
                {
                    player.achievements.Add(new Achievement() { playerId = player.id, title = numberOfPerfectGames + "th Perfect Game", image = "/Assets/Logos/milestonePerfectGame.png", dateTime = DateTime.Now });
                }

                if (player.totalScore == 30 && game.multicolour && !game.multiColourIsWild && numberOfPerfectGames % 10 == 0)
                {
                    player.achievements.Add(new Achievement() { playerId = player.id, title = numberOfPerfectGames + "th Perfect Game", image = "/Assets/Logos/milestonePerfectGame.png", dateTime = DateTime.Now });
                }

                int numberOfNoLivesLost = playerRepo.GetNumberOfGamesWithNoLivesLost(player.id);

                if (player.livesLost == 0 && numberOfNoLivesLost % 10 == 0)
                {
                    player.achievements.Add(new Achievement() { playerId = player.id, title = numberOfNoLivesLost + "th No Lives Lost", image = "/Assets/Logos/milestoneNoLivesLost.png", dateTime = DateTime.Now });
                }
            }
        }

        internal void CheckForEverScores()
        {
            foreach (Player player in game.players)
            {
                if (player.totalScore < playerGameRepo.GetWorstEverScore())
                {
                    player.achievements.Add(new Achievement() { playerId = player.id, title = "New Worst Ever Score (" + player.totalScore + ")", image = "/Assets/Logos/globalDown.png", dateTime = DateTime.Now });
                }

                if (player.totalScore > playerGameRepo.GetBestEverScore())
                {
                    player.achievements.Add(new Achievement() { playerId = player.id, title = "New Best Ever Score (" + player.totalScore + ")", image = "/Assets/Logos/globalUp.png", dateTime = DateTime.Now });
                }
            }
        }

        internal void CheckForPersonalScores()
        {
            foreach (Player player in game.players)
            {
                if (playerRepo.GetNumberOfGamesById(player.id) > 0 && player.totalScore < playerRepo.GetWorstScoreById(player.id))
                {
                    player.achievements.Add(new Achievement() { playerId = player.id, title = "New Personal Worst Score (" + player.totalScore + ")", image = "/Assets/Logos/personalDown.png", dateTime = DateTime.Now });
                }

                if (playerRepo.GetNumberOfGamesById(player.id) > 0 && player.totalScore > playerRepo.GetBestScoreById(player.id))
                {
                    player.achievements.Add(new Achievement() { playerId = player.id, title = "New Personal Best Score (" + player.totalScore + ")", image = "/Assets/Logos/personalUp.png", dateTime = DateTime.Now });
                }
            }
        }
    }
}

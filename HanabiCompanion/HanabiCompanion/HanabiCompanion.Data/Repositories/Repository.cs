using SQLite.Net;
using System.IO;
using HanabiCompanion.Data.Models;
using Windows.Storage;

namespace HanabiCompanion.Data.Repositories
{
    public abstract class Repository
    {
        protected static SQLiteConnection connection
        {
            get
            {
                return new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), Path.Combine(ApplicationData.Current.LocalFolder.Path, "Hanabi.sql"));
            }
        }

        public void CreateTables()
        {
            if (connection.GetTableInfo(nameof(Player)).Count == 0)
            {
                connection.CreateTable<Player>();
            }

            if (connection.GetTableInfo(nameof(Game)).Count == 0)
            {
                connection.CreateTable<Game>();
            }

            if (connection.GetTableInfo(nameof(PlayerGame)).Count == 0)
            {
                connection.CreateTable<PlayerGame>();
            }

            if (connection.GetTableInfo(nameof(Achievement)).Count == 0)
            {
                connection.CreateTable<Achievement>();
            }
        }

        public void DropTables()
        {
            connection.DropTable<Player>();
            connection.DropTable<Game>();
            connection.DropTable<PlayerGame>();
            connection.DropTable<Achievement>();
        }
    }
}

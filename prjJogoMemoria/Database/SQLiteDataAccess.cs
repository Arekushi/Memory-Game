using Dapper;
using prjJogoMemoria.Model;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace prjJogoMemoria.Database
{
    public class SQLiteDataAccess
    {
        public static List<Player> SelectHighScore()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Player>("SELECT * FROM Player", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void UpdateHighScore(Player player)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("UPDATE Player set highScore = @highScore", player);
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

    }
}

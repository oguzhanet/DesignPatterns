using Strategy.DesignPattern.WebApp.Enums;

namespace Strategy.DesignPattern.WebApp.Entities
{
    public class Settings
    {
        public static string claimDatabaseType = "databasetype";

        public EDatabaseType DatabaseType;

        public EDatabaseType GetDefaultDataBaseType => EDatabaseType.MSSqlServer;
    }
}

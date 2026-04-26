using System.IO;
using System.Xml.Serialization;

namespace HeroEngine.Core.Models
{
    [XmlRoot("GameConfig")]
    public class GameConfig
    {
        public double LevelMultiplier { get; set; } = 1.15;
        public double CriticalHitChance { get; set; } = 0.20;
        public int MaxCombatRounds { get; set; } = 20;
        public int MaxHeroesPerBattle { get; set; } = 4;
    }

    public static class GameConfigManager
    {
        private static readonly string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "game_config.xml");

        public static GameConfig LoadConfig()
        {
            if (!File.Exists(filePath))
            {
                var defaultConfig = new GameConfig();
                SaveConfig(defaultConfig);
                return defaultConfig;
            }

            XmlSerializer serializer = new XmlSerializer(typeof(GameConfig));
            using (StreamReader reader = new StreamReader(filePath))
            {
                return (GameConfig)serializer.Deserialize(reader);
            }
        }

        public static void SaveConfig(GameConfig config)
        {
            string directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

            XmlSerializer serializer = new XmlSerializer(typeof(GameConfig));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, config);
            }
        }
    }
}
using HeroEngine.Core.Data;

namespace HeroEngine.Core.Combat;

/// <summary>
/// Provides static utility methods to log battle events simultaneously to the console and a local text file.
/// </summary>
public static class BattleLogger
{
    private const string separator = "======================================================";
    private const string logInitializeMSG = "               BATTLE LOG INITIALIZED              ";
    private const string battleStartMSG = "                    BATTLE STARTS                     ";

    private static readonly string PublicLogPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Data", "battleLog.txt");
    private static readonly string PrivateLogPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "battleLog.txt");
    public static List<string> CurrentBattleLogs { get; private set; } = new List<string>();

    /// <summary>
    /// Initializes a new battle log session. 
    /// Overwrites any existing log file with a fresh header to signify the start of a new battle.
    /// </summary>
    public static void Initialize()
    {
        CurrentBattleLogs.Clear();

        EnsureFileIsClean(PublicLogPath);
        EnsureFileIsClean(PrivateLogPath);

        Log(separator);
        Log(logInitializeMSG);
        Log(battleStartMSG);
        Log(separator);
        Log("");
    }

    /// <summary>
    /// Outputs a provided message to the console and appends it to the battle log file.
    /// </summary>
    public static void Log(string message)
    {
        CurrentBattleLogs.Add(message);

        string logEntry = $"[{DateTime.Now:HH:mm:ss}] {message}";
        TXTManager.Apppend(PublicLogPath, logEntry);
        TXTManager.Apppend(PrivateLogPath, logEntry);
    }
    private static void EnsureFileIsClean(string path)
    {
        string directory = Path.GetDirectoryName(path);
        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
        File.WriteAllText(path, string.Empty);
    }
}
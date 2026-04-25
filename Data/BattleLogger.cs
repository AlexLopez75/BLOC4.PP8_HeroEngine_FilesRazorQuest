namespace BLOC4.PP8_HeroEngine_FilesRazorQuest.Data;

/// <summary>
/// Provides static utility methods to log battle events simultaneously to the console and a local text file.
/// </summary>
public static class BattleLogger
{
    private const string filePath = "Files/battleLog.txt";
    private const string separator = "======================================================";
    private const string logInitializeMSG = "               BATTLE LOG INITIALIZED              ";
    private const string fileNotFoundMSG = "[ERROR] File not found: {0}";
    private const string unableToWriteMSG = "[ERROR] Unable to write into log: {0}";
    
    /// <summary>
    /// Initializes a new battle log session. 
    /// Overwrites any existing log file with a fresh header to signify the start of a new battle.
    /// </summary>
    /// <remarks>
    /// This method safely handles <see cref="FileNotFoundException"/> and prints an error to the console 
    /// if the target file path cannot be resolved.
    /// </remarks>
    public static void Initialize()
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(filePath, false)) //false lets us rewrite the file.
            {
                sw.WriteLine(separator);
                sw.WriteLine(logInitializeMSG);
                sw.WriteLine(separator);
                sw.WriteLine("");
            }
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine(fileNotFoundMSG, ex.Message);
        }
    }

    /// <summary>
    /// Outputs a provided message to the console and appends it to the battle log file.
    /// </summary>
    /// <param name="message">The text string to be logged.</param>
    /// <remarks>
    /// Opens the file stream in append mode (<c>true</c>) so previous log entries are preserved. 
    /// If an error occurs during the file writing process, it is caught and an error message is printed to the console.
    /// </remarks>
    public static void Log(string message)
    {
        Console.WriteLine(message);

        try
        {
            using (StreamWriter sw = new StreamWriter(filePath, true)) //true lets us add text without rewriting the file.
            {
                sw.WriteLine(message);
            }
        }
        catch (Exception ex) 
        {
            Console.WriteLine(unableToWriteMSG, ex.Message);
        }
    }
}
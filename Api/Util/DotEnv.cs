namespace Common.Util;

public static class DotEnv
{
    private static void Load(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Could not find file at {filePath}");
            return;
        }

        foreach (var line in File.ReadAllLines(filePath))
        {
            var parts = SplitByFirstOccurrence(line, '=');
            if (parts.Length != 2)
                continue;
            Environment.SetEnvironmentVariable(parts[0], parts[1]);
        }
    }

    public static string[] SplitByFirstOccurrence(string input, char delimiter)
    {
        var index = input.IndexOf(delimiter);
        if (index < 0)
            // Delimiter not found, return array with original string as only element
            return new[] { input };
        // Split into two parts
        return new[]
        {
            input.Substring(0, index),
            input.Substring(index + 1)
        };
    }

    public static void LoadEnv()
    {
        var root = Directory.GetCurrentDirectory();
        var dotenvPath = Path.Combine(root, ".env");
        Console.WriteLine(dotenvPath);
        Load(dotenvPath);
        // var baseDir = AppContext.BaseDirectory;
        // var dotenvPath = Path.Combine(baseDir, ".env");
        // Console.WriteLine(dotenvPath);
        // Load(dotenvPath);
    }
}
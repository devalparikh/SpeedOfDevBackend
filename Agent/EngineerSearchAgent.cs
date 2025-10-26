namespace Agent;

public class EngineerSearchAgent : EngineerAgent, IEngineerSearchAgent
{
    public EngineerSearchAgent()
    {
        SystemPrompt =
            $"{base.SystemPrompt} " +
            $"{SearchSystemPrompt}";
    }

    public override string SystemPrompt { get; }

    protected internal static string SearchSystemPrompt { get; } =
        "You have the ability to search the web! " +
        "Please use web data and fetch the latest information. " +
        "Ensure the sources are cited and reliable.";
}
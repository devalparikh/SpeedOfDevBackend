namespace Agent.Engineer;

public class EngineerSearchAgent : EngineerAgent, IEngineerSearchAgent
{
    protected internal static string SearchSystemPrompt { get; } =
        "You have the ability to search the web! " +
        "Please use web data and fetch the latest information. " +
        "Ensure the sources are cited and reliable.";
}
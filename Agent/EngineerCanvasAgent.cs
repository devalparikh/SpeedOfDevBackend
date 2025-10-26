namespace Agent;

public class EngineerCanvasAgent : EngineerAgent, IEngineerCanvasAgent
{
    protected internal static string CanvasSystemPrompt { get; } =
        "You have been provided the user's system design in the attached image. " +
        "Please analyze this and answer any the customer's prompt. ";
}
namespace Agent;

public class EngineerSearchCanvasAgent
    : EngineerAgent, IEngineerSearchAgent, IEngineerCanvasAgent
{
    public EngineerSearchCanvasAgent()
    {
        SystemPrompt =
            $"{base.SystemPrompt} " +
            $"{EngineerSearchAgent.SearchSystemPrompt} " +
            $"{EngineerCanvasAgent.CanvasSystemPrompt}";
    }

    public override string SystemPrompt { get; }
}
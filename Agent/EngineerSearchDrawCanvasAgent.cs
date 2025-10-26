namespace Agent;

public class EngineerSearchDrawCanvasAgent
    : EngineerAgent, IEngineerSearchAgent, IEngineerCanvasAgent
{
    public EngineerSearchDrawCanvasAgent()
    {
        SystemPrompt =
            $"{base.SystemPrompt} " +
            $"{EngineerSearchAgent.SearchSystemPrompt} " +
            $"{EngineerDrawAgent.DrawSystemPrompt} " +
            $"{EngineerCanvasAgent.CanvasSystemPrompt}";
    }

    public override string SystemPrompt { get; }
}
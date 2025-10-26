namespace Agent;

public class EngineerDrawCanvasAgent : EngineerAgent, IEngineerCanvasAgent
{
    public EngineerDrawCanvasAgent()
    {
        SystemPrompt =
            $"{base.SystemPrompt} " +
            $"{EngineerDrawAgent.DrawSystemPrompt} " +
            $"{EngineerCanvasAgent.CanvasSystemPrompt} ";
    }

    public override string SystemPrompt { get; }
}
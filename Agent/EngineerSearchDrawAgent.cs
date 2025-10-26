namespace Agent;

public class EngineerSearchDrawAgent : EngineerAgent, IEngineerSearchAgent
{
    public EngineerSearchDrawAgent()
    {
        SystemPrompt =
            $"{base.SystemPrompt} " +
            $"{EngineerSearchAgent.SearchSystemPrompt} " +
            $"{EngineerDrawAgent.DrawSystemPrompt}";
    }

    public override string SystemPrompt { get; }

    public override bool UseVisionModality { get; } = true;
    public override bool UseWebSearch { get; } = true;
}
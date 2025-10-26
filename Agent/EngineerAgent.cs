namespace Agent;

public class EngineerAgent : BaseAgent
{
    public override string SystemPrompt { get; set; } =
        "You are an expert staff engineer that designs and analyzes system architecture. " +
        "You analyze for security, performance, scalability, reliability, etc. " +
        "Please ask follow up questions if it helps you design better.";
}
namespace Agent;

public class EngineerDrawAgent : EngineerAgent, IEngineerDrawAgent
{
    protected internal static string DrawSystemPrompt { get; } =
        "You have been asked to output a modified version of the diagram based on results! " +
        "After the answer is ready, produce mermaid diagram code. " +
        "Ensure the mermaid diagram code follows correct syntax, otherwise the import to Excali will fail.";
}
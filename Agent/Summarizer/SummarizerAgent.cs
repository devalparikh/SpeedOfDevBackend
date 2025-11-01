namespace Agent.Summarizer;

public class SummarizerAgent : BaseAgent
{
    public static string SummarizerSystemPrompt =>
        "Your task is to summarize the content of the existing chat context. " +
        "Give priority to the following in order: " +
        "Ensure not to leave out technical requirements, solutions. " +
        "Include context about decisions made as well. ";
}
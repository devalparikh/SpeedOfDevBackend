using Agent;

namespace Examples;

public static class ExampleAgent
{
    public static async Task<string> Run(
        AgentCapabilities capabilities,
        string prompt)
    {
        AgentFactory agentFactory = new(capabilities);
        var agent = agentFactory.Build();
        var answer = await agent.AIAgent.RunAsync(prompt);
        return $"agent: {nameof(capabilities)}\n" +
               $"asked: {prompt}\n" +
               $"answer: {answer}";
    }
}
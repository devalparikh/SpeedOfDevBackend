using Agent;

namespace Examples;

public static class ExampleAgent<T>
    where T : BaseAgent
{
    public static async Task<string> Run(string prompt)
    {
        AgentFactory agentFactory = new(typeof(T));
        var agent = agentFactory.Build();
        var answer = await agent.AIAgent.RunAsync(prompt);
        return ($"agent: {typeof(T)}\n" +
                          $"asked: {prompt}\n" +
                          $"answer: {answer}");
    }
}
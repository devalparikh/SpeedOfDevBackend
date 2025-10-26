using Agent;
using Common.Util;
using OpenAI;

namespace UnitTest;

[TestClass]
public class AgentTest
{
    private static string Prompt { get; } = "Create an scalable ML training system design. what is the cheapest solution out there?";

    [TestInitialize]
    public void Setup()
    {
        // TODO: instead of using live env, mock agent methods
        DotEnv.LoadEnv();
    }
    
    [TestMethod]
    [DataRow(typeof(EngineerAgent), "")]
    [DataRow(typeof(EngineerCanvasAgent), "image")]
    [DataRow(typeof(EngineerDrawAgent), "mermaid")]
    [DataRow(typeof(EngineerDrawCanvasAgent), "mermaid", "image")]
    [DataRow(typeof(EngineerSearchAgent), "web")]
    [DataRow(typeof(EngineerSearchDrawAgent), "web", "mermaid")]
    [DataRow(typeof(EngineerSearchCanvasAgent), "web", "image")]
    [DataRow(typeof(EngineerSearchDrawCanvasAgent), "web", "mermaid", "image")]
    public async Task SystemPromptTest(
        Type engineerClass,
        params string[] extraAnswerAssertions)
    {
        var agentFactory = new AgentFactory(engineerClass);
        var prompt = agentFactory.SystemPrompt;
        
        Console.WriteLine(prompt);

        Assert.Contains("You are an expert staff engineer", prompt);
        AssertAnswerContainsString(prompt, extraAnswerAssertions);
    }
    
    [TestMethod]
    // [DataRow(typeof(EngineerAgent), false)]
    // [DataRow(typeof(EngineerCanvasAgent), false)]
    // [DataRow(typeof(EngineerDrawAgent), false)]
    // [DataRow(typeof(EngineerDrawCanvasAgent), false)]
    // [DataRow(typeof(EngineerSearchAgent), true)]
    // [DataRow(typeof(EngineerSearchDrawAgent), true)]
    // [DataRow(typeof(EngineerSearchCanvasAgent), true)]
    [DataRow(typeof(EngineerSearchDrawCanvasAgent), true)]
    public async Task UseWebTest(
        Type engineerClass,
        bool useWeb)
    {
        var agent = (BaseAgent)Activator.CreateInstance(engineerClass)!;
        Assert.IsTrue(agent is IEngineerSearchAgent == useWeb);
    }

    [TestMethod]
    // [DataRow(typeof(EngineerAgent), "")]
    // [DataRow(typeof(EngineerCanvasAgent), "")]
    // [DataRow(typeof(EngineerDrawAgent), "")]
    // [DataRow(typeof(EngineerDrawCanvasAgent), "")]
    // [DataRow(typeof(EngineerSearchAgent), "")]
    // [DataRow(typeof(EngineerSearchDrawAgent), "")]
    // [DataRow(typeof(EngineerSearchCanvasAgent), "")]
    [DataRow(typeof(EngineerSearchDrawCanvasAgent), "mermaid")]
    public async Task AgentResponseTest(
        Type engineerClass,
        params string[] extraAnswerAssertions)
    {
        var agentFactory = new AgentFactory(engineerClass);
        var agent = agentFactory.Build();

        var answer = await agent.Agent.RunAsync(Prompt);
        Console.WriteLine(answer);
        
        Assert.IsNotNull(answer);
        AssertAnswerContainsString(answer, extraAnswerAssertions);
    }

    private static void AssertAnswerContainsString<T>(T answer, string[] expectedAnswers)
    {
        if (expectedAnswers.Length == 0) return;
        foreach (var expectedAnswer in expectedAnswers)
        {
            Console.WriteLine(answer);
            Console.WriteLine(expectedAnswer);
            if (!string.IsNullOrEmpty(expectedAnswer)) Assert.Contains(expectedAnswer, answer.ToString());
        }
    }
}
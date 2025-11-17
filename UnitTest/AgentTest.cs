using Agent;
using Agent.Engineer;
using Common.Util;
using Examples;

namespace UnitTest;

[TestClass]
public class AgentTest
{
    private static string Prompt { get; } =
        "Create an scalable ML training system design. what is the cheapest solution out there?";

    [TestInitialize]
    public void Setup()
    {
        // TODO: instead of using live env, mock agent methods
        DotEnv.LoadEnv();
    }

    [TestMethod]
    [DataRow(AgentCapabilities.Engineer, "")]
    [DataRow(AgentCapabilities.EngineerSee, "image")]
    [DataRow(AgentCapabilities.EngineerDraw, "mermaid")]
    [DataRow(AgentCapabilities.EngineerDrawAndSee, "mermaid", "image")]
    [DataRow(AgentCapabilities.EngineerSearch, "web")]
    [DataRow(AgentCapabilities.EngineerSearchAndDraw, "web", "mermaid")]
    [DataRow(AgentCapabilities.EngineerSearchAndSee, "web", "image")]
    [DataRow(AgentCapabilities.EngineerSearchAndDrawAndSee, "web", "mermaid", "image")]
    public void SystemPromptTest(
        AgentCapabilities agentCapabilities,
        params string[] extraAnswerAssertions)
    {
        var agentFactory = new AgentFactory(agentCapabilities);
        var prompt = agentFactory.SystemPrompt;

        Console.WriteLine(prompt);

        Assert.Contains("You are an expert staff engineer", prompt);
        AssertAnswerContainsString(prompt, extraAnswerAssertions);
    }

    // [TestMethod]
    // // [DataRow(typeof(EngineerAgent), false)]
    // // [DataRow(typeof(EngineerCanvasAgent), false)]
    // // [DataRow(typeof(EngineerDrawAgent), false)]
    // // [DataRow(typeof(EngineerDrawCanvasAgent), false)]
    // // [DataRow(typeof(EngineerSearchAgent), true)]
    // // [DataRow(typeof(EngineerSearchDrawAgent), true)]
    // // [DataRow(typeof(EngineerSearchCanvasAgent), true)]
    // [DataRow(AgentCapabilities.EngineerSearchAndDrawAndSee, true)]
    // public void UseWebTest(
    //     AgentCapabilities agentCapabilities,
    //     bool useWeb)
    // {
    //     var agent = (BaseAgent)Activator.CreateInstance(engineerClass)!;
    //     Assert.IsTrue(agent is IEngineerSearchAgent == useWeb);
    // }

    [TestMethod]
    // [DataRow(AgentCapabilities.Engineer, "")]
    // [DataRow(AgentCapabilities.EngineerSee, "")]
    // [DataRow(AgentCapabilities.EngineerDraw, "")]
    // [DataRow(AgentCapabilities.EngineerDrawAndSee, "")]
    // [DataRow(AgentCapabilities.EngineerSearch, "")]
    // [DataRow(AgentCapabilities.EngineerSearchAndDraw, "")]
    // [DataRow(AgentCapabilities.EngineerSearchAndSee, "")]
    [DataRow(AgentCapabilities.EngineerSearchAndDrawAndSee, "mermaid")]
    public async Task AgentResponseTest(
        AgentCapabilities agentCapabilities,
        params string[] extraAnswerAssertions)
    {
        var agentFactory = new AgentFactory(agentCapabilities);
        var agent = agentFactory.Build();

        var answer = await agent.AIAgent.RunAsync(Prompt);
        Console.WriteLine(answer);

        Assert.IsNotNull(answer);
        AssertAnswerContainsString(answer, extraAnswerAssertions);
    }

    private static void AssertAnswerContainsString<T>(T answer, string[] expectedAnswers)
    {
        if (expectedAnswers.Length == 0) return;
        if (answer is null) Assert.Fail();
        foreach (var expectedAnswer in expectedAnswers)
        {
            Console.WriteLine(answer);
            Console.WriteLine(expectedAnswer);
            if (!string.IsNullOrEmpty(expectedAnswer)) Assert.Contains(expectedAnswer, answer!.ToString()!);
        }
    }
}
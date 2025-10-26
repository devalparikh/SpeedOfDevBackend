using Agent;
using Common.Util;

namespace UnitTest;

[TestClass]
public class AgentTest
{
    private static string Prompt { get; } = "Create an scalable ML training system design";

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
        var agent = (BaseAgent)Activator.CreateInstance(engineerClass)!;

        var prompt = agent.GetSystemPrompt();
        Console.WriteLine(prompt);

        Assert.Contains("You are an expert staff engineer", prompt);
        AssertAnswerContainsString(prompt, extraAnswerAssertions);
    }

    [TestMethod]
    // [DataRow(typeof(EngineerAgent), "")]
    // [DataRow(typeof(EngineerCanvasAgent), "")]
    // [DataRow(typeof(EngineerDrawAgent), "")]
    // [DataRow(typeof(EngineerDrawCanvasAgent), "")]
    // [DataRow(typeof(EngineerSearchAgent), "")]
    // [DataRow(typeof(EngineerSearchDrawAgent), "")]
    // [DataRow(typeof(EngineerSearchCanvasAgent), "")]
    [DataRow(typeof(EngineerSearchDrawCanvasAgent), "")]
    public async Task AgentResponseTest(
        Type engineerClass,
        params string[] extraAnswerAssertions)
    {
        var agent = (BaseAgent)Activator.CreateInstance(engineerClass)!;

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
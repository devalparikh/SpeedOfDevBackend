using System.ClientModel;
using Agent.Engineer;
using Azure.AI.OpenAI;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using OpenAI;
using OpenAI.Chat;

namespace Agent;

public class AgentFactory
{
    private const string MODEL_DEFAULT = MODEL_GPT_4O_MINI;
    private const string MODEL_GPT_4O_MINI = "gpt-4o-mini";
    private const string MODEL_GPT_4O = "gpt-4o";

    private readonly AgentCapabilities capabilities = AgentCapabilities.None;

    public AgentFactory()
    {
    }

    public AgentFactory(AgentCapabilities capabilities)
    {
        this.capabilities = capabilities;
    }

    private Type Type { get; }

    private static AzureOpenAIClient AzureOpenAIClient =>
        new(
            new Uri($"https://{AzureResource}.openai.azure.com"),
            ApiKeyCredential);

    private static string AzureResource =>
        new(Environment.GetEnvironmentVariable("AZURE_OPENAI_RESOURCE")!);

    private static ApiKeyCredential ApiKeyCredential =>
        new(Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY")!);

    private bool UseWebSearch => capabilities.HasFlag(AgentCapabilities.Search);
    private bool UseVisionModality => capabilities.HasFlag(AgentCapabilities.SeeCanvas);
    public string SystemPrompt => BuildSystemPrompt();

    public BaseAgent Build()
    {
        var aiAgent = BuildAIAgent();
        var agent = new BaseAgent(capabilities);
        agent.AIAgent = aiAgent;
        return agent;
    }

    private ChatClientAgent BuildAIAgent()
    {
        var chatClient = GetChatClient();
        var tools = GetAITools();
        var className = Type.Name;
        var systemPrompt = BuildSystemPrompt();
        return chatClient
            .CreateAIAgent(
                systemPrompt,
                className,
                tools: tools);
    }

    private ChatClient GetChatClient()
    {
        var model = MODEL_DEFAULT;
        if (UseVisionModality || UseWebSearch) model = MODEL_GPT_4O;

        return AzureOpenAIClient.GetChatClient(model);
    }

    private string BuildSystemPrompt()
    {
        string systemPrompt = "";

        foreach (AgentCapabilities capability in Prompts.CapabilityToPrompt.Keys)
        {
            Console.WriteLine(capabilities);
            if (AgentCapabilities.None.HasFlag(capability)) continue;
            if (!capabilities.HasFlag(capability)) continue;
            systemPrompt = $"{systemPrompt} {Prompts.CapabilityToPrompt[capability]}";
        }

        return systemPrompt;
    }

    private AITool[] GetAITools()
    {
        var tools = new List<AITool>();

        if (UseWebSearch) tools.Add(new HostedWebSearchTool());

        return tools.ToArray();
    }
}
using System.ClientModel;
using Azure.AI.OpenAI;
using Microsoft.Agents.AI;

namespace Agent;

public abstract class BaseAgent : IEngineerAgent
{
    public BaseAgent()
    {
        AzureOpenAIClient = new AzureOpenAIClient(
            new Uri($"https://{AzureResource}.openai.azure.com"),
            ApiKeyCredential);

        var aiAgentFactory = new AIAgentFactory(this);
        Agent = aiAgentFactory.Build();
    }

    public AzureOpenAIClient AzureOpenAIClient { get; }

    public AIAgent Agent { get; set; }

    public abstract string SystemPrompt { get; }

    private string AzureResource { get; } = new(Environment.GetEnvironmentVariable("AZURE_OPENAI_RESOURCE")!);

    private ApiKeyCredential ApiKeyCredential { get; } =
        new(Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY")!);

    public virtual bool UseVisionModality { get; } = false;

    public virtual bool UseWebSearch { get; } = false;

    public string GetSystemPrompt()
    {
        return SystemPrompt;
    }
}
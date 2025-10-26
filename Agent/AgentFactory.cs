using System.ClientModel;
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
    
    private static readonly Type DEFAULT_AGENT_TYPE = typeof(EngineerAgent);

    private Type _type = DEFAULT_AGENT_TYPE;
    
    public AgentFactory(Type type)
    {
        _type = type;
        UpdateSystemPrompt();
    }

    public string SystemPrompt { get; set; } = EngineerAgent.EngineerSystemPrompt;
    
    private static AzureOpenAIClient AzureOpenAIClient => 
        new(
            endpoint: new Uri($"https://{AzureResource}.openai.azure.com"), 
            credential: ApiKeyCredential);
    
    private static string AzureResource => 
        new(Environment.GetEnvironmentVariable("AZURE_OPENAI_RESOURCE")!);

    private static ApiKeyCredential ApiKeyCredential => 
        new(Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY")!);

    // Markers using marker interfaces
    private bool UseWebSearch => typeof(IEngineerSearchAgent).IsAssignableFrom(_type);
    private bool UseDrawToCanvas => typeof(IEngineerDrawAgent).IsAssignableFrom(_type);
    private bool UseVisionModality => typeof(IEngineerCanvasAgent).IsAssignableFrom(_type);
    
    public BaseAgent Build()
    {
        var aiAgent = BuildAIAgent();
        var agent = (BaseAgent)Activator.CreateInstance(_type)!;
        agent.AIAgent = aiAgent;
        return agent;
    }

    private AIAgent BuildAIAgent()
    {
        var chatClient = GetChatClient();
        var tools = GetAITools();
        var className = _type.Name;
        return chatClient
            .CreateAIAgent(
                instructions: SystemPrompt,
                name: className,
                tools: tools);
    }

    private ChatClient GetChatClient()
    {
        var model = MODEL_DEFAULT;
        if (UseVisionModality || UseWebSearch) model = MODEL_GPT_4O;

        return AzureOpenAIClient.GetChatClient(model);
    }

    private void UpdateSystemPrompt()
    {
        if (UseWebSearch)
        {
            SystemPrompt = $"{SystemPrompt} {EngineerSearchAgent.SearchSystemPrompt}";
        }
        
        if (UseDrawToCanvas)
        {
            SystemPrompt = $"{SystemPrompt} {EngineerDrawAgent.DrawSystemPrompt}";
        }
        
        if (UseVisionModality)
        {
            SystemPrompt = $"{SystemPrompt} {EngineerCanvasAgent.CanvasSystemPrompt}";
        }
    }

    private AITool[] GetAITools()
    {
        var tools = new List<AITool>();

        if (UseWebSearch) tools.Add(new HostedWebSearchTool());

        return tools.ToArray();
    }
}

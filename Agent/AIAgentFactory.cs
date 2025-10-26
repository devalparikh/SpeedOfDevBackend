using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using OpenAI;
using OpenAI.Chat;

namespace Agent;

public class AIAgentFactory
{
    private const string MODEL_DEFAULT = MODEL_GPT_4O_MINI;
    private const string MODEL_GPT_4O_MINI = "gpt-4o-mini";
    private const string MODEL_GPT_4O = "gpt-4o";

    private readonly BaseAgent _agent;

    public AIAgentFactory(BaseAgent agent)
    {
        _agent = agent;
    }

    private bool UseWebSearch => _agent is IEngineerSearchAgent;
    private bool UseDrawToCanvas => _agent is IEngineerDrawAgent;
    private bool UseVisionModality => _agent is IEngineerCanvasAgent;
    

    public AIAgent Build()
    {
        UpdateSystemPrompt();
        var chatClient = GetChatClient();
        var tools = GetAITools();
        var className = _agent.GetType().Name;
        return chatClient
            .CreateAIAgent(
                instructions: _agent.SystemPrompt,
                name: className,
                tools: tools);
    }

    private ChatClient GetChatClient()
    {
        var model = MODEL_DEFAULT;
        var useVision = UseVisionModality;
        var useWeb = UseWebSearch;
        if (useVision || useWeb) model = MODEL_GPT_4O;

        return _agent.AzureOpenAIClient.GetChatClient(model);
    }

    private void UpdateSystemPrompt()
    {
        string prompt = _agent.SystemPrompt;

        if (UseWebSearch)
        {
            prompt = $"{prompt} {EngineerSearchAgent.SearchSystemPrompt}";
        }
        
        if (UseDrawToCanvas)
        {
            prompt = $"{prompt} {EngineerDrawAgent.DrawSystemPrompt}";
        }
        
        if (UseVisionModality)
        {
            prompt = $"{prompt} {EngineerCanvasAgent.CanvasSystemPrompt}";
        }

        _agent.SystemPrompt = prompt;
    }

    private AITool[] GetAITools()
    {
        var tools = new List<AITool>();

        var useWeb = UseWebSearch;
        if (UseWebSearch) tools.Add(new HostedWebSearchTool());

        return tools.ToArray();
    }
}

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

    public AIAgent Build()
    {
        var chatClient = GetChatClient();
        var tools = GetAITools();
        return chatClient
            .CreateAIAgent(
                _agent.SystemPrompt,
                "SpeedOf.Dev",
                tools: tools);
    }

    private ChatClient GetChatClient()
    {
        var model = MODEL_DEFAULT;
        var useVision = _agent.UseVisionModality;
        var useWeb = _agent.UseWebSearch;
        if (useVision || useWeb) model = MODEL_GPT_4O;

        return _agent.AzureOpenAIClient.GetChatClient(model);
    }

    private AITool[] GetAITools()
    {
        var tools = new List<AITool>();

        var useWeb = _agent.UseWebSearch;
        if (_agent.UseWebSearch) tools.Add(new HostedWebSearchTool());

        return tools.ToArray();
    }
}
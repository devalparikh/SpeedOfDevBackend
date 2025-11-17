using Microsoft.Agents.AI;

namespace Agent;

public class BaseAgent
{
    private readonly AgentCapabilities _capabilities;

    public BaseAgent(AgentCapabilities capabilities)
    {
        _capabilities = capabilities;
    }

    public AIAgent AIAgent { get; set; }
}
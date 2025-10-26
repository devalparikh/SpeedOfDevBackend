using System.ClientModel;
using Azure.AI.OpenAI;
using Microsoft.Agents.AI;

namespace Agent;

public abstract class BaseAgent : IEngineerAgent
{
    public AIAgent AIAgent { get; set; }
}
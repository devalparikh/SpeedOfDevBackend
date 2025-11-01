using Agent;
using Agent.Engineer;
using Agent.Summarizer;
using Microsoft.Agents.AI;
using Microsoft.Agents.AI.Workflows;
using Microsoft.Agents.AI.Workflows.Reflection;

namespace Examples;

public class Draft
{
    public static async void OrchestratorDraft()
    {
        // Create agents
        var engineerAgent = new AgentFactory().Build<EngineerAgent>();
        var summarizerAgent = new AgentFactory().Build<SummarizerAgent>();

        // Pull ai agents
        AIAgent engineerAIAgent = engineerAgent.AIAgent;
        AIAgent summarizer = summarizerAgent.AIAgent;
        
        // https://learn.microsoft.com/en-us/agent-framework/tutorials/workflows/simple-concurrent-workflow?pivots=programming-language-csharp

        // Create the executors
        UppercaseExecutor uppercase = new();
        ReverseTextExecutor reverse = new();

        // Build the workflow by connecting executors sequentially
        WorkflowBuilder workflowBuilder = new(uppercase);
        workflowBuilder.AddEdge(uppercase, reverse).WithOutputFrom(reverse);
        var workflow = workflowBuilder.Build();
        
        // Execute the workflow with input data
        await using Run run = await InProcessExecution.RunAsync(workflow, "Hello, World!");
        foreach (WorkflowEvent evt in run.NewEvents)
        {
            switch (evt)
            {
                case ExecutorCompletedEvent executorComplete:
                    Console.WriteLine($"{executorComplete.ExecutorId}: {executorComplete.Data}");
                    break;
                case WorkflowOutputEvent workflowOutput:
                    Console.WriteLine($"Workflow '{workflowOutput.SourceId}' outputs: {workflowOutput.Data}");
                    break;
            }
        }
    }
}


// Executor - deterministic

/// <summary>
/// First executor: converts input text to uppercase.
/// </summary>
internal sealed class UppercaseExecutor() : ReflectingExecutor<UppercaseExecutor>("UppercaseExecutor"),
    IMessageHandler<string, string>
{
    public ValueTask<string> HandleAsync(string input, IWorkflowContext context, CancellationToken cancellationToken = default)
    {
        // Convert input to uppercase and pass to next executor
        return ValueTask.FromResult(input.ToUpper());
    }
}

/// <summary>
/// Second executor: reverses the input text and completes the workflow.
/// </summary>
internal sealed class ReverseTextExecutor() : ReflectingExecutor<ReverseTextExecutor>("ReverseTextExecutor"),
    IMessageHandler<string, string>
{
    public ValueTask<string> HandleAsync(string input, IWorkflowContext context, CancellationToken cancellationToken = default)
    {
        // Reverse the input text
        return ValueTask.FromResult(new string(input.Reverse().ToArray()));
    }
}
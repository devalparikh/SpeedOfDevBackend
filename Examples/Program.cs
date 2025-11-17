using Agent;
using Examples;

ExampleAgent
    .Run(AgentCapabilities.None, "Design a jwt based authentication system for a social media app.");

ExampleAgent
    .Run(AgentCapabilities.Draw,
        "Create a ML training system that can scale to 10k tps of ingested new data. " +
        "New models should deliver weekly.");

ExampleAgent
    .Run(AgentCapabilities.EngineerSearchAndDraw,
        "Create a ML training system that can scale to 10k tps of ingested new data. " +
        "New models should deliver weekly. " +
        "Decide between various cloud based services and optimize for cost.");
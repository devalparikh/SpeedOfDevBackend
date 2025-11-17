namespace Agent;

[Flags]
public enum AgentCapabilities
{
    // TODO: need to split
    None = 0,
    Engineer = 1 << 0,
    SeeCanvas = 1 << 1,
    Search = 1 << 2,
    Draw = 1 << 3,
    Summarize = 1 << 4,

    EngineerDraw = Engineer | Draw,
    EngineerSee = Engineer | SeeCanvas,
    EngineerDrawAndSee = Engineer | Draw | SeeCanvas,
    EngineerSearch = Engineer | Search,
    EngineerSearchAndDraw = Engineer | Search | Draw,
    EngineerSearchAndSee = Engineer | Search | SeeCanvas,
    EngineerSearchAndDrawAndSee = Engineer | Search | Draw | SeeCanvas
}
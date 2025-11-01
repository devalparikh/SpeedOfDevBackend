namespace Agent.Engineer;

// Using as marker classes to indicate certain types of engineers
// Using markers since this is not a tree inheritance structure, it can be any combination of these features 
// Canvas - can view the canvas
// Search - can use web search
// Draw - can draw/write to the canvas
public interface IEngineerAgent;

public interface IEngineerCanvasAgent : IEngineerAgent;

public interface IEngineerSearchAgent : IEngineerAgent;

public interface IEngineerDrawAgent : IEngineerAgent;
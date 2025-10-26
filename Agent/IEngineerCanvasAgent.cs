namespace Agent;

public interface IEngineerCanvasAgent : IEngineerAgent
{
    public new bool UseVisionModality => true;
}
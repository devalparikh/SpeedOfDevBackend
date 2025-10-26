namespace Agent;

public interface IEngineerSearchAgent : IEngineerAgent
{
    public new bool UseWebSearch => true;
}
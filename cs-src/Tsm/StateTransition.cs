namespace Tsm;

public interface StateTransition
{
    string FromState { get; }
    void Transit(StateData data);
}

public delegate void StateTransitionAction(StateData data);

namespace Tsm;

public abstract class StateTransitionBase : StateTransition
{
    protected StateTransitionBase(string fromState) => FromState = fromState;
    
    public string FromState { get; }

    public abstract void Transit(StateData data);
}
namespace Tsm;

public abstract class StateTransitionBase : StateTransition
{
    public virtual Task TransitAsync(StateData data, CancellationToken cancellationToken)
    {
        Transit(data);
        return Task.CompletedTask;
    }

    public virtual void Transit(StateData data) { }
    
}

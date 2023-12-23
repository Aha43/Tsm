namespace Tsm;

public interface StateTransition
{
    Task TransitAsync(StateData data, CancellationToken cancellationToken);
}

public delegate Task StateTransitionActionAsync(StateData data, CancellationToken cancellationToken);

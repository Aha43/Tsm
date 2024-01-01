using Tsm.Domain;

namespace Tsm.Abstraction;

public interface IStateTransition
{
    Task TransitAsync(StateData data, CancellationToken cancellationToken);
    void Transit(StateData data);
}

public delegate Task StateTransitionActionAsync(StateData data, CancellationToken cancellationToken);

public delegate void StateTransitionAction(StateData data);

using Tsm.Domain;

namespace Tsm.Abstraction;

public interface IStateTransition
{
    Task TransitAsync(StateData data, CancellationToken cancellationToken);
}

public delegate Task StateTransitionActionAsync(StateData data, CancellationToken cancellationToken);

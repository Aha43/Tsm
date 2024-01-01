using Tsm.Abstraction;
using Tsm.Domain;

namespace Tsm.Infrastructure;

public abstract class StateTransitionBase : IStateTransition
{
    public virtual Task TransitAsync(StateData data, CancellationToken cancellationToken)
    {
        Transit(data);
        return Task.CompletedTask;
    }

    public virtual void Transit(StateData data) { }
    
}

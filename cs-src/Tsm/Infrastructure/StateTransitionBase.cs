using Tsm.Abstraction;
using Tsm.Domain;

namespace Tsm.Infrastructure;

public abstract class StateTransitionBase : IStateTransition
{
    public virtual Task TransitAsync(StateMachineData machineData, CancellationToken cancellationToken)
    {
        Transit(machineData);
        return Task.CompletedTask;
    }

    public virtual void Transit(StateMachineData machineData) { }
    
}

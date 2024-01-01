using Tsm.Domain;

namespace Tsm.Abstraction;

public interface IStateTransition
{
    Task TransitAsync(StateMachineData machineData, CancellationToken cancellationToken);
    void Transit(StateMachineData machineData);
}

public delegate Task StateTransitionActionAsync(StateMachineData machineData, CancellationToken cancellationToken);

public delegate void StateTransitionAction(StateMachineData machineData);

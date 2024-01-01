using Tsm.Domain;
using Tsm.Infrastructure;

namespace Tsm.Abstraction;

public interface IStateMachine
{
    StateMachine AddStartTransition(StateTransitionActionAsync t);
    StateMachine AddStartTransition(IStateTransition t);
    StateMachine AddTransition(string fromState, StateTransitionActionAsync t);
    StateMachine AddTransition(string fromState, IStateTransition t);
    Task<StateData> RunAsync(StateMachineParameter p, CancellationToken cancellationToken = default);
}
using Tsm.Domain;
using Tsm.Infrastructure;

namespace Tsm.Abstraction;

public interface IStateMachine
{
    StateMachine AddStartTransition(StateTransitionActionAsync t);
    StateMachine AddStartTransition(StateTransitionAction t);
    StateMachine AddStartTransition(IStateTransition t);
    StateMachine AddTransition(string fromState, StateTransitionActionAsync t);
    StateMachine AddTransition(string fromState, StateTransitionAction t);
    StateMachine AddTransition(string fromState, IStateTransition t);
    StateData Run(StateMachineParameter p);
    Task<StateData> RunAsync(StateMachineParameter p, CancellationToken cancellationToken = default);
}
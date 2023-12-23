using System.Data;

namespace Tsm;

public class StateMachine
{
    private Dictionary<string, StateTransitionActionAsync> _stateTransitionFunctions = new();

    public bool GotTransition(string fromState) => _stateTransitionFunctions.ContainsKey(fromState);

    private void CheckTransition(string fromState)
    {
        if (GotTransition(fromState))
        {
            throw new DuplicateNameException(fromState);
        }
    }

    private StateTransitionActionAsync GetStateTransition(string fromState)
    {
        if (_stateTransitionFunctions.TryGetValue(fromState, out var t)) return t;
        throw new TransitionNotFoundException(fromState);
    }

    public StateMachine AddTransition(string fromState, StateTransitionActionAsync t)
    {
        CheckTransition(fromState);
        _stateTransitionFunctions[fromState] = t;
        return this;
    }

    public StateMachine AddTransition(string fromState, StateTransition t) => AddTransition(fromState, t.TransitAsync);

    public async Task<StateData> RunAsync(CancellationToken cancellationToken = default)
    {
        var retVal = new StateData();
        
        while (retVal.State != null)
        {
            var t = GetStateTransition(retVal.State);
            await t.Invoke(retVal, cancellationToken);
        } 
        
        return retVal;
    }
    
}

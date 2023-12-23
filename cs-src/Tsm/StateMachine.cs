using System.Data;

namespace Tsm;

public class StateMachine
{
    private Dictionary<string, StateTransitionAction> _stateTransitionFunctions = new();
    private Dictionary<string, StateTransition> _stateTransitions = new();

    public bool GotTransition(string fromState)
        => _stateTransitionFunctions.ContainsKey(fromState) || _stateTransitions.ContainsKey(fromState);

    private void CheckTransition(string fromState)
    {
        if (GotTransition(fromState))
        {
            throw new DuplicateNameException(fromState);
        }
    }

    private StateTransitionAction GetStateTransition(string fromState)
    {
        if (_stateTransitionFunctions.TryGetValue(fromState, out var t)) return t;
        if (_stateTransitions.TryGetValue(fromState, out var to)) return to.Transit;
        throw new TransitionNotFoundException(fromState);
    }

    public StateMachine AddTransition(string fromState, StateTransitionAction t)
    {
        CheckTransition(fromState);
        _stateTransitionFunctions[fromState] = t;
        return this;
    }

    public StateData Run()
    {
        var retVal = new StateData();
        
        while (retVal.State != null)
        {
            var t = GetStateTransition(retVal.State);
            t.Invoke(retVal);
        } 
        
        return retVal;
    }
    
}

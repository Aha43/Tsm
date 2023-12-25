using System.Data;

namespace Tsm;

public class StateMachine
{
    private readonly Dictionary<string, StateTransitionActionAsync> _stateTransitionFunctions = new();

    public StateMachine()
    {
        foreach (var t in GetTransitionsByReflection())
        {
            AddTransition(t.state, t.f);
        }
    }

    private static IEnumerable<(string state, StateTransitionActionAsync f)> GetTransitionsByReflection()
    {
        var transitionType = typeof(IStateTransition);
        
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var assembly in assemblies)
        {
            foreach (var t in assembly.GetTypes())
            {
                if (t is not { IsClass: true } || t.IsAbstract || !t.IsSubclassOf(transitionType)) continue;

                IStateTransition? current = null;
                
                foreach (var a in GetStateAttributes(t))
                {
                    current ??= Activator.CreateInstance<IStateTransition>();
                    yield return (a.State, current.TransitAsync);
                }
            }
        }
    }

    private static IEnumerable<StateAttribute> GetStateAttributes(Type t)
    {
        var stateAttributeType = typeof(StateAttribute);
        foreach (var a in t.GetCustomAttributes(stateAttributeType, false))
        {
            yield return (StateAttribute)a;
        }
    }

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

    public StateMachine AddTransition(string fromState, IStateTransition t) => AddTransition(fromState, t.TransitAsync);

    public async Task<StateData> RunAsync(int n, CancellationToken cancellationToken = default)
    {
        if (n < 0)
        {
            throw new ArgumentException($"n < 0 : {n}");
        }
        
        var retVal = new StateData();

        var i = 0;
        while (retVal.State != null)
        {
            if (n > 0 && i == n) return retVal;
            var t = GetStateTransition(retVal.State);
            await t.Invoke(retVal, cancellationToken);
            i++;
        } 
        
        return retVal;
    }
    
}

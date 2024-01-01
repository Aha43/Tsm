using System.Data;
using System.Reflection;
using Tsm.Abstraction;
using Tsm.Attributes;
using Tsm.Domain;
using Tsm.Exceptions;

namespace Tsm.Infrastructure;

public class StateMachine : IStateMachine
{
    private readonly Dictionary<string, StateTransitionActionAsync> _stateTransitionAsyncFunctions = new();
    private readonly Dictionary<string, StateTransitionAction> _stateTransitionFunctions = new();

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

    private static IEnumerable<StateAttribute> GetStateAttributes(ICustomAttributeProvider t)
    {
        var stateAttributeType = typeof(StateAttribute);
        foreach (var a in t.GetCustomAttributes(stateAttributeType, false))
        {
            yield return (StateAttribute)a;
        }
    }

    private bool GotAsyncTransition(string fromState) => _stateTransitionAsyncFunctions.ContainsKey(fromState);
    
    private bool GotTransition(string fromState) => _stateTransitionFunctions.ContainsKey(fromState);

    private void CheckAsyncTransition(string fromState)
    {
        if (GotAsyncTransition(fromState))
        {
            throw new DuplicateNameException(fromState);
        }
    }
    
    private void CheckTransition(string fromState)
    {
        if (GotTransition(fromState))
        {
            throw new DuplicateNameException(fromState);
        }
    }

    private StateTransitionActionAsync GetStateAsyncTransition(string fromState)
    {
        if (_stateTransitionAsyncFunctions.TryGetValue(fromState, out var t)) return t;
        throw new TransitionNotFoundException(fromState);
    }
    
    private StateTransitionAction GetStateTransition(string fromState)
    {
        if (_stateTransitionFunctions.TryGetValue(fromState, out var t)) return t;
        throw new TransitionNotFoundException(fromState);
    }

    public StateMachine AddStartTransition(StateTransitionActionAsync t) 
        => AddTransition(IntrinsicStates.Start, t);

    public StateMachine AddStartTransition(StateTransitionAction t) => AddTransition(IntrinsicStates.Start, t);

    public StateMachine AddStartTransition(IStateTransition t) => AddTransition(IntrinsicStates.Start, t);

    public StateMachine AddTransition(string fromState, StateTransitionActionAsync t)
    {
        CheckAsyncTransition(fromState);
        _stateTransitionAsyncFunctions[fromState] = t;
        return this;
    }

    public StateMachine AddTransition(string fromState, StateTransitionAction t)
    {
        CheckTransition(fromState);
        _stateTransitionFunctions[fromState] = t;
        return this;
    }

    public StateMachine AddTransition(string fromState, IStateTransition t)
        => AddTransition(fromState, t.TransitAsync).AddTransition(fromState, t.Transit);
    
    public async Task<StateData> RunAsync(StateMachineParameter p, CancellationToken cancellationToken = default)
    {
        var retVal = new StateData();
        
        retVal.SetStateData(p.StateData);

        var trial = p.Trial;

        var i = 0;
        var n = p.N;
        while (!retVal.State.IsEnd())
        {
            if (n > 0 && i == n) return retVal;
            var t = GetStateAsyncTransition(retVal.State);
            await t.Invoke(retVal, cancellationToken);
            i++;
        }

        trial?.AddBreadCrumb(new BreadCrumb
        {
            State = "end",
            Representation = retVal.GetStateData<object>()?.ToString()
        });

        return retVal;
    }
    
    public StateData Run(StateMachineParameter p)
    {
        var retVal = new StateData();
        
        retVal.SetStateData(p.StateData);

        var trial = p.Trial;

        var i = 0;
        var n = p.N;
        while (!retVal.State.IsEnd())
        {
            if (n > 0 && i == n) return retVal;
            var t = GetStateTransition(retVal.State);
            t.Invoke(retVal);
            i++;
        }

        trial?.AddBreadCrumb(new BreadCrumb
        {
            State = "end",
            Representation = retVal.GetStateData<object>()?.ToString()
        });

        return retVal;
    }
    
}

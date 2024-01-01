using Tsm.Abstraction;
using Tsm.Infrastructure;

namespace Tsm.Domain;

public sealed class StateMachineData
{
    private string _state = IntrinsicStates.Start;
    
    public StateMachineParameter Parameter { get; }

    public IStateTrial? Trial => Parameter.Trial;

    public string State
    {
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("state name can not be the empty string");
            }

            _state = value;
        }

        get => _state;
    } 
    
    public T? GetStateData<T>() where T : class => Parameter.StateData as T;

    internal StateMachineData(StateMachineParameter p) => Parameter = p;

}


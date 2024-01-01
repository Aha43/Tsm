using Tsm.Abstraction;
using Tsm.Infrastructure;

namespace Tsm.Domain;

public record class StateMachineParameter
{
    private readonly int _n = 0;

    public IStateTrial? Trial { get; set; } = new DefaultStateTrial(); 

    public int N
    {
        init
        {
            if (value < 0)
            {
                throw new IndexOutOfRangeException($"N < 0 : {value}");
            }

            _n = value;
        }

        get => _n;
    }

    public object? StateData { init; get; } = null; 
    
}
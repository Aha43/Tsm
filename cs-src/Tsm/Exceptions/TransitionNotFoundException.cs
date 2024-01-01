namespace Tsm.Exceptions;

public sealed class TransitionNotFoundException : Exception
{
    internal TransitionNotFoundException(string fromState)
        : base($"Transition from state {fromState} not found")
    {
        
    }
}
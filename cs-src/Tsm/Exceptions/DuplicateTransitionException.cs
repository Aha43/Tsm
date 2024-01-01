namespace Tsm.Exceptions;

public sealed class DuplicateTransitionException : Exception
{
    internal DuplicateTransitionException(string fromState)
        : base($"Transition from state already {fromState} exists")
    {
        
    }
}
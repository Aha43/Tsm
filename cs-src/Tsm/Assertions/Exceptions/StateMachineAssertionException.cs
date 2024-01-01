namespace Tsm.Assertions.Exceptions;

public abstract class StateMachineAssertionException : Exception
{
    internal StateMachineAssertionException() { }
    internal StateMachineAssertionException(string msg) : base(msg) { }
}

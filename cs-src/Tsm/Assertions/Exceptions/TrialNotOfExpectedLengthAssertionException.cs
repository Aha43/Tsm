namespace Tsm.Assertions.Exceptions;

public sealed class TrialNotOfExpectedLengthAssertionException : StateMachineAssertionException
{
    internal TrialNotOfExpectedLengthAssertionException(int expected, int actual) :
        base($"Expected trial be of length {expected} but was of length {actual}")
    {
        
    }
}
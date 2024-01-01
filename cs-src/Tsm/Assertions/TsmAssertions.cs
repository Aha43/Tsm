using Tsm.Abstraction;
using Tsm.Assertions.Exceptions;
using Tsm.Domain;

namespace Tsm.Assertions;

public static class TsmAssertions
{
    public static IStateTrial HasTrial(this StateMachineParameter p)
    {
        if (p.Trial == null) throw new NoTrialAssertionException();
        return p.Trial;
    }

    public static IStateTrial HasTrial(this StateMachineData s) => s.Parameter.HasTrial();

    public static IStateTrial IsOfLength(this IStateTrial trial, int expectedLength)
    {
        if (trial.Count() != expectedLength)
        {
            throw new TrialNotOfExpectedLengthAssertionException(expectedLength, trial.Count());
        }

        return trial;
    }
    
}
using Tsm.Attributes;
using Tsm.Domain;
using Tsm.Infrastructure;

namespace TsmTest.TestTransitions;

[State]
public class SingleState : StateTransitionBase
{
    public override void Transit(StateMachineData machineData)
    {
        machineData.State = IntrinsicStates.End;
    }
}
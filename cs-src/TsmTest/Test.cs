using Tsm.Infrastructure;

namespace TsmTest;

public class Test
{
    [Fact]
    public void SingleStateTest()
    {
        var stateMachine = new StateMachine();
        var s = stateMachine.Run();
    }
}
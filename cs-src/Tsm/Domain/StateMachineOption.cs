namespace Tsm.Domain;

public sealed class StateMachineOption
{
    public Func<string, bool> StateTransitionFilters { get; set; } = s => true;
}
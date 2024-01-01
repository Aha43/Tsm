namespace Tsm.Infrastructure;

public static class IntrinsicStates
{
    public const string Start = "start";
    public const string End = "end";

    public static bool IsIntrinsicState(this string state) => state.IsStart() || state.IsEnd();
    public static bool IsStart(this string state) => Start.Equals(state);
    public static bool IsEnd(this string state) => End.Equals(state);
}
namespace Tsm;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class StateAttribute : Attribute
{
    public string State { get; }

    public StateAttribute(string state) => State = state;
}
namespace Tsm;

public sealed class StateData
{
    private object? _data = null;

    public string? State { get; set; } = "start";

    public void SetStateData(object? data) => _data = data;
    public T? GetStateData<T>() where T : class => _data as T;
}


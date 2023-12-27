namespace Tsm;

public sealed class StateData
{
    private object? _data = null;

    private string _state = IntrinsicStates.Start;

    public string State
    {
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("state name can not be the empty string");
            }

            _state = value;
        }

        get => _state;
    } 

    public void SetStateData(object? data) => _data = data;
    public T? GetStateData<T>() where T : class => _data as T;
}


using System.Collections;
using Tsm.Abstraction;
using Tsm.Domain;

namespace Tsm.Infrastructure;

public class DefaultStateTrial : IStateTrial
{
    private readonly List<BreadCrumb> _trial = new();
    
    public IEnumerator<BreadCrumb> GetEnumerator() => _trial.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IStateTrial AddBreadCrumb(BreadCrumb breadCrumb)
    {
        _trial.Add(breadCrumb);
        return this;
    }

    public int Length => _trial.Count;

    public BreadCrumb this[int idx] => _trial[idx];
    
}
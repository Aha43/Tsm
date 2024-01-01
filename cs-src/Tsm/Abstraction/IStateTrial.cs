using Tsm.Domain;

namespace Tsm.Abstraction;

public interface IStateTrial : IEnumerable<BreadCrumb>
{
    IStateTrial AddBreadCrumb(BreadCrumb breadCrumb);
    BreadCrumb this[int idx] { get; }
}
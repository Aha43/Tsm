using Tsm.Domain;

namespace Tsm.Abstraction;

public interface IStateTrial : IEnumerable<BreadCrumb>
{
    IStateTrial AddBreadCrumb(BreadCrumb breadCrumb);
    int Length { get; }
    BreadCrumb this[int idx] { get; }
}
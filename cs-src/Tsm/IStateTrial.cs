namespace Tsm;

public interface IStateTrial : IEnumerable<BreadCrumb>
{
    IStateTrial AddBreadCrumb(BreadCrumb breadCrumb);
    int Length { get; }
    BreadCrumb this[int idx] { get; }
}
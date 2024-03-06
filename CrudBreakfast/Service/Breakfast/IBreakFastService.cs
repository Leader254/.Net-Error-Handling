using CrudBreakfast.Models;
using ErrorOr;

namespace CrudBreakfast.Service.Breakfast
{
    public interface IBreakFastService
    {
        Task<ErrorOr<Created>> CreateBreakFast(BreakFast breakfast);
        Task<ErrorOr<BreakFast>> GetSingleBreakFast(Guid id);
        // get all breakfasts
        Task<IEnumerable<BreakFast>> GetAllBreakFast();
        // update breakfast
        Task<ErrorOr<UpdateBreakfast>> UpdateBreakFast(Guid id, BreakFast breakfast);
        Task<ErrorOr<Deleted>> DeleteBreakFast(Guid id);
    }
}
using CrudBreakfast.Models;
using ErrorOr;

namespace CrudBreakfast.Service.Breakfast
{
    public interface IBreakFastService
    {
        ErrorOr<Created> CreateBreakFast(BreakFast breakfast);
        ErrorOr<BreakFast> GetSingleBreakFast(Guid id);
        // get all breakfasts
        Task<IEnumerable<BreakFast>> GetAllBreakFast();
        // update breakfast
        ErrorOr<UpdateBreakfast> UpdateBreakFast(Guid id, BreakFast breakfast);
        ErrorOr<Deleted> DeleteBreakFast(Guid id);
    }
}
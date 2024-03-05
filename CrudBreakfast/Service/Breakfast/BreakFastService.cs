using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudBreakfast.Models;
using CrudBreakfast.ServiceErrors;
using ErrorOr;

namespace CrudBreakfast.Service.Breakfast
{
    public class BreakFastService : IBreakFastService
    {
        private static readonly Dictionary<Guid, BreakFast> _breakfasts = new();
        public ErrorOr<Created> CreateBreakFast(BreakFast breakfast)
        {
            _breakfasts.Add(breakfast.Id, breakfast);
            return Result.Created;
        }

        public ErrorOr<Deleted> DeleteBreakFast(Guid id)
        {
            // check if the breakfast exists
            if (!_breakfasts.ContainsKey(id))
            {
                throw new Exception("Breakfast not found");
            }
            _breakfasts.Remove(id);
            return Result.Deleted;
        }

        public async Task<IEnumerable<BreakFast>> GetAllBreakFast()
        {
            var breakfasts = _breakfasts.Values.ToList();
            return await Task.FromResult(breakfasts);
        }

        public ErrorOr<BreakFast> GetSingleBreakFast(Guid id)
        {
        // check if the breakfast exists
            if(_breakfasts.TryGetValue(id, out var breakfast))
            {
                return breakfast;
            }
            return Errors.Breakfast.NotFound;
        }

        public ErrorOr<UpdateBreakfast> UpdateBreakFast(Guid id, BreakFast breakfast)
        {
            var isNewlyCreated = !_breakfasts.ContainsKey(breakfast.Id);
            _breakfasts[id] = breakfast;
            return new UpdateBreakfast(isNewlyCreated);
        }
    }
}
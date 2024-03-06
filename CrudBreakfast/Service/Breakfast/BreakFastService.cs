using AutoMapper;
using CrudBreakfast.Data;
using CrudBreakfast.Models;
using CrudBreakfast.ServiceErrors;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace CrudBreakfast.Service.Breakfast
{
    public class BreakFastService : IBreakFastService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BreakFastService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ErrorOr<Created>> CreateBreakFast(BreakFast breakfast)
        {
            await _context.Breakfasts.AddAsync(breakfast);
            await _context.SaveChangesAsync();
            return Result.Created;
        }

        public async Task<ErrorOr<Deleted>> DeleteBreakFast(Guid id)
        {
            var breakfast = await _context.Breakfasts.Where(b => b.Id == id).FirstOrDefaultAsync();
            if (breakfast != null)
            {
                _context.Breakfasts.Remove(breakfast);
                await _context.SaveChangesAsync();
                return Result.Deleted;
            }
            return Errors.Breakfast.NotFound;
        }

        public async Task<IEnumerable<BreakFast>> GetAllBreakFast()
        {
            return await _context.Breakfasts.ToListAsync();
        }

        public async Task<ErrorOr<BreakFast>> GetSingleBreakFast(Guid id)
        {
            var breakfast = await _context.Breakfasts.Where(b => b.Id == id).FirstOrDefaultAsync();
            if (breakfast != null)
            {
                return breakfast;
            }
            return Errors.Breakfast.NotFound;
        }

        public async Task<ErrorOr<UpdateBreakfast>> UpdateBreakFast(Guid id, BreakFast breakfast)
        {
            _context.Breakfasts.Update(breakfast);
            await _context.SaveChangesAsync();
            return new UpdateBreakfast();
        }
    }
}
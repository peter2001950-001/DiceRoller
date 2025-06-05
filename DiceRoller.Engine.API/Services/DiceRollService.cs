using AutoMapper;
using DiceRoller.Domain.Abstractions.Pagination;
using DiceRoller.Domain.Abstractions.Repositories;
using DiceRoller.Domain.Entities;
using DiceRoller.Engine.API.Models.Requests;
using DiceRoller.Engine.API.Models.Responses;
using DiceRoller.Engine.API.Services.Abstractions;

namespace DiceRoller.Engine.API.Services
{
    public class DiceRollService : IDiceRollService
    {
        private readonly IDiceRollRepository _diceRollRepository;
        private readonly IMapper _mapper;

        public DiceRollService(IDiceRollRepository diceRollRepository, IMapper mapper)
        {
            _diceRollRepository = diceRollRepository;
            _mapper = mapper;
        }

        public async Task<DiceRollResponse> RollTheDiceAsync(User user)
        {
            var random = new Random();

            var diceRoll = new DiceRoll()
            {
                DiceRoll1 = random.Next(1, 6),
                DiceRoll2 = random.Next(1, 6),
                User = user
            };

            var result = await _diceRollRepository.AddAsync(diceRoll);
            var response = _mapper.Map<DiceRollResponse>(result);
            return response;
        }

        public async Task<PaginatedResult<DiceRollResponse>> GetAllPaginatedAsync(User user, GetDiceRollRequest request)
        {
            var result = new PaginatedResult<DiceRoll>();

            if (request.Year.HasValue && request.Month.HasValue && request.Day.HasValue)
            {
                var startDateOnly = new DateOnly(request.Year.Value, request.Month.Value, request.Day.Value);
                var endDateOnly = startDateOnly.AddDays(1);

                result = await _diceRollRepository.GetPagedAsync(request,
                    x => x.CreatedOn > startDateOnly.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc) && x.CreatedOn < endDateOnly.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc) && x.User.Id == user.Id);
            }
            else if (request.Year.HasValue && request.Month.HasValue)
            {
                var startDateOnly = new DateOnly(request.Year.Value, request.Month.Value, 1);
                var endDateOnly = startDateOnly.AddMonths(1);

                result = await _diceRollRepository.GetPagedAsync(request,
                    x => x.CreatedOn > startDateOnly.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc) && x.CreatedOn < endDateOnly.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc) && x.User.Id == user.Id);
            }
            else if (request.Year.HasValue)
            {
                var startDateOnly = new DateOnly(request.Year.Value,  1,  1);
                var endDateOnly = new DateOnly(request.Year.Value, 12, 31);

                result = await _diceRollRepository.GetPagedAsync(request,
                    x => x.CreatedOn > startDateOnly.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc) && x.CreatedOn < endDateOnly.ToDateTime(TimeOnly.MaxValue, DateTimeKind.Utc) && x.User.Id == user.Id);
            }
            else
            {
                result = await _diceRollRepository.GetPagedAsync(request, x => x.User.Id == user.Id);
            }

            return new PaginatedResult<DiceRollResponse>()
            {
                Count = result.Count,
                SortFields = result.SortFields,
                StartAt = result.StartAt,
                TotalCount = result.TotalCount,
                Data = _mapper.Map<List<DiceRollResponse>>(result.Data)
            };
        }
    }
}

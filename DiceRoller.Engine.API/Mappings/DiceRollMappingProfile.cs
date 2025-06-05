using AutoMapper;
using DiceRoller.Domain.Entities;
using DiceRoller.Engine.API.Models.Responses;

namespace DiceRoller.Engine.API.Mappings
{
    public class DiceRollMappingProfile : Profile
    {
        public DiceRollMappingProfile()
        {
            CreateMap<DiceRoll, DiceRollResponse>()
                .ForMember(x => x.Dice1, p => p.MapFrom(t => t.DiceRoll1))
                .ForMember(x => x.Dice2, p => p.MapFrom(t => t.DiceRoll2));
        }
    }
}

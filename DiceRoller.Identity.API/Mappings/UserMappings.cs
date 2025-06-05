using AutoMapper;
using DiceRoller.Domain.Entities;
using DiceRoller.Identity.API.Models.Requests;

namespace DiceRoller.Identity.API.Mappings
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<RegisterRequest, User>()
                .ForMember(x => x.PasswordHash, o => o.Ignore());
        }
    }
}

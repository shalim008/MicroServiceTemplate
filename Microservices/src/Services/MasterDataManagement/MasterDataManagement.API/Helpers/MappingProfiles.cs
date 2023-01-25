using AutoMapper;
using MasterDataManagement.API.Dtos;
using MasterDataManagement.Core.Entities;

namespace MasterDataManagement.API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            CreateMap<SysOwner, SysOwnerDto>().ReverseMap();
        }
    }
}
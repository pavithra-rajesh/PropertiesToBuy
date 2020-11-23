using AutoMapper;
using Funda.Contracts;
using Funda.Services.Dto;

namespace Funda.Services.Mappings {
  public class MappingProfile : Profile {
    public MappingProfile() {
      CreateMap<AanbodResponse, GetAanbodResponse>();
    }
  }
}

using AutoMapper;
using ApplicationRequest = Rossko.ElasticWebForm.Application.ElasticSearch.Data;
using Rossko.ElasticWebForm.Web.Data;

namespace Rossko.ElasticWebForm.Web.Profiles
{
    public class OemCatalogRequestProfile: Profile
    {
        public OemCatalogRequestProfile()
        {
            CreateMap<OemCatalogRequest, ApplicationRequest.OemCatalogDataRequest>()
                .ForMember(
                    dest => dest.StartDate,
                    opt => opt.MapFrom(src => $"{src.StartDate}")
                )
                .ForMember(
                    dest => dest.EndDate,
                    opt => opt.MapFrom(src => src.EndDate)
                )
                .ForMember(
                    dest => dest.Event,
                    opt => opt.MapFrom(src => src.Event)
                );
        }
    }
}

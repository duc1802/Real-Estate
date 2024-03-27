using AutoMapper;
using RealEstate.Models.Domain;
using RealEstate.Models.DTO;

namespace RealEstate.Mappings
{
    public class AutoMapperProfiles:Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<Nation, NationDto>().ReverseMap();
            CreateMap<AddNationRequestDto, Nation>().ReverseMap();
            CreateMap<UpdateNationRequestDto, Nation>().ReverseMap();

            CreateMap<AddCityRequestDto, City>().ReverseMap();
            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<UpdateCityRequestDto, City>().ReverseMap();

            CreateMap<AddDistrictRequestDto, District>().ReverseMap();
            CreateMap<UpdateDistrictRequestDto, District>().ReverseMap();
            CreateMap<District, DistrictDto>().ReverseMap();

            CreateMap<AddWardRequestDto, Ward>().ReverseMap();
            CreateMap<UpdateWardRequestDto, Ward>().ReverseMap();
            CreateMap<Ward, WardDto>().ReverseMap();

            CreateMap<AddStreetRequestDto, Street>().ReverseMap();
            CreateMap<UpdateStreetRequestDto, Street>().ReverseMap();
            CreateMap<Street, StreetDto>().ReverseMap();

            CreateMap<AddPackageRequestDto, Package>().ReverseMap();
            CreateMap<UpdatePackageRequestDto, Package>().ReverseMap();
            CreateMap<Package, PackageDto>().ReverseMap();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Transportsystem_GoogleMaps.Dtos;
using Transportsystem_GoogleMaps.Models;

namespace Transportsystem_GoogleMaps.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Package, PackageDto>();
            Mapper.CreateMap<Driver, DriverDto>();
            Mapper.CreateMap<Delivery, DeliveryDto>();


            Mapper.CreateMap<PackageDto, Package>()
                .ForMember(p => p.Id, opt => opt.Ignore());

            Mapper.CreateMap<DriverDto, Driver>();
            Mapper.CreateMap<DeliveryDto, Delivery>();
        }
    }
}
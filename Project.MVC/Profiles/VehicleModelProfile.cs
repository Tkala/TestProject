using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.MVC.Profiles
{
    public class CarVehicleModelProfile : Profile
    {
        public CarVehicleModelProfile()
        {

            CreateMap<Project.MVC.Models.VehicleModel, Project.Service.Models.VehicleModel>();
            CreateMap<Project.Service.Models.VehicleModel, Project.MVC.Models.VehicleModel>();

        }
    }
}
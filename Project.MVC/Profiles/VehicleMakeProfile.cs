using AutoMapper;
using Project.MVC.Models;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.MVC.Profiles
{
    public class CarVehicleMakeProfile : Profile
    {
        public CarVehicleMakeProfile()
        {

            CreateMap<Project.MVC.Models.VehicleMake, Project.Service.Models.VehicleMake>();
            CreateMap<Project.Service.Models.VehicleMake, Project.MVC.Models.VehicleMake>();

        }
    }
}
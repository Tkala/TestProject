﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Models
{
    public interface IVehicleMake
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        //public ICollection<VehicleModel> VehicleModels { get; set; }
        //public VehicleModel VehicleModel { get; set; }
    }
}

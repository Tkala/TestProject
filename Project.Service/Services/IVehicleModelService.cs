using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services
{
    public interface IVehicleModelService
    {
        Task CreateVehicleModel(VehicleModel vehicleModel);
        Task EditVehicleModel(int id, VehicleModel vehicleModel);
        Task DeleteVehicleModel(int id);
    }
}

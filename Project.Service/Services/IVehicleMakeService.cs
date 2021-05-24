using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services
{
    public interface IVehicleMakeService
    {
        Task CreateVehicleMake(VehicleMake vehicleMake);
        Task EditVehicleMake(int id, VehicleMake vehicleMake);
        Task DeleteVehicleMake(int id);
    }

}

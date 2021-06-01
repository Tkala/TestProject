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
        Task CreateVehicleModelAsync(VehicleModel vehicleModel);
        Task EditVehicleModelAsync(int id, VehicleModel vehicleModel);
        Task DeleteVehicleModelAsync(int id);
        Task<PaginatedList<VehicleModel>> VehicleModelPagingAsync(string sortOrder, string currentFilter, string searchString, int? pageNumber);
        Task<VehicleModel> FindVehicleModelAsync(int? id);
        //Task<List<VehicleModel>> GetVehicleModelsAsync();
    }
}

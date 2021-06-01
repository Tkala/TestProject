using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Project.Service.Services
{
    public interface IVehicleMakeService
    {
        Task CreateVehicleMakeAsync(VehicleMake vehicleMake);
        Task EditVehicleMakeAsync(int id, VehicleMake vehicleMake);
        Task DeleteVehicleMakeAsync(int id);
        Task<PaginatedList<VehicleMake>> VehicleMakePagingAsync(string sortOrder, string currentFilter, string searchString, int? pageNumber);
        Task<VehicleMake> FindVehicleMakeAsync(int? id);
        Task<List<VehicleMake>> GetVehicleMakesAsync();
    }

}

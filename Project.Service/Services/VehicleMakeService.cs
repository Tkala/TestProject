using Microsoft.EntityFrameworkCore;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services
{
    public class VehicleMakeService : IVehicleMakeService
    {
        public MyContext _context { get; set; }
        public VehicleMakeService(MyContext context)
        {
            _context = context;
        }


        public async Task CreateVehicleMake(VehicleMake vehicleMake)
        {
            _context.Add(vehicleMake);
            await _context.SaveChangesAsync();
        }

        public async Task EditVehicleMake(int id, VehicleMake vehicleMake)
        {
            _context.Update(vehicleMake);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVehicleMake(int id)
        {
            var vehicleMake = await _context.VehicleMakes
                .FirstOrDefaultAsync(m => m.Id == id);
            _context.VehicleMakes.Remove(vehicleMake);
            await _context.SaveChangesAsync();

        }




    }
}
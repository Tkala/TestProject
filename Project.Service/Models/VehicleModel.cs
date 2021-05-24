using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Models
{
    public class VehicleModel : IVehicleModel
    {
        [Key]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Abrv")]
        public string Abrv { get; set; }

        
        public int MakeId { get; set; }
        [ForeignKey("MakeId")]

        public VehicleMake VehicleMake { get; set; }
    }
}

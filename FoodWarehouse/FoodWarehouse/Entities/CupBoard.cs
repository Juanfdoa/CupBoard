using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodWarehouse.Entities
{
    public class CupBoard
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Products Product { get; set; }
        public DateTime? ExpireDate { get; set; }
        public int Quantity { get; set; }
        public int StatusId { get; set; }
        public status Status { get; set; }
        
    }
}

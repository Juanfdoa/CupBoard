using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodWarehouse.DTOs
{
    public class CupBoardCreateDTO
    {
        public int ProductId { get; set; }
        public DateTime ExpireDate { get; set; }
        public int Quantity { get; set; }
        public int StatusId { get; set; }
    }
}

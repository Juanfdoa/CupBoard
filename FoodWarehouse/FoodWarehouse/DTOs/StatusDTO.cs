using FoodWarehouse.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodWarehouse.DTOs
{
    public class StatusDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CupBoardDTO> CupBoards { get; set; }

    }
}

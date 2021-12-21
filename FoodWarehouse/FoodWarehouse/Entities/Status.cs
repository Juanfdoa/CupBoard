using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodWarehouse.Entities
{
    public class status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CupBoard> CupBoards { get; set; }
    }
}

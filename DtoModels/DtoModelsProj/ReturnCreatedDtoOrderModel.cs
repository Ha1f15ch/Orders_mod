using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoModelsProj
{
    public class ReturnCreatedDtoOrderModel
    {
        public bool IsCreated { get; set; } = false;
        public int OrderId { get; set; }
    }
}

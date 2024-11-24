using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoModelsProj
{
    public class CreateOrderDto
    {
        public string Dto_TitleName { get; set; }
        public string Dto_Adress { get; set; }
        public string Dto_Description { get; set; }
        public int Dto_DayToDelay { get; set; }
        public string Dto_ContactInformation { get; set; }
        public int Dto_UserIdCreated { get; set; }
        public int? Dto_UserIdAssigner { get; set; } = null;
        public string? Dto_OrderStatusId { get; set; } = "N";
        public string? Dto_OrderPriorityId { get; set; }
    }
}

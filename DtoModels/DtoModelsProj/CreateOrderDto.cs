using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoModelsProj
{
    public class CreateOrderDto
    {
        public string Dto_TitleName { get;}
        public string Dto_Adress { get;}
        public string Dto_Description { get;}
        public int Dto_DayToDelay { get;}
        public string Dto_ContactInformation { get;}
        public int Dto_UserIdCreated { get;}
        public int? Dto_UserIdAssigner = null;
        public string? Dto_OrderStatusId = "N";
        public string? Dto_OrderPriorityId { get;}
    }
}

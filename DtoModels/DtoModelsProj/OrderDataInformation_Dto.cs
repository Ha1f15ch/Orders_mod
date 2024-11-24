using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoModelsProj
{
    public class OrderDataInformation_Dto
    {
        public Order? Order { get; set; }
        public OrderPriority? OrderPriority { get; set; }
        public OrderStatus? OrderStatus { get; set; }
        public CustomerProfile? CustomerProfile { get; set; }
        public EmployerProfile? EmployerProfile { get; set; }
        public UserProfile? Customer_UserProfile { get; set; }
        public UserProfile? Employer_UserProfile { get; set; }
        public bool HasError { get; set; } = false;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoModelsProj
{
    public class UserProfileModelDto
    {
        public int UserProfileId { get; set; }
        public string UserProfileFirstName { get; set; }
        public string UserProfileMiddleName { get; set; }
        public string UserProfileLastName { get; set; }
        public DateTime UserProfileBirthday { get; set; }
        public int UserProfileAge { get; set; }
        public bool UserProfileIsActived { get; set; }
        public DateTime UserProfileDateCreatedAt { get; set; }
        public DateTime UserProfileDateUpdatedAt { get; set; }
    }
}

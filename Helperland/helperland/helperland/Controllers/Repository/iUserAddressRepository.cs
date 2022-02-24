using helperland.Controllers.Data;
using helperland.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace helperland.Controllers.Repository
{
    public class iUserAddressRepository
    {
        private readonly HelperlandContext helperlandContext;

        public iUserAddressRepository(HelperlandContext helperlandContext)
        {
            this.helperlandContext = helperlandContext;
        }

        public void AddUserAddress(UserAddress userAddress)
        {
            helperlandContext.UserAddresses.Add(userAddress);
            helperlandContext.SaveChanges();
            return;
        }

    }
}

using helperland.Controllers.Data;
using helperland.Models.Data;
using helperland.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace helperland.Controllers.Repository
{
    public class iServiceRepository
    {
        private readonly HelperlandContext helperlandContext;

        public iServiceRepository(HelperlandContext helperlandContext)
        {
            this.helperlandContext = helperlandContext;
        }

        public ServiceRequest AddService(AddService addService,String UserID)
        {
            ServiceRequest serviceRequest = new ServiceRequest();
            serviceRequest.UserId = int.Parse(UserID);
            serviceRequest.ServiceId = 0;

            serviceRequest.ServiceStartDate = addService.ServiceStartDate;
            serviceRequest.ZipCode = addService.PostalCode;
            serviceRequest.ServiceHours = addService.ServiceHours;

            var total_cost = 300 + (addService.ExtraServices.Count * 12.5);

            serviceRequest.SubTotal = (decimal)total_cost;
            serviceRequest.TotalCost = (decimal)total_cost;
            serviceRequest.HasPets = addService.HasPets ?? false;
            serviceRequest.Comments = addService.Comments;

            helperlandContext.ServiceRequests.Add(serviceRequest);
            helperlandContext.SaveChanges();

            int id = serviceRequest.ServiceRequestId;

            for(var i = 0; i < addService.ExtraServices.Count; i++)
            {
                ServiceRequestExtra serviceRequestExtra = new ServiceRequestExtra();
                serviceRequestExtra.ServiceExtraId = addService.ExtraServices[i];
                serviceRequestExtra.ServiceRequestId = id;

                helperlandContext.ServiceRequestExtras.Add(serviceRequestExtra);
                helperlandContext.SaveChanges();
            }

            var UserAddress = addService.userAddress;

            ServiceRequestAddress serviceRequestAddress = new ServiceRequestAddress()
            {
                ServiceRequestId = id,
                AddressLine1 = UserAddress.AddressLine1,
                AddressLine2 = UserAddress.AddressLine2,
                City = UserAddress.City,
                State = UserAddress.State,
                PostalCode = UserAddress.PostalCode,
                Email = UserAddress.Email,
                Mobile = UserAddress.Mobile
            };

            helperlandContext.ServiceRequestAddresses.Add(serviceRequestAddress);
            helperlandContext.SaveChanges();

            return serviceRequest;
        }

    }
}

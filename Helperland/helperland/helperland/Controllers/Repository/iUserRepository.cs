using helperland.Models.Data;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using helperland.Controllers.Data;
using helperland.Models.ViewModel;

namespace helperland.Controllers.repo
{
    public class iUserRepository : Controller
    {
        private readonly HelperlandContext helperContext;

        public iUserRepository(HelperlandContext helperContext)
        {
            this.helperContext = helperContext;
        }

        public void Add(User user)
        {
            var user1 = helperContext.Users.FirstOrDefault(u=> u.Email == user.Email);
            if(user1 != null)
            {
                return;
            }
            else
            {
                user.ModifiedBy = user.UserId;
                user.CreatedDate = DateTime.Now;
                user.ModifiedDate = DateTime.Now;
                user.WorksWithPets = false;
                user.IsRegisteredUser = true;
                user.IsApproved = false;
                user.IsActive = true;
                user.IsDeleted = false;

                helperContext.Users.Add(user);
                helperContext.SaveChanges();
            }  
        }
        public void AddServiceProvider(User user)
        {
            user.UserTypeId = 1;
            Add(user);
        }
        public void AddCustomer(User user)
        {
            user.UserTypeId = 2;
            Add(user);
        }
        
        public User login(loginView loginView)
        {
            return helperContext.Users.FirstOrDefault(u => u.Email == loginView.Email);
        }

        public User login(ForgetPasswordView forgetPasswordView)
        {
            return helperContext.Users.FirstOrDefault(u => u.Email == forgetPasswordView.Email);
        }

        public User login(User user)
        {
            return helperContext.Users.FirstOrDefault(u => u.Email == user.Email);
        }

        public Boolean IsServiceProvidedOnPostal(ZipCodeView zipCodeView)
        {
            var isFound = helperContext.Users.FirstOrDefault(u => u.ZipCode == zipCodeView.ZipCode);
            if(isFound == null) return false;
            return true;
        }

        public List<UserAddress> GetUserAddress(String UserID)
        {
            var useradresses = helperContext.UserAddresses.Where(u=> u.UserId == int.Parse(UserID)).ToList();
            return useradresses;
        }
    }
}

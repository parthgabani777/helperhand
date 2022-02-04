using helperland.Controllers.Data;
using helperland.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace helperland.Controllers.Repository
{
    public class iStateRepository
    {
        private readonly HelperlandContext helperlandContext;

        public iStateRepository(HelperlandContext helperlandContext)
        {
            this.helperlandContext = helperlandContext;
        }

        public void create(State state)
        {
            helperlandContext.States.Add(state);
            helperlandContext.SaveChanges();
        }
    }
}

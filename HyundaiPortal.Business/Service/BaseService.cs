using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HyundaiPortal.Business.Service
{
    public class BaseService
    {
        public HyundaiEntities HyundaiContext;

        public BaseService()
        {
            HyundaiContext = new HyundaiEntities();
            HyundaiContext.ContextOptions.ProxyCreationEnabled = false;
            HyundaiContext.ContextOptions.LazyLoadingEnabled = true;
            HyundaiContext.CommandTimeout = 300;
        }
    }
}

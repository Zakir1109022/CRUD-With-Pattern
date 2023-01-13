using CRUD.Common.TenantConfig;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD.Common.Services
{
   public interface ITenantService
    {
        public Tenant GetTenant();
    }
}

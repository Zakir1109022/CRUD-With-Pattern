using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD.Common.TenantConfig
{
    public class TenantSettings : ITenantSettings
    {
        public List<Tenant> Tenants { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD.Common.TenantConfig
{
    public class Tenant
    {
        public string Name { get; set; }
        public string TID { get; set; }
        public string ConnectionString { get; set; }
    }
}

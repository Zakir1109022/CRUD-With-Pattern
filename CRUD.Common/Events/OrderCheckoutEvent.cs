﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD.Common.Events
{
    public class OrderCheckoutEvent
    {
        public Guid RequestId { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
    }
}

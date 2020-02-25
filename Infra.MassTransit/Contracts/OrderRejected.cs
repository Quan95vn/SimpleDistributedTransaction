﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface OrderRejected
    {
        Guid OrderId { get; }

        string ErrorMessage { get; }
    }
}
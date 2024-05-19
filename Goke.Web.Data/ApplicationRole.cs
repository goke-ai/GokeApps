﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goke.Web.Data
{
    public class ApplicationRole
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public bool IsSelected { get; set; }
    }
}

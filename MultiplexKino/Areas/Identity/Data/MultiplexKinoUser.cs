﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MultiplexKino.Areas.Identity.Data;

// Add profile data for application users by adding properties to the MultiplexKinoUser class
public class MultiplexKinoUser : IdentityUser
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

}

public class ApplicationRole : IdentityRole
{

}


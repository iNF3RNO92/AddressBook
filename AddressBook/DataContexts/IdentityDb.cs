using AddressBook.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AddressBook.DataContexts
{
    public class IdentityDb : IdentityDbContext<ApplicationUser>
    {

        public IdentityDb() : base("DefaultConnection")
        {
        }

        public static IdentityDb Create()
        {
            return new IdentityDb();
        }

    }
}
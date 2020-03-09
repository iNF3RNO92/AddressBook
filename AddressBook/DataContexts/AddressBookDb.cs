using Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AddressBook.DataContexts
{
    public class AddressBookDb : DbContext
    {

        public AddressBookDb() : base("DefaultConnection")
        {
            Database.SetInitializer<AddressBookDb>(null);
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactCellphone> ContactCellphoneNumbers { get; set; }
        public DbSet<ContactEmailAddress> ContactEmailAddresses { get; set; }

    }
}
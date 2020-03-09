using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [Table("Contact")]
    public partial class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }

    [Table("ContactCellphone")]
    public partial class ContactCellphone
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public string CellphoneNumber { get; set; }
    }

    [Table("ContactEmailAddress")]
    public partial class ContactEmailAddress
    {
        public int Id { get; set; }     
        public int ContactId { get; set; }
        public string EmailAddress { get; set; }
    }
}

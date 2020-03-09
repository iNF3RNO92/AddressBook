using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AddressBook.Models
{
    public class ContactModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }

        public string FullName
        {
            get
            {
                return Name + " " + Surname;
            }
        }
    }

    public class ContactCellphoneModel
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        [Required]
        [MaxLength(12,ErrorMessage = "Maximum Cellphone Number Length exceeded")]        
        public string CellphoneNumber { get; set; }
    }

    public class ContactEmaillAddressModel
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Maximum Email Length Exceeded")]        
        [EmailAddress]
        public string EmailAddress { get; set; }
    }
}
using AddressBook.DataContexts;
using AddressBook.Models;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AddressBook.Controllers
{
    [AllowAnonymous]
    public class ContactController : Controller
    {
        AddressBookDb db = new AddressBookDb();

        public ActionResult ContactList()
        {
            List<ContactModel> model;

            model = (from c in db.Contacts
                     select new ContactModel
                     {
                         Id = c.Id,
                         Name = c.Name,
                         Surname = c.Surname
                     }).ToList();

            return View(model);
        }

        public ActionResult CreateContact()
        {
            ContactModel model = new ContactModel();
            return View(model);
        }

        public ActionResult DeleteContact(int Id)
        {
            var contact = db.Contacts.Find(Id);
            var emails = db.ContactEmailAddresses.Where(x => x.ContactId == Id);
            var cells = db.ContactCellphoneNumbers.Where(x => x.ContactId == Id);

            db.ContactEmailAddresses.RemoveRange(emails);
            db.ContactCellphoneNumbers.RemoveRange(cells);
            db.Contacts.Remove(contact);

            db.SaveChanges();

            return RedirectToAction("ContactList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateContact(ContactModel data)
        {
            if (db.Contacts.Where(x => x.Name == data.Name && x.Surname == data.Surname).Any())
            {
                ModelState.AddModelError(string.Empty, data.Name + " " + data.Surname + " combination already exists.");
            }

            if (!ModelState.IsValid)
            {
                return View(data);
            }

            Contact newContact = new Contact()
            {
                Name = data.Name,
                Surname = data.Surname
            };

            db.Contacts.Add(newContact);
            db.SaveChanges();

            return RedirectToAction("ContactList");
        }

        public ActionResult EditContact(int Id)
        {
            ContactModel model;

            model = (from c in db.Contacts
                     where c.Id == Id
                     select new ContactModel()
                     {
                         Id = c.Id,
                         Name = c.Name,
                         Surname = c.Surname
                     }).Single();

            return View(model);
        }

        [HttpGet]
        public ActionResult _CellphoneNumberTable(int Id)
        {
            List<ContactCellphoneModel> model;

            model = (from c in db.ContactCellphoneNumbers
                     where c.ContactId == Id
                     select new ContactCellphoneModel
                     {
                         Id = c.Id,
                         ContactId = c.ContactId,
                         CellphoneNumber = c.CellphoneNumber
                     }).ToList();

            return PartialView(model);
        }

        [HttpGet]
        public ActionResult _EmailAddressTable(int Id)
        {
            List<ContactEmaillAddressModel> model;

            model = (from c in db.ContactEmailAddresses
                     where c.ContactId == Id
                     select new ContactEmaillAddressModel
                     {
                         Id = c.Id,
                         ContactId = c.ContactId,
                         EmailAddress = c.EmailAddress
                     }).ToList();

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult AddContactEmail(int contactId, string emailAddress)
        {
            if (db.ContactEmailAddresses.Where(e => e.ContactId == contactId && e.EmailAddress == emailAddress).Any())
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            ContactEmailAddress newEmailAddress = new ContactEmailAddress()
            {
                ContactId = contactId,
                EmailAddress = emailAddress
            };

            db.ContactEmailAddresses.Add(newEmailAddress);
            db.SaveChanges();

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddContactCell(int contactId, string cellNumber)
        {
            if(db.ContactCellphoneNumbers.Where(c => c.ContactId == contactId && c.CellphoneNumber == cellNumber).Any())
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            ContactCellphone newContactCell = new ContactCellphone()
            {
                ContactId = contactId,
                CellphoneNumber = cellNumber
            };

            db.ContactCellphoneNumbers.Add(newContactCell);
            db.SaveChanges();

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteCell(int Id)
        {
            var cell = db.ContactCellphoneNumbers.Find(Id);
            db.ContactCellphoneNumbers.Remove(cell);
            db.SaveChanges();

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteEmail(int Id)
        {
            var email = db.ContactEmailAddresses.Find(Id);
            db.ContactEmailAddresses.Remove(email);
            db.SaveChanges();

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateContactName(int Id, string Name)
        {
            var contact = db.Contacts.Find(Id);
            contact.Name = Name;
            db.SaveChanges();

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateContactSurname(int Id, string Surname)
        {
            var contact = db.Contacts.Find(Id);
            contact.Surname = Surname;
            db.SaveChanges();

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
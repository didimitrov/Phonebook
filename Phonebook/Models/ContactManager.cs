using Phonebook.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phonebook.Models
{
    public class ContactManager
    {
        private readonly FileManager file;
        private List<Contact> contacts { get; set; }

        public ContactManager()
        {
            file = new FileManager();
            contacts = file.ReadFromFile();
        }

        public void Add()
        {
            Console.WriteLine("Enter firstName:");
            var firstName = Console.ReadLine();
            Console.WriteLine("Enter lastName:");
            var lastName = Console.ReadLine();
            Console.WriteLine("Enter phone number:");
            var phoneNumber = Console.ReadLine();
            Console.WriteLine("Enter personal number:");
            var personalNumber = Console.ReadLine();

            Contact contact = new Contact
            {
                Firstname = firstName,
                Lastname = lastName,
                PhoneNumber = phoneNumber,
                PersonalNumber = personalNumber
            };

            contacts.Add(contact);

            bool success = file.AddContactToFile(contact);
            if (success)
            {
                Console.WriteLine("Contact is saved.");
            }           
        }
        
        public void RemoveContact()
        {
            if (!contacts.Any())
            {
                Console.WriteLine("There is no contacts to delete.");
            }
            else
            {
                Console.WriteLine("Enter firstName of the contact you want to remove");
                var firstName = Console.ReadLine();
                Console.WriteLine("Enter lastname of the contact you want to remove");
                var lastName = Console.ReadLine();

                if (contacts.Any(c => c.Firstname == firstName && c.Lastname == lastName))
                {
                    contacts.RemoveAll(c => c.Firstname == firstName && c.Lastname == lastName);
                    bool isSuccessful = file.UpdateContactOnFile(contacts);

                    if (isSuccessful)
                    {
                        Console.WriteLine("Contact is deleted.");
                    }
                }
                else
                {
                    Console.WriteLine("Contact is not found.");
                }
                
           }          
        }

        public void Search()
        {
            Console.WriteLine("Select search option: firstname | lastname | phone | egn ");
            SearchOption searchOption = EnumMapper.ReadSearchOption(Console.ReadLine().ToLower().Trim());
            PrintContacts(GetSearchResult(searchOption));
        }

        public void PrintAll()
        {
            PrintContacts(contacts);
        }

        public void UpdateUserInformation()
        {
            Console.WriteLine("Enter firstname on existing contact to be updated");
            var oldFirstName = Console.ReadLine();

            var itemsToUpdate = contacts.Where(c => c.Firstname == oldFirstName).ToList();

            if (itemsToUpdate.Any())
            {
                Console.WriteLine("Enter a new first Name");
                var newFirstName = Console.ReadLine();
                Console.WriteLine("Enter a new last Name");
                var newLastName = Console.ReadLine();
                Console.WriteLine("Enter a new phone number");
                var newPhone = Console.ReadLine();
                Console.WriteLine("Enter a new personal number");
                var newPersonalNumber = Console.ReadLine();

                UpdateContactList(itemsToUpdate, newFirstName, newLastName, newPhone, newPersonalNumber);
            }
            else
            {
                Console.WriteLine("Contact is not found.");
            }          
        }

        private void UpdateContactList(List<Contact> contactToUpdate, string newFirstName, string newLastName, string newPhone, string newPersonalNumber)
        {
            var updatedContacts = contacts.Except(contactToUpdate).ToList();

            contactToUpdate.ForEach(c2u => { c2u.Firstname = newFirstName; c2u.Lastname = newLastName; c2u.PhoneNumber = newPhone; c2u.PersonalNumber = newPersonalNumber; });

            updatedContacts.AddRange(contactToUpdate);

            if (file.UpdateContactOnFile(updatedContacts))
                Console.WriteLine("Contact is successfuly updated.");
        }

        private List<Contact> GetSearchResult(SearchOption searchOption)
        {
            string searchValue;
            List<Contact> searchResult = null;

            switch (searchOption)
            {
                case SearchOption.firstname:
                    Console.WriteLine("Enter first name for search");
                    searchValue = Console.ReadLine();
                    searchResult = contacts.Where(c => c.Firstname.ToLower().Contains(searchValue.ToLower())).ToList();
                    break;
                case SearchOption.lastname:
                    Console.WriteLine("Enter last name for search");
                    searchValue = Console.ReadLine();
                    searchResult = contacts.Where(c => c.Lastname.ToLower().Contains(searchValue.ToLower())).ToList();
                    break;
                case SearchOption.phone:
                    Console.WriteLine("Enter phone for search");
                    searchValue = Console.ReadLine();
                    searchResult = contacts.Where(c => c.PhoneNumber.ToLower().Contains(searchValue.ToLower())).ToList();
                    break;
                case SearchOption.egn:
                    Console.WriteLine("Enter egn for search");
                    searchValue = Console.ReadLine();
                    searchResult = contacts.Where(c => c.PersonalNumber.ToLower().Contains(searchValue.ToLower())).ToList();
                    break;
                case SearchOption.unknown:
                default:
                    Console.WriteLine("No such option for search.");
                    break;
            }

            return searchResult;
        }

        public void PrintContacts(List<Contact> contacts)
        {
            if (contacts != null)
            {
                if (contacts.Any())
                {
                    foreach (Contact contact in contacts)
                    {
                        Console.WriteLine($"FirstName: {contact.Firstname}, LastName: {contact.Lastname}, Phone: {contact.PhoneNumber}, EGN: {contact.PersonalNumber}");
                    }
                }
                else
                {
                    Console.WriteLine("Contact list is empty.");
                }
            }
        }
    }
}

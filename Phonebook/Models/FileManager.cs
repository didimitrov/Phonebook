using System;
using System.Collections.Generic;
using System.IO;

namespace Phonebook.Models
{
    class FileManager
    {
        private readonly string FilePath = @"D:\Phonebook.txt";

        public FileManager()
        {
            if (!File.Exists(FilePath))
            {
                File.AppendAllLines(FilePath, new[] { "" });
            }           
        }

        public bool AddContactToFile(Contact contact)
        {
            bool isSuccessful = false;
            if (contact != null)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(FilePath, true))
                    {
                        sw.WriteLine(contact.Firstname + "," + contact.Lastname + "," + contact.PhoneNumber + "," + contact.PersonalNumber + ",");
                        isSuccessful = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something went wrong" + ex);
                }
            }
           
            return isSuccessful;
        }

        public List<Contact> ReadFromFile()
        {
            string textLine;
            var contacts = new List<Contact>();
            try
            {
                using (StreamReader streamReader = new StreamReader(FilePath))
                {
                    while ((textLine = streamReader.ReadLine()) != null)
                    {
                        if (!string.IsNullOrEmpty(textLine))
                        {
                            string[] contactInfo = textLine.Split(',');
                            Contact contact = new Contact
                            {
                                Firstname = contactInfo[0],
                                Lastname = contactInfo[1],
                                PhoneNumber = contactInfo[2],
                                PersonalNumber = contactInfo[3]
                            };
                            contacts.Add(contact);
                        }                      
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong!" + ex);
            }

            return contacts;
        }

        public bool UpdateContactOnFile(List<Contact> contactList)
        {
            bool isSuccessful = false;

            if (contactList != null)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(FilePath))
                    {
                        foreach (var contact in contactList)
                        {
                            sw.WriteLine(contact.Firstname + "," + contact.Lastname + "," + contact.PhoneNumber + "," + contact.PersonalNumber + ",");
                        }
                        isSuccessful = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something went wrong! " + ex);
                }
            }
            
            return isSuccessful;
        }
    }
}

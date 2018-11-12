using Phonebook.Enums;
using Phonebook.Models;
using System;

namespace Phonebook
{
    class Program
    {
        static void Main()
        { 
            Menu();
            MenuOption menuOption = EnumMapper.ReadMenuOption(Console.ReadLine().ToLower().Trim());
            MenuWorker(menuOption);
        }
     
        private static void Menu()
        {
            Console.WriteLine("---------------------MENU-------------------");
            Console.WriteLine("Add | View | Delete | Update | Search | Quit");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Select option");
        }

        private static void MenuWorker(MenuOption menuOption)
        {
            ContactManager contactManager = new ContactManager();

            switch (menuOption)
            {
                case MenuOption.add:
                    contactManager.Add();
                    Continue();
                    break;
                case MenuOption.delete:
                    contactManager.PrintAll();
                    contactManager.RemoveContact();
                    Continue();
                    break;
                case MenuOption.update:
                    contactManager.UpdateUserInformation();
                    Continue();
                    break;
                case MenuOption.view:
                    contactManager.PrintAll();
                    Continue();
                    break;
                case MenuOption.search:
                    contactManager.Search();
                    Continue();
                    break;
                case MenuOption.quit:
                    break;
                case MenuOption.unknown:
                default:
                    Console.WriteLine();
                    Console.WriteLine("Please, read menu options and try again.");
                    Console.ReadKey(true);
                    Console.Clear();
                    Main();
                    break;
            }
        }

        private static void Continue()
        {
            Console.WriteLine("Continue: Y | N");
            AnswerOption answer = EnumMapper.ReadContinueOption(Console.ReadLine().ToLower().Trim()); 
            switch (answer)
            {
                case AnswerOption.n:
                case AnswerOption.unknown:
                    break;
                case AnswerOption.y:
                default:
                    Console.Clear();
                    Main();
                    break;
            }
        }
    }
}

    


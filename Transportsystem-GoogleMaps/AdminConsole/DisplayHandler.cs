using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportsystem_GoogleMaps.Models;

namespace AdminConsole
{
    class DisplayHandler
    {
        private DatabaseController _dbController = new DatabaseController();
        private ConsoleColor _color = ConsoleColor.Magenta;

        public void DiplayStart()
        {
            
            bool stayInLoop = true;
            while (stayInLoop)
            {
                Console.Clear();
                Console.ForegroundColor = _color;
                Console.WriteLine("MAIN MENU");
                Console.WriteLine("-------------------------");
                Console.WriteLine("1. Create new object");
                Console.WriteLine("2. Display existing objects");
                Console.WriteLine("Type \"e\" to exit");
                Console.WriteLine("");
                Console.ResetColor();
                string selected = Console.ReadLine();
                switch (selected)
                {
                    case "1":
                        CreationMenu();
                        break;
                    case "2":
                        ViewMenu();
                        break;
                    case "e":
                        stayInLoop = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }
        }

        public void CreationMenu()
        {
            bool stayInLoop = true;
            while (stayInLoop)
            {
                Console.Clear();
                Console.ForegroundColor = _color;
                Console.WriteLine("CREATION MENU");
                Console.WriteLine("-------------------------");
                Console.WriteLine("1. Package");
                Console.WriteLine("2. Driver");
                Console.WriteLine("Type \"e\" to return");
                Console.ResetColor();
                string selected = Console.ReadLine();
                switch (selected)
                {
                    case "1":
                        PackageCreation();
                        break;
                    case "2":
                        DriverCreation();
                        break;
                    case "e":
                        stayInLoop = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;

                }
            }
            
        }

        public void ViewMenu()
        {
            bool stayInLoop = true;
            while (stayInLoop)
            {
                Console.Clear();
                Console.ForegroundColor = _color;
                Console.WriteLine("VIEW MENU");
                Console.WriteLine("-------------------------");
                Console.WriteLine("1. Package");
                Console.WriteLine("2. Driver");
                Console.WriteLine("Type \"e\" to return");
                Console.ResetColor();
                string selected = Console.ReadLine();
                switch (selected)
                {
                    case "1":
                        PackageView();
                        break;
                    case "2":
                        DriverView();
                        break;
                    case "e":
                        stayInLoop = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;

                }
            }
        }

        public void DriverCreation()
        {
            Driver d = new Driver(); 
            Console.Clear();
            Console.ForegroundColor = _color;
            Console.WriteLine("DRIVER CREATION");
            Console.WriteLine("-------------------------");
            Console.ResetColor();
            Console.WriteLine("");
            Console.Write("Name: ");
            d.Name = Console.ReadLine();
            Console.Write("Phone: ");
            d.PhoneNumber = Console.ReadLine();
            _dbController.SaveDriver(d);
        }

        public void PackageCreation()
        {
            Package p  = new Package();
            Console.Clear();
            Console.ForegroundColor = _color;
            Console.WriteLine("PACKAGE CREATION");
            Console.WriteLine("-------------------------");
            Console.ResetColor();
            Console.WriteLine("");
            Console.Write("Content: ");
            p.Content = Console.ReadLine();
            Console.Write("Destination: ");
            p.Destination = Console.ReadLine();
            _dbController.SavePackage(p);

        }

        public void DriverView()
        {
            Console.Clear();
            Console.WriteLine("DRIVER LIST");
            Console.WriteLine("-------------------------");
            _dbController.GetDrivers();
        }

        public void PackageView()
        {
            Console.Clear();
            Console.WriteLine("PACKAGE LIST");
            Console.WriteLine("-------------------------");
            _dbController.GetPackages();
        }
    }
}

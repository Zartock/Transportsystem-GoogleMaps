using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Transportsystem_GoogleMaps.Models;
using Transportsystem_GoogleMaps.ViewModels;

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
                Console.WriteLine("3. Clustering");
                Console.WriteLine("4. Get route");
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
                    case "3":
                        Clustering();
                        break;
                    case "4":
                        GetRouteSelection();
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

        public void DriverView()
        {
            Console.Clear();
            Console.WriteLine("DRIVER LIST");
            Console.WriteLine("-------------------------");


            bool stayInLoop = true;

            while (stayInLoop)
            {
                List<Driver> drivers = new List<Driver>(_dbController.GetDrivers());
                Console.Clear();
                for (int i = 1; i <= drivers.Count(); i++)
                {
                    Console.WriteLine(i + ".");
                    Console.WriteLine("    ID: " + drivers.ElementAt(i - 1).Id);
                    Console.WriteLine("    Name: " + drivers.ElementAt(i - 1).Name);
                    Console.WriteLine("    Phone number: " + drivers.ElementAt(i - 1).PhoneNumber);
                    Console.WriteLine("------------------------------");
                }
                Console.WriteLine("");
                Console.WriteLine("Modify: m {id}");
                Console.WriteLine("Delete: d {id}");
                Console.WriteLine("Exit: \"e\"");
                string command = Console.ReadLine();
                if (!String.IsNullOrEmpty(command))
                {
                    switch (command[0])
                    {
                        case 'm':
                            Console.WriteLine("Modify on ID: " + GetIdFromCommand(command));
                            DriverModification(GetIdFromCommand(command));
                            break;
                        case 'd':
                            Console.WriteLine("Delete on ID: " + GetIdFromCommand(command));
                            _dbController.DeleteDriver(GetIdFromCommand(command));
                            break;
                        case 'e':
                            Console.WriteLine("Exit");
                            stayInLoop = false;
                            break;
                        default:
                            Console.WriteLine("Invalid command");
                            break;
                    }
                }
                
            }
        }

        public void PackageView()
        {
            Console.Clear();
            Console.WriteLine("PACKAGE LIST");
            Console.WriteLine("-------------------------");

            bool stayInLoop = true;

            while (stayInLoop)
            {
                List<Package> packages = new List<Package>(_dbController.GetPackages());
                Console.Clear();
                for (int i = 1; i <= packages.Count(); i++)
                {
                    Console.WriteLine(i + ".");
                    Console.WriteLine("    ID: " + packages.ElementAt(i - 1).Id);
                    Console.WriteLine("    Content: " + packages.ElementAt(i - 1).Content);
                    Console.WriteLine("    Destination: " + packages.ElementAt(i - 1).Destination);
                    Console.WriteLine("------------------------------");
                }

                Console.WriteLine("");
                Console.WriteLine("Modify: m {id}");
                Console.WriteLine("Delete: d {id}");
                Console.WriteLine("Exit: \"e\"");
                string command = Console.ReadLine();
                if (!String.IsNullOrEmpty(command))
                {
                    switch (command[0])
                    {
                        case 'm':
                            Console.WriteLine("Modify on ID: " + GetIdFromCommand(command));
                            PackageModification(GetIdFromCommand(command));
                            break;
                        case 'd':
                            Console.WriteLine("Delete on ID: " + GetIdFromCommand(command));
                            _dbController.DeletePackage(GetIdFromCommand(command));
                            break;
                        case 'e':
                            Console.WriteLine("Exit");
                            stayInLoop = false;
                            break;
                        default:
                            Console.WriteLine("Invalid command");
                            break;
                    }
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
            Package p = new Package();
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

        public void DriverModification(int id)
        {
            var driverInDb = _dbController.GetDriverById(id);
            Driver modedDriver = new Driver();
            Console.Clear();
            Console.ForegroundColor = _color;
            Console.WriteLine("DRIVER MODIFICATION");
            Console.WriteLine("-------------------------");
            Console.Write("Previous Name: "); Console.ResetColor(); Console.Write(driverInDb.Name + "\n"); Console.ForegroundColor = _color;
            Console.Write("Previous Phone: "); Console.ResetColor(); Console.Write(driverInDb.PhoneNumber + "\n"); Console.ForegroundColor = _color;
            Console.WriteLine("");
            Console.Write("New name: "); Console.ResetColor();
            modedDriver.Name = Console.ReadLine();
            Console.ForegroundColor = _color;
            Console.Write("New phone: "); Console.ResetColor();
            modedDriver.PhoneNumber = Console.ReadLine();
            _dbController.UpdateDriver(id, modedDriver);
        }

        public void PackageModification(int id)
        {
            var packageInDb = _dbController.GetPackageById(id);
            Package modedPackage = new Package();
            Console.Clear();
            Console.ForegroundColor = _color;
            Console.WriteLine("DRIVER MODIFICATION");
            Console.WriteLine("-------------------------");
            Console.Write("Previous Content: "); Console.ResetColor(); Console.Write(packageInDb.Content + "\n"); Console.ForegroundColor = _color;
            Console.Write("Previous Destination: "); Console.ResetColor(); Console.Write(packageInDb.Destination + "\n"); Console.ForegroundColor = _color;
            Console.WriteLine("");
            Console.Write("New Content: "); Console.ResetColor();
            modedPackage.Content = Console.ReadLine();
            Console.ForegroundColor = _color;
            Console.Write("New Destination: "); Console.ResetColor();
            modedPackage.Destination = Console.ReadLine();
            _dbController.UpdatePackage(id, modedPackage);
        }


        public void Clustering()
        {
            _dbController.Clustering();
        }

        public int GetIdFromCommand(string command)
        {
            return Int32.Parse(command.Split(' ')[1]);
        }

        public void GetRouteSelection()
        {
            Console.Clear();
            Console.WriteLine("DRIVER LIST");
            Console.WriteLine("-------------------------");

            List<Driver> drivers = new List<Driver>(_dbController.GetDrivers());

            for (int i = 1; i <= drivers.Count(); i++)
            {
                Console.WriteLine(i + ".");
                Console.WriteLine("    ID: " + drivers.ElementAt(i - 1).Id);
                Console.WriteLine("    Name: " + drivers.ElementAt(i - 1).Name);
                Console.WriteLine("    Phone number: " + drivers.ElementAt(i - 1).PhoneNumber);
                Console.WriteLine("------------------------------");
            }

            Console.WriteLine("");
            Console.Write("Select ID of driver to view:  ");
            try
            {
                int selected = Int32.Parse(Console.ReadLine());
                _dbController.GetRoute(selected);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
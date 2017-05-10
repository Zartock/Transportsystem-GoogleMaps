using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Transportsystem_GoogleMaps.Models;
using Transportsystem_GoogleMaps.ViewModels;

namespace AdminApiConsole
{
    class DisplayHandler
    {
        private ApiComunicator _api = new ApiComunicator();
        private ConsoleColor _color = ConsoleColor.Magenta;

        public void DisplayStart()
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
                        DateSelect();
                        //Clustering();
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

        public void DateSelect()
        {
            Console.Clear();
            Console.Write("Select Date {YYYY-MM-DD}: ");
            var input = Console.ReadLine();
            var rgx = new Regex(@"^\d{4}-\d{2}-\d{2}");
            if (rgx.IsMatch(input))
            {
                Console.WriteLine("matches");
                if (Convert.ToDateTime(input) - DateTime.Today <= TimeSpan.FromDays(0))
                    Console.WriteLine("Date already passed?");
                else
                {
                    Console.WriteLine("Date has not passed yet");
                    Clustering(Convert.ToDateTime(input));
                }      
            }
            else
                Console.WriteLine("does not match");
            Console.WriteLine("Press any key to continue");
            Console.Read();
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
                List<Driver> drivers = new List<Driver>(_api.GetDrivers());
                Console.Clear();
                for (int i = 1; i <= drivers.Count(); i++)
                {
                    Console.WriteLine(i + ".");
                    Console.WriteLine("    ID: " + drivers.ElementAt(i - 1).Id);
                    Console.WriteLine("    Name: " + drivers.ElementAt(i - 1).Name);
                    Console.WriteLine("    Phone number: " + drivers.ElementAt(i - 1).PhoneNumber);
                    Console.WriteLine("    Social security number: " + drivers.ElementAt(i - 1).PersonalNumber);
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
                            _api.DeleteDriver(GetIdFromCommand(command));
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
                List<Package> packages = new List<Package>(_api.GetPackages());
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
                            _api.DeletePackage(GetIdFromCommand(command));
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
            Console.Write("Social Security Number: ");
            d.PersonalNumber = Console.ReadLine();
            _api.SaveDriver(d);
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
            _api.SavePackage(p);
        }

        public void DriverModification(int id)
        {
            var driverInDb = _api.GetDriverById(id);
            Console.Clear();
            Console.ForegroundColor = _color;
            Console.WriteLine("DRIVER MODIFICATION");
            Console.WriteLine("-------------------------");
            Console.Write("Previous Name: "); Console.ResetColor(); Console.Write(driverInDb.Name + "\n"); Console.ForegroundColor = _color;
            Console.Write("Previous Phone: "); Console.ResetColor(); Console.Write(driverInDb.PhoneNumber + "\n"); Console.ForegroundColor = _color;
            Console.Write("Previous SSN: "); Console.ResetColor(); Console.Write(driverInDb.PersonalNumber + "\n"); Console.ForegroundColor = _color;
            Console.WriteLine("");
            Console.Write("New name: "); Console.ResetColor();
            driverInDb.Name = Console.ReadLine();
            Console.ForegroundColor = _color;
            Console.Write("New phone: "); Console.ResetColor();
            driverInDb.PhoneNumber = Console.ReadLine();
            Console.ForegroundColor = _color;
            Console.Write("New Social Security Number: "); Console.ResetColor();
            driverInDb.PersonalNumber = Console.ReadLine();
            _api.UpdateDriver(id, driverInDb);
        }

        public void PackageModification(int id)
        {
            var packageInDb = _api.GetPackageById(id);
            Console.Clear();
            Console.ForegroundColor = _color;
            Console.WriteLine("DRIVER MODIFICATION");
            Console.WriteLine("-------------------------");
            Console.Write("Previous Content: "); Console.ResetColor(); Console.Write(packageInDb.Content + "\n"); Console.ForegroundColor = _color;
            Console.Write("Previous Destination: "); Console.ResetColor(); Console.Write(packageInDb.Destination + "\n"); Console.ForegroundColor = _color;
            Console.WriteLine("");
            Console.Write("New Content: "); Console.ResetColor();
            packageInDb.Content = Console.ReadLine();
            Console.ForegroundColor = _color;
            Console.Write("New Destination: "); Console.ResetColor();
            packageInDb.Destination = Console.ReadLine();
            _api.UpdatePackage(id, packageInDb);
        }


        public void Clustering(DateTime date)
        {
            _api.DeleteDeliveryRoutes();
            var packages = _api.GetPackages();
            var driverList = _api.GetDrivers();
            int drivers = driverList.Count();

            Delivery d = new Delivery(drivers, packages);

            LinkedList<PackageCluster> clusters = d.clustering();
            List<DeliveryRoute> routes = new List<DeliveryRoute>();

            for (int i = 0; i < clusters.Count; i++)
            {
                List<Package> packageList = new List<Package>(clusters.ElementAt(i).AssignedPackages);
                for (int j = 0; j < packageList.Count; j++)
                {
                    routes.Add(new DeliveryRoute(driverList.ElementAt(i), packageList.ElementAt(j)));
                }
            }
            _api.AddDeliveryRoutes(routes, date);
        }

        public int GetIdFromCommand(string command)
        {
            return Int32.Parse(command.Split(' ')[1]);
        }

        public void GetRouteSelection()
        {
           
        }

        public static void printError(WebException e)
        {
            using (var stream = e.Response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                Console.WriteLine("There was an error handling this request" + "   " + reader.ReadToEnd() + "\n Press any key to continue");
                Console.ReadKey();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Transportsystem_GoogleMaps.Models;

namespace AdminApiConsole
{
    class ApiComunicator
    {
        private string serverURL = "http://localhost:52497";
        public List<Driver> GetDrivers()
        {
            WebRequest request = WebRequest.Create(serverURL + "/Api/Driver/");
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.  
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.  
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.  
            string responseFromServer = reader.ReadToEnd();
            var list = JsonConvert.DeserializeObject<List<Driver>>(responseFromServer);
            return list;
        }

        public Driver GetDriverById(int id)
        {
            WebRequest request = WebRequest.Create(serverURL + "/Api/Driver/" + id);
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.  
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.  
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.  
            string responseFromServer = reader.ReadToEnd();
            var driver = JsonConvert.DeserializeObject<Driver>(responseFromServer);
            return driver;
        }

        public List<Package> GetPackages()
        {
            WebRequest request = WebRequest.Create(serverURL + "/Api/Package/");
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.  
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.  
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.  
            string responseFromServer = reader.ReadToEnd();
            var list = JsonConvert.DeserializeObject<List<Package>>(responseFromServer);
            return list;
        }

  
        public Package GetPackageById(int id)
        {
            WebRequest request = WebRequest.Create(serverURL + "/Api/Package/" + id);
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.  
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.  
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.  
            string responseFromServer = reader.ReadToEnd();
            var package = JsonConvert.DeserializeObject<Package>(responseFromServer);
            return package;
        }

        public void SaveDriver(Driver d)
        {
            // Create a request using a URL that can receive a post.   
            WebRequest request = WebRequest.Create(serverURL + "/Api/Driver");
            // Set the Method property of the request to POST.  
            request.Method = "POST";
            string postData = JsonConvert.SerializeObject(d);
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.  
            request.ContentType = "application/json";
            // Set the ContentLength property of the WebRequest.  
            request.ContentLength = byteArray.Length;
            // Get the request stream.  
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.  
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.  
            dataStream.Close();
            try
            {
                WebResponse response = request.GetResponse();
            }
            catch (WebException e)
            {
                DisplayHandler.printError(e);
            }
        }

        public void AddDeliveryRoutes(List<DeliveryRoute> deliveryRoutes, DateTime date)
        {
            //var drivers = GetDrivers();
            //var packages = GetPackages();

            //// Create a request using a URL that can receive a post.   
            //WebRequest request = WebRequest.Create(serverURL + "/Api/DeliveryRoute");
            //// Set the Method property of the request to POST.  
            //request.Method = "POST";
            //string postData = JsonConvert.SerializeObject(deliveryRoutes);
            //byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            //// Set the ContentType property of the WebRequest.  
            //request.ContentType = "application/json";
            //// Set the ContentLength property of the WebRequest.  
            //request.ContentLength = byteArray.Length;
            //// Get the request stream.  
            //Stream dataStream = request.GetRequestStream();
            //// Write the data to the request stream.  
            //dataStream.Write(byteArray, 0, byteArray.Length);
            //// Close the Stream object.  
            //dataStream.Close();
            //try
            //{
            //    WebResponse response = request.GetResponse();
            //}
            //catch (WebException e)
            //{
            //    DisplayHandler.printError(e);
            //}

            WebRequest request = WebRequest.Create(serverURL + "/Api/DeliveryRoute/" + date.ToString().Split(' ')[0]);
            request.Method = "POST";
            request.ContentLength = 0;
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.  
            Stream dataStream = response.GetResponseStream();
        }

        public void SavePackage(Package p)
        {
            // Create a request using a URL that can receive a post.   
            WebRequest request = WebRequest.Create(serverURL + "/Api/Package");
            // Set the Method property of the request to POST.  
            request.Method = "POST";
            string postData = JsonConvert.SerializeObject(p);
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.  
            request.ContentType = "application/json";
            // Set the ContentLength property of the WebRequest.  
            request.ContentLength = byteArray.Length;
            // Get the request stream.  
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.  
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.  
            dataStream.Close();
            try
            {
                WebResponse response = request.GetResponse();
            }
            catch (WebException e)
            {
                DisplayHandler.printError(e);
            }
        }

  

        public void DeleteDriver(int id)
        {
            WebRequest request = WebRequest.Create(serverURL + "/Api/Driver/" + id);
            // Set the Method property of the request to POST.  
            request.Method = "DELETE";
            WebResponse response = request.GetResponse();

        }

        public void DeletePackage(int id)
        {
            WebRequest request = WebRequest.Create(serverURL + "/Api/Package/" + id);
            // Set the Method property of the request to POST.  
            request.Method = "DELETE";
            WebResponse response = request.GetResponse();
        }

        public void DeleteDeliveryRoutes()
        {
            WebRequest request = WebRequest.Create(serverURL + "/Api/DeliveryRoute/");
            // Set the Method property of the request to POST.  
            request.Method = "DELETE";
            WebResponse response = request.GetResponse();
        }

        public void UpdatePackage(int id, Package p)
        {
            WebRequest request = WebRequest.Create(serverURL + "/Api/Package/" + id);
            request.Method = "PUT";
            string postData = JsonConvert.SerializeObject(p);
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.  
            request.ContentType = "application/json";
            // Set the ContentLength property of the WebRequest.  
            request.ContentLength = byteArray.Length;
            // Get the request stream.  
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.  
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.  
            dataStream.Close();
            try
            {
                WebResponse response = request.GetResponse();
            }
            catch (WebException e)
            {
                DisplayHandler.printError(e);
            }
        }
        public void UpdateDriver(int id, Driver d)
        {
            WebRequest request = WebRequest.Create(serverURL + "/Api/Driver/" + id);
            request.Method = "PUT";
            string postData = JsonConvert.SerializeObject(d);
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.  
            request.ContentType = "application/json";
            // Set the ContentLength property of the WebRequest.  
            request.ContentLength = byteArray.Length;
            // Get the request stream.  
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.  
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.  
            dataStream.Close();
            try
            {
                WebResponse response = request.GetResponse();
            }
            catch (WebException e)
            {
                DisplayHandler.printError(e);
            }
        }
    }
}

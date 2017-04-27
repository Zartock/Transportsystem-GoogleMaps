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
    class Program
    {
        static void Main(string[] args)
        {
            //WebRequest request = WebRequest.Create("http://localhost:52497/Api/Driver/");
            //WebResponse response = request.GetResponse();
            //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            //// Get the stream containing content returned by the server.  
            //Stream dataStream = response.GetResponseStream();
            //// Open the stream using a StreamReader for easy access.  
            //StreamReader reader = new StreamReader(dataStream);
            //// Read the content.  
            //string responseFromServer = reader.ReadToEnd();

            //var list = JsonConvert.DeserializeObject<List<Driver>>(responseFromServer);
            //Console.ReadLine();
            DisplayHandler d = new DisplayHandler();
            d.DisplayStart();
        }
    }
}

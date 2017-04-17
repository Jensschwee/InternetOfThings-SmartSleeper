using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CMS.Helpers;
using CMS.Models;
using Newtonsoft.Json;

namespace CMS.DAL
{
    public class SensorReadingDal
    {
        public async Task<List<SensorReadingModel>> GetAllSensorReadings(string deviceId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Backend.GetBackendBaseAdress() + "sensorreadings/device/" + deviceId);
                HttpResponseMessage response = await client.GetAsync("");
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var readings = JsonConvert.DeserializeObject<List<SensorReadingModel>>(responseBody);
                    if (readings != null)
                    {
                        return readings;
                    }
                }
                return null;
            }
        }

    }
}

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
        private string teststring = "[{\"id\":13,\"deviceID\":\"1CC3D0\",\"server_time\":\"2017-04-14T07:23:10+00:00\",\"sensor_time\":\"2017-04-14T07:23:02+00:00\",\"system\":\"SIGFOX\",\"humidity\":32.74,\"lux\":35.74,\"temperature\":24.63,\"pressure\":1010,\"raw_data\":\"0fc8cca00df6933f\"},{\"id\":14,\"deviceID\":\"1CC3D0\",\"server_time\":\"2017-04-14T07:33:16+00:00\",\"sensor_time\":\"2017-04-14T07:33:14+00:00\",\"system\":\"SIGFOX\",\"humidity\":30.75,\"lux\":45.88,\"temperature\":25.63,\"pressure\":1010,\"raw_data\":\"0fc8c03011ec9407\"},{\"id\":15,\"deviceID\":\"1CC3D0\",\"server_time\":\"2017-04-14T07:43:24+00:00\",\"sensor_time\":\"2017-04-14T07:43:22+00:00\",\"system\":\"SIGFOX\",\"humidity\":30.86,\"lux\":36.8,\"temperature\":25.75,\"pressure\":1010,\"raw_data\":\"0fc8c0e00e60941e\"},{\"id\":16,\"deviceID\":\"1CC3D0\",\"server_time\":\"2017-04-14T08:19:12+00:00\",\"sensor_time\":\"2017-04-14T08:19:02+00:00\",\"system\":\"SIGFOX\",\"humidity\":27.12,\"lux\":30.84,\"temperature\":27.46,\"pressure\":1010,\"raw_data\":\"0fc8a9800c0c9574\"},{\"id\":17,\"deviceID\":\"1CC3D0\",\"server_time\":\"2017-04-14T09:15:12+00:00\",\"sensor_time\":\"2017-04-14T09:15:05+00:00\",\"system\":\"SIGFOX\",\"humidity\":26.97,\"lux\":30.57,\"temperature\":28.46,\"pressure\":1010,\"raw_data\":\"0fc8a8900bf1963d\"},{\"id\":18,\"deviceID\":\"1CC3D0\",\"server_time\":\"2017-04-14T09:20:14+00:00\",\"sensor_time\":\"2017-04-14T09:20:12+00:00\",\"system\":\"SIGFOX\",\"humidity\":22.97,\"lux\":42.87,\"temperature\":32.33,\"pressure\":1010,\"raw_data\":\"0fc88f9010bf9942\"},{\"id\":19,\"deviceID\":\"1CC3D0\",\"server_time\":\"2017-04-14T09:25:19+00:00\",\"sensor_time\":\"2017-04-14T09:25:18+00:00\",\"system\":\"SIGFOX\",\"humidity\":26.19,\"lux\":44.14,\"temperature\":27.45,\"pressure\":1010,\"raw_data\":\"0fc8a3b0113e9572\"},{\"id\":20,\"deviceID\":\"1CC3D0\",\"server_time\":\"2017-04-14T09:30:23+00:00\",\"sensor_time\":\"2017-04-14T09:30:22+00:00\",\"system\":\"SIGFOX\",\"humidity\":26.57,\"lux\":45.31,\"temperature\":28.18,\"pressure\":1010,\"raw_data\":\"0fc8a61011b39605\"},{\"id\":21,\"deviceID\":\"1CC3D0\",\"server_time\":\"2017-04-14T10:26:31+00:00\",\"sensor_time\":\"2017-04-14T10:26:25+00:00\",\"system\":\"SIGFOX\",\"humidity\":28.73,\"lux\":35.29,\"temperature\":27.35,\"pressure\":1010,\"raw_data\":\"0fc8b3900dc9955e\"},{\"id\":22,\"deviceID\":\"1CC3D0\",\"server_time\":\"2017-04-14T10:31:31+00:00\",\"sensor_time\":\"2017-04-14T10:31:30+00:00\",\"system\":\"SIGFOX\",\"humidity\":29.08,\"lux\":33.82,\"temperature\":27.29,\"pressure\":1010,\"raw_data\":\"0fc8b5c00d369552\"},{\"id\":23,\"deviceID\":\"1CC3D0\",\"server_time\":\"2017-04-14T11:27:39+00:00\",\"sensor_time\":\"2017-04-14T11:27:32+00:00\",\"system\":\"SIGFOX\",\"humidity\":26.91,\"lux\":53.37,\"temperature\":29.51,\"pressure\":1011,\"raw_data\":\"0fcca83014d9970e\"},{\"id\":24,\"deviceID\":\"1CC3D0\",\"server_time\":\"2017-04-14T11:32:39+00:00\",\"sensor_time\":\"2017-04-14T11:32:38+00:00\",\"system\":\"SIGFOX\",\"humidity\":30.29,\"lux\":54.44,\"temperature\":26.2,\"pressure\":1010,\"raw_data\":\"0fc8bd5015449479\"},{\"id\":25,\"deviceID\":\"1CC3D0\",\"server_time\":\"2017-04-14T11:47:56+00:00\",\"sensor_time\":\"2017-04-14T11:47:55+00:00\",\"system\":\"SIGFOX\",\"humidity\":31.53,\"lux\":37.36,\"temperature\":25.79,\"pressure\":1010,\"raw_data\":\"0fc8c5100e989427\"}]";
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
                else
                {
                    var readings = JsonConvert.DeserializeObject<List<SensorReadingModel>>(teststring);
                    if (readings != null)
                    {
                        return readings;
                    }
                }
                return null;
            }
        }

        public async Task<List<SensorReadingModel>> GetAllSensorReadings(string deviceId, long timeFrom, long timeTo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Backend.GetBackendBaseAdress() + "sensorreadings/device/" + deviceId + "?from="+ timeFrom + "&to=" + timeTo);
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
                else
                {
                    var readings = JsonConvert.DeserializeObject<List<SensorReadingModel>>(teststring);
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

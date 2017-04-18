using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class SensorReadingModel
    {
        public SensorReadingModel() { }

        public SensorReadingModel(double temperature)
        {
            this.temperature = temperature;
        }

        public int id { get; set; }
        public string deviceID { get; set; }
        public string server_time { get; set; }
        public string sensor_time { get; set; }

        public DateTime SensorTime => Convert.ToDateTime(sensor_time);

        public DateTime ServerTime => Convert.ToDateTime(server_time);

        public string system { get; set; }
        public double humidity { get; set; }
        public double lux { get; set; }
        public double temperature { get; set; }
        public int pressure { get; set; }
        public string raw_data { get; set; }
    }
}

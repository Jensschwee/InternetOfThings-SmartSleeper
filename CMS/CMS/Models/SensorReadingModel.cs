using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class SensorReadingModel
    {
        public SensorReadingModel() { }

        public SensorReadingModel(double time, double temperature)
        {
            Time = _dateOrigin.AddSeconds(Convert.ToDouble(time));
            Temperature = temperature;
        }

        private DateTime _dateOrigin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        private DateTime _time;

        public DateTime Time
        {
            get { return _time; }
            set { _time = value; }
        }

        public double Temperature { get; set; }

        public double Humidity { get; set; }

        public double Lux { get; set; }

        public int? Pressure { get; set; }
    }
}

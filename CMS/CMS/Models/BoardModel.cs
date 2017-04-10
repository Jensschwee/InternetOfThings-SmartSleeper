using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class BoardModel
    {
        public BoardModel()
        {
        }

        public BoardModel(string name, string deviceId)
        {
            this.Name = name;
            this.DeviceId = deviceId;
        }

        public string Name { get; set; }

        public string DeviceId { get; set; }
    }
}

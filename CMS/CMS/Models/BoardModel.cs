using System;
using System.Collections;
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
            this.device_name = name;
            this.deviceID = deviceId;
        }

        public string deviceID { get; set; }
        public string created_at { get; set; }
        public string device_name { get; set; }
        public string username { get; set; }
        public string user_username { get; set; }
    }
}

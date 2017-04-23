using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CMS.Helpers;
using Newtonsoft.Json;

namespace CMS.DAL
{
    public class UserDal
    {
        public async Task<bool> Login(string user, string pass)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Backend.GetBackendBaseAdress() + "users/login");
                var stringContent = new StringContent(JsonConvert.SerializeObject(new { username = user, password = pass}), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(client.BaseAddress, stringContent);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> RegisterUser(string user, string pass)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Backend.GetBackendBaseAdress() + "users/create");
                var stringContent = new StringContent(JsonConvert.SerializeObject(new { username = user, password = pass }), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(client.BaseAddress, stringContent);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

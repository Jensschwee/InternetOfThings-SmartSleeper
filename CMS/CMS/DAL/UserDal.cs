using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CMS.Helpers;

namespace CMS.DAL
{
    public class UserDal
    {
        public async Task<bool> Login(string username, string password)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Backend.GetBackendBaseAdress() + "users/login/" + username + "/" + password);
                HttpResponseMessage response = await client.GetAsync("");
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return true;
        }

        public async Task<bool> RegisterUser(string username, string password)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Backend.GetBackendBaseAdress() + "users/create/" + username + "/" + password);
                HttpResponseMessage response = await client.PostAsync(client.BaseAddress, null);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return true;
        }
    }
}

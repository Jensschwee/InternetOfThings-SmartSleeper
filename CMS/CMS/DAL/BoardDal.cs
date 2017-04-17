using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using CMS.Helpers;
using Newtonsoft.Json;

namespace CMS.DAL
{
    public class BoardDal
    {
        public async Task<bool> SendtBoardRegister(BoardModel board, string currentUser)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Backend.GetBackendBaseAdress() + "users/devices/register/" + currentUser + "/" + board.deviceID + "/" + board.device_name);
                HttpResponseMessage response = await client.PostAsync(client.BaseAddress, null);
                if (response.IsSuccessStatusCode)
                    return true;
            }
            return false;
        }

        public async Task<List<BoardModel>> GetAllBoards(string currentUser)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Backend.GetBackendBaseAdress() + "users/devices/" + currentUser);
                HttpResponseMessage response = await client.GetAsync("");
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var test = JsonConvert.DeserializeObject<List<BoardModel>>(responseBody);
                    if (test != null)
                    {
                        return test;
                    }
                }
                return null;
            }
        }



    }
}

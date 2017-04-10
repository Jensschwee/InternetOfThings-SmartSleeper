using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using CMS.Helpers;

namespace CMS.DAL
{
    public class BoardDal
    {
        public async Task<bool> SendtBoardRegister(BoardModel board, string currentUser)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Backend.GetBackendBaseAdress() + "device/register/" + currentUser + "/" + board.DeviceId + "/" + board.Name);
                HttpResponseMessage response = await client.PostAsync(client.BaseAddress, null);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }

    }
}

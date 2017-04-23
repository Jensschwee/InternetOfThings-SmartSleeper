using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
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
                client.BaseAddress = new Uri(Backend.GetBackendBaseAdress() + "users/devices/register");
                var stringContent = new StringContent(JsonConvert.SerializeObject(new { username = currentUser, deviceid = board.deviceID, devicename = board.device_name}), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(client.BaseAddress, stringContent);
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
                    var boards = JsonConvert.DeserializeObject<List<BoardModel>>(responseBody);
                    if (boards != null)
                    {
                        return boards;
                    }
                }
                return null;
            }
        }

        public async Task<BoardModel> GetBoards(string currentUser, string deviceId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Backend.GetBackendBaseAdress() + "users/device/" + currentUser + "/" + deviceId);
                HttpResponseMessage response = await client.GetAsync("");
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var board = JsonConvert.DeserializeObject<BoardModel>(responseBody);
                    if (board != null)
                    {
                        return board;
                    }
                }
                return null;
            }
        }

        public async Task<bool> DeleteBoard(BoardModel board, string currentUser)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Backend.GetBackendBaseAdress() + "users/devices/" + currentUser + "/" + board.deviceID);
                HttpResponseMessage response = await client.DeleteAsync(client.BaseAddress);
                if (response.IsSuccessStatusCode)
                    return true;
            }
            return false;
        }



    }
}

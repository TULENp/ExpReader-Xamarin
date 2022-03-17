using DAL.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ExpReader.Services
{
    static class DBService
    {
        private static readonly string MainIp = "192.168.0.103";
        private static HttpClient client = new HttpClient();
        
        public static async Task<string> GetUserBooks(int userid)
        {
            // Do not remove configure await, it does not work without this!
            string jsonstring = await client.GetStringAsync($"http://{MainIp}/Books/GetUserBooks/{userid}").ConfigureAwait(false);
            return jsonstring;
        }

        public static async Task<string> GetAllBooks()
        {
            // Do not remove configure await, it does not work without this!
            string jsonstring = await client.GetStringAsync($"http://{MainIp}/Books/GetAllBooks").ConfigureAwait(false);
            return jsonstring;
        }

        public static void SetUserBookStats(UserBook userBook)
        {
            client.PostAsJsonAsync($"http://{MainIp}/UserBook/SetUserBook", userBook);
        }
        public static void AddUserBookStats(UserBook userBook)
        {
            client.PostAsJsonAsync($"http://{MainIp}/UserBook/AddUserBook", userBook);
        }

        public static async Task<string> GetUserBookStats(int userid)
        {
            string jsonstring = await client.GetStringAsync($"http://{MainIp}/UserBook/GetUserBookStats/{userid}").ConfigureAwait(false);
            return jsonstring;
        }
        public static void UpdateDb()
        {
            var stats = JsonConvert.DeserializeObject<List<string>>(Preferences.Get("BookStats", string.Empty));
            foreach (var statId in stats)
            {
                var stat = JsonConvert.DeserializeObject<UserBook>(Preferences.Get(statId, string.Empty));
                SetUserBookStats(stat);
            }
        }

        public static async Task<string> LogIn(string log, string pas)
        {
            string json = await client.GetStringAsync($"http://{MainIp}/User/SignIn?login={log}&password={pas}").ConfigureAwait(false);
            return json;
        }

        public static async Task<int> GetUserId(string log, string pas)
        {
            string json = await client.GetStringAsync($"http://{MainIp}/User/SignIn?login={log}&password={pas}").ConfigureAwait(false);
            User temp = JsonConvert.DeserializeObject<User>(json);
            return temp.Id;
        }

        public static byte[] DownloadBook(string filename)
        {
            byte[] array = client.GetByteArrayAsync($"http://{MainIp}/Files/Books/{filename}").Result;
            return array;
        }

        public static string GetUserStats(int userid)
        {
            string json = client.GetStringAsync($"http://{MainIp}/UserStats/GetUserStats/{userid}").Result;
            return json;
        }
    }
    
}

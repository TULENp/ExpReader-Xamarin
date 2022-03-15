using DAL.Models;
using System.Net.Http;
using System.Threading.Tasks;

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

        public static void SetUserBook(UserBook userBook)
        {
            client.PostAsJsonAsync($"http://{MainIp}/UserBook/SetUserBook", userBook);
        }
    }
    
}

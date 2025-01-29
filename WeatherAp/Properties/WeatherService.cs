using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace WeatherAp
{
    public static class WeatherService
    {
        private const string ApiKey = "8b74933aa20bef2cf9f61e5fd293fdf9 ";
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";

        public static async Task<string> GetWeatherAsync(double lat, double lon)
        {
            using HttpClient client = new HttpClient();
            string url = $"{BaseUrl}?lat={lat}&lon={lon}&appid={ApiKey}&units=metric";

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                JObject data = JObject.Parse(json);
                string description = data["weather"][0]["description"].ToString();
                double temperature = double.Parse(data["main"]["temp"].ToString());

                return $"{description}, {temperature}°C";
            }
            return "Unable to retrieve weather data";
        }
    }
}

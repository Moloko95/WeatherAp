using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices.Sensors; // Correct namespace for Geolocation in .NET MAUI
using System;
using System.Threading.Tasks;

namespace WeatherAp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            LoadWeatherAsync();
        }

        private async void OnRefreshClicked(object sender, EventArgs e)
        {
            await LoadWeatherAsync();
        }

        private async Task LoadWeatherAsync()
        {
            try
            {
                Location location = await Geolocation.GetLastKnownLocationAsync();

                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium
                    });
                }

                if (location != null)
                {
                    LocationLabel.Text = $"Lat: {location.Latitude}, Lon: {location.Longitude}";
                    string weather = await WeatherService.GetWeatherAsync(location.Latitude, location.Longitude);
                    WeatherLabel.Text = $"Weather: {weather}";
                }
                else
                {
                    LocationLabel.Text = "Location unavailable.";
                    WeatherLabel.Text = "Weather unavailable.";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}

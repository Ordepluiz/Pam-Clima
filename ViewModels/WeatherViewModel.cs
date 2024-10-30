using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.ViewModels
{
    public partial class WeatherViewModel : ObservableObject
    {
        private const string apiKey = "f4daf4013b3ddf75bff5e212b7754cda";
        private const string endpoint = "https://api.openweathermap.org/data/2.5/weather";

        [ObservableProperty]
        private WeatherData weatherData;

        [ObservableProperty]
        private string city;

        public WeatherViewModel()
        {
            City = "São Paulo";
            GetWeatherDataCommand = new RelayCommand(async () => await GetWeatherData());
        }

        public RelayCommand GetWeatherDataCommand { get; }

        private async Task GetWeatherData()
        {
            using HttpClient client = new HttpClient();
            string url = $"{endpoint}?q={City}&appid={apiKey}&units=metric";
            try
            {
                var response = await client.GetStringAsync(url);
                WeatherData = JsonConvert.DeserializeObject<WeatherData>(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

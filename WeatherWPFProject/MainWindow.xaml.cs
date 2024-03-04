using Microsoft.Maps.MapControl.WPF;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WeatherWPFProject
{
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
        }



        private async void Dblclick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            Point mousePosition = e.GetPosition(map);
            Location pinLocation = map.ViewportPointToLocation(mousePosition);

            Pushpin pin = new Pushpin();
            pin.Location = pinLocation;
            map.Children.Add(pin);

            var weatherInfo = await GetWeatherForLocation(pinLocation);
            if (weatherInfo != null)
            {
                ShowWeatherData(weatherInfo);
            }
        }



        private async Task<WeatherInfo.Root> GetWeatherForLocation(Location location)
        {
            string url = $"http://api.openweathermap.org/data/2.5/weather?lat={location.Latitude}&lon={location.Longitude}&appid=39cadc7a931606acedb86ea47e72c040";
            using (HttpClient client = new HttpClient())
            {
                string response = await client.GetStringAsync(url);
                return JsonSerializer.Deserialize<WeatherInfo.Root>(response);
            }
        }



        private void ShowWeatherData(WeatherInfo.Root weatherInfo)
        {

            double temp = weatherInfo.main.temp - 273.15;
            MessageBox.Show(
                $"Description: {weatherInfo.weather[0].description}.\n" +
                $"Temp: {(float)System.Math.Round(temp, 2)}°C\n" +s
                $"Pressure: {weatherInfo.main.pressure}hPa\n" +
                $"Wind: {weatherInfo.wind.speed}m/s\n" +
                $"Sunrise: {new System.DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).AddSeconds(weatherInfo.sys.sunrise)}\n" +
                $"Sunset: {new System.DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).AddSeconds(weatherInfo.sys.sunset)}\n"

            );
        }

        private async void buttonclick(object sender, RoutedEventArgs e)
        {
            string searchTerm = searchBox.Text;
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                MessageBox.Show("Please enter a location name.");
                return;
            }

            string url = $"http://api.openweathermap.org/data/2.5/weather?q={Uri.EscapeDataString(searchTerm)}&appid=39cadc7a931606acedb86ea47e72c040";
            using (HttpClient client = new HttpClient())
            {
                string response = await client.GetStringAsync(url);
                var locationInfo = JsonSerializer.Deserialize<WeatherInfo.Root>(response);
                if (locationInfo != null)
                {
                    ShowLocationOnMap(locationInfo.coord.lat, locationInfo.coord.lon);
                    ShowWeatherData(locationInfo);
                }
                else
                {
                    MessageBox.Show("Location not found.");
                }
            }
        }
        private void ShowLocationOnMap(double latitude, double longitude)
        {
            Location location = new Location(latitude, longitude);
            map.SetView(location, map.ZoomLevel);
            Pushpin pin = new Pushpin();
            pin.Location = location;
            map.Children.Add(pin);
        }

    }

    }






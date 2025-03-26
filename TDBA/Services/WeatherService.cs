namespace TDBA.Services;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using dotenv.net;
using Microsoft.Extensions.Logging;

public class WeatherService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly ILogger<WeatherService> _logger;

    public WeatherService(HttpClient httpClient, ILogger<WeatherService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;

        // Load API key from .env
        DotEnv.Load();
        _apiKey = Environment.GetEnvironmentVariable("OPENWEATHER_API_KEY") ?? "";

        if (string.IsNullOrEmpty(_apiKey))
        {
            _logger.LogError("OpenWeather API key is missing!");
        }
    }

    public async Task<WeatherData?> GetCurrentWeather()
    {
        if (string.IsNullOrEmpty(_apiKey)) return null;

        try
        {
            double latitude = 38.2527;
            double longitude = -85.7585;

            string url = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={_apiKey}&units=imperial";

            return await _httpClient.GetFromJsonAsync<WeatherData>(url);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error fetching weather: {ex.Message}");
            return null;
        }
    }
}

// Add the WeatherData class
public class WeatherData
{
    public Main? Main { get; set; }
    public Wind? Wind { get; set; }
    public string? Name { get; set; } // Make Name nullable
}

public class Main
{
    public double Temp { get; set; }
    public double Pressure { get; set; }
    public double Humidity { get; set; }
}

public class Wind
{
    public double Speed { get; set; }
    public double Deg { get; set; }
}

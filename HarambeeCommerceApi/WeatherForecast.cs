namespace HarambeeCommerceApi;

public class WeatherForecast
{
    public DateTime Date { get; set; }

    public long TemperatureC { get; set; }

    public long TemperatureF => 32 + (long)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}
internal class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Enter the city for which you want to check the weather details : ");
        string? enteredCityName = Console.ReadLine();
        if(enteredCityName == null)
        {
            enteredCityName = args[0];
        }
        await CustomWeatherClientTool.WeatherDetails.getDetails(enteredCityName);
    }
}
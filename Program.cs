using System.Collections.Generic;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using WheatherForecastConsole;

var ApiKey = "c87d180368ccb05488b4db3805db2762";
var Сlient = new HttpClient();

string GetWind(int deg) =>
    deg switch
    {
        >= 0 and < 15 or >= 345 and < 360 => "Northern",
        >= 15 and < 75 => "northeast",
        >= 75 and < 105 => "Oriental",
        >= 105 and < 165 => "southeast",
        >= 165 and < 195 => "Southern",
        >= 195 and < 255 => "southwestern",
        >= 255 and < 285 => "West",
        >= 285 and < 345 => "northwestern",
        _ => string.Empty
    };

while (true)
{
    Console.WriteLine("weather forecast for 3 days");
    Console.Write("enter your city: ");//ввод города
    var city = Console.ReadLine();
    Console.WriteLine("Request in progress...");
    var response = await Сlient.GetAsync(@$"https://api.openweathermap.org/data/2.5/forecast?q={HttpUtility.UrlEncode(city)}&appid={ApiKey}&units=metric&lang=ru");
    if (response.IsSuccessStatusCode)
    {
        var result = await response.Content.ReadAsStringAsync();
        var info = JsonConvert.DeserializeObject<WheatherInfo>(result);
        Console.Clear();
         Console.WriteLine(
            $"City weather {info.City.Name}, {info.City.Country} on {DateTime.Now} - {info.List[0].Weather[0].Description}\n" +
            $"Air temperature - {Math.Round(info.List[0].Main.Temp, 1)}°С\n" +
            $"Feel like - {Math.Round(info.List[0].Main.Feels_like, 1)}°С\n" +
            $"Wind - {info.List[0].Wind.Speed}m/s, {GetWind(info.List[0].Wind.Deg)}\n" +
            $"Humidity - {info.List[0].Main.Humidity}%\n" +
            $"Pressure - {Math.Round(info.List[0].Main.Grnd_level / 1.33322, 2)} mmHg Art. (normal pressure - 760 mm Hg. Art.)\n\n" +
            $"Weather forecast for 3 days:\n");
        int cursorX = Console.GetCursorPosition().Left;
        int cursorY = Console.GetCursorPosition().Top;
        int nextDayIter = 8;
        string infoString;
        DateTime iterDate;
        List currentDateWeather;
        for (int i = 0; i < 3; i++)
        {
            currentDateWeather = info.List[nextDayIter];
            //Дата
            iterDate = DateTime.Parse(currentDateWeather.Dt_txt);
            Console.Write($"{iterDate.ToShortDateString(),25} | ");
            Console.SetCursorPosition(cursorX, cursorY + 1);
            
            //День недели
            infoString = iterDate.ToString("abs");
            infoString = infoString[0].ToString().ToUpper() + infoString.Substring(1);
            Console.Write($"{infoString,25} | ");
            Console.SetCursorPosition(cursorX, cursorY + 2);
            
            //Температура
            infoString = $"{Math.Round(currentDateWeather.Main.Temp_min, 2)}...{Math.Round(currentDateWeather.Main.Temp_max, 2)}";
            Console.Write($"{infoString,25} | ");
            Console.SetCursorPosition(cursorX, cursorY + 3);
            
            //Описание погоды
            infoString = currentDateWeather.Weather[0].Description;
            infoString = infoString[0].ToString().ToUpper() + infoString.Substring(1);
            Console.Write($"{infoString,25} | ");
            cursorX = Console.GetCursorPosition().Left;
            Console.SetCursorPosition(cursorX, cursorY);

            nextDayIter += 8;
        }
        Console.SetCursorPosition(0, cursorY + 6);
    }
    else
    {
        Console.WriteLine("City entered incorrectly!");//Ошибка
    }
    Console.WriteLine("Press ENTER to ask again");//повторный запрос
    Console.ReadLine();
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Data
{
    public interface IWeatherForcastService
    {
        Task<WeatherForecast[]> GetForecastAsync(DateTime startDate);
    }
}

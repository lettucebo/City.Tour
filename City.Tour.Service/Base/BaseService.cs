using System.Configuration;

namespace City.Tour.Service.Base
{
    public class BaseService
    {
        protected static string CityTourConnStr =>
            ConfigurationManager.ConnectionStrings["CityTourConnection"]?.ConnectionString;
    }
}

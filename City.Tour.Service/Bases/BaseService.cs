using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace City.Tour.Service.Bases
{
    public class BaseService
    {
        protected static string CityTourConnStr =>
            ConfigurationManager.ConnectionStrings["CityTourConnection"]?.ConnectionString;
    }
}

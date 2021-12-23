using AutomationFramework.Libraries;
using Newtonsoft.Json;
using System.IO;

namespace AutomationFramework.Helpers
{
    public class AppSettings
    {
        public static dynamic GetProperty()
        {
            return JsonConvert.DeserializeObject(File.ReadAllText(UtilityLibrary.GetCurrentPath() + "appSettings.json"));
        }
    }
}

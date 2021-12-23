using System.IO;
using AutomationFramework.Libraries;
using SharpYaml.Serialization;

namespace AutomationFramework.Configuration.YamlConfig
{
    public class PropertyConfig
    {
        private YamlStream _yaml;
        public Settings settings;
        public Site site;

        public PropertyConfig()
        {
            LoadYaml();
        }

        public struct Settings
        {
            public string Browser { get; set; }
            public string DeviceType { get; set; }
            public int ShowOnMonitor { get; set; }
            public string TestCategory { get; set; }
            public string RunInParallel { get; set; }
            public string PassedStepScreenshot { get; set; }
            public string FailedStepScreenshot { get; set; }
        }

        public struct Site
        {
            public string ProductName { get; set; }
            public string SiteAlias { get; set; }
            public string Environment { get; set; }
        }
        private void LoadYaml()
        {
            string parentOfStartupPath = UtilityLibrary.GetCurrentPath();
            string yamlPath = parentOfStartupPath + "Config.yaml";

            if (_yaml == null)
                _yaml = ReadFromFile(yamlPath);

            var mapping = (YamlMappingNode)_yaml.Documents[0].RootNode;
            var items = (YamlMappingNode)mapping.Children[new YamlScalarNode("Site")];

            site.ProductName = get_attribute(items, "ProductName");
            site.SiteAlias = get_attribute(items, "SiteAlias");
            site.Environment = get_attribute(items, "Environment");
            System.Diagnostics.Debug.WriteLine("ProductName     is: " + site.ProductName);
            System.Diagnostics.Debug.WriteLine("SiteAlias       is: " + site.SiteAlias);
            System.Diagnostics.Debug.WriteLine("Environment is: " + site.Environment);

            items = (YamlMappingNode)mapping.Children[new YamlScalarNode("Settings")];
            settings.Browser = get_attribute(items, "Browser");
            settings.DeviceType = get_attribute(items, "DeviceType");
            settings.ShowOnMonitor = System.Convert.ToInt32(get_attribute(items, "ShowOnMonitor"));
            settings.TestCategory = get_attribute(items, "TestCategory");
            settings.RunInParallel = get_attribute(items, "RunInParallel");
            settings.PassedStepScreenshot = get_attribute(items, "PassedStepScreenshot");
            settings.FailedStepScreenshot = get_attribute(items, "FailedStepScreenshot");
            System.Diagnostics.Debug.WriteLine("Browser         is: " + settings.Browser);
            System.Diagnostics.Debug.WriteLine("Device         is: " + settings.DeviceType);
            System.Diagnostics.Debug.WriteLine("ShowOnMonitor   is: " + settings.ShowOnMonitor);
            System.Diagnostics.Debug.WriteLine("RunInParallel   is: " + settings.RunInParallel);
            System.Diagnostics.Debug.WriteLine("PassedStepScreenshot   is: " + settings.PassedStepScreenshot);
            System.Diagnostics.Debug.WriteLine("FailedStepScreenshot   is: " + settings.FailedStepScreenshot);
        }
        private string get_attribute(YamlMappingNode itemName, string attr)
        {
            return itemName.Children[new YamlScalarNode(attr)].ToString();
        }
        private YamlStream ReadFromFile(string path)
        {
            var input = new StreamReader(path);
            var yaml = new YamlStream();
            yaml.Load(input);
            return yaml;
        }
    }
}

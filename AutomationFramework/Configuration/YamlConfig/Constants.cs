namespace AutomationFramework.Configuration.YamlConfig
{
    public static class Constants
    {
        public class TestEnvironment
        {
            public const string QA_ENVIRONMENT = "QA";
            public const string DEV_ENVIRONMENT = "DEV";
            public const string STAGING_ENVIRONMENT = "STAG";
            public const string PRODUCTION_ENVIRONMENT = "PROD";
        }
        public class TestBrowsers
        {
            public const string CHROME = "Chrome";
            public const string FIREFOX = "Firefox";
            public const string EDGE = "Edge";
            public const string IE11 = "IE11";
        }
        public class TestMobileDevices
        {
            public const string Chrome_Emu_Nexus5 = "Nexus 5";
            public const string Chrome_Emu_GalaxyS5 = "Galaxy S5";
            public const string Chrome_Emu_Pixel2 = "Pixel 2";
            public const string Chrome_Emu_Pixel2XL = "Pixel 2 XL";
            public const string Chrome_Emu_iPhone5 = "iPhone 5/SE";
            public const string Chrome_Emu_iPhone678 = "iPhone 6/7/8";
            public const string Chrome_Emu_iPhoneX = "iPhone X";
            public const string Chrome_Emu_iPad = "iPad";
            public const string Chrome_Emu_iPadPro = "iPad Pro";
        }
        public class TestCategory
        {
            public const string CategoryBVT = "BVT";
            public const string CategoryP1 = "P1";
            public const string CategoryP2 = "P2";
            public const string CategoryP3 = "P3";
        }
    }
}

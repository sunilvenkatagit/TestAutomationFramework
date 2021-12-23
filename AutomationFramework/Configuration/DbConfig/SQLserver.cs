using System.Data.SqlClient;
using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Microsoft.Win32.SafeHandles;
using AutomationFramework.Configuration.YamlConfig;
using AutomationFramework.Helpers;

namespace AutomationFramework.Configuration.DbConfig
{
    public class SQLserver : Impersonation
    {
        private readonly string serverName;
        private readonly string dataBase;
        private readonly string connectionString;

        public SQLserver()
        {
            PropertyConfig myConfig = new PropertyConfig();

            switch (myConfig.site.Environment)
            {
                case Constants.TestEnvironment.QA_ENVIRONMENT:
                    serverName = AppSettings.GetProperty().CoombsSQLdb.ServerName;
                    dataBase = AppSettings.GetProperty().CoombsSQLdb.Database;
                    connectionString = $"Server={serverName}; " +
                                       $"Database={dataBase}; " +
                                       $"Integrated Security=True";
                    break;
                case Constants.TestEnvironment.DEV_ENVIRONMENT:
                    serverName = AppSettings.GetProperty().CoombsSQLdb.ServerName;
                    dataBase = AppSettings.GetProperty().CoombsSQLdb.Database;
                    connectionString = $"Server={serverName}; " +
                                       $"Database={dataBase}; " +
                                       $"Integrated Security=True";
                    break;
            }
        }

        public SqlConnection OpenConnection()
        {
            string domain = AppSettings.GetProperty().CoombsSQLdb.Domain;
            string userName = AppSettings.GetProperty().CoombsSQLdb.UserName;
            string password = AppSettings.GetProperty().CoombsSQLdb.Password;

            var connection = ImpersonateAsDifferUser(domain, userName, password, connectionString); ;

            return connection;
        }
    }

    public class Impersonation
    {
        public SqlConnection ImpersonateAsDifferUser(string domain, string username, string password, string connectionString, bool impersonate = true)
        {
            var conn = new SqlConnection(connectionString);

            if (impersonate)
            {
                const int LOGON32_LOGON_INTERACTIVE = 2;
                const int LOGON32_PROVIDER_DEFAULT = 0;

                Console.WriteLine("Before impersonation: " + WindowsIdentity.GetCurrent().Name);

                bool returnValue = LogonUser(username, domain, password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, out SafeAccessTokenHandle safeAccessTokenHandle);

                if (false == returnValue)
                {
                    int ret = Marshal.GetLastWin32Error();
                    Console.WriteLine("LogonUser failed with error code : {0}", ret);
                    throw new System.ComponentModel.Win32Exception(ret);
                }

                Console.WriteLine("Did LogonUser Succeed? " + (returnValue ? "Yes" : "No"));

                var connection = WindowsIdentity.RunImpersonated(
                                    safeAccessTokenHandle,
                                    () =>
                                    {
                                        Console.WriteLine("During impersonation: " + WindowsIdentity.GetCurrent().Name);
                                        conn.Open();
                                        return conn;
                                    }
                                    );

                Console.WriteLine($"After impersonation: {WindowsIdentity.GetCurrent().Name}\n");

                return connection;
            }
            else
            {
                conn.Open();
                return conn;
            }
        }

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword,
                                            int dwLogonType, int dwLogonProvider, out SafeAccessTokenHandle phToken);
    }
}

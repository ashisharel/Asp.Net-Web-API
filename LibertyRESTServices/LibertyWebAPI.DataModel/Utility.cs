using System;
using System.Configuration;

namespace LibertyWebAPI.DataModel
{
    public class Utility
    {
        private static volatile Utility instance;
        private static object syncRoot = new Object();

        private Utility() { }

        /// <summary>
        /// Property to get singleton instance of the class
        /// </summary>
        public static Utility Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Utility();
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Get connection string from the config
        /// </summary>
        /// <param name="env"></param>
        /// <returns></returns>
        public static string GetConnectionString()
        {
            string env = GetConfigValue("env");

            env = string.IsNullOrWhiteSpace(env) ? string.Empty : env.ToUpper();
            
            switch (env)
            {
                case "DEV":
                    return ConfigurationManager.ConnectionStrings["db_LUD_DEV"].ConnectionString;
                    
                case "QA":
                    return ConfigurationManager.ConnectionStrings["db_LUD_QA"].ConnectionString;
                    
                case "UAT":
                    return ConfigurationManager.ConnectionStrings["db_LUD_UAT"].ConnectionString;
                    
                case "PROD":
                    return ConfigurationManager.ConnectionStrings["db_LUD_PROD"].ConnectionString;
                    
                default:
                    return string.Empty;
            }
        }

        public static string GetConfigValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}

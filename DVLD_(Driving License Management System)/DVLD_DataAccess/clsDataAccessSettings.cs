using System;
using System.Configuration;


namespace DVLD_DataAccess
{
    public class clsDataAccessSettings
    {
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager
                       .ConnectionStrings["DVLDConnection"]
                       .ConnectionString;
            }
        }
    }
}
    
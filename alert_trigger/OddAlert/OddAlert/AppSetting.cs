using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace PCKLIB
{
    public class AppSettings<T> where T : new()
    {
        private const string DEFAULT_FILENAME = "settings.ini";

        public void Save(string fileName = DEFAULT_FILENAME)
        {
            try
            {
                File.WriteAllText(fileName, (new JavaScriptSerializer()).Serialize(this));
            }
            catch (Exception e)
            {
                Console.WriteLine("## App Setting Saving Failed : " + e.Message);
            }
        }

        public static void Save(T pSettings, string fileName = DEFAULT_FILENAME)
        {
            File.WriteAllText(fileName, (new JavaScriptSerializer()).Serialize(pSettings));
        }

        public static T Load(string fileName = DEFAULT_FILENAME)
        {
            try
            { 
                T t = new T();
                if (File.Exists(fileName))
                    t = (new JavaScriptSerializer()).Deserialize<T>(File.ReadAllText(fileName));
                else
                    return default(T);
                return t;
            }
            catch (Exception e)
            {
                Console.WriteLine("## App Setting Loading Failed : " + e.Message);
                return default(T);
            }
        }
    }

    public class Credential
    {
        public string user;
        public string pwd;
        public Credential() { user = pwd = ""; }
        public Credential(string _user, string _pwd)
        {
            user = _user; pwd = _pwd;
        }
    }
    public class UserSetting : AppSettings<UserSetting>
    {
        public Credential dyn_odd_credential = new Credential();
        public int alert_level = 5;
        public Credential api_credential = new Credential();
        public string api_endpoint = "http://127.0.0.1:8000/api";
    }
}

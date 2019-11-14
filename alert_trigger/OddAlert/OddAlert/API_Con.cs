/*
 * Helper class to be used for connection to API (Django Backend)
 * David Piao 2019
*/
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace OddAlert
{
    public class API_Con
    {
        public string api_endp; // endpoint URL
        public string token; // API token
        
        public API_Con(string _endpoint)
        {
            api_endp = _endpoint;
        }

        // login using the credential given by the setting file
        // return: true on success false on failure
        public async Task<bool> login()
        {
            try
            {
                string endp = $"{api_endp}/auth";
                string data = "{" +
                        $"\"username\":\"{MainApp.g_setting.api_credential.user}\"," +
                        $"\"password\":\"{MainApp.g_setting.api_credential.pwd}\"" +
                               "}";
                string resp = await WRequest.post_response(endp, data);
                var json = JObject.Parse(resp);
                token = json["token"].ToString();
                return true;
            }
            catch (Exception ex)
            {
                MainApp.log_error("Login failed." + ex.Message);
            }
            return false;
        }

        // get all notificatoins from the api
        // return: List of notification on success, null on failure
        public async Task<List<Notification>> get_notifications()
        {
            try
            {
                string endp = $"{api_endp}/notifications";
                var header = new Dictionary<string, string>()
                {
                    { "Authorization" , $"Token {token}"}
                };

                List<Notification> ret = new List<Notification>();

                string resp = await WRequest.get_response(endp, header, "");
                var jarray = JArray.Parse(resp);
                foreach (var jobj in jarray)
                {
                    var note = new JavaScriptSerializer().Deserialize<Notification>(jobj.ToString());
                    ret.Add(note);
                }
                
                return ret;
            }
            catch (Exception ex)
            {
                MainApp.log_error("Loading notifications from API failed. " + ex.Message);
            }
            return null;
        }

        // get tracking notificatoins from the api, this notification will be further supervised by tracking app
        // return: List of notification on success, null on failure
        public async Task<List<Notification>> get_tracking_notifications()
        {
            try
            {
                string endp = $"{api_endp}/trackingnotifications";
                var header = new Dictionary<string, string>()
                {
                    { "Authorization" , $"Token {token}"}
                };

                List<Notification> ret = new List<Notification>();

                string resp = await WRequest.get_response(endp, header, "");
                var jarray = JArray.Parse(resp);
                foreach (var jobj in jarray)
                {
                    var note = new JavaScriptSerializer().Deserialize<Notification>(jobj.ToString());
                    ret.Add(note);
                }

                return ret;
            }
            catch (Exception ex)
            {
                MainApp.log_error("Loading notifications from API failed. " + ex.Message);
            }
            return null;
        }

        public async Task<bool> create_notification(Notification note)
        {
            try
            {
                string endp = $"{api_endp}/notifications";
                var header = new Dictionary<string, string>()
                {
                    { "Authorization" , $"Token {token}"}
                };
                var json = new JavaScriptSerializer().Serialize(note);
                string resp = await WRequest.post_response(endp, json, header);
                return true;
            }
            catch (Exception ex)
            {
                MainApp.log_error("Creating notification failed. " + ex.Message);
            }
            return false;
        }

        public async Task<bool> update_notification(int id, Notification note)
        {
            try
            {
                string endp = $"{api_endp}/notifications/{id}";
                var header = new Dictionary<string, string>()
                {
                    { "Authorization" , $"Token {token}"}
                };
                var json = new JavaScriptSerializer().Serialize(note);
                string resp = await WRequest.put_response(endp, json, header);
                return true;
            }
            catch (Exception ex)
            {
                MainApp.log_error("Updating notification failed. " + ex.Message);
            }
            return false;
        }
    }
}

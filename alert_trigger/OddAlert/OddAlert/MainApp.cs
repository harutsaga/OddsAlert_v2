using PCKLIB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OddAlert
{
    static class MainApp
    {
        public static UserSetting g_setting;
        public static SimpleAES g_aes = new SimpleAES();
        public static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static TimeSpan g_time_diff;
        public static MainFrm m_main_frm;
        public static bool g_working = true;
        public static bool DEBUG = false;
        public static API_Con g_api = null;
        public static bool g_api_login = false;
        [STAThread]
        static async Task Main()
        {
            MainApp.log_info("Start...");

            // Load setting
            MainApp.log_info("Load setting...");
            Load_setting();
            g_setting.Save();

            // Calculate the time differences between Local and Australia
            var cst_tz = TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time");
            var local_tz = TimeZoneInfo.Local;
            var now = DateTimeOffset.UtcNow;
            TimeSpan cst_off = cst_tz.GetUtcOffset(now);
            TimeSpan local_off = local_tz.GetUtcOffset(now);
            g_time_diff = local_off - cst_off;

            // Global exception
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;

            MainApp.log_info("Log in...");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            g_api = new API_Con(MainApp.g_setting.api_endpoint);
            g_api_login = await connect_db();

            m_main_frm = new MainFrm();
            Application.Run(m_main_frm);
        }

        public static async Task<bool> connect_db()
        {
            if (!await g_api.login())
            {
                MessageBox.Show("Failed to connect to backend service.");
                Environment.Exit(0);
            }
            MainApp.log_info("Backend API connected.");
            return true;
        }

        public static DateTime AuNow()
        {
            return DateTime.Now.Subtract(g_time_diff);
        }
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                var ex = (Exception)e.ExceptionObject;
                logger.Error(ex.ToString() + @"Unhandled Exception");
            }
            finally
            {
                Application.Exit();
            }
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            try
            {
                logger.Error(e.Exception.ToString() + @"Thread Exception");
            }
            finally
            {
                //Application.Exit();
            }
        }
        private static void Load_setting()
        {
            g_setting = UserSetting.Load();
            if (g_setting == null)
                g_setting = new UserSetting();
            g_setting.alert_level = Math.Max(g_setting.alert_level, 5);
            return;
        }

        public static void Exit_app(string message)
        {
            MessageBox.Show(message);
            logger.Error(message);
            Environment.Exit(0);
        }

        public static void log_info(string msg, bool msgbox = false)
        {
            try
            {
                logger.Info(msg);
                if (msgbox)
                    MessageBox.Show(msg);
                if (m_main_frm != null)
                    m_main_frm.update_status(DateTime.Now.ToString("hh:MM:ss fff") + "   " + msg);
            }
            catch (Exception ex)
            {

            }
        }

        public static void log_error(string msg, bool msgbox = false)
        {
            try
            {
                logger.Error(msg);
                if (msgbox)
                    MessageBox.Show(msg);
            }
            catch (Exception ex)
            {

            }
        }
    }
}

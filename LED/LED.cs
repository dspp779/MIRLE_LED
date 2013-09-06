using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LED
{
    static class LED
    {
        public static SettingForm settingForm;
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new CPower_CSharp.Form1());
            settingForm = new SettingForm();
            Application.Run(settingForm);
        }
    }
}

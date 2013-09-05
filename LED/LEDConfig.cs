using INI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LED
{
    public static class LEDConfig
    {
        // file path for ini setting
        private readonly static IniFile iniFile = new IniFile(Application.StartupPath + @"\setting.ini");
        // file path for instant message list
        private readonly static string binPath = Application.StartupPath + @"\setting.bin";


        private static string _IpAddr = "192.168.1.222";
        private static int _port = 5200;
        private static string _IDCode = "255.255.255.255";
        private static int _timeout = 600;

        public readonly static Font defaultFont = new Font("細明體", 16, FontStyle.Regular, GraphicsUnit.Pixel);

        public static string IpAddr
        {
            get { return _IpAddr; }
            set
            {

                String[] ipFrag = value.Split('.');
                if (ipFrag.Length != 4)
                {
                    throw new FormatException("Invalid IPv4 Address\n(xxx.xxx.xxx.xxx)");
                }
                foreach (string str in ipFrag)
                {
                    int i = int.Parse(str);
                    if (i < 0 || i > 255)
                    {
                        throw new FormatException("Invalid IPv4 Address (0~255)");
                    }
                }
                _IpAddr = value;
                iniFile.IniWriteValue("NET", "IP", _IpAddr);
            }
        }
        public static int port
        {
            get { return _port; }
            set
            {
                _port = value;
                iniFile.IniWriteValue("NET", "PORT", _port.ToString());
            }
        }
        public static string IDCode
        {
            get { return _IDCode; }
            set
            {
                _IDCode = value;
                iniFile.IniWriteValue("NET", "IDCODE", _IDCode);
            }
        }
        public static int timeout
        {
            get { return _timeout; }
            set
            {
                _timeout = value;
                iniFile.IniWriteValue("NET", "TIMEOUT", _timeout.ToString());
            }
        }

        public static void loadConfig()
        {
            try
            {
                IpAddr = loadVal("NET", "IP");
                IDCode = loadVal("NET", "IDCODE");
                port = readValAsInt32("NET", "PORT");
                timeout = readValAsInt32("NET", "TIMEOUT");
            }
            catch
            {
            }
            finally
            {
                IpAddr = _IpAddr;
                IDCode = _IDCode;
                port = _port;
                timeout = _timeout;
            }
        }

        private static string loadVal(string section, string key)
        {
            return iniFile.IniReadValue(section, key);
        }
        private static int readValAsInt32(string section, string key)
        {
            return int.Parse(loadVal(section, key));
        }


        #region -- Instant Message List --
        public static List<IMSetting> loadIMList()
        {
            // load bin to list
            if (!File.Exists(binPath))
            {
                return new List<IMSetting>();
            }
            else
            {
                using (Stream stream = File.Open(binPath, FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    return (List<IMSetting>)bin.Deserialize(stream);
                }
            }
        }
        // save list to file as binary
        public static void saveIMList(List<IMSetting> IMList)
        {
            using (Stream stream = File.Open(binPath, FileMode.Create))
            {
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(stream, IMList);
            }
        }
        #endregion

    }
}

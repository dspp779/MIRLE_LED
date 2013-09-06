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
    /* LEDConfig is a static class which contains
     * default font style
     * setting ini file path with CP5200 configuration,
     * and message list object binary file path.
     * Also, with some utility method can be called.
     * */
    public static class LEDConfig
    {
        // file path for ini setting
        private readonly static IniFile iniFile = new IniFile(Application.StartupPath + @"\setting.ini");
        // file path for instant message list
        private readonly static string binPath = Application.StartupPath + @"\setting.bin";

        // CP5200 default net configurations
        private static string _IpAddr = "192.168.1.222";
        private static int _port = 5200;
        private static string _IDCode = "255.255.255.255";
        private static int _timeout = 200;

        // CP5200 display configuration
        public static int delayTime = 3000;

        // default font style
        public readonly static Font defaultFont = new Font("細明體", 16F, FontStyle.Regular, GraphicsUnit.Pixel);

        #region -- CP5200 configuration properties --
        // IP address property
        public static string IpAddr
        {
            get { return _IpAddr; }
            set
            {
                /* verification of an IP address
                 * which verifing four fragments and value.
                 * */
                ipStringVerify(value);
                // set IP value
                _IpAddr = value;
                // save to ini
                iniFile.IniWriteValue("NET", "IP", _IpAddr);
            }
        }
        // port property
        public static int port
        {
            get { return _port; }
            set
            {
                // verification of port
                if (value > 65535 || value < 0)
                {
                    throw new FormatException("Invalid Port (0~65535)");
                }

                // set port value
                _port = value;
                // save to ini
                iniFile.IniWriteValue("NET", "PORT", _port.ToString());
            }
        }
        // ID code property
        public static string IDCode
        {
            get { return _IDCode; }
            set
            {
                /* verification of an IP address
                 * which verifing four fragments and value.
                 * */
                ipStringVerify(value);
                // set id code value
                _IDCode = value;
                // save to ini
                iniFile.IniWriteValue("NET", "IDCODE", _IDCode);
            }
        }
        // timeout property
        public static int timeout
        {
            get { return _timeout; }
            set
            {
                _timeout = value;
                iniFile.IniWriteValue("NET", "TIMEOUT", _timeout.ToString());
            }
        }
        #endregion


        // load neet configuration value of CP5200
        public static void loadConfig()
        {
            try
            {
                IpAddr = readVal("NET", "IP");
                IDCode = readVal("NET", "IDCODE");
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


        #region -- INI read methods --

        // read value from ini, return string
        private static string readVal(string section, string key)
        {
            return iniFile.IniReadValue(section, key);
        }
        // read integer value from ini by parsing string value from ini
        private static int readValAsInt32(string section, string key)
        {
            return int.Parse(readVal(section, key));
        }

        #endregion


        // ip string verification
        private static void ipStringVerify(string ip)
        {
            // split to four fragments
            String[] ipFrag = ip.Split('.');
            if (ipFrag.Length != 4)
            {
                throw new FormatException("Invalid IPv4 Address\n(xxx.xxx.xxx.xxx)");
            }
            // very every fragment
            foreach (string str in ipFrag)
            {
                int i = int.Parse(str);
                // value of each frag is in range of 0 ~ 255
                if (i < 0 || i > 255)
                {
                    throw new FormatException("Invalid IPv4 Address (0~255)");
                }
            }
        }


        #region -- Instant Messages Load & Save --

        public static List<IMSetting> loadIMList()
        {
            // check file existence
            if (!File.Exists(binPath))
            {
                // return empty list
                return new List<IMSetting>();
            }
            // load bin to list from file
            else
            {
                /* 'using' key word would make instance exist in the scope
                 * and dispose the instance when leaving the block.
                 * Therefore, the file stream will close automatically.
                 * */
                using (Stream stream = File.Open(binPath, FileMode.Open))
                {
                    // binary formatter can deserialize a file into an object
                    BinaryFormatter bin = new BinaryFormatter();
                    // cast to list object and return
                    return (List<IMSetting>)bin.Deserialize(stream);
                }
            }
        }
        // save list to file as binary
        public static void saveIMList(List<IMSetting> IMList)
        {
            /* 'using' key word would make instance exist in the scope
             * and dispose the instance when leaving the block.
             * Therefore, the file stream will close automatically.
             * */
            using (Stream stream = File.Open(binPath, FileMode.Create))
            {
                // binary formatter can serialize an object into a file
                BinaryFormatter bin = new BinaryFormatter();
                // serialize into file stream
                bin.Serialize(stream, IMList);
            }
        }

        #endregion

    }
}

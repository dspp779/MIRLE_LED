using CPower_CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LED
{
    /* this is a connection class for communication with CP5200
     * */
    public static class LEDConnection
    {
        // get integer presentation of an IP string
        private static uint GetIP(string strIp)
        {
            System.Net.IPAddress ipaddress = System.Net.IPAddress.Parse(strIp);
            uint lIp = BitConverter.ToUInt32(ipaddress.GetAddressBytes(), 0);
            //調整IP字節序
            lIp = ((lIp & 0xFF000000) >> 24) + ((lIp & 0x00FF0000) >> 8) + ((lIp & 0x0000FF00) << 8) + ((lIp & 0x000000FF) << 24);
            return (lIp);
        }

        // initialize communication  configuration
        public static void InitComm()
        {
            uint dwIPAddr = GetIP(LEDConfig.IpAddr);
            uint dwIDCode = GetIP(LEDConfig.IDCode);
            int nIPPort = LEDConfig.port;
            if (dwIPAddr != 0 && dwIDCode != 0)
            {
                CP5200.CP5200_Net_Init(dwIPAddr, LEDConfig.port, dwIDCode, LEDConfig.timeout);
            }
        }

        // send text
        public static bool CP5200_SendText(string str)
        {
            int nRet = CP5200.CP5200_Net_SendText(Convert.ToByte(1), 0, Marshal.StringToHGlobalAnsi(str), 0xFF, 16, 3, 0, 3, 5);

            return nRet >= 0;
        }

        // send image
        public static bool CP5200_SendImg(TextImage img, int effect)
        {
            int nRet = CP5200.CP5200_Net_SendPicture(Convert.ToByte(1), 0, 0, 0, img.Width, img.Height,
                    Marshal.StringToHGlobalAnsi(img.path), 0, effect, 3, 0);

            return nRet >= 0;
        }
    }
}

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace INI
{
    /// <summary>
    /// Create a New INI file to store or load data
    /// </summary>
    internal static class NativeMethods
    {
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        public static extern bool WritePrivateProfileString(string section,
            string key, string val, string filePath);
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        public static extern int GetPrivateProfileString(string section,
            string key, string def, StringBuilder retVal, int size, string filePath);
    }

    public class IniFile
    {
        public string path;
        /// <summary>
        /// INIFile Constructor.
        /// </summary>
        /// <PARAM name="INIPath"></PARAM>
        public IniFile(string INIPath)
        {
            path = INIPath;
        }
        /// <summary>
        /// Write Data to the INI File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// Section name
        /// <PARAM name="Key"></PARAM>
        /// Key Name
        /// <PARAM name="Value"></PARAM>
        /// Value Name
        public void IniWriteValue(string Section, string Key, string Value)
        {
            NativeMethods.WritePrivateProfileString(Section, Key, Value, this.path);
        }

        /// <summary>
        /// Read Data Value From the Ini File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// <PARAM name="Key"></PARAM>
        /// <PARAM name="Path"></PARAM>
        /// <returns></returns>
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = NativeMethods.GetPrivateProfileString(
                Section, Key, "", temp, 255, this.path);
            return temp.ToString();

        }
    }
}

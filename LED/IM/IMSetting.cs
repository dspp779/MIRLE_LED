using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LED
{
    [Serializable]
    public class IMSetting : InstantMessage
    {
        /* IMSetting is an extenstion of InstantMessage
         * with format verification and 
         * 
         * */
        private string _priorString;
        private string _node;
        private string _tag;
        private string _field;
        private string _format;
        private string _unit;

        public override string priorString
        {
            get
            {
                return _priorString;
            }
            set {
                /*if (!(value is string) || value.Length > 15 || value.Length == 0)
                {
                    throw new FormatException("長度超出範圍(1~15)");
                }*/
                _priorString = value;
            }
        }

        public override string source
        {
            get
            {
                return _node + '.' + _tag + '.' + _field;
            }
            set
            {
                if (!(value is string))
                {
                    throw new FormatException();
                }
                else
                {
                    Regex r = new Regex(@"^(\w+?)\.(\w+?)\.(\w+?)$");
                    Match m = r.Match(value);
                    if (m.Success)
                    {
                        _node = m.Groups[1].Value;
                        _tag = m.Groups[2].Value;
                        _field = m.Groups[3].Value;
                    }
                    else
                    {
                        throw new FormatException("Tag格式錯誤\r\nex: FIX.AI01.A_CV");
                    }
                }
            }
        }

        public override string format
        {
            get
            {
                return _format;
            }
            set
            {
                if (!(value is string))
                {
                    throw new FormatException();
                }
                else
                {
                    Regex r = new Regex(@"^(#{1,4})(\.(#{1,3}))?$");
                    Match m = r.Match(value);
                    if (m.Success && m.Groups[1].Value.Length + m.Groups[3].Value.Length == 4)
                    {
                        _format = value;
                    }
                    else
                    {
                        throw new FormatException("不合規定的format");
                    }
                }
            }
        }

        public override string unit
        {
            get
            {
                return _unit;
            }
            set
            {
                if (!(value is string) || value.Length > 12)
                {
                    throw new FormatException("單位字串超出範圍");
                }
                _unit = value;
            }
        }

        public IMSetting()
        {
        }

        public IMSetting(string str, string tag, string format, string unit, int color)
        {
            set(str, tag, format, unit, color);
        }
        
        public virtual string node
        {
            get {
                return _node;
            }
        }

        public virtual string tag
        {
            get
            {
                return _tag;
            }
        }

        public virtual string field
        {
            get
            {
                return _field;
            }
        }

        public string getVal(float val)
        {
            int i = _format.IndexOf('.');
            string format = i < 0 ? "" : _format.Substring(i).Replace('#', '0');
            return string.Format("{0} {1:0" + format + "} {2}", priorString, val, unit);
        }
    }
}

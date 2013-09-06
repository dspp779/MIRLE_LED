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
         * with format verification and format formatting
         * that build message string, including formatting
         * Eda value and composition with prior string and
         * unit.
         * */
        private string _priorString;
        private string _node;
        private string _tag;
        private string _field;
        private string _format;
        private string _unit;

        /* prior string is string put in front of all,
         * which should not be longer than 15.
         * */
        public override string priorString
        {
            get
            {
                return _priorString;
            }
            set {
                /*if (!(value is string) || value.Length > 15)
                {
                    throw new FormatException("長度超出範圍(0~15)");
                }*/
                _priorString = value;
            }
        }
        /* source is a complete presentation form of Eda query element,
         * which would have form like 'node.tag.field'.
         * */
        public override string source
        {
            get
            {
                // complete source string
                return _node + '.' + _tag + '.' + _field;
            }
            set
            {
                // if not a string
                if (!(value is string))
                {
                    throw new FormatException();
                }
                else
                {
                    // parse the source string using regular expression
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
                        throw new FormatException("Tag格式錯誤(ex: FIX.AI01.F_CV)");
                    }
                }
            }
        }
        /* format defines the presentation precision of the Eda value,
         * which would have form like '###.#', '##.##'.
         * */
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
                    // recognize the format using regular expression
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
        /* unit is a string that represent the unit of Eda value,
         * which should not be longer than 12.
         * */
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

        public IMSetting(InstantMessage im)
            : base(im)
        {
        }

        public IMSetting(string str, string tag, string format, string unit, int color)
            : base(str, tag, format, unit, color)
        {
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

        // formatting message string from Eda value
        public string getVal(float val)
        {
            int i = _format.IndexOf('.');
            string format = i < 0 ? "" : _format.Substring(i).Replace('#', '0');
            return string.Format("{0} {1:0" + format + "} {2}", priorString, val, unit);
        }
        public string getVal(string str)
        {
            return string.Format("{0} {1} {2}", priorString, str, unit);
        }
    }
}

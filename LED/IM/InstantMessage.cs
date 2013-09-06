using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LED
{
    /* InstantMessage is a simple class representing a message
     * with only field name and its type.
     * If you would like to take a look of format specification,
     * you may look to class IMSetting
     * */
    [Serializable]
    public class InstantMessage
    {
        public virtual string priorString
        {
            get;
            set;
        }
        public virtual string source
        {
            get;
            set;
        }
        public virtual string format
        {
            get;
            set;
        }
        public virtual string unit
        {
            get;
            set;
        }
        public virtual int color
        {
            get;
            set;
        }

        public InstantMessage(InstantMessage im)
        {
            set(im);
        }

        public InstantMessage(string str, string tag, string format, string unit, int color)
        {
            set(str, tag, format, unit, color);
        }
        
        // set value from InstantMessage
        public void set(InstantMessage im)
        {
            set(im.priorString, im.source, im.format, im.unit, im.color);
        }
        // set vallue from values
        public void set(string str, string tag, string format, string unit, int color)
        {
            this.priorString = str;
            this.source = tag;
            this.format = format;
            this.unit = unit;
            this.color = color;
        }
    }
}

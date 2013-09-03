using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using INI;

namespace IniExample
{
    public partial class Form1 : Form
    {
        IniFile file = new IniFile(Application.StartupPath + "\\server.ini");

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = file.IniReadValue("Server", "IP");
                textBox2.Text = file.IniReadValue("Server", "port");
                textBox3.Text = file.IniReadValue("Server", "userName");
                MessageBox.Show("讀取操作完成");
            }
            catch (Exception ex)
            {
                MessageBox.Show("成功失敗\n"+ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                file.IniWriteValue("Server", "IP", textBox1.Text);
                file.IniWriteValue("Server", "port", textBox2.Text);
                file.IniWriteValue("Server", "userName", textBox3.Text);
                MessageBox.Show("寫入操作完成");
            }
            catch (Exception ex)
            {
                MessageBox.Show("寫入失敗：檔案唯讀\n" + ex.Message);
            }
        }
    }
}

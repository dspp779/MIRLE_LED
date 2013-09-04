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
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GeFanuc.iFixToolkit.Adapter;
using CPower_CSharp;
using System.Runtime.InteropServices;

namespace LED
{
    public partial class SettingForm : Form
    {
        // file path for ini setting
        private readonly static IniFile iniFile = new IniFile(Application.StartupPath + @"\setting.ini");
        // file path for instant message list
        private readonly static string binPath = Application.StartupPath + @"\setting.bin";


        private List<IMSetting> IMList;
        private InstantMessage presentIM = new InstantMessage();

        private bool refreshSignal = false;
        private object signalLock = new object();

        // CP5200 LED controller related variable
        private int m_nTimeout = 600;

        #region -- initialization --

        public SettingForm()
        {
            InitializeComponent();
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            try
            {
                // load screen setting
                Screen_IP.Text = iniFile.IniReadValue("SCREEN", "IP");
                // load alarm setting
                // load im
                loadIMList();
                // init data grid view
                initDataGrid();
                toolStripStatusLabel.Text = "設定載入完成";
            }
            catch (Exception)
            {
                toolStripStatusLabel.Text = "設定載入失敗";
            }

            dataGridView_IM.ClearSelection();

            // add event handler
            dataGridView_IM.SelectionChanged += new EventHandler(dataGridView_IM_SelectionChanged);
            // input data change handler
            im_string.TextChanged += new EventHandler(InputData_Change);
            im_tag.TextChanged += new EventHandler(InputData_Change);
            im_format.SelectedIndexChanged += new EventHandler(InputData_Change);
            im_unit.TextChanged += new EventHandler(InputData_Change);
            im_colorButton.ForeColorChanged += new EventHandler(InputData_Change);

            // start eda data refresh worker
            ThreadPool.QueueUserWorkItem(new WaitCallback(RefreshPreviewWorker));
        }

        private void initDataGrid()
        {
            dataGridView_IM.Rows.Clear();
            dataGridView_IM.Rows.Add(IMList.Count);

            int i = 0;
            foreach (IMSetting im in IMList)
            {
                setRow(i++, im.priorString, im.source, im.format, im.unit, Color.FromArgb(im.color));
            }
        }

        #endregion

        #region -- settings --
        private void loadIMList()
        {
            // load bin to list
            if (!File.Exists(binPath))
            {
                IMList = new List<IMSetting>();
            }
            else
            {
                using (Stream stream = File.Open(binPath, FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    IMList = (List<IMSetting>)bin.Deserialize(stream);
                }
            }
        }
        // save list to file as binary
        private void saveIMList()
        {
            using (Stream stream = File.Open(binPath, FileMode.Create))
            {
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(stream, IMList);
            }
        }

        private void saveIni()
        {
            String[] ipFrag = Screen_IP.Text.Split('.');
            if (ipFrag.Length != 4)
            {
                throw new FormatException("Invalid IPv4 Address\n(x.x.x.x)");
            }
            foreach (string str in ipFrag)
            {
                int i;
                if (!int.TryParse(str, out i) || i < 0 || i > 255)
                {
                    throw new FormatException("Invalid IPv4 Address\nx.x.x.x(x:0~255)");
                }
            }
            iniFile.IniWriteValue("SCREEN", "IP", Screen_IP.Text);
            Invoke(new UIHandler(RefreshStatus), new Object[] { "ini設定檔保存完成" });
        }
        #endregion

        // data refresh worker
        private void RefreshPreviewWorker(object o)
        {
            IMSetting ims = new IMSetting();
            while (true)
            {
                try
                {
                    lock (signalLock)
                    {
                        ims.set(presentIM);

                        refreshSignal = false;

                        float f;
                        short nErr = Eda.GetOneFloat(ims.node, ims.tag, ims.field, out f);

                        if (nErr != FixError.FE_OK)
                        {
                            RefreshMessage("????");
                        }
                        else
                        {
                            string str = string.Format("{0:" + ims.format.Replace('x', '0') + "}", f);
                            RefreshMessage(presentIM.priorString + str + presentIM.unit);
                        }
                    }
                }
                catch (DllNotFoundException)
                {
                    RefreshMessage("????");
                    RefreshStatus("請確認是否開啟ifix");
                }
                catch (Exception ex)
                {
                    if (this.IsDisposed)
                    {
                        break;
                    }
                    else
                    {
                        try
                        {
                            RefreshMessage(ex.Message);
                        }
                        catch (ObjectDisposedException)
                        {
                        }
                    }
                }
                finally
                {
                    SpinWait.SpinUntil(() => refreshSignal, 1000);
                }
            }
        }

        #region -- UI delegate --

        delegate void UIHandler(string str);
        private void RefreshMessage(string str)
        {
            if (Disposing || IsDisposed)
            {
                return;
            }
            //refresh preview area
            Invoke(new UIHandler(RefreshPreview), new Object[] { str });

            //CP5200_SendText(PreviewResult.Text);

            // create temporal image
            TextImage tempImg = new TextImage(str, Font, Color.FromArgb(presentIM.color), Color.Black);
            // send temporal image
            CP5200_SendImg(tempImg);
            // delete temporal image
            tempImg.release();
        }
        private void RefreshPreview(string str)
        {
            PreviewResult.Text = str;
            PreviewResult.ForeColor = Color.FromArgb(presentIM.color);
        }
        private void RefreshStatus(string str)
        {
            Invoke(new UIHandler((o)=> toolStripStatusLabel.Text = str), new Object[] { null });
        }

        #endregion

        #region -- IM manipulation --

        private void inputIM()
        {
            // modify selected cells
            if (dataGridView_IM.SelectedCells.Count > 1)
            {
                foreach (DataGridViewTextBoxCell cell in dataGridView_IM.SelectedCells)
                {
                    setIMData(cell);
                }
            }
            // modify row the only selected cell belongs to
            else if (dataGridView_IM.SelectedCells.Count == 1)
            {
                setIM(dataGridView_IM.CurrentRow.Index, presentIM.priorString, presentIM.source,
                    presentIM.format, presentIM.unit, presentIM.color);
            }
            // add new row
            else
            {
                // set im
                addIM(presentIM.priorString, presentIM.source,
                    presentIM.format, presentIM.unit, presentIM.color);
            }
        }
        private void addIM(string str, string tag, string format, string unit, int color)
        {
            IMSetting ims = new IMSetting(str, tag, format, unit, color);
            IMList.Add(ims);
            addRow(str, tag, format, unit, Color.FromArgb(color));
        }
        private void setIM(int index, string str, string tag, string format, string unit, int color)
        {
            IMList[index].set(presentIM);
            // set dataGirdView row
            setRow(index, str, tag, format, unit, Color.FromArgb(color));
        }

        // dataGirdView refresher
        private void addRow(string str, string tag, string format, string unit, Color color)
        {
            // add row
            dataGridView_IM.Rows.Add(1);
            // set dataGridView row value
            setRow(dataGridView_IM.Rows.Count - 1, str, tag, format, unit, color);
        }
        private void setRow(int index, string str, string tag, string format, string unit, Color color)
        {
            // set dataGridView row value
            DataGridViewCellCollection cells = dataGridView_IM.Rows[index].Cells;
            cells[IMData_string.Index].Value = str;
            cells[IMData_tag.Index].Value = tag;
            cells[IMData_format.Index].Value = format;
            cells[IMData_unit.Index].Value = unit;
            cells[IMData_color.Index].Style.BackColor = color;
        }

        private void setIMData(DataGridViewTextBoxCell cell)
        {
            int row = cell.RowIndex;
            int col = cell.ColumnIndex;

            if (col == IMData_string.Index)
            {
                cell.Value = presentIM.priorString;
                IMList[row].priorString = presentIM.priorString;
            }
            else if (col == IMData_tag.Index)
            {
                cell.Value = presentIM.source;
                IMList[row].source = presentIM.source;
            }
            else if (col == IMData_format.Index)
            {
                cell.Value = presentIM.format;
                IMList[row].format = presentIM.format;
            }
            else if (col == IMData_unit.Index)
            {
                cell.Value = presentIM.unit;
                IMList[row].unit = presentIM.unit;
            }
            else if (col == IMData_color.Index)
            {
                IMList[row].color = presentIM.color;
                dataGridView_IM.Rows[row].Cells[col].Style.BackColor
                    = Color.FromArgb(presentIM.color);
            }
        }

        #endregion

        #region -- event handler --

        // input data change handler
        private void InputData_Change(object sender, EventArgs e)
        {
            presentIM.set(im_string.Text, im_tag.Text, im_format.Text, im_unit.Text, im_colorButton.ForeColor.ToArgb());
            if (!refreshSignal)
            {
                lock (signalLock)
                {
                    refreshSignal = true;
                }
            }
        }
        
        // color event handler
        private void im_colorButton_Click(object sender, EventArgs e)
        {
            if (im_colorDialog.ShowDialog() == DialogResult.OK)
            {
                im_colorButton.ForeColor = im_colorDialog.Color;
            }
        }
        private void im_colorButton_ForeColorChanged(object sender, EventArgs e)
        {
            Color c = im_colorButton.ForeColor;
            int luma = (int)(c.R * 0.3 + c.G * 0.59 + c.B * 0.11);
            im_colorButton.BackColor = luma < 128 ? Color.White : Color.Black;
        }

        //dataGridView handler
        private void dataGridView_IM_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            IMList.RemoveAt(e.RowIndex);
            saveIMList();
        }
        private void dataGridView_IM_SelectionChanged(object sender, EventArgs e)
        {
            inputButton.Text = "Set";

            int index = dataGridView_IM.CurrentRow.Index;
            lock (signalLock)
            {
                im_format.Text = IMList[index].format;
                im_tag.Text = IMList[index].source;
                im_string.Text = IMList[index].priorString;
                im_unit.Text = IMList[index].unit;
                im_colorButton.ForeColor = Color.FromArgb(IMList[index].color);
            }
        }


        private void button_save_Click(object sender, EventArgs e)
        {
            try
            {
                saveIni();
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void inputButton_Click(object sender, EventArgs e)
        {
            try
            {
                inputIM();
                // save modification
                saveIMList();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }

            // clear selection
            dataGridView_IM.ClearSelection();
            // reset input button
            inputButton.Text = "Add";
        }

        #endregion

        #region -- CP5200 --
        private uint GetIP(string strIp)
        {
            System.Net.IPAddress ipaddress = System.Net.IPAddress.Parse(strIp);
            uint lIp = BitConverter.ToUInt32(ipaddress.GetAddressBytes(), 0);
            //調整IP字節序
            lIp = ((lIp & 0xFF000000) >> 24) + ((lIp & 0x00FF0000) >> 8) + ((lIp & 0x0000FF00) << 8) + ((lIp & 0x000000FF) << 24);
            return (lIp);
        }

        private void InitComm()
        {
                uint dwIPAddr = GetIP("192.168.1.222");
                uint dwIDCode = GetIP("255.255.255.255");
                int nIPPort = 5200;
                if (dwIPAddr != 0 && dwIDCode != 0)
                {
                    CP52000.CP5200_Net_Init(dwIPAddr, nIPPort, dwIDCode, m_nTimeout);
                }
        }

        private void CP5200_SendText(string str)
        {

            InitComm();
            int nRet;
            // Network
            nRet = CP52000.CP5200_Net_SendText(Convert.ToByte(1), 0, Marshal.StringToHGlobalAnsi(str), 0xFF, 16, 3, 0, 3, 5);

            if (nRet >= 0)
            {
                RefreshStatus("Send Message to CP5200 Success");
            }
            else
            {
                RefreshStatus("Send Message to CP5200 Fail");
            }
        }

        private void CP5200_SendImg(TextImage img)
        {

            InitComm();
            int nRet = 0;
            // Network
            nRet = CP52000.CP5200_Net_SendPicture(Convert.ToByte(1), 0, 0, 0, img.Width, img.Height,
                    Marshal.StringToHGlobalAnsi(img.path), 1, 0, 3, 0);

            if (nRet >= 0)
            {
                RefreshStatus("Send Message to CP5200 Success");
            }
            else
            {
                RefreshStatus("Send Message to CP5200 Fail");
            }
        }
        #endregion
    }
}

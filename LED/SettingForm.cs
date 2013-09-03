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

namespace LED
{
    public partial class SettingForm : Form
    {
        private IniFile iniFile = new IniFile(Application.StartupPath + @"\setting.ini");
        private string binPath = Application.StartupPath + @"\setting.bin";
        private List<IMSetting> IMList;
        private InstantMessage presentIM = new InstantMessage();

        private bool refreshSignal = false;
        private object signalLock = new object();

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
                loadIMSetting();
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
            //ThreadPool.QueueUserWorkItem(new WaitCallback(RefreshPreviewWorker));
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

        private void loadIMSetting()
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
        private void saveIMSetting()
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

        /*private void RefreshPreviewWorker(object o)
        {
            IMSetting ims = new IMSetting();
            while (true)
            {
                try
                {
                    lock (signalLock)
                    {
                        ims.set(presentIM);

                        StringBuilder sbData = new StringBuilder(80);
                        short nErr = Eda.GetOneAscii(ims.node, ims.tag, ims.field, sbData);
                        String str = sbData.ToString();

                        if (nErr != FixError.FE_OK)
                        {
                            Invoke(new UIHandler(RefreshPreview), new Object[] { "????" });
                        }
                        else
                        {
                            str = string.Format("{0:" + ims.format.Replace('x', '0') + "}", float.Parse(str));
                            Invoke(new UIHandler(RefreshPreview), new Object[] { str });
                        }
                        refreshSignal = false;
                    }
                }
                catch (Exception)
                {
                    if (this.IsDisposed)
                    {
                        break;
                    }
                    else
                    {
                        try
                        {
                            Invoke(new UIHandler(RefreshPreview), new Object[] { "????" });
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
        }*/

        delegate void UIHandler(string str);
        private void RefreshPreview(string str)
        {
            PreviewResult.Text = presentIM.priorString + str + presentIM.unit;
            PreviewResult.ForeColor = Color.FromArgb(presentIM.color);
        }

        private void RefreshStatus(string str)
        {
            toolStripStatusLabel.Text = str;
        }

        private void InputData_Change(object sender, EventArgs e)
        {
            lock (signalLock)
            {
                presentIM.set(im_string.Text, im_tag.Text, im_format.Text, im_unit.Text, im_colorButton.ForeColor.ToArgb());
                refreshSignal = true;
            }
            RefreshPreview(" ");
        }

        private void inputButton_Click(object sender, EventArgs e)
        {
            try
            {
                inputIM();
                // save modification
                saveIMSetting();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }

            // clear selection and reset input button
            dataGridView_IM.ClearSelection();
            inputButton.Text = "Add";
        }

        private void inputIM()
        {
            // modify selected
            if (dataGridView_IM.SelectedCells.Count > 1)
            {
                foreach (DataGridViewTextBoxCell cell in dataGridView_IM.SelectedCells)
                {
                    setIMData(cell);
                }
            }
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
            RefreshPreview(" ");
        }

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

        private void dataGridView_IM_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            IMList.RemoveAt(e.RowIndex);
            saveIMSetting();
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
    }
}

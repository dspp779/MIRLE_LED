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
        // list of instant messages
        private List<IMSetting> IMList;
        // current message derived from textBoxes
        private InstantMessage presentIM = new InstantMessage();

        // signal represent if refreshment is needed or not
        private bool refreshSignal = false;
        // synchronization lock for refreshsignal
        private object signalLock = new object();
                
        #region -- initialization --

        public SettingForm()
        {
            InitializeComponent();
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            try
            {
                // load net setting
                loadConfig();
                // load alarm setting
                // load im
                IMList = LEDConfig.loadIMList();
                // init data grid view
                initDataGrid();
                toolStripStatusLabel.Text = "設定載入完成";
            }
            catch (Exception ex)
            {
                toolStripStatusLabel.Text = "設定載入失敗:" + ex.Message;
            }

            // initialization
            dataGridView_IM.ClearSelection();
            label_help.Text = "";

            // add event handler
            dataGridView_IM.SelectionChanged += new EventHandler(dataGridView_IM_SelectionChanged);

            // start eda data refresh worker
            ThreadPool.QueueUserWorkItem(new WaitCallback(MessageRefresher));
        }

        private void loadConfig()
        {
            // load config from ini file
            LEDConfig.loadConfig();
            // fill config to textBoxes
            textBox_IP.Text = LEDConfig.IpAddr;
            textBox_port.Text = LEDConfig.port.ToString();
            textBox_idcode.Text = LEDConfig.IDCode;
            textBox_timeout.Text = LEDConfig.timeout.ToString();
            // init CP5200 connection
            LEDConnection.InitComm();
        }

        private void initDataGrid()
        {
            // clear gridView
            dataGridView_IM.Rows.Clear();
            // pre-create empty row
            dataGridView_IM.Rows.Add(IMList.Count);

            // fill instant messages values
            int i = 0;
            foreach (IMSetting im in IMList)
            {
                setRow(i++, im.priorString, im.source, im.format, im.unit, Color.FromArgb(im.color));
            }
        }

        #endregion

        #region -- EDA message refresher --

        // data refresh worker
        private void MessageRefresher(object o)
        {
            while (true)
            {
                try
                {
                    // start eda data refresh worker
                    RefreshMessageWorker(null);
                    //ThreadPool.QueueUserWorkItem(new WaitCallback(RefreshMessageWorker));
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

        private void RefreshMessageWorker(object o)
        {
            try
            {
                /* IMSetting is extention of InstanMessage
                 * with the capability of field examination.
                 * */
                IMSetting ims = new IMSetting();
                ims.set(presentIM);

                refreshSignal = false;
                // retrieve value from ifix EDA
                float f;
                short nErr = Eda.GetOneFloat(ims.node, ims.tag, ims.field, out f);
                if (nErr != FixError.FE_OK)
                {
                    RefreshMessage("????");
                }
                else
                {
                    RefreshMessage(ims.getVal(f));
                }
            }
            catch (DllNotFoundException)
            {
                RefreshMessage("ifix連接失敗");
                RefreshStatus("請確認是否開啟ifix");
            }
            catch (FormatException ex)
            {
                RefreshMessage("格式錯誤" + ex.Message);
            }

        }

        private void RefreshMessage(string str)
        {
            if (Disposing || IsDisposed)
            {
                return;
            }

            //CP5200_SendText(PreviewResult.Text);

            // create temporal image
            TextImage tempImg = new TextImage(str, LEDConfig.defaultFont, Color.FromArgb(presentIM.color), Color.Black);

            try
            {
                // send temporal image
                LEDConnection.CP5200_SendImg(tempImg);
                //refresh preview area
                Invoke(new UIHandler(RefreshPreview), new Object[] { str });
            }
            catch (Exception ex)
            {
                //refresh preview area
                Invoke(new UIHandler(RefreshPreview), new Object[] { ex.Message });
            }
            finally
            {
                // delete temporal image
                tempImg.release();
            }
        }

        #endregion

        #region -- UI delegate --

        delegate void UIHandler(string str);
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
            // add im into list
            IMSetting ims = new IMSetting(str, tag, format, unit, color);
            IMList.Add(ims);
            // add im information to datagridview
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

            // set value to different column
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

        // color button event handler
        private void im_colorButton_Click(object sender, EventArgs e)
        {
            if (im_colorDialog.ShowDialog() == DialogResult.OK)
            {
                im_colorButton.ForeColor = im_colorDialog.Color;
            }
        }
        // color button change event
        private void im_colorButton_ForeColorChanged(object sender, EventArgs e)
        {
            presentIM.color = im_colorButton.ForeColor.ToArgb();
            Color c = im_colorButton.ForeColor;
            int luma = (int)(c.R * 0.3 + c.G * 0.59 + c.B * 0.11);
            im_colorButton.BackColor = luma < 128 ? Color.White : Color.Black;
        }

        //dataGridView handler
        private void dataGridView_IM_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            IMList.RemoveAt(e.RowIndex);
            LEDConfig.saveIMList(IMList);
        }
        // selection change reflect to textBoxes
        private void dataGridView_IM_SelectionChanged(object sender, EventArgs e)
        {
            inputButton.Text = "Set";

            int index = dataGridView_IM.CurrentRow.Index;
            lock (signalLock)
            {
                im_string.Text = IMList[index].priorString;
                im_format.Text = IMList[index].format;
                im_tag.Text = IMList[index].source;
                im_unit.Text = IMList[index].unit;
                im_colorButton.ForeColor = Color.FromArgb(IMList[index].color);
                refreshSignal = true;
            }
        }

        private void textBox_IpPress(object sender, KeyPressEventArgs e)
        {
            // ip allow digit and dot
            e.Handled = !(digitTextBoxTest(e.KeyChar) || e.KeyChar == '.');
        }
        private void textBox_DigitPress(object sender, KeyPressEventArgs e)
        {
            // set false if KeyChar is digit or control char; otherwise, true.
            e.Handled = !digitTextBoxTest(e.KeyChar);
        }
        private static bool digitTextBoxTest(char c)
        {
            // allow press of digit key or control key
            return Char.IsDigit(c) || Char.IsControl(c);
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            try
            {
                LEDConfig.IpAddr = textBox_IP.Text;
                LEDConfig.port = int.Parse(textBox_port.Text);
                LEDConfig.IDCode = textBox_idcode.Text;
                LEDConfig.timeout = int.Parse(textBox_timeout.Text);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // refresh textBox
                loadConfig();
            }
        }
        private void inputButton_Click(object sender, EventArgs e)
        {
            try
            {
                inputIM();
                // save modification
                LEDConfig.saveIMList(IMList);
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

        private void button_ping_Click(object sender, EventArgs e)
        {
            // To do : test LED connection 
        }

        #endregion

        #region -- textBox change event --
        private void im_string_TextChanged(object sender, EventArgs e)
        {
            presentIM.priorString = im_string.Text;
        }
        private void im_tag_TextChanged(object sender, EventArgs e)
        {
            presentIM.source = im_tag.Text;
        }
        private void im_format_SelectedIndexChanged(object sender, EventArgs e)
        {
            presentIM.format = im_format.Text;
        }
        private void im_unit_TextChanged(object sender, EventArgs e)
        {
            presentIM.unit = im_unit.Text;
        }
        #endregion

    }
}

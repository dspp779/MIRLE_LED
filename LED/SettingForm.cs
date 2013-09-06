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
using CPower_CSharp;
using System.Runtime.InteropServices;

namespace LED
{
    public partial class SettingForm : Form
    {
        // list of instant messages
        private List<IMSetting> IMList;
        // current message derived from textBoxes
        private InstantMessage presentIM = new InstantMessage("", "", "", "", 0);

        // Eda refresh worker
        EdaWorker EdaWorker;

        #region -- initialization --

        public SettingForm()
        {
            InitializeComponent();
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.CenterToScreen();
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

            this.EdaWorker = new EdaWorker(presentIM);
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

            // fill instant message values
            int i = 0;
            foreach (IMSetting im in IMList)
            {
                setRow(i++, im.priorString, im.source, im.format, im.unit, Color.FromArgb(im.color));
            }
        }

        #endregion

        #region -- UI delegate --

        // Here's delegate and refresh methods for UI modification
        delegate void UIHandler(string str);
        internal void RefreshPreview(string str)
        {
            if (InvokeRequired)
            {
                Invoke(new UIHandler(RefreshPreview), new Object[] { str });
            }
            else
            {
                // set preview string
                PreviewResult.Text = str;
                // set colot
                PreviewResult.ForeColor = Color.FromArgb(presentIM.color);
                this.Refresh();
            }
        }
        internal void RefreshStatus(string str)
        {
            if (InvokeRequired)
            {
                Invoke(new UIHandler((o) => toolStripStatusLabel.Text = str), new Object[] { null });
            }
            else
            {
                toolStripStatusLabel.Text = str;
            }

        }

        #endregion

        #region -- IM dataGridView manipulation --

        // message input method, include add and modify message
        private void inputIM()
        {
            // if there's multiplc cell selected, modify alll selected cells to corresponding textBoxes value
            if (dataGridView_IM.SelectedCells.Count > 1)
            {
                foreach (DataGridViewTextBoxCell cell in dataGridView_IM.SelectedCells)
                {
                    setIMData(cell);
                }
            }
            // if only one cell is selected, modify the row it belongs to
            else if (dataGridView_IM.SelectedCells.Count == 1)
            {
                setIM(dataGridView_IM.CurrentRow.Index, presentIM.priorString, presentIM.source,
                    presentIM.format, presentIM.unit, presentIM.color);
            }
            // if none selected, add new message
            else
            {
                // add im
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
            // set message value
            IMList[index].set(presentIM);
            // set dataGirdView row
            setRow(index, str, tag, format, unit, Color.FromArgb(color));
        }

        // dataGirdView refresher
        private void addRow(string str, string tag, string format, string unit, Color color)
        {
            // add an empty row
            dataGridView_IM.Rows.Add(1);
            // fill the row value
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

            // set column value to corresponding textBox
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
            // show color dialog to choose character color
            if (im_colorDialog.ShowDialog() == DialogResult.OK)
            {
                // set color button to the color user choose
                im_colorButton.ForeColor = im_colorDialog.Color;
            }
        }
        // color button change event
        private void im_colorButton_ForeColorChanged(object sender, EventArgs e)
        {
            // the fore color of color button is also the color of present message 
            presentIM.color = im_colorButton.ForeColor.ToArgb();

            /* compute the gray scale of the fore color and set background color
             * so that user can see color button clearly
             * */
            Color c = im_colorButton.ForeColor;
            // compute the lumanice
            int luma = (int)(c.R * 0.3 + c.G * 0.59 + c.B * 0.11);
            // set background color for the color button
            im_colorButton.BackColor = luma < 128 ? Color.White : Color.Black;
        }

        //dataGridView handler
        private void dataGridView_IM_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            // remove corresponding item in the list
            IMList.RemoveAt(e.RowIndex);
            // save the modification
            LEDConfig.saveIMList(IMList);
        }
        // selection change reflect to textBoxes
        private void dataGridView_IM_SelectionChanged(object sender, EventArgs e)
        {
            // any selection of cell in dataGridView are considered to modify
            inputButton.Text = "Set";
            // set textBoxes to the value of the selected message
            int index = dataGridView_IM.CurrentRow.Index;
            // lock signal to ensure refresh signal consistency

            im_colorButton.ForeColor = Color.FromArgb(IMList[index].color);
            im_string.Text = IMList[index].priorString;
            im_format.Text = IMList[index].format;
            im_tag.Text = IMList[index].source;
            im_unit.Text = IMList[index].unit;

            EdaWorker.refresh(1);
        }

        private void textBox_IpPress(object sender, KeyPressEventArgs e)
        {
            // ip input only allow digit and dot
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

        // save ini setting button clicked
        private void button_save_Click(object sender, EventArgs e)
        {
            try
            {
                // set config values
                LEDConfig.IpAddr = textBox_IP.Text;
                LEDConfig.port = int.Parse(textBox_port.Text);
                LEDConfig.IDCode = textBox_idcode.Text;
                LEDConfig.timeout = int.Parse(textBox_timeout.Text);
            }
            catch (FormatException ex)
            {
                // show exception message if there's any format exception
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // refresh config textBoxes
                loadConfig();
            }
        }
        private void inputButton_Click(object sender, EventArgs e)
        {
            try
            {
                // message input method, including add and modify.
                inputIM();
                // save modification made
                LEDConfig.saveIMList(IMList);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }

            // clear dataGridView selection
            dataGridView_IM.ClearSelection();
            // reset input button to "Add" since all selection are cleared
            inputButton.Text = "Add";
        }

        private void button_ping_Click(object sender, EventArgs e)
        {
            label_help.Text = "Connecting...";
            this.Refresh();
            // test LED connection 
            // init communication and refresh text of label_help as connection status 
            label_help.Text = LEDConnection.InitComm() ? "Connection Success" : "Connection Fail";
        }

        #endregion

        #region -- message textBox change event --
        /* here's the method of text changed event for message,
         * which is that set current value to presentIM.
         * */
        private void im_string_TextChanged(object sender, EventArgs e)
        {
            presentIM.priorString = im_string.Text;
            EdaWorker.refresh();
        }
        private void im_tag_TextChanged(object sender, EventArgs e)
        {
            presentIM.source = im_tag.Text;
            EdaWorker.refresh();
        }
        private void im_format_SelectedIndexChanged(object sender, EventArgs e)
        {
            presentIM.format = im_format.Text;
            EdaWorker.refresh();
        }
        private void im_unit_TextChanged(object sender, EventArgs e)
        {
            presentIM.unit = im_unit.Text;
            EdaWorker.refresh();
        }
        #endregion

    }
}

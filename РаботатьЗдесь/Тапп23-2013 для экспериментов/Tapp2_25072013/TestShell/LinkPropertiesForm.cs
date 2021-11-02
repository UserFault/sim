using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Mary;

namespace TestShell
{
    public partial class LinkPropertiesForm : Form
    {
        private MLinkTemplate m_link;
       
        public LinkPropertiesForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Link object
        /// </summary>
        public MLinkTemplate Link
        {
            get { return m_link; }
            set { m_link = value; }
        }


        

        private void LinkPropertiesForm_Load(object sender, EventArgs e)
        {
            ShowLink();
        }

        /// <summary>
        /// Show link in form
        /// </summary>
        private void ShowLink()
        {
            if (m_link.Axis == null) textBox_AxisId.Text = "";
            else textBox_AxisId.Text = NavigatorForm.getCellName(m_link.Axis);
            
            if (m_link.Description == null) textBox_Descr.Text = "";
            else textBox_Descr.Text = m_link.Description;

            if (m_link.downCellID == null) textBox_DownCellId.Text = "";
            else textBox_DownCellId.Text = NavigatorForm.getCellName(m_link.downCellID);

            if (m_link.isActive.HasValue) checkBox_Active.Checked = m_link.isActive.Value;
            else checkBox_Active.Checked = false;

            if (m_link.ServiceFlag.HasValue) textBox_ServiceFlag.Text = m_link.ServiceFlag.Value.ToString();
            else textBox_ServiceFlag.Text = "";

            if (m_link.State == null) textBox_StateId.Text = "";
            else textBox_StateId.Text = NavigatorForm.getCellName(m_link.State);

            if (m_link.tableId.HasValue) textBox_TableId.Text = m_link.tableId.Value.ToString();
            else textBox_TableId.Text = "";

            if (m_link.upCellID == null) textBox_UpCellID.Text = "";
            else textBox_UpCellID.Text = NavigatorForm.getCellName(m_link.upCellID);

            return;
        }

        private void checkData()
        {
            if (String.IsNullOrEmpty(textBox_AxisId.Text))
                throw new Exception("Invalid axis");
            if (String.IsNullOrEmpty(textBox_DownCellId.Text))
                throw new Exception("Invalid down cell");
            if (String.IsNullOrEmpty(textBox_StateId.Text))
                throw new Exception("Invalid state id");
            if (String.IsNullOrEmpty(textBox_ServiceFlag.Text))
                throw new Exception("Invalid service flag value");
            if (String.IsNullOrEmpty(textBox_TableId.Text))
                throw new Exception("Invalid table id"); 
            if (String.IsNullOrEmpty(textBox_UpCellID.Text))
                throw new Exception("Invalid up cell"); 
        }

        private void storeData()
        {
            checkData();
            m_link.Description = textBox_Descr.Text;
            m_link.isActive = checkBox_Active.Checked;
            m_link.ServiceFlag = Int32.Parse(textBox_ServiceFlag.Text);
            //m_link.tableId = Int32.Parse(textBox_TableId.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //start browser for downcell id cell
            MCell cell = NavigatorForm.SelectCell("Select downcell for link", m_link.downCellID);
            if (cell == null) return;
            m_link.downCell = cell;
            m_link.downCellID = cell.CellID;
            //ShowLink(); //refresh link view
            if (m_link.downCellID == null) textBox_DownCellId.Text = "";
            else textBox_DownCellId.Text = NavigatorForm.getCellName(m_link.downCellID);
        }

        private void button_UpCellIDSelect_Click(object sender, EventArgs e)
        {
            //start browser for upcell id cell
             MCell cell = NavigatorForm.SelectCell("Select upcell for link", m_link.upCellID);
            if (cell == null) return;
            m_link.upCell = cell;
            m_link.upCellID = cell.CellID;
            //ShowLink(); //refresh link view
            if (m_link.upCellID == null) textBox_UpCellID.Text = "";
            else textBox_UpCellID.Text = NavigatorForm.getCellName(m_link.upCellID);
        }

        private void button_AxisIDSelect_Click(object sender, EventArgs e)
        {
            //start browser for axis id cell
            MCell cell = NavigatorForm.SelectCell("Select Axis cell for link", m_link.Axis);
            if (cell == null) return;
            m_link.Axis = cell.CellID;
            //ShowLink(); //refresh link view
            if (m_link.Axis == null) textBox_AxisId.Text = "";
            else textBox_AxisId.Text = NavigatorForm.getCellName(m_link.Axis);
        }

        private void button_StateIdSelect_Click(object sender, EventArgs e)
        {
            //start browser for linkstate id cell
            MCell cell = NavigatorForm.SelectCell("Select State cell for link", m_link.State);
            if (cell == null) return;
            m_link.State = cell.CellID;
            //ShowLink(); //refresh link view
            if (m_link.State == null) textBox_StateId.Text = "";
            else textBox_StateId.Text = NavigatorForm.getCellName(m_link.State);
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            storeData();
            if (m_link.downCellID.isEqual(m_link.upCellID))
            {
                MessageBox.Show("Invalid link: selflinked cell", "Error");
                TestShellForm.playError();
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Show and edit link properties
        /// </summary>
        /// <param name="title">Form title</param>
        /// <param name="li">Link for editing</param>
        /// <returns></returns>
        public static bool CreateLink(string title, MLink li)
        {
            LinkPropertiesForm f = new LinkPropertiesForm();
            f.Text = title;
            f.Link = new MLinkTemplate(li);
            if (f.ShowDialog() == DialogResult.OK)
            {
                //copy output values to link 
                li.Axis = f.Link.Axis;
                li.Description = f.Link.Description;
                li.downCell = f.Link.downCell;
                li.downCellID = f.Link.downCellID;
                li.isActive = f.Link.isActive.Value;
                //li.ServiceFlag = f.Link.ServiceFlag.Value;  not change
                li.State = f.Link.State;
                //li.TableId = f.Link.tableId.Value;
                li.upCell = f.Link.upCell;
                li.upCellID = f.Link.upCellID;
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Show and edit link properties
        /// </summary>
        /// <param name="title">Form title</param>
        /// <param name="li">Link for editing</param>
        /// <returns></returns>
        public static bool EditLink(string title, MLink li)
        {
            LinkPropertiesForm f = new LinkPropertiesForm();
            f.Text = title;
            f.button_DownCellIDSelect.Enabled = false;
            f.button_UpCellIDSelect.Enabled = false;
            f.Link = new MLinkTemplate(li);
            if (f.ShowDialog() == DialogResult.OK)
            {
                //copy output values to link 
                li.Axis = f.Link.Axis;
                li.Description = f.Link.Description;
                //li.downCell = f.Link.downCell;
                //li.downCellID = f.Link.downCellID;
                li.isActive = f.Link.isActive.Value;
                //li.ServiceFlag = f.Link.ServiceFlag.Value;  not change
                li.State = f.Link.State;
                //li.TableId = f.Link.tableId.Value;
                //li.upCell = f.Link.upCell;
                //li.upCellID = f.Link.upCellID;
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Show and edit link properties
        /// </summary>
        /// <param name="title">Form title</param>
        /// <param name="li">Link for editing</param>
        /// <returns></returns>
        public static void ShowLink(string title, MLink li)
        {
            LinkPropertiesForm f = new LinkPropertiesForm();
            f.Text = title;
            f.Link = new MLinkTemplate(li);
            f.ShowDialog();
            f.Dispose();
            return;
        }

    }
}

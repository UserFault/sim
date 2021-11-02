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
    public partial class CellPropertiesForm : Form
    {
        private MCellTemplate m_cell;
        
        public CellPropertiesForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Cell
        /// </summary>
        public MCellTemplate Cell
        {
            get { return m_cell; }
            set { m_cell = value; }
        }

        private void ShowCell()
        {
            if (m_cell.CellID == null) textBox_CellID.Text = "";
            else textBox_CellID.Text = m_cell.CellID.ToString();

            if (m_cell.CreaTime.HasValue) textBox_CellCreated.Text = m_cell.CreaTime.Value.ToString();
            else textBox_CellCreated.Text = "";

            if (m_cell.Description == null) textBox_CellDescr.Text = "";
            else textBox_CellDescr.Text = m_cell.Description;

            if (m_cell.isActive.HasValue) checkBox_CellActive.Checked = m_cell.isActive.Value;
            else checkBox_CellActive.Checked = false;

            if (m_cell.ModiTime.HasValue) textBox_CellModified.Text = m_cell.ModiTime.Value.ToString();
            else textBox_CellModified.Text = "";

            if (m_cell.Name == null) textBox_CellName.Text = "";
            else textBox_CellName.Text = m_cell.Name;

            if (m_cell.ReadOnly.HasValue) checkBox_CellReadOnly.Checked = m_cell.ReadOnly.Value;
            else checkBox_CellReadOnly.Checked = false;

            if (m_cell.ServiceFlag.HasValue) textBox_CellServiceFlag.Text = m_cell.ServiceFlag.Value.ToString();
            else textBox_CellServiceFlag.Text = "";

            if (m_cell.State == null) textBox_CellStateId.Text = "";
            else textBox_CellStateId.Text = NavigatorForm.getCellName(m_cell.State);

            if (m_cell.TypeId == null) textBox_CellTypeId.Text = "";
            else textBox_CellTypeId.Text = NavigatorForm.getCellName(m_cell.TypeId);

            if (m_cell.Value == null) textBox_CellData.Text = "";
            else textBox_CellData.Text = m_cell.Value.Length.ToString() + " bytes";

            if (m_cell.ValueTypeId == null) textBox_CellDataTypeID.Text = "";
            else textBox_CellDataTypeID.Text = NavigatorForm.getCellName(m_cell.ValueTypeId);

        }

        private void checkData()
        {
            if (String.IsNullOrEmpty(textBox_CellDataTypeID.Text))
                throw new Exception("Invalid datatype");
            if (String.IsNullOrEmpty(textBox_CellName.Text))
                throw new Exception("Invalid cell name");
            if (String.IsNullOrEmpty(textBox_CellStateId.Text))
                throw new Exception("Invalid cell state");
            if (String.IsNullOrEmpty(textBox_CellTypeId.Text))
                throw new Exception("Invalid cell type");
            //if (String.IsNullOrEmpty())
            //    throw new Exception("Invalid ");
            //if (String.IsNullOrEmpty())
            //    throw new Exception("Invalid ");
        }

        private void SaveCell()
        {
            checkData();
            m_cell.Description = textBox_CellDescr.Text;
            m_cell.isActive = checkBox_CellActive.Checked;
            m_cell.Name = textBox_CellName.Text;
            m_cell.ReadOnly = checkBox_CellReadOnly.Checked;
            

        }

        private void button_CheckNameUnical_Click(object sender, EventArgs e)
        {
            string s = textBox_CellName.Text;
            if(String.IsNullOrEmpty(s)) return;
            if (NavigatorForm.Engine.CellIsUniqueName(s) == true)
            {
                MessageBox.Show(this, String.Format("No cells with name {0}!", s), "Unical check is success", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show(this, String.Format("Already exists one or more cells with name {0}!", s), "Unical check is fail", MessageBoxButtons.OK);
            }
            return;
            
        }

        private void button_TypeSelect_Click(object sender, EventArgs e)
        {
            //Запустить новый браузер для выбора ячейки типа ячейки
            MCell cell = NavigatorForm.SelectCell("Select Type cell for cell", m_cell.TypeId);
            if (cell == null) return;
            m_cell.TypeId = cell.CellID;
            //refresh link view
            if (m_cell.TypeId == null) textBox_CellTypeId.Text = "";
            else textBox_CellTypeId.Text = NavigatorForm.getCellName(m_cell.TypeId);
        }

        private void button_StateSelect_Click(object sender, EventArgs e)
        {
            //Запустить новый браузер для выбора ячейки состояния ячейки
            MCell cell = NavigatorForm.SelectCell("Select State cell for cell", m_cell.State);
            if (cell == null) return;
            m_cell.State = cell.CellID;
            //ShowCell(); //refresh link view
            if (m_cell.State == null) textBox_CellStateId.Text = "";
            else textBox_CellStateId.Text = NavigatorForm.getCellName(m_cell.State);
        }

        private void button_CellDataTypeSelect_Click(object sender, EventArgs e)
        {
            //Запустить новый браузер для выбора ячейки типа данных ячейки
            MCell cell = NavigatorForm.SelectCell("Select Datatype cell for cell", m_cell.ValueTypeId);
            if (cell == null) return;
            m_cell.ValueTypeId = cell.CellID;
            //ShowCell(); //refresh link view
            if (m_cell.ValueTypeId == null) textBox_CellDataTypeID.Text = "";
            else textBox_CellDataTypeID.Text = NavigatorForm.getCellName(m_cell.ValueTypeId);
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            SaveCell();
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void CellPropertiesForm_Load(object sender, EventArgs e)
        {
            //инициализировать форму в соответствии с причиной вызова
            ShowCell();
        }

        ///// <summary>
        ///// Test form for controls visibility
        ///// </summary>
        //public static void TestThisForm()
        //{
        //    CellPropertiesForm f = new CellPropertiesForm();
        //    f.textBox_CellCreated.Text = DateTime.Now.ToString();
        //    MID m = new MID(77);
        //    f.textBox_CellID.Text = m.ToString();
        //    f.textBox_CellName.Text = "test cell name";
        //    f.textBox_CellServiceFlag.Text = ((int)-1).ToString();
        //    f.Text = "Test cell form";
        //    f.ShowDialog();
        //}

        /// <summary>
        /// Edit cell
        /// </summary>
        /// <param name="title">Form title</param>
        /// <param name="cell">Cell for edit</param>
        /// <returns>Returns true if OK, false if Cancel</returns>
        public static bool EditCell(string title, MCell cell)
        {
            CellPropertiesForm f = new CellPropertiesForm();
            f.Text = title;
            f.Cell = new MCellTemplate(cell);
            if (f.ShowDialog() == DialogResult.OK)
            {
                //copy some data to cell and return true
                //cell.CellID = f.Cell.CellID; - do not copy
                //cell.CellMode = f.Cell.CellMode;
                //cell.CreaTime = f.Cell.CreaTime;
                cell.Description = f.Cell.Description;
                cell.isActive = f.Cell.isActive.Value;
                cell.Name = f.Cell.Name;
                cell.ReadOnly = f.Cell.ReadOnly.Value;
                cell.State = f.Cell.State;
                cell.TypeId = f.Cell.TypeId;
                //cell.Value = f.Cell.Value;
                cell.ValueTypeId = f.Cell.ValueTypeId;
                return true;

            }
            else return false;
        }



    }
}

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
    /// <summary>
    /// Форма показа всех ячеек структуры сущностей, находящихся сейчас в памяти.
    /// //TAG:RENEW-13112017
    /// todo: надо показывать также и ячейки, хранящиеся в БД. 
    /// </summary>
    public partial class ListAllCellsForm : Form
    {
        /// <summary>
        /// Выходной идентификатор выбранной ячейки
        /// </summary>
        internal MID m_Result;

        public ListAllCellsForm()
        {
            m_Result = null;
            InitializeComponent();
        }

        private void ListAllCellsForm_Load(object sender, EventArgs e)
        {


        }

        internal void FillListView(List<Mary.MCell> cells)
        {
            listView1.BeginUpdate();
            this.UseWaitCursor = true;
            //создать столбцы в листвиев
            FillListViewWithHeaders(this.listView1);

            bool Details = (this.listView1.View == View.Details);
            //вывести в листвиев список ячеек
            foreach (MCell cell in cells)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Tag = cell.CellID;
                lvi.ToolTipText = String.Format("Cell: {0}", cell.ToString());
                lvi.ImageIndex = 0;
                lvi.Text = cell.Name;

                if (Details)
                {
                    lvi.SubItems.Add(cell.CellID.ToString());
                    lvi.SubItems.Add(cell.Description);
                    lvi.SubItems.Add(NavigatorForm.getCellName(cell.TypeId));
                    lvi.SubItems.Add(NavigatorForm.getCellName(cell.State));
                    lvi.SubItems.Add(NavigatorForm.getCellName(cell.ValueTypeId));
                }

                listView1.Items.Add(lvi);
            }
            listView1.EndUpdate();
            this.UseWaitCursor = false;

            return;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count > 0)
                this.m_Result = (MID)listView1.SelectedItems[0].Tag;

            return;
        }



        internal static MID ShowAllCells(Dictionary<int, Mary.MCell>.ValueCollection valueCollection, IWin32Window owner)
        {
            //создать форму
            ListAllCellsForm f = new ListAllCellsForm();
            //создать столбцы в листвиев
            //вывести в листвиев список ячеек
            List<MCell> cells = new List<MCell>(valueCollection);
            f.FillListView(cells);
            DialogResult dr = f.ShowDialog(owner);
            MID result = null;
            if (dr == DialogResult.OK)
            {
                result = f.m_Result;
            }

            f = null;
            GC.Collect();
            //отдать выбранную ячейку - элемент списка
            return result;
        }

        /// <summary>
        /// Clear and fill specified ListView control with column headers for class details view
        /// </summary>
        /// <param name="lv"></param>
        private void FillListViewWithHeaders(ListView lv)
        {
            lv.Groups.Clear();//clear groups
            lv.Columns.Clear();//clear columns
            lv.Items.Clear();//clear items
            if (lv.View == View.Details)
            {
                lv.Columns.Add("Name", 100);
                lv.Columns.Add("ID", 100);
                lv.Columns.Add("Comment", 100);
                lv.Columns.Add("Type", 100);
                lv.Columns.Add("State", 100);
                lv.Columns.Add("Datatype", 100);
            }
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;

            this.Close();
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            this.m_Result = (MID)listView1.SelectedItems[0].Tag;
            DialogResult = DialogResult.OK;
            this.Close();
            return;
        }

    }
}

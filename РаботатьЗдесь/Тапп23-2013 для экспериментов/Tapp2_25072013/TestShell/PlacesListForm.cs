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
    public partial class PlacesListForm : Form
    {
        public PlacesListForm()
        {
            InitializeComponent();
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            //show selected item in textboxes
            ListViewItem lvi = listView1.SelectedItems[0];
            textBox_cellid.Text = lvi.SubItems[1].Text;
            textBox_descr.Text = lvi.SubItems[2].Text;
            textBox_Name.Text = lvi.Text;
        }

        private void button_navi_Click(object sender, EventArgs e)
        {
            //Запустить новый браузер для выбора ячейки типа ячейки
            MCell cell = NavigatorForm.SelectCell(String.Format("Select cell for {0} place", textBox_Name.Text), NavigatorForm.getPlaceCell("World"));
            if (cell == null) return;
            textBox_cellid.Text = cell.CellID.toU64().ToString();
        }



        private void button_remove_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox_Name.Text)) return;
            //else
            NavigatorForm.Places.Remove(textBox_Name.Text);
            ShowItems(); //update view
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox_cellid.Text))
            {
                MessageBox.Show("Invalid cell identifier!", "Error");
                return;
            }
            //check name unical
            if (String.IsNullOrEmpty(textBox_Name.Text))
            {
                MessageBox.Show("Invalid place name!", "Error");
                return;
            }
            if (NavigatorForm.Places.GetByName(textBox_Name.Text) != null)
            {
                MessageBox.Show(String.Format("Place {0} already exists!", textBox_Name.Text), "Error");
                return;
            }

            //add item to list
            NavigatorForm.Places.Add(ParseId(textBox_cellid.Text), MUtility.createXmlSafeString(textBox_Name.Text), MUtility.createXmlSafeString(textBox_descr.Text));
            ShowItems();
        }

        /// <summary>
        /// Parse id value string. Returns 0 if id is invalid
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private UInt64 ParseId(string s)
        {
            UInt64 val;
            try
            {
                val = UInt64.Parse(s);
            }
            catch (Exception e)
            {
                MessageBox.Show(String.Format("{0}: /n{1}", e.GetType().ToString(), e.Message), "Error");
                val = 0;
            }
            return val;

        }

        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PlacesListForm_Load(object sender, EventArgs e)
        {

            ShowItems();
        }

        /// <summary>
        /// Show listview items
        /// </summary>
        private void ShowItems()
        {
            PlacesCollection pc = NavigatorForm.Places;
            listView1.Items.Clear();
            
            foreach (PlaceRecord p in pc.Records)
            {
                ListViewItem lv0 = new ListViewItem(p.Name);
                lv0.SubItems.Add(p.ID.ToString());
                lv0.SubItems.Add(p.Description);
                listView1.Items.Add(lv0);
            }
        }

        private void PlacesListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //save places in file
            NavigatorForm.NaviSettings.Save(NavigatorForm.Engine.ProjectManager.ProjectFolderPath);
        }
        /// <summary>
        /// Show form for edit places
        /// </summary>
        public static void ShowPlacesForm()
        {
            PlacesListForm f = new PlacesListForm();
            f.ShowDialog();
            f.Dispose();
            return;
        }
    }
}

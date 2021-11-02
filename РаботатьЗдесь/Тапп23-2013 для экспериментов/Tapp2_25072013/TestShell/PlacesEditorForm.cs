using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TestShell
{
    public partial class PlacesEditorForm : Form
    {
        private Object m_showedItem;

        public PlacesEditorForm()
        {
            InitializeComponent();
        }

#region Handlers
        private void PlacesEditorForm_Load(object sender, EventArgs e)
        {
            ShowItems();
        }

        private void PlacesEditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //save places in file
            NavigatorForm.NaviSettings.PlacesEditorFormSize = this.Size;
            NavigatorForm.NaviSettings.Save(NavigatorForm.Engine.SolutionManager.SolutionFolderPath);
        }

        #region Context meny Root handlers
        private void newCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //show form of category properties for edit
            //create new category
            PlacesCollection p = new PlacesCollection("New category", "User-defined category");
            NavigatorForm.NaviSettings.Add(p);
            ShowItems();
        }

        private void collapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView_places.CollapseAll();
        }
        #endregion

        #region Context meny Category handlers
        private void newPlaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //show form for new place then add to current category

            TreeNode t = treeView_places.SelectedNode;
            if (t.Level != 1) return;
            PlacesCollection pc = (PlacesCollection)t.Tag;
            PlaceRecord p = new PlaceRecord(0, "New Place", "New user-defined place");
            pc.Add(p);
            ShowItems();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode t = treeView_places.SelectedNode;
            if (t.Level == 1) NavigatorForm.NaviSettings.CategoryList.Remove((PlacesCollection)t.Tag);
            ShowItems();
        }
        #endregion
        #region Context meny Place handlers
        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TreeNode t = treeView_places.SelectedNode;
            if (t.Level == 2)
            {
                PlacesCollection p = (PlacesCollection)t.Parent.Tag;
                p.Records.Remove((PlaceRecord)t.Tag);
            }
            ShowItems();
        }
        #endregion

#endregion

        private void ShowItems()
        {
            NavigatorSettings set = NavigatorForm.NaviSettings;
            this.UseWaitCursor = true;
            treeView_places.BeginUpdate();
            treeView_places.Nodes.Clear();

            //bold font for categories
            Font catf = new Font(treeView_places.Font, FontStyle.Bold);
            //main tree node
            TreeNode tm = new TreeNode("Places", 0, 0);
            tm.ToolTipText = "Places collection";
            treeView_places.Nodes.Add(tm);
            foreach (PlacesCollection pc in set.CategoryList)
            {
                TreeNode tc = new TreeNode(pc.CategoryName, 1, 1);
                tc.ToolTipText = pc.CategoryDescription;
                tc.NodeFont = catf;
                tc.Tag = pc;
                tm.Nodes.Add(tc);
                foreach (PlaceRecord p in pc.Records)
                {
                    TreeNode tp = new TreeNode(p.Name, 2, 2);
                    tp.ToolTipText = p.Description;
                    tp.Tag = p;
                    tc.Nodes.Add(tp);
                }
            }
            treeView_places.EndUpdate();
            this.UseWaitCursor = false;
            return;
        }


        /// <summary>
        /// Show form for edit places
        /// </summary>
        internal static void ShowEditor(Form navigatorForm)
        {
            PlacesEditorForm f = new PlacesEditorForm();
            f.Size = NavigatorForm.NaviSettings.PlacesEditorFormSize;
            f.ShowDialog(navigatorForm);
            f.Dispose();
            return;
        }

        private void treeView_places_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode t = e.Node;
            treeView_places.SelectedNode = t;

            ShowItem(t.Tag);
            Point pt = this.treeView_places.PointToScreen(e.Location);

          
            if (e.Button == MouseButtons.Right)
            {
                //show context menu

                switch (t.Level)
                {
                    case 0:
                        contextMenuStrip_Root.Show(pt);
                        break;
                    case 1:
                        contextMenuStrip_Category.Show(pt);
                        break;
                    case 2:
                        contextMenuStrip_Place.Show(pt);
                        break;
                    default:
                        break;
                }
            }
            return;
        }

        void ShowItem(Object item)
        {
            if (item == null) return;
            m_showedItem = item;
            Type ty = item.GetType();
            if (Type.Equals(ty, typeof(PlacesCollection)))
                ShowCategory((PlacesCollection)item);
            else if (Type.Equals(ty, typeof(PlaceRecord)))
                ShowPlace((PlaceRecord)item);
        }

        void ShowCategory(PlacesCollection p)
        {
            //hide id-related controls
            label_cellid.Visible = false;
            textBox_cellid.Visible = false;
            button_cell.Visible = false;
            //fill name and descr
            textBox_comment.Text = p.CategoryDescription;
            textBox_name.Text = p.CategoryName;
            return;       
        }

        void ShowPlace(PlaceRecord p)
        {
            //show id-related controls
            //hide id-related controls
            label_cellid.Visible = true;
            textBox_cellid.Visible = true;
            button_cell.Visible = true;
            //fill name and descr
            textBox_comment.Text = p.Description;
            textBox_name.Text = p.Name;
            textBox_cellid.Text = p.ID.ToString();
            return;  
        }


        private void button_cell_Click(object sender, EventArgs e)
        {
            //show new navigator form to select cell for id
            //Запустить новый браузер для выбора ячейки типа ячейки
            UInt64 i = parseId2(); //get id from textbox
            Mary.MCell cell;
            //if id valid, navigator starts from id
            //else navigator starts from World
            //TODO optimize code
            if (i == 0)//TAG:RENEW-13112017 - старый код не работает с временными ячейками совсем.
            {
                cell = NavigatorForm.getPlaceCell("World");
                if (cell == null)
                {
                    cell = NavigatorForm.Engine.CellGet("World"); //- в движке совсем нет функций поиска ячеек по имени.
                    if (cell == null)
                        cell = NavigatorForm.Engine.CellGetAny();
                }
                cell = NavigatorForm.SelectCell(String.Format("Select cell for {0} place", textBox_name.Text), cell);
            }
            else
                cell = NavigatorForm.SelectCell(String.Format("Select cell for {0} place", textBox_name.Text), Mary.MID.fromU64(i));
             //show cell
            if (cell == null) return;
            textBox_cellid.Text = cell.CellID.toU64().ToString();
        }

        private void button_reset_Click(object sender, EventArgs e)
        {
            //reload textboxes from item
            ShowItem(m_showedItem);
        }

        private void button_apply_Click(object sender, EventArgs e)
        {
            Type ty = this.m_showedItem.GetType();
            if (Type.Equals(ty, typeof(PlacesCollection)))
            {
                PlacesCollection p = (PlacesCollection)m_showedItem;
                p.CategoryName = textBox_name.Text;
                p.CategoryDescription = textBox_comment.Text;
            }
            else if (Type.Equals(ty, typeof(PlaceRecord)))
            {
                PlaceRecord r = (PlaceRecord)m_showedItem;
                r.Name = textBox_name.Text;
                r.Description = textBox_comment.Text;
                //parse id
                UInt64 res = parseId();
                r.ID = res;
                if (res == 0)
                {
                    //пользователь может намеренно обнулить идентификатор для места
                    //но если нет, его надо предупредить
                    MessageBox.Show(this, "Invalid identifier value!", "Error");
                }
                
             }
            //update treeview
            ShowItems();
            
             return;
        }

        private UInt64 parseId()
        {
            UInt64 res;
            if (UInt64.TryParse(textBox_cellid.Text, out res) == true)
                return res;
            else return 0;
        }

        private UInt64 parseId2()
        {
            int res;
            if (Int32.TryParse(textBox_cellid.Text, out res) == true)
            {
                UInt64 r = 0;
                r = r | ((UInt64)((UInt32)res));
                return r;
            }
            else return 0;
        }


    }
}

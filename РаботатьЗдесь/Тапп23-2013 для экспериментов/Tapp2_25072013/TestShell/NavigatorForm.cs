using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Mary;
using System.IO;

namespace TestShell
{

    public partial class NavigatorForm : Form
    {
        /// <summary>
        /// Current cell
        /// </summary>
        private MCell m_currentCell;
        /// <summary>
        /// Array of cells to return
        /// </summary>
        private MCell[] m_ReturnCells;

        /// <summary>
        /// Container reference
        /// </summary>
        private static MEngine m_container;
        /// <summary>
        /// Places collection reference
        /// </summary>
        private static NavigatorSettings m_Places;
        ///// <summary>
        ///// Sound player object for success sound confirmation play
        ///// </summary>
        //private System.Media.SoundPlayer m_player;

        

        public NavigatorForm()
        {
            InitializeComponent();
            //m_player = new System.Media.SoundPlayer();
        }

        /// <summary>
        /// Relayout form layouts
        /// </summary>
        private void initLayout()
        {
            //TODO: Наброски, недоделано.
            if (NavigatorForm.NaviSettings.NavigatorLayoutVertical)
            {
                //tableLayoutPanel1
                //panel1
                //listView_Down
                //listView_Up
                // 
                // tableLayoutPanel1
                // 
                this.tableLayoutPanel1.ColumnCount = 1;
                this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
                this.tableLayoutPanel1.Controls.Add(this.listView_Down, 0, 2);
                this.tableLayoutPanel1.Controls.Add(this.listView_Up, 0, 0);
                this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
                this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
                this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
                this.tableLayoutPanel1.Name = "tableLayoutPanel1";
                this.tableLayoutPanel1.RowCount = 3;
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
                this.tableLayoutPanel1.Size = new System.Drawing.Size(593, 448);
                this.tableLayoutPanel1.TabIndex = 0;
                // 
                // listView_Down
                // 
                this.listView_Down.Dock = System.Windows.Forms.DockStyle.Fill;
                //this.listView_Down.Location = new System.Drawing.Point(3, 302);
                //this.listView_Down.Size = new System.Drawing.Size(587, 143);
                // 
                // listView_Up
                // 
                this.listView_Up.Dock = System.Windows.Forms.DockStyle.Fill;
                //this.listView_Up.Location = new System.Drawing.Point(3, 3);
                //this.listView_Up.Size = new System.Drawing.Size(587, 143);

                // 
                // panel1
                // 
                this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
                this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                this.panel1.Location = new System.Drawing.Point(3, 152);
                this.panel1.Size = new System.Drawing.Size(587, 144);

            }
            else
            {
                // 
                // tableLayoutPanel1
                // 
                this.tableLayoutPanel1.RowCount = 1;
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
                this.tableLayoutPanel1.Controls.Add(this.listView_Down, 0, 2);
                this.tableLayoutPanel1.Controls.Add(this.listView_Up, 0, 0);
                this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
                this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
                this.tableLayoutPanel1.ColumnCount = 3;
                this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
                this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
                this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
                this.tableLayoutPanel1.Size = new System.Drawing.Size(593, 448);
                this.tableLayoutPanel1.TabIndex = 0;
                // 
                // listView_Down
                // 
                this.listView_Down.Dock = System.Windows.Forms.DockStyle.Fill;
                //this.listView_Down.Location = new System.Drawing.Point(3, 302);
                //this.listView_Down.Size = new System.Drawing.Size(587, 143);
                // 
                // listView_Up
                // 
                this.listView_Up.Dock = System.Windows.Forms.DockStyle.Fill;
                //this.listView_Up.Location = new System.Drawing.Point(3, 3);
                //this.listView_Up.Size = new System.Drawing.Size(587, 143);

                // 
                // panel1
                // 
                this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
                this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                this.panel1.Location = new System.Drawing.Point(3, 152);
                this.panel1.Size = new System.Drawing.Size(587, 144);

            }
        }

        public NavigatorForm(string title, MCell cell)
        {
            InitializeComponent();
            //m_player = new System.Media.SoundPlayer();
            this.Text = title;
            this.m_currentCell = cell;

        }
        /// <summary>
        /// Current cell in this form
        /// </summary>
        public MCell CurrentCell
        {
            get { return m_currentCell; }
            set { m_currentCell = value; }
        }

        /// <summary>
        /// Get array of cells to return
        /// </summary>
        public MCell[] ReturnCells
        {
            get { return m_ReturnCells; }
        }
        /// <summary>
        /// Static reference to container
        /// </summary>
        public static MEngine Engine
        {
            get { return m_container; }
            set { m_container = value; }
        }
        /// <summary>
        /// Static reference to places collection
        /// </summary>
        public static NavigatorSettings NaviSettings
        {
            get { return m_Places; }
            set { m_Places = value; }
        }

        #region Event handlers

        #region Listview handlers
        private void listView_Up_ItemActivate(object sender, EventArgs e)
        {
            MID id = MID.fromU64((ulong) listView_Up.SelectedItems[0].Tag);
            ShowCell(id);
        }

        private void listView_Down_ItemActivate(object sender, EventArgs e)
        {
            MID id = MID.fromU64((ulong) listView_Down.SelectedItems[0].Tag);
            ShowCell(id);
        }
        #endregion

        #region Form hanlders
        private void NavigatorForm_Load(object sender, EventArgs e)
        {

                ShowCell(m_currentCell);
        }

        #endregion

        #region ListView modes
        private void largeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView_Up.View = View.LargeIcon;
            listView_Down.View = View.LargeIcon;
            ShowCell(m_currentCell);
        }

        private void smallIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView_Up.View = View.SmallIcon;
            listView_Down.View = View.SmallIcon;
            ShowCell(m_currentCell);
        }

        private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView_Up.View = View.Details;
            listView_Down.View = View.Details;
            ShowCell(m_currentCell);
        }

        private void listToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView_Up.View = View.List;
            listView_Down.View = View.List;
            ShowCell(m_currentCell);
        }

        private void tileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView_Up.View = View.Tile;
            listView_Down.View = View.Tile;
            ShowCell(m_currentCell);
        }
        #endregion

        #region main menu and toolbar handlers
        /// <summary>
        /// Menu Action-Cell-Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //создать ячейку           
            MCell c = m_container.CellCreate(MCellMode.Normal);
            if (c == null) return;
            //set default cell identifiers
            c.State = NavigatorForm.getPlaceCellId("DefaultCellState");
            c.TypeId = NavigatorForm.getPlaceCellId("DefaultCellType");
            c.ValueTypeId = NavigatorForm.getPlaceCellId("DefaultCellDatatype");
            //показать форму ячейки
            if (CellPropertiesForm.EditCell("Create cell", c) == true)
            {       //сделать новую ячейку текущей
                m_currentCell = c;
                ShowCell(m_currentCell);
            }
            else c.S1_Delete();
        }



        /// <summary>
        /// Menu Action-Cell-Edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CellPropertiesForm.EditCell("Edit cell", m_currentCell);
            ShowCell(m_currentCell);
        }

        /// <summary>
        /// Menu Action-Link-Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MLink li = new MLink();
            //set cells as current cell for simplify user work
            li.downCellID = m_currentCell.CellID;
            li.upCellID = m_currentCell.CellID;
            //set defaults
            li.Axis = NavigatorForm.getPlaceCellId("DefaultLinkAxis");
            li.State = NavigatorForm.getPlaceCellId("DefaultLinkState");
            //show link editor
            bool res = LinkPropertiesForm.CreateLink("Create link", li);
            if (res == true)
            {
                MCell cup = m_container.CellGet(li.upCellID);
                MCell cdn = m_container.CellGet(li.downCellID);
                MLink link = cup.S1_createLink(li.Axis, MAxisDirection.Down, cdn);
                link.isActive = li.isActive;
                link.State = li.State;
                link.upCell = cup;
                link.downCell = cdn;
            }

            ShowCell(m_currentCell);
            return;

        }

        /// <summary>
        /// Menu Action-Container-Edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //Показать форму контейнера
            //после закрытия формы проверить и применить изменения
            MessageBox.Show("Not implemented!", "message");
        }

        /// <summary>
        /// Edit places
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            PlacesEditorForm.ShowEditor(this);
        }

        private void placesToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            //add items to places menu if any changes exists

            if (placesToolStripMenuItem.DropDownItems.Count != NavigatorForm.NaviSettings.CategoryList.Count + 2)
            {
                placesToolStripMenuItem.DropDownItems.Clear();
                //add category items
                foreach (PlacesCollection pc in NavigatorForm.NaviSettings.CategoryList)
                {
                    ToolStripMenuItem tc = new ToolStripMenuItem();
                    
                    tc.Text = pc.CategoryName;
                    tc.ToolTipText = pc.CategoryDescription;
                    placesToolStripMenuItem.DropDownItems.Add(tc);
                    //add place items
                    foreach (PlaceRecord p in pc.Records)
                    {
                        ToolStripMenuItem t = new ToolStripMenuItem();
                        t.Text = p.Name;
                        t.ToolTipText = p.Description;
                        t.Tag = p.ID;
                        t.Click += new EventHandler(t_Click);
                        tc.DropDownItems.Add(t);
                    }
                }
                //add Edit items
                placesToolStripMenuItem.DropDownItems.Add(this.toolStripSeparator2);
                placesToolStripMenuItem.DropDownItems.Add(this.editToolStripMenuItem3);
                return;
            }
        }
        /// <summary>
        /// Places item clicked in Places menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void t_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem t = (ToolStripMenuItem)sender;
            //move to id
            MID id = MID.fromU64((UInt64)t.Tag);
            ShowCell(id);
        }

        private void returnCurrentCellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //return current cell
            DialogResult = DialogResult.OK;
            this.Close();
            TestShellForm.playBeep();
        }

#endregion




        #region Listview context menu handlers
        private void contextMenuStrip_Up_Opening(object sender, CancelEventArgs e)
        {
            //добавить в пункт меню все связи с кликнутой ячейкой
            //установить обработчик, в котором пользователь мог бы редактировать параметры связи. 

            //editLinkToolStripMenuItem.DropDownItems
            if (listView_Up.SelectedItems.Count == 0) return;
            //get selected cell
            MID cellId = MID.fromU64((ulong)listView_Up.SelectedItems[0].Tag);
            //create template and search links between cells
            MLinkTemplate tmp = new MLinkTemplate();
            tmp.downCellID = m_currentCell.CellID;
            tmp.upCellID = cellId;
            MLinkCollection coll = m_currentCell.Links.getLinks(tmp);
            //list all links in context menu
            editLinkToolStripMenuItem.DropDownItems.Clear();
            if (coll.Items.Count > 0)
            {
                foreach (MLink li in coll.Items)
                {
                    string sl = LinkToString(li); //get string representation of link
                    ToolStripMenuItem t = new ToolStripMenuItem();
                    t.Click += new System.EventHandler(noLinksToolStripMenuItem_Click);
                    t.Tag = li;
                    t.Text = sl;
                    editLinkToolStripMenuItem.DropDownItems.Add(t);
                }
            }
            else editLinkToolStripMenuItem.DropDownItems.Add("No links");
            //
            return;
        }
        private void contextMenuStrip_Down_Opening(object sender, CancelEventArgs e)
        {
            //добавить в пункт меню все связи с кликнутой ячейкой
            //установить обработчик, в котором пользователь мог бы редактировать параметры связи. 

            //editLinkToolStripMenuItem.DropDownItems
            if (listView_Down.SelectedItems.Count == 0) return;
            //get selected cell
            MID cellId = MID.fromU64((ulong)listView_Down.SelectedItems[0].Tag);
            //create template and search links between cells
            MLinkTemplate tmp = new MLinkTemplate();
            tmp.upCellID = m_currentCell.CellID;
            tmp.downCellID = cellId;
            MLinkCollection coll = m_currentCell.Links.getLinks(tmp);
            //list all links in context menu
            this.toolStripMenuItem_Down.DropDownItems.Clear();
            if (coll.Items.Count > 0)
            {
                foreach (MLink li in coll.Items)
                {
                    string sl = LinkToString(li); //get string representation of link
                    ToolStripMenuItem t = new ToolStripMenuItem();
                    t.Click += new System.EventHandler(noLinksToolStripMenuItem_Click);
                    t.Tag = li;
                    t.Text = sl;
                    toolStripMenuItem_Down.DropDownItems.Add(t);
                }
            }
            else toolStripMenuItem_Down.DropDownItems.Add("No links");
            //
            return;
        }
        private void noLinksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem t = (ToolStripMenuItem)sender;
            MLink li = (MLink)t.Tag;
            //show link properties form for edit link
            LinkPropertiesForm.EditLink("Edit link", li);
            //update view
            ShowCell(m_currentCell);
        }
        #endregion









        #endregion



        private void ShowCell(MID cellId)
        {
            MCell cell = m_container.CellGet(cellId);
            //create headers for Detail mode
            //create listview items
            //setup menus and toolbars
            m_currentCell = cell;
            //show cell data
            ShowCell(m_currentCell);
            //play sound
            TestShellForm.playClick();
        }

        private void ShowCell(MCell cell)
        {
            //if cell ref is null, show special version
            if (cell == null)
            {
                ShowCell();
                return;
            }
            
            //show progressbar in status bar
            int nums = cell.Links.Items.Count;
            if(nums > 256) toolStripProgressBar1.Visible = true;
            int step = nums / 16;
            int counter = 0;
            toolStripProgressBar1.Value = 0;
            toolStripProgressBar1.Maximum = 16;
            toolStripProgressBar1.Minimum = 0;
            this.StatusLabel_status.Text = "Clearing views...";
            Application.DoEvents();
            this.UseWaitCursor = true;
            listView_Up.BeginUpdate();
            listView_Down.BeginUpdate();
            //store grid width
            StoreGridWidth();
            //clear groups, items, columns, add columns
            FillListViewWithHeaders(listView_Up);
            FillListViewWithHeaders(listView_Down);
            //load grid width
            LoadGridWidth();
            
            //show linked cells
            ListView lv;
            //groups
            Dictionary<UInt64, ListViewGroup> gdiUp = new Dictionary<ulong, ListViewGroup>();
            Dictionary<UInt64, ListViewGroup> gdiDn = new Dictionary<ulong, ListViewGroup>();
            Dictionary<UInt64, ListViewGroup> gdi;
            //flag for optimizing speed - create subitems in Detail viewmode only.
            bool Details = (listView_Down.View == View.Details); 
            foreach (MLink li in cell.Links.Items)
            {
                if (li.getAxisDirection(cell.CellID) == MAxisDirection.Up)
                {
                    lv = listView_Up;
                    gdi = gdiUp;
                }
                else
                {
                    lv = listView_Down;
                    gdi = gdiDn;
                }
                //groups
                ListViewGroup lvg;
                UInt64 id = li.Axis.toU64();
                if (!gdi.ContainsKey(id))
                {
                    lvg = new ListViewGroup(getCellName(li.Axis));
                    gdi.Add(id, lvg);
                    lv.Groups.Add(lvg);
                }
                else lvg = gdi[id];

                //create items
                ListViewItem lvi = new ListViewItem();
                lvi.Group = lvg;
                MID linkedCellId = li.getLinkedCellId(cell.CellID);
                lvi.Tag = linkedCellId.toU64();
                MCell linkedCell = m_container.CellGet(linkedCellId);
                //name
                lvi.ToolTipText = String.Format("Link: {0}\nCell: {1}", li.ToString(), linkedCell.ToString());
                lvi.ImageIndex = 0;
                //lvi.Name = linkedCell.Name;
                lvi.Text = linkedCell.Name;
                lvi.ForeColor = getLinkColor(li);
                //optimize speed
                if (Details)
                {
                    lvi.SubItems.Add(linkedCell.CellID.ToString());
                    lvi.SubItems.Add(linkedCell.Description);
                    lvi.SubItems.Add(getCellName(linkedCell.TypeId));
                    lvi.SubItems.Add(getCellName(linkedCell.State));
                    lvi.SubItems.Add(getCellName(linkedCell.ValueTypeId));
                }

                lv.Items.Add(lvi);
                //update progressbar
                counter++;
                if (counter > step)
                {
                    counter = 0;
                    toolStripProgressBar1.Value += 1;
                    this.StatusLabel_status.Text = String.Format("Loading {0} items...", nums);
                    Application.DoEvents();
                }
            }

            listView_Up.EndUpdate();
            listView_Down.EndUpdate();
            this.UseWaitCursor = false;
            //setup middle panel
            label_CellID.Text = cell.CellID.ToString();
            label_CellName.Text = cell.Name;
            textBox_Datatype.Text = getCellName(cell.ValueTypeId);
            textBox_Descr.Text = cell.Description;
            textBox_State.Text = getCellName(cell.State);
            textBox_Type.Text = getCellName(cell.TypeId);
            checkBox_Active.Checked = cell.isActive;
            checkBox_ReadOnly.Checked = cell.ReadOnly;
            //setup menus

            toolStripProgressBar1.Visible = false;
            this.StatusLabel_status.Text = "Loading completed";
            //play sound
            TestShellForm.playClick();
        }

        /// <summary>
        /// Show form for null cell
        /// </summary>
        /// <returns></returns>
        private void ShowCell()
        {
            this.StatusLabel_status.Text = "Cell not exists";
            this.UseWaitCursor = true;
            listView_Down.BeginUpdate();
            listView_Up.BeginUpdate();
            //store grid width
            StoreGridWidth();
            //clear groups, items, columns, add columns
            FillListViewWithHeaders(listView_Up);
            FillListViewWithHeaders(listView_Down);
            //load grid width
            LoadGridWidth();
            listView_Up.EndUpdate();
            listView_Down.EndUpdate();
            this.UseWaitCursor = false;
            //setup middle panel
            label_CellID.Text = "Null";
            label_CellName.Text = "Warning: Null cell reference!";
            textBox_Datatype.Text = "";
            textBox_Descr.Text = "Cell not exists";
            textBox_State.Text = "";
            textBox_Type.Text = "";
            checkBox_Active.Checked = false;
            checkBox_ReadOnly.Checked = false;
            //play error sound 
            TestShellForm.playError();
        }

        private void LoadGridWidth()
        {
            
        }

        private void StoreGridWidth()
        {
            
        }

        /// <summary>
        /// Get name by ID
        /// </summary>
        /// <param name="cellId"></param>
        /// <returns></returns>
        public static string getCellName(MID cellId)
        {
            MCell c = m_container.CellGet(cellId);
            if (c != null) return c.Name;
            else return cellId.ToString();
        }

        /// <summary>
        /// Get link color: Active link is black; Inactive link is gray;
        /// </summary>
        /// <param name="li"></param>
        /// <returns></returns>
        private Color getLinkColor(MLink li)
        {
            if (li.isActive) return Color.Black;
            else return Color.Gray;
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

        ///// <summary>
        ///// Play sound file Click - Short operation finished (Refresh or soon...)
        ///// </summary>
        //private void playClick()
        //{
        //    m_player.Stream = Properties.Resources.click;
        //    m_player.Play();
        //}

        ///// <summary>
        ///// Play sound file Ding - Long operation finished
        ///// </summary>
        //private void playFinish()
        //{
        //    m_player.Stream = Properties.Resources.ding;
        //    m_player.Play();
        //}



        /// <summary>
        /// Start navigator - for first form only
        /// </summary>
        /// <param name="eng"></param>
        internal static void LaunchNavigator(MEngine eng)
        {
            MID startid;
            //load places file if exists
            NavigatorSettings pc = NavigatorSettings.Load(eng.SolutionManager.SolutionFolderPath);
            //set up static ref
            m_Places = pc;
            m_container = eng;
            //get startup cell id from system category
            //TODO: optimize here
            PlaceRecord p = pc.GetCategorySystem().GetByName("World");
            MCell cc;
            if (p != null)
            {
                startid = MID.fromU64(p.ID);
                //check cell exists
                cc = eng.CellGet(startid);
                if (cc == null)
                    cc = eng.CellGetAny();
                //check any cells exists
                if (cc == null) startid = MID.InvalidID;
                else startid = cc.CellID;
            }
            else
            {
                cc = eng.CellGetAny();
                //check any cells exists
                if (cc == null) startid = MID.InvalidID;
                else startid = cc.CellID;
            }
            //load form
            NavigatorForm f = new NavigatorForm();
            f.CurrentCell = eng.CellGet(startid);

            //show form
            f.Text = "Free navigation"; // navigation job title
            f.ShowDialog();
        }
        /// <summary>
        /// Return place cell or any cell if place not found
        /// </summary>
        /// <param name="placeName"></param>
        /// <returns></returns>
        internal static MCell getPlaceCell(string placeName)
        {
            PlaceRecord p = m_Places.GetByName(placeName);
            if (p != null)
            {
                MID startid = MID.fromU64(p.ID);
                return m_container.CellGet(startid);
            }
            else
                return m_container.CellGetAny();
        }
        /// <summary>
        /// Return ID of place cell or any cell if place not found or invalid cell ID if any cells not found
        /// </summary>
        /// <param name="placeName"></param>
        /// <returns></returns>
        internal static MID getPlaceCellId(string placeName)
        {
            MCell c = getPlaceCell(placeName);
            if (c == null) return MID.InvalidID;
            else return c.CellID;
        }

        /// <summary>
        /// Create string representation of link
        /// </summary>
        /// <param name="li"></param>
        /// <returns></returns>
        private string LinkToString(MLink li)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} - {1} Ax:{2}", li.upCellID.ToString(), li.downCellID.ToString(), getCellName(li.Axis));
            if(li.isActive) sb.Append(" A! "); else sb.Append(" D! ");
            sb.AppendFormat("St:{0} #{1}", getCellName(li.State), li.TableId);
            return sb.ToString();
        }



        /// <summary>
        /// Show navigator and return current cell
        /// </summary>
        /// <param name="title">Navigator title</param>
        /// <param name="id">Start cell id</param>
        /// <returns></returns>
        internal static MCell SelectCell(string title, MID id)
        {
            MCell c = m_container.CellGet(id);
            return SelectCell(title, c);
        }

        /// <summary>
        /// Show navigator and return current cell
        /// </summary>
        /// <param name="title">Navigator title</param>
        /// <param name="place">Start place name</param>
        /// <returns></returns>
        internal static MCell SelectCell(string title, string place)
        {
            MCell c = NavigatorForm.getPlaceCell(place);
            return SelectCell(title, c);
        }

        /// <summary>
        /// Show navigator and return current cell
        /// </summary>
        /// <param name="title">Navigator title</param>
        /// <param name="cell">Start cell</param>
        /// <returns></returns>
        internal static MCell SelectCell(string title, MCell cell)
        {
            NavigatorForm f = new NavigatorForm(title, cell);
            MCell c;
            if (f.ShowDialog() == DialogResult.OK)
                c = f.CurrentCell;
            else c = null;
            f.Dispose();
            return c;
        }

        /// <summary>
        /// Menu View - List of all cells
        /// TAG:RENEW-13112017
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listOfAllCellsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //показать все ячейки контейнера общим списком (в детайл лист виев или в обычном, с панелью свойств)
            MID cellid = ListAllCellsForm.ShowAllCells(m_container.Cells.Items, this);
            //если возвращен null, не изменяем ничего в родительском навигаторе.
            if (cellid == null)
                return;
            //иначе - переводим навигатор на новую ячейку.
            this.ShowCell(cellid);
            return;
        }


    }
}

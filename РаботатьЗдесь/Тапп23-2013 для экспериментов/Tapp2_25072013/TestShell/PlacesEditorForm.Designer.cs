namespace TestShell
{
    partial class PlacesEditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlacesEditorForm));
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Node3");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Node4");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Node1", new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Node2");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Node0", new System.Windows.Forms.TreeNode[] {
            treeNode8,
            treeNode9});
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.treeView_places = new System.Windows.Forms.TreeView();
            this.contextMenuStrip_Root = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newCategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_Category = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newPlaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_Place = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_cellid = new System.Windows.Forms.Label();
            this.textBox_cellid = new System.Windows.Forms.TextBox();
            this.button_cell = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_comment = new System.Windows.Forms.TextBox();
            this.button_apply = new System.Windows.Forms.Button();
            this.button_reset = new System.Windows.Forms.Button();
            this.contextMenuStrip_Root.SuspendLayout();
            this.contextMenuStrip_Category.SuspendLayout();
            this.contextMenuStrip_Place.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "services.ico");
            this.imageList1.Images.SetKeyName(1, "openfolderHS.png");
            this.imageList1.Images.SetKeyName(2, "text.ico");
            // 
            // treeView_places
            // 
            this.treeView_places.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_places.ImageIndex = 0;
            this.treeView_places.ImageList = this.imageList1;
            this.treeView_places.Location = new System.Drawing.Point(3, 3);
            this.treeView_places.Name = "treeView_places";
            treeNode6.ImageIndex = 2;
            treeNode6.Name = "Node3";
            treeNode6.Text = "Node3";
            treeNode7.ImageIndex = 2;
            treeNode7.Name = "Node4";
            treeNode7.Text = "Node4";
            treeNode8.ImageIndex = 1;
            treeNode8.Name = "Node1";
            treeNode8.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            treeNode8.Text = "Node1";
            treeNode9.ImageIndex = 1;
            treeNode9.Name = "Node2";
            treeNode9.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            treeNode9.Text = "Node2";
            treeNode10.Name = "Node0";
            treeNode10.Text = "Node0";
            this.treeView_places.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode10});
            this.treeView_places.SelectedImageIndex = 0;
            this.treeView_places.ShowNodeToolTips = true;
            this.treeView_places.Size = new System.Drawing.Size(122, 221);
            this.treeView_places.TabIndex = 0;
            this.treeView_places.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_places_NodeMouseClick);
            // 
            // contextMenuStrip_Root
            // 
            this.contextMenuStrip_Root.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newCategoryToolStripMenuItem,
            this.collapseAllToolStripMenuItem});
            this.contextMenuStrip_Root.Name = "contextMenuStrip1";
            this.contextMenuStrip_Root.Size = new System.Drawing.Size(153, 48);
            // 
            // newCategoryToolStripMenuItem
            // 
            this.newCategoryToolStripMenuItem.Name = "newCategoryToolStripMenuItem";
            this.newCategoryToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newCategoryToolStripMenuItem.Text = "New category";
            this.newCategoryToolStripMenuItem.Click += new System.EventHandler(this.newCategoryToolStripMenuItem_Click);
            // 
            // collapseAllToolStripMenuItem
            // 
            this.collapseAllToolStripMenuItem.Name = "collapseAllToolStripMenuItem";
            this.collapseAllToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.collapseAllToolStripMenuItem.Text = "Collapse all";
            this.collapseAllToolStripMenuItem.Click += new System.EventHandler(this.collapseAllToolStripMenuItem_Click);
            // 
            // contextMenuStrip_Category
            // 
            this.contextMenuStrip_Category.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newPlaceToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenuStrip_Category.Name = "contextMenuStrip_Category";
            this.contextMenuStrip_Category.Size = new System.Drawing.Size(147, 48);
            // 
            // newPlaceToolStripMenuItem
            // 
            this.newPlaceToolStripMenuItem.Name = "newPlaceToolStripMenuItem";
            this.newPlaceToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.newPlaceToolStripMenuItem.Text = "New place...";
            this.newPlaceToolStripMenuItem.Click += new System.EventHandler(this.newPlaceToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // contextMenuStrip_Place
            // 
            this.contextMenuStrip_Place.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem1});
            this.contextMenuStrip_Place.Name = "contextMenuStrip_Place";
            this.contextMenuStrip_Place.Size = new System.Drawing.Size(117, 26);
            // 
            // deleteToolStripMenuItem1
            // 
            this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(116, 22);
            this.deleteToolStripMenuItem1.Text = "Delete";
            this.deleteToolStripMenuItem1.Click += new System.EventHandler(this.deleteToolStripMenuItem1_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 190F));
            this.tableLayoutPanel1.Controls.Add(this.treeView_places, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(318, 227);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_reset);
            this.panel1.Controls.Add(this.button_apply);
            this.panel1.Controls.Add(this.textBox_comment);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBox_name);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button_cell);
            this.panel1.Controls.Add(this.textBox_cellid);
            this.panel1.Controls.Add(this.label_cellid);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(131, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(184, 221);
            this.panel1.TabIndex = 1;
            // 
            // label_cellid
            // 
            this.label_cellid.AutoSize = true;
            this.label_cellid.Location = new System.Drawing.Point(5, 8);
            this.label_cellid.Name = "label_cellid";
            this.label_cellid.Size = new System.Drawing.Size(41, 13);
            this.label_cellid.TabIndex = 0;
            this.label_cellid.Text = "Cell ID:";
            this.label_cellid.Visible = false;
            // 
            // textBox_cellid
            // 
            this.textBox_cellid.Location = new System.Drawing.Point(45, 4);
            this.textBox_cellid.Name = "textBox_cellid";
            this.textBox_cellid.Size = new System.Drawing.Size(100, 20);
            this.textBox_cellid.TabIndex = 1;
            this.textBox_cellid.Visible = false;
            // 
            // button_cell
            // 
            this.button_cell.Location = new System.Drawing.Point(151, 3);
            this.button_cell.Name = "button_cell";
            this.button_cell.Size = new System.Drawing.Size(30, 23);
            this.button_cell.TabIndex = 2;
            this.button_cell.Text = "..";
            this.button_cell.UseVisualStyleBackColor = true;
            this.button_cell.Visible = false;
            this.button_cell.Click += new System.EventHandler(this.button_cell_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Name:";
            // 
            // textBox_name
            // 
            this.textBox_name.Location = new System.Drawing.Point(5, 47);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(173, 20);
            this.textBox_name.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Description:";
            // 
            // textBox_comment
            // 
            this.textBox_comment.Location = new System.Drawing.Point(5, 91);
            this.textBox_comment.Multiline = true;
            this.textBox_comment.Name = "textBox_comment";
            this.textBox_comment.Size = new System.Drawing.Size(173, 94);
            this.textBox_comment.TabIndex = 6;
            // 
            // button_apply
            // 
            this.button_apply.Location = new System.Drawing.Point(103, 191);
            this.button_apply.Name = "button_apply";
            this.button_apply.Size = new System.Drawing.Size(75, 23);
            this.button_apply.TabIndex = 7;
            this.button_apply.Text = "Apply";
            this.button_apply.UseVisualStyleBackColor = true;
            this.button_apply.Click += new System.EventHandler(this.button_apply_Click);
            // 
            // button_reset
            // 
            this.button_reset.Location = new System.Drawing.Point(5, 191);
            this.button_reset.Name = "button_reset";
            this.button_reset.Size = new System.Drawing.Size(75, 23);
            this.button_reset.TabIndex = 8;
            this.button_reset.Text = "Reset";
            this.button_reset.UseVisualStyleBackColor = true;
            this.button_reset.Click += new System.EventHandler(this.button_reset_Click);
            // 
            // PlacesEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 227);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PlacesEditorForm";
            this.Text = "PlacesEditorForm";
            this.Load += new System.EventHandler(this.PlacesEditorForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PlacesEditorForm_FormClosing);
            this.contextMenuStrip_Root.ResumeLayout(false);
            this.contextMenuStrip_Category.ResumeLayout(false);
            this.contextMenuStrip_Place.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TreeView treeView_places;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Root;
        private System.Windows.Forms.ToolStripMenuItem newCategoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collapseAllToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Category;
        private System.Windows.Forms.ToolStripMenuItem newPlaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Place;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_cellid;
        private System.Windows.Forms.Button button_cell;
        private System.Windows.Forms.TextBox textBox_cellid;
        private System.Windows.Forms.Button button_apply;
        private System.Windows.Forms.TextBox textBox_comment;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_reset;

    }
}
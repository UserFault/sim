namespace TestShell
{
    partial class PlacesListForm
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("No places...");
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_save = new System.Windows.Forms.Button();
            this.button_remove = new System.Windows.Forms.Button();
            this.button_close = new System.Windows.Forms.Button();
            this.textBox_descr = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button_navi = new System.Windows.Forms.Button();
            this.textBox_cellid = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.listView1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 156F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(464, 401);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_save);
            this.panel1.Controls.Add(this.button_remove);
            this.panel1.Controls.Add(this.button_close);
            this.panel1.Controls.Add(this.textBox_descr);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBox_Name);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.button_navi);
            this.panel1.Controls.Add(this.textBox_cellid);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 248);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(461, 150);
            this.panel1.TabIndex = 0;
            // 
            // button_save
            // 
            this.button_save.Location = new System.Drawing.Point(293, 123);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(75, 23);
            this.button_save.TabIndex = 9;
            this.button_save.Text = "Save";
            this.toolTip1.SetToolTip(this.button_save, "Add new place or save changes in existing place");
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // button_remove
            // 
            this.button_remove.Location = new System.Drawing.Point(3, 123);
            this.button_remove.Name = "button_remove";
            this.button_remove.Size = new System.Drawing.Size(75, 23);
            this.button_remove.TabIndex = 8;
            this.button_remove.Text = "Remove";
            this.toolTip1.SetToolTip(this.button_remove, "Remove selected place from list");
            this.button_remove.UseVisualStyleBackColor = true;
            this.button_remove.Click += new System.EventHandler(this.button_remove_Click);
            // 
            // button_close
            // 
            this.button_close.Location = new System.Drawing.Point(374, 123);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(75, 23);
            this.button_close.TabIndex = 7;
            this.button_close.Text = "Close";
            this.toolTip1.SetToolTip(this.button_close, "Close this form");
            this.button_close.UseVisualStyleBackColor = true;
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // textBox_descr
            // 
            this.textBox_descr.AcceptsReturn = true;
            this.textBox_descr.AcceptsTab = true;
            this.textBox_descr.Location = new System.Drawing.Point(69, 59);
            this.textBox_descr.Multiline = true;
            this.textBox_descr.Name = "textBox_descr";
            this.textBox_descr.Size = new System.Drawing.Size(380, 58);
            this.textBox_descr.TabIndex = 6;
            this.toolTip1.SetToolTip(this.textBox_descr, "Enter short description of place");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Description";
            // 
            // textBox_Name
            // 
            this.textBox_Name.Location = new System.Drawing.Point(69, 3);
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.Size = new System.Drawing.Size(380, 20);
            this.textBox_Name.TabIndex = 4;
            this.toolTip1.SetToolTip(this.textBox_Name, "Enter name for new place");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Name";
            // 
            // button_navi
            // 
            this.button_navi.Location = new System.Drawing.Point(229, 27);
            this.button_navi.Name = "button_navi";
            this.button_navi.Size = new System.Drawing.Size(28, 23);
            this.button_navi.TabIndex = 2;
            this.button_navi.Text = "..";
            this.toolTip1.SetToolTip(this.button_navi, "Open new navigator to specify cell for new place");
            this.button_navi.UseVisualStyleBackColor = true;
            this.button_navi.Click += new System.EventHandler(this.button_navi_Click);
            // 
            // textBox_cellid
            // 
            this.textBox_cellid.Location = new System.Drawing.Point(69, 29);
            this.textBox_cellid.Name = "textBox_cellid";
            this.textBox_cellid.Size = new System.Drawing.Size(154, 20);
            this.textBox_cellid.TabIndex = 1;
            this.toolTip1.SetToolTip(this.textBox_cellid, "Select cell for place");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "CellId";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader1,
            this.columnHeader3});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.listView1.Location = new System.Drawing.Point(3, 3);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(461, 239);
            this.listView1.TabIndex = 1;
            this.toolTip1.SetToolTip(this.listView1, "Select place for change");
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView1_ItemSelectionChanged);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "CellId";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Description";
            this.columnHeader3.Width = 300;
            // 
            // PlacesListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 401);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PlacesListForm";
            this.Text = "List of Places";
            this.toolTip1.SetToolTip(this, "Edit navigator places");
            this.Load += new System.EventHandler(this.PlacesListForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PlacesListForm_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button button_close;
        private System.Windows.Forms.TextBox textBox_descr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_Name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_navi;
        private System.Windows.Forms.TextBox textBox_cellid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button button_remove;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
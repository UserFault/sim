namespace TestShell
{
    partial class TestShellForm
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
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.specialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formsTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.snapshotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createSnapshotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSnapshotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createBaseInfrastructureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.loadCsvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(292, 220);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(292, 266);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(292, 22);
            this.statusStrip1.TabIndex = 0;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(109, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.projectToolStripMenuItem,
            this.testToolStripMenuItem,
            this.snapshotToolStripMenuItem,
            this.testToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(292, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // projectToolStripMenuItem
            // 
            this.projectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.toolStripSeparator2,
            this.specialToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            this.projectToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.projectToolStripMenuItem.Text = "Project";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = global::TestShell.Properties.Resources.DocumentHS;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.newToolStripMenuItem.Text = "New...";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = global::TestShell.Properties.Resources.openfolderHS;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.openToolStripMenuItem.Text = "SolutionOpen...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(120, 6);
            // 
            // specialToolStripMenuItem
            // 
            this.specialToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.clearToolStripMenuItem});
            this.specialToolStripMenuItem.Name = "specialToolStripMenuItem";
            this.specialToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.specialToolStripMenuItem.Text = "Special";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::TestShell.Properties.Resources.DeleteHS;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.deleteToolStripMenuItem.Text = "Delete...";
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Image = global::TestShell.Properties.Resources.DeleteHS;
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(120, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formsTestToolStripMenuItem,
            this.createBaseInfrastructureToolStripMenuItem});
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.testToolStripMenuItem.Text = "Structure";
            // 
            // formsTestToolStripMenuItem
            // 
            this.formsTestToolStripMenuItem.Name = "formsTestToolStripMenuItem";
            this.formsTestToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.formsTestToolStripMenuItem.Text = "Navigator...";
            this.formsTestToolStripMenuItem.Click += new System.EventHandler(this.formsTestToolStripMenuItem_Click);
            // 
            // snapshotToolStripMenuItem
            // 
            this.snapshotToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createSnapshotToolStripMenuItem,
            this.loadSnapshotToolStripMenuItem});
            this.snapshotToolStripMenuItem.Name = "snapshotToolStripMenuItem";
            this.snapshotToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.snapshotToolStripMenuItem.Text = "Snapshot";
            // 
            // createSnapshotToolStripMenuItem
            // 
            this.createSnapshotToolStripMenuItem.Name = "createSnapshotToolStripMenuItem";
            this.createSnapshotToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.createSnapshotToolStripMenuItem.Text = "Create snapshot";
            this.createSnapshotToolStripMenuItem.Click += new System.EventHandler(this.createSnapshotToolStripMenuItem_Click);
            // 
            // loadSnapshotToolStripMenuItem
            // 
            this.loadSnapshotToolStripMenuItem.Name = "loadSnapshotToolStripMenuItem";
            this.loadSnapshotToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.loadSnapshotToolStripMenuItem.Text = "Load snapshot...";
            this.loadSnapshotToolStripMenuItem.Click += new System.EventHandler(this.loadSnapshotToolStripMenuItem_Click);
            // 
            // createBaseInfrastructureToolStripMenuItem
            // 
            this.createBaseInfrastructureToolStripMenuItem.Name = "createBaseInfrastructureToolStripMenuItem";
            this.createBaseInfrastructureToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.createBaseInfrastructureToolStripMenuItem.Text = "Create initial system";
            this.createBaseInfrastructureToolStripMenuItem.ToolTipText = "Создать начальную инфраструктуруячеек и связей";
            this.createBaseInfrastructureToolStripMenuItem.Click += new System.EventHandler(this.createBaseInfrastructureToolStripMenuItem_Click);
            // 
            // testToolStripMenuItem1
            // 
            this.testToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadCsvToolStripMenuItem});
            this.testToolStripMenuItem1.Name = "testToolStripMenuItem1";
            this.testToolStripMenuItem1.Size = new System.Drawing.Size(40, 20);
            this.testToolStripMenuItem1.Text = "Test";
            // 
            // loadCsvToolStripMenuItem
            // 
            this.loadCsvToolStripMenuItem.Name = "loadCsvToolStripMenuItem";
            this.loadCsvToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadCsvToolStripMenuItem.Text = "LoadCsv...";
            this.loadCsvToolStripMenuItem.Click += new System.EventHandler(this.loadCsvToolStripMenuItem_Click);
            // 
            // TestShellForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "TestShellForm";
            this.Text = "TestShell";
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem snapshotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createSnapshotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSnapshotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formsTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem specialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createBaseInfrastructureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem loadCsvToolStripMenuItem;

    }
}


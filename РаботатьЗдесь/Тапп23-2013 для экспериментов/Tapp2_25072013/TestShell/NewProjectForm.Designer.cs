namespace TestShell
{
    partial class NewProjectForm
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
            this.textBox_ProjectName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_ProjectDescription = new System.Windows.Forms.TextBox();
            this.textBox_parentFolder = new System.Windows.Forms.TextBox();
            this.button_browseFolder = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBox_databaseType = new System.Windows.Forms.ComboBox();
            this.numericUpDown_Timeout = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_UserPassword = new System.Windows.Forms.TextBox();
            this.textBox_UserName = new System.Windows.Forms.TextBox();
            this.textBox_dbName = new System.Windows.Forms.TextBox();
            this.textBox_SqlPath = new System.Windows.Forms.TextBox();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Timeout)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox_ProjectName
            // 
            this.textBox_ProjectName.Location = new System.Drawing.Point(11, 32);
            this.textBox_ProjectName.MaxLength = 256;
            this.textBox_ProjectName.Name = "textBox_ProjectName";
            this.textBox_ProjectName.Size = new System.Drawing.Size(315, 20);
            this.textBox_ProjectName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(228, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Project name  (Max 256 chars without spaces):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Project description text:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Parent folder for project:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Enabled = false;
            this.label4.Location = new System.Drawing.Point(8, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Database server or file path:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Enabled = false;
            this.label5.Location = new System.Drawing.Point(7, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(232, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "New database name (16 chars without spaces):";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Enabled = false;
            this.label6.Location = new System.Drawing.Point(7, 141);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Existing  user name:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Enabled = false;
            this.label7.Location = new System.Drawing.Point(7, 181);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "User password:";
            // 
            // textBox_ProjectDescription
            // 
            this.textBox_ProjectDescription.Location = new System.Drawing.Point(11, 71);
            this.textBox_ProjectDescription.MaxLength = 8192;
            this.textBox_ProjectDescription.Multiline = true;
            this.textBox_ProjectDescription.Name = "textBox_ProjectDescription";
            this.textBox_ProjectDescription.Size = new System.Drawing.Size(315, 53);
            this.textBox_ProjectDescription.TabIndex = 9;
            // 
            // textBox_parentFolder
            // 
            this.textBox_parentFolder.Location = new System.Drawing.Point(11, 143);
            this.textBox_parentFolder.Name = "textBox_parentFolder";
            this.textBox_parentFolder.Size = new System.Drawing.Size(282, 20);
            this.textBox_parentFolder.TabIndex = 10;
            // 
            // button_browseFolder
            // 
            this.button_browseFolder.Location = new System.Drawing.Point(298, 140);
            this.button_browseFolder.Name = "button_browseFolder";
            this.button_browseFolder.Size = new System.Drawing.Size(28, 23);
            this.button_browseFolder.TabIndex = 11;
            this.button_browseFolder.Text = "...";
            this.button_browseFolder.UseVisualStyleBackColor = true;
            this.button_browseFolder.Click += new System.EventHandler(this.button_browseFolder_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button_browseFolder);
            this.groupBox1.Controls.Add(this.textBox_ProjectName);
            this.groupBox1.Controls.Add(this.textBox_parentFolder);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox_ProjectDescription);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(337, 175);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.comboBox_databaseType);
            this.groupBox2.Controls.Add(this.numericUpDown_Timeout);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.textBox_UserPassword);
            this.groupBox2.Controls.Add(this.textBox_UserName);
            this.groupBox2.Controls.Add(this.textBox_dbName);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.textBox_SqlPath);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(12, 193);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(337, 257);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Database";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(110, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Select database type:";
            // 
            // comboBox_databaseType
            // 
            this.comboBox_databaseType.FormattingEnabled = true;
            this.comboBox_databaseType.Location = new System.Drawing.Point(11, 38);
            this.comboBox_databaseType.Name = "comboBox_databaseType";
            this.comboBox_databaseType.Size = new System.Drawing.Size(314, 21);
            this.comboBox_databaseType.TabIndex = 12;
            this.comboBox_databaseType.SelectedIndexChanged += new System.EventHandler(this.comboBox_databaseType_SelectedIndexChanged);
            // 
            // numericUpDown_Timeout
            // 
            this.numericUpDown_Timeout.Enabled = false;
            this.numericUpDown_Timeout.Location = new System.Drawing.Point(262, 227);
            this.numericUpDown_Timeout.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.numericUpDown_Timeout.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDown_Timeout.Name = "numericUpDown_Timeout";
            this.numericUpDown_Timeout.Size = new System.Drawing.Size(63, 20);
            this.numericUpDown_Timeout.TabIndex = 11;
            this.numericUpDown_Timeout.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Enabled = false;
            this.label8.Location = new System.Drawing.Point(7, 227);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(147, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Connection timeout, seconds:";
            // 
            // textBox_UserPassword
            // 
            this.textBox_UserPassword.Enabled = false;
            this.textBox_UserPassword.Location = new System.Drawing.Point(9, 198);
            this.textBox_UserPassword.MaxLength = 128;
            this.textBox_UserPassword.Name = "textBox_UserPassword";
            this.textBox_UserPassword.Size = new System.Drawing.Size(316, 20);
            this.textBox_UserPassword.TabIndex = 9;
            // 
            // textBox_UserName
            // 
            this.textBox_UserName.Enabled = false;
            this.textBox_UserName.Location = new System.Drawing.Point(10, 158);
            this.textBox_UserName.MaxLength = 128;
            this.textBox_UserName.Name = "textBox_UserName";
            this.textBox_UserName.Size = new System.Drawing.Size(315, 20);
            this.textBox_UserName.TabIndex = 8;
            // 
            // textBox_dbName
            // 
            this.textBox_dbName.Enabled = false;
            this.textBox_dbName.Location = new System.Drawing.Point(9, 118);
            this.textBox_dbName.MaxLength = 16;
            this.textBox_dbName.Name = "textBox_dbName";
            this.textBox_dbName.Size = new System.Drawing.Size(316, 20);
            this.textBox_dbName.TabIndex = 7;
            // 
            // textBox_SqlPath
            // 
            this.textBox_SqlPath.Enabled = false;
            this.textBox_SqlPath.Location = new System.Drawing.Point(9, 78);
            this.textBox_SqlPath.Name = "textBox_SqlPath";
            this.textBox_SqlPath.Size = new System.Drawing.Size(317, 20);
            this.textBox_SqlPath.TabIndex = 6;
            // 
            // button_Cancel
            // 
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point(272, 456);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 14;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(191, 456);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 15;
            this.button_OK.Text = "Create";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // NewProjectForm
            // 
            this.AcceptButton = this.button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancel;
            this.ClientSize = new System.Drawing.Size(360, 493);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "NewProjectForm";
            this.Text = "Create new project";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Timeout)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_ProjectName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_ProjectDescription;
        private System.Windows.Forms.TextBox textBox_parentFolder;
        private System.Windows.Forms.Button button_browseFolder;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox_UserPassword;
        private System.Windows.Forms.TextBox textBox_UserName;
        private System.Windows.Forms.TextBox textBox_dbName;
        private System.Windows.Forms.TextBox textBox_SqlPath;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.NumericUpDown numericUpDown_Timeout;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox_databaseType;
    }
}
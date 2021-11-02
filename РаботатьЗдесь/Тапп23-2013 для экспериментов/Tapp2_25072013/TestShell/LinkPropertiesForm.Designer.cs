namespace TestShell
{
    partial class LinkPropertiesForm
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
            this.textBox_TableId = new System.Windows.Forms.TextBox();
            this.textBox_UpCellID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox_Active = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.textBox_DownCellId = new System.Windows.Forms.TextBox();
            this.textBox_Descr = new System.Windows.Forms.TextBox();
            this.textBox_StateId = new System.Windows.Forms.TextBox();
            this.textBox_AxisId = new System.Windows.Forms.TextBox();
            this.textBox_ServiceFlag = new System.Windows.Forms.TextBox();
            this.button_StateIdSelect = new System.Windows.Forms.Button();
            this.button_UpCellIDSelect = new System.Windows.Forms.Button();
            this.button_DownCellIDSelect = new System.Windows.Forms.Button();
            this.button_AxisIDSelect = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_TableId
            // 
            this.textBox_TableId.Location = new System.Drawing.Point(93, 10);
            this.textBox_TableId.Name = "textBox_TableId";
            this.textBox_TableId.ReadOnly = true;
            this.textBox_TableId.Size = new System.Drawing.Size(192, 20);
            this.textBox_TableId.TabIndex = 0;
            this.toolTip1.SetToolTip(this.textBox_TableId, "Table id for constant link");
            // 
            // textBox_UpCellID
            // 
            this.textBox_UpCellID.Location = new System.Drawing.Point(93, 36);
            this.textBox_UpCellID.Name = "textBox_UpCellID";
            this.textBox_UpCellID.ReadOnly = true;
            this.textBox_UpCellID.Size = new System.Drawing.Size(192, 20);
            this.textBox_UpCellID.TabIndex = 0;
            this.toolTip1.SetToolTip(this.textBox_UpCellID, "ID of upper cell");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Table #:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Up cell ID:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Down cell ID:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Description:";
            // 
            // checkBox_Active
            // 
            this.checkBox_Active.AutoSize = true;
            this.checkBox_Active.Location = new System.Drawing.Point(13, 116);
            this.checkBox_Active.Name = "checkBox_Active";
            this.checkBox_Active.Size = new System.Drawing.Size(56, 17);
            this.checkBox_Active.TabIndex = 6;
            this.checkBox_Active.Text = "Active";
            this.toolTip1.SetToolTip(this.checkBox_Active, "Lnk is active");
            this.checkBox_Active.UseVisualStyleBackColor = true;
            // 
            // textBox_DownCellId
            // 
            this.textBox_DownCellId.Location = new System.Drawing.Point(93, 62);
            this.textBox_DownCellId.Name = "textBox_DownCellId";
            this.textBox_DownCellId.ReadOnly = true;
            this.textBox_DownCellId.Size = new System.Drawing.Size(192, 20);
            this.textBox_DownCellId.TabIndex = 2;
            this.toolTip1.SetToolTip(this.textBox_DownCellId, "ID of downer cell");
            // 
            // textBox_Descr
            // 
            this.textBox_Descr.Location = new System.Drawing.Point(12, 187);
            this.textBox_Descr.Multiline = true;
            this.textBox_Descr.Name = "textBox_Descr";
            this.textBox_Descr.Size = new System.Drawing.Size(307, 63);
            this.textBox_Descr.TabIndex = 8;
            this.toolTip1.SetToolTip(this.textBox_Descr, "Link description");
            // 
            // textBox_StateId
            // 
            this.textBox_StateId.Location = new System.Drawing.Point(93, 139);
            this.textBox_StateId.Name = "textBox_StateId";
            this.textBox_StateId.ReadOnly = true;
            this.textBox_StateId.Size = new System.Drawing.Size(192, 20);
            this.textBox_StateId.TabIndex = 6;
            this.toolTip1.SetToolTip(this.textBox_StateId, "ID of state cell");
            // 
            // textBox_AxisId
            // 
            this.textBox_AxisId.Location = new System.Drawing.Point(93, 89);
            this.textBox_AxisId.Name = "textBox_AxisId";
            this.textBox_AxisId.ReadOnly = true;
            this.textBox_AxisId.Size = new System.Drawing.Size(192, 20);
            this.textBox_AxisId.TabIndex = 4;
            this.toolTip1.SetToolTip(this.textBox_AxisId, "Id of axis cell");
            // 
            // textBox_ServiceFlag
            // 
            this.textBox_ServiceFlag.Location = new System.Drawing.Point(93, 256);
            this.textBox_ServiceFlag.Name = "textBox_ServiceFlag";
            this.textBox_ServiceFlag.Size = new System.Drawing.Size(100, 20);
            this.textBox_ServiceFlag.TabIndex = 9;
            this.toolTip1.SetToolTip(this.textBox_ServiceFlag, "Link service flag value, for internal use only.");
            // 
            // button_StateIdSelect
            // 
            this.button_StateIdSelect.Location = new System.Drawing.Point(291, 137);
            this.button_StateIdSelect.Name = "button_StateIdSelect";
            this.button_StateIdSelect.Size = new System.Drawing.Size(28, 23);
            this.button_StateIdSelect.TabIndex = 7;
            this.button_StateIdSelect.Text = "..";
            this.toolTip1.SetToolTip(this.button_StateIdSelect, "Select state cell");
            this.button_StateIdSelect.UseVisualStyleBackColor = true;
            this.button_StateIdSelect.Click += new System.EventHandler(this.button_StateIdSelect_Click);
            // 
            // button_UpCellIDSelect
            // 
            this.button_UpCellIDSelect.Location = new System.Drawing.Point(291, 34);
            this.button_UpCellIDSelect.Name = "button_UpCellIDSelect";
            this.button_UpCellIDSelect.Size = new System.Drawing.Size(28, 23);
            this.button_UpCellIDSelect.TabIndex = 1;
            this.button_UpCellIDSelect.Text = "..";
            this.toolTip1.SetToolTip(this.button_UpCellIDSelect, "Select upper cell");
            this.button_UpCellIDSelect.UseVisualStyleBackColor = true;
            this.button_UpCellIDSelect.Click += new System.EventHandler(this.button_UpCellIDSelect_Click);
            // 
            // button_DownCellIDSelect
            // 
            this.button_DownCellIDSelect.Location = new System.Drawing.Point(291, 61);
            this.button_DownCellIDSelect.Name = "button_DownCellIDSelect";
            this.button_DownCellIDSelect.Size = new System.Drawing.Size(28, 23);
            this.button_DownCellIDSelect.TabIndex = 3;
            this.button_DownCellIDSelect.Text = "..";
            this.toolTip1.SetToolTip(this.button_DownCellIDSelect, "Select downer cell");
            this.button_DownCellIDSelect.UseVisualStyleBackColor = true;
            this.button_DownCellIDSelect.Click += new System.EventHandler(this.button5_Click);
            // 
            // button_AxisIDSelect
            // 
            this.button_AxisIDSelect.Location = new System.Drawing.Point(291, 87);
            this.button_AxisIDSelect.Name = "button_AxisIDSelect";
            this.button_AxisIDSelect.Size = new System.Drawing.Size(28, 23);
            this.button_AxisIDSelect.TabIndex = 5;
            this.button_AxisIDSelect.Text = "..";
            this.toolTip1.SetToolTip(this.button_AxisIDSelect, "Select axis cell");
            this.button_AxisIDSelect.UseVisualStyleBackColor = true;
            this.button_AxisIDSelect.Click += new System.EventHandler(this.button_AxisIDSelect_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(244, 287);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 11;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(163, 287);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 10;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "State Id:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Axis ID:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 256);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Service flag:";
            // 
            // LinkPropertiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 321);
            this.Controls.Add(this.button_AxisIDSelect);
            this.Controls.Add(this.button_DownCellIDSelect);
            this.Controls.Add(this.button_UpCellIDSelect);
            this.Controls.Add(this.button_StateIdSelect);
            this.Controls.Add(this.textBox_ServiceFlag);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox_AxisId);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox_StateId);
            this.Controls.Add(this.textBox_Descr);
            this.Controls.Add(this.textBox_DownCellId);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.checkBox_Active);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_UpCellID);
            this.Controls.Add(this.textBox_TableId);
            this.Name = "LinkPropertiesForm";
            this.Text = "LinkPropertiesForm";
            this.Load += new System.EventHandler(this.LinkPropertiesForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_TableId;
        private System.Windows.Forms.TextBox textBox_UpCellID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox_Active;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.TextBox textBox_DownCellId;
        private System.Windows.Forms.TextBox textBox_Descr;
        private System.Windows.Forms.TextBox textBox_StateId;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_AxisId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_ServiceFlag;
        private System.Windows.Forms.Button button_StateIdSelect;
        private System.Windows.Forms.Button button_UpCellIDSelect;
        private System.Windows.Forms.Button button_DownCellIDSelect;
        private System.Windows.Forms.Button button_AxisIDSelect;
    }
}
namespace TestShell
{
    partial class CellPropertiesForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_CellID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_CellName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_CellDescr = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_CellDataTypeID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_CellData = new System.Windows.Forms.TextBox();
            this.checkBox_CellActive = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_CellCreated = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_CellModified = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_CellServiceFlag = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_CellStateId = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox_CellTypeId = new System.Windows.Forms.TextBox();
            this.checkBox_CellReadOnly = new System.Windows.Forms.CheckBox();
            this.button_CheckNameUnical = new System.Windows.Forms.Button();
            this.button_TypeSelect = new System.Windows.Forms.Button();
            this.button_StateSelect = new System.Windows.Forms.Button();
            this.button_CellDataTypeSelect = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID:";
            // 
            // textBox_CellID
            // 
            this.textBox_CellID.Location = new System.Drawing.Point(79, 9);
            this.textBox_CellID.Name = "textBox_CellID";
            this.textBox_CellID.ReadOnly = true;
            this.textBox_CellID.Size = new System.Drawing.Size(100, 20);
            this.textBox_CellID.TabIndex = 1;
            this.toolTip1.SetToolTip(this.textBox_CellID, "Cell identifier");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Name:";
            // 
            // textBox_CellName
            // 
            this.textBox_CellName.Location = new System.Drawing.Point(79, 37);
            this.textBox_CellName.Name = "textBox_CellName";
            this.textBox_CellName.Size = new System.Drawing.Size(251, 20);
            this.textBox_CellName.TabIndex = 0;
            this.toolTip1.SetToolTip(this.textBox_CellName, "Cell name");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Description:";
            // 
            // textBox_CellDescr
            // 
            this.textBox_CellDescr.Location = new System.Drawing.Point(12, 81);
            this.textBox_CellDescr.Multiline = true;
            this.textBox_CellDescr.Name = "textBox_CellDescr";
            this.textBox_CellDescr.Size = new System.Drawing.Size(355, 60);
            this.textBox_CellDescr.TabIndex = 2;
            this.toolTip1.SetToolTip(this.textBox_CellDescr, "Cell description");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Datatype:";
            // 
            // textBox_CellDataTypeID
            // 
            this.textBox_CellDataTypeID.Location = new System.Drawing.Point(79, 200);
            this.textBox_CellDataTypeID.Name = "textBox_CellDataTypeID";
            this.textBox_CellDataTypeID.ReadOnly = true;
            this.textBox_CellDataTypeID.Size = new System.Drawing.Size(164, 20);
            this.textBox_CellDataTypeID.TabIndex = 7;
            this.toolTip1.SetToolTip(this.textBox_CellDataTypeID, "Cell datatype identifier");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 232);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Data:";
            // 
            // textBox_CellData
            // 
            this.textBox_CellData.Location = new System.Drawing.Point(79, 228);
            this.textBox_CellData.Name = "textBox_CellData";
            this.textBox_CellData.ReadOnly = true;
            this.textBox_CellData.Size = new System.Drawing.Size(289, 20);
            this.textBox_CellData.TabIndex = 14;
            this.toolTip1.SetToolTip(this.textBox_CellData, "Cell data bytes");
            // 
            // checkBox_CellActive
            // 
            this.checkBox_CellActive.AutoSize = true;
            this.checkBox_CellActive.Location = new System.Drawing.Point(283, 149);
            this.checkBox_CellActive.Name = "checkBox_CellActive";
            this.checkBox_CellActive.Size = new System.Drawing.Size(56, 17);
            this.checkBox_CellActive.TabIndex = 9;
            this.checkBox_CellActive.Text = "Active";
            this.checkBox_CellActive.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 258);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Created:";
            // 
            // textBox_CellCreated
            // 
            this.textBox_CellCreated.Location = new System.Drawing.Point(79, 254);
            this.textBox_CellCreated.Name = "textBox_CellCreated";
            this.textBox_CellCreated.ReadOnly = true;
            this.textBox_CellCreated.Size = new System.Drawing.Size(100, 20);
            this.textBox_CellCreated.TabIndex = 12;
            this.toolTip1.SetToolTip(this.textBox_CellCreated, "Cell created timestamp");
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(195, 258);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Modified:";
            // 
            // textBox_CellModified
            // 
            this.textBox_CellModified.Location = new System.Drawing.Point(268, 254);
            this.textBox_CellModified.Name = "textBox_CellModified";
            this.textBox_CellModified.ReadOnly = true;
            this.textBox_CellModified.Size = new System.Drawing.Size(100, 20);
            this.textBox_CellModified.TabIndex = 14;
            this.toolTip1.SetToolTip(this.textBox_CellModified, "Last modified timestamp");
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 284);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Service flag:";
            // 
            // textBox_CellServiceFlag
            // 
            this.textBox_CellServiceFlag.Location = new System.Drawing.Point(79, 280);
            this.textBox_CellServiceFlag.Name = "textBox_CellServiceFlag";
            this.textBox_CellServiceFlag.Size = new System.Drawing.Size(100, 20);
            this.textBox_CellServiceFlag.TabIndex = 14;
            this.toolTip1.SetToolTip(this.textBox_CellServiceFlag, "Cell service flag value");
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 178);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "State ID:";
            // 
            // textBox_CellStateId
            // 
            this.textBox_CellStateId.Location = new System.Drawing.Point(79, 174);
            this.textBox_CellStateId.Name = "textBox_CellStateId";
            this.textBox_CellStateId.ReadOnly = true;
            this.textBox_CellStateId.Size = new System.Drawing.Size(164, 20);
            this.textBox_CellStateId.TabIndex = 5;
            this.toolTip1.SetToolTip(this.textBox_CellStateId, "Cell state identifier");
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 151);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Type ID:";
            // 
            // textBox_CellTypeId
            // 
            this.textBox_CellTypeId.Location = new System.Drawing.Point(79, 147);
            this.textBox_CellTypeId.Name = "textBox_CellTypeId";
            this.textBox_CellTypeId.ReadOnly = true;
            this.textBox_CellTypeId.Size = new System.Drawing.Size(164, 20);
            this.textBox_CellTypeId.TabIndex = 3;
            this.toolTip1.SetToolTip(this.textBox_CellTypeId, "Cell type identifier");
            // 
            // checkBox_CellReadOnly
            // 
            this.checkBox_CellReadOnly.AutoSize = true;
            this.checkBox_CellReadOnly.Location = new System.Drawing.Point(283, 176);
            this.checkBox_CellReadOnly.Name = "checkBox_CellReadOnly";
            this.checkBox_CellReadOnly.Size = new System.Drawing.Size(74, 17);
            this.checkBox_CellReadOnly.TabIndex = 10;
            this.checkBox_CellReadOnly.Text = "Read only";
            this.checkBox_CellReadOnly.UseVisualStyleBackColor = true;
            // 
            // button_CheckNameUnical
            // 
            this.button_CheckNameUnical.Location = new System.Drawing.Point(337, 36);
            this.button_CheckNameUnical.Name = "button_CheckNameUnical";
            this.button_CheckNameUnical.Size = new System.Drawing.Size(30, 23);
            this.button_CheckNameUnical.TabIndex = 1;
            this.button_CheckNameUnical.Text = "V";
            this.toolTip1.SetToolTip(this.button_CheckNameUnical, "Check cell name unical in project");
            this.button_CheckNameUnical.UseVisualStyleBackColor = true;
            this.button_CheckNameUnical.Click += new System.EventHandler(this.button_CheckNameUnical_Click);
            // 
            // button_TypeSelect
            // 
            this.button_TypeSelect.Location = new System.Drawing.Point(249, 146);
            this.button_TypeSelect.Name = "button_TypeSelect";
            this.button_TypeSelect.Size = new System.Drawing.Size(24, 23);
            this.button_TypeSelect.TabIndex = 4;
            this.button_TypeSelect.Text = "..";
            this.toolTip1.SetToolTip(this.button_TypeSelect, "Browse project and select cell for type");
            this.button_TypeSelect.UseVisualStyleBackColor = true;
            this.button_TypeSelect.Click += new System.EventHandler(this.button_TypeSelect_Click);
            // 
            // button_StateSelect
            // 
            this.button_StateSelect.Location = new System.Drawing.Point(249, 173);
            this.button_StateSelect.Name = "button_StateSelect";
            this.button_StateSelect.Size = new System.Drawing.Size(24, 23);
            this.button_StateSelect.TabIndex = 6;
            this.button_StateSelect.Text = "..";
            this.toolTip1.SetToolTip(this.button_StateSelect, "Browse project and select cell as state");
            this.button_StateSelect.UseVisualStyleBackColor = true;
            this.button_StateSelect.Click += new System.EventHandler(this.button_StateSelect_Click);
            // 
            // button_CellDataTypeSelect
            // 
            this.button_CellDataTypeSelect.Location = new System.Drawing.Point(249, 199);
            this.button_CellDataTypeSelect.Name = "button_CellDataTypeSelect";
            this.button_CellDataTypeSelect.Size = new System.Drawing.Size(24, 23);
            this.button_CellDataTypeSelect.TabIndex = 8;
            this.button_CellDataTypeSelect.Text = "..";
            this.toolTip1.SetToolTip(this.button_CellDataTypeSelect, "Browse project and select cell use as Datatype");
            this.button_CellDataTypeSelect.UseVisualStyleBackColor = true;
            this.button_CellDataTypeSelect.Click += new System.EventHandler(this.button_CellDataTypeSelect_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(295, 316);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 12;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(214, 316);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 11;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // CellPropertiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 349);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_StateSelect);
            this.Controls.Add(this.button_CellDataTypeSelect);
            this.Controls.Add(this.button_TypeSelect);
            this.Controls.Add(this.textBox_CellDataTypeID);
            this.Controls.Add(this.button_CheckNameUnical);
            this.Controls.Add(this.textBox_CellID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_CellStateId);
            this.Controls.Add(this.textBox_CellServiceFlag);
            this.Controls.Add(this.checkBox_CellReadOnly);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_CellModified);
            this.Controls.Add(this.textBox_CellTypeId);
            this.Controls.Add(this.textBox_CellData);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox_CellName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_CellCreated);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_CellDescr);
            this.Controls.Add(this.checkBox_CellActive);
            this.Name = "CellPropertiesForm";
            this.Text = "CellPropertiesForm";
            this.Load += new System.EventHandler(this.CellPropertiesForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_CellID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_CellName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_CellDescr;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_CellDataTypeID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_CellData;
        private System.Windows.Forms.CheckBox checkBox_CellActive;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_CellCreated;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_CellModified;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_CellServiceFlag;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox_CellStateId;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox_CellTypeId;
        private System.Windows.Forms.CheckBox checkBox_CellReadOnly;
        private System.Windows.Forms.Button button_StateSelect;
        private System.Windows.Forms.Button button_TypeSelect;
        private System.Windows.Forms.Button button_CheckNameUnical;
        private System.Windows.Forms.Button button_CellDataTypeSelect;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
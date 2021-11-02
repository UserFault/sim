using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TestShell
{
    public partial class NewProjectForm : Form
    {
        public NewProjectForm()
        {
            InitializeComponent();

            this.comboBox_databaseType.Items.Add(Mary.MDatabaseType.NoDatabase);
            this.comboBox_databaseType.Items.Add(Mary.MDatabaseType.MicrosoftSqlServer2005);
            this.comboBox_databaseType.Items.Add(Mary.MDatabaseType.MsAccess);
            this.comboBox_databaseType.Items.Add(Mary.MDatabaseType.MySql5);
            this.comboBox_databaseType.Items.Add(Mary.MDatabaseType.Sqlite3);
            this.comboBox_databaseType.SelectedIndex = 0;
        }
        /// <summary>
        /// Project SqlServer user name
        /// </summary>
        public string UserName
        {
            get { return textBox_UserName.Text; }
            set { textBox_UserName.Text = value; }
        }
        /// <summary>
        /// Project SqlServer user password
        /// </summary>
        public string UserPassword
        {
            get { return textBox_UserPassword.Text; ; }
            set { textBox_UserPassword.Text = value; }
        }
        /// <summary>
        /// Project name
        /// </summary>
        public string ProjectName
        {
            get { return textBox_ProjectName.Text; }
            set { textBox_ProjectName.Text = value; }
        }
        /// <summary>
        /// Project description
        /// </summary>
        public string ProjectDescription
        {
            get { return textBox_ProjectDescription.Text; }
            set { textBox_ProjectDescription.Text = value; }
        }
        /// <summary>
        /// Project database name
        /// </summary>
        public string DatabaseName
        {
            get { return textBox_dbName.Text; }
            set { textBox_dbName.Text = value; }
        }
        /// <summary>
        /// Project parent folder
        /// </summary>
        public string ParentFolder
        {
            get { return textBox_parentFolder.Text; }
            set { textBox_parentFolder.Text = value; }
        }
        /// <summary>
        /// Project SqlServer path
        /// </summary>
        public string SqlServerPath
        {
            get { return textBox_SqlPath.Text; }
            set { textBox_SqlPath.Text = value; }
        }

        public Mary.MDatabaseType DatabaseType
        {
            get { return (Mary.MDatabaseType) Enum.Parse(typeof(Mary.MDatabaseType), comboBox_databaseType.Text); }
            set { comboBox_databaseType.SelectedItem = value; }
        }

        /// <summary>
        /// Project SqlServer timeout
        /// </summary>
        public int Timeout
        {
            get { return (int) numericUpDown_Timeout.Value; }
            set { numericUpDown_Timeout.Value = value; }
        }

        /// <summary>
        /// Close with OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_OK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Close with cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Cancel_Click(object sender, EventArgs e)
        {
            
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Show folder dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_browseFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            fd.Description = "Select folder to place project";
            fd.RootFolder = Environment.SpecialFolder.MyDocuments;
            fd.ShowNewFolderButton = true;
            if (fd.ShowDialog(this) == DialogResult.OK)
            {
                textBox_parentFolder.Text = fd.SelectedPath;
            }
        }

        private void comboBox_databaseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool en = (this.DatabaseType != Mary.MDatabaseType.NoDatabase);
            this.textBox_dbName.Enabled = en;
            this.textBox_SqlPath.Enabled = en;
            this.textBox_UserName.Enabled = en;
            this.textBox_UserPassword.Enabled = en;
            this.label4.Enabled = en;
            this.label5.Enabled = en;
            this.label6.Enabled = en;
            this.label7.Enabled = en;
            this.label8.Enabled = en;

            return;
        }




    }
}

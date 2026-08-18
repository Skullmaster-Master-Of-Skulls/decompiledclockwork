namespace AutoComboBox
{
	// Token: 0x0200006A RID: 106
	public partial class ImportSettingsDialog : global::System.Windows.Forms.Form
	{
		// Token: 0x060003CC RID: 972 RVA: 0x0001F310 File Offset: 0x0001E310
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.components != null)
				{
					this.components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0001F34C File Offset: 0x0001E34C
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::AutoComboBox.ImportSettingsDialog));
			this.label1 = new global::System.Windows.Forms.Label();
			this.btn_testConnection = new global::System.Windows.Forms.Button();
			this.txt_databaseName = new global::System.Windows.Forms.TextBox();
			this.label7 = new global::System.Windows.Forms.Label();
			this.label6 = new global::System.Windows.Forms.Label();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.p_usernamePassword = new global::System.Windows.Forms.Panel();
			this.txt_password = new global::System.Windows.Forms.TextBox();
			this.txt_username = new global::System.Windows.Forms.TextBox();
			this.label4 = new global::System.Windows.Forms.Label();
			this.label5 = new global::System.Windows.Forms.Label();
			this.chk_useIntegratedSecurity = new global::System.Windows.Forms.CheckBox();
			this.btn_findServer = new global::System.Windows.Forms.Button();
			this.label3 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.mainMenu1 = new global::System.Windows.Forms.MainMenu(this.components);
			this.menuItem1 = new global::System.Windows.Forms.MenuItem();
			this.MENU_viewConnectionString = new global::System.Windows.Forms.MenuItem();
			this.menuItem3 = new global::System.Windows.Forms.MenuItem();
			this.menuItem4 = new global::System.Windows.Forms.MenuItem();
			this.menuItem5 = new global::System.Windows.Forms.MenuItem();
			this.cmb_server = new global::AutoComboBox.AutoComboBox();
			this.cmb_databaseTypes = new global::AutoComboBox.AutoComboBox();
			this.label11 = new global::System.Windows.Forms.Label();
			this.toolTip1 = new global::System.Windows.Forms.ToolTip(this.components);
			this.textBox1 = new global::System.Windows.Forms.TextBox();
			this.textBox2 = new global::System.Windows.Forms.TextBox();
			this.tabControl1 = new global::System.Windows.Forms.TabControl();
			this.tabPage1 = new global::System.Windows.Forms.TabPage();
			this.tabPage2 = new global::System.Windows.Forms.TabPage();
			this.label12 = new global::System.Windows.Forms.Label();
			this.label13 = new global::System.Windows.Forms.Label();
			this.imageList1 = new global::System.Windows.Forms.ImageList(this.components);
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.btn_save = new global::System.Windows.Forms.ToolStripButton();
			this.btn_cancel = new global::System.Windows.Forms.ToolStripButton();
			this.panel1.SuspendLayout();
			this.p_usernamePassword.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.label1.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label1.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label1.Location = new global::System.Drawing.Point(0, 12);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(487, 18);
			this.label1.TabIndex = 0;
			this.label1.Text = "SQL Select Command:";
			this.label1.TextAlign = global::System.Drawing.ContentAlignment.BottomLeft;
			this.toolTip1.SetToolTip(this.label1, "The SQL command to load data from the remote database (results will be stored in one table)");
			this.btn_testConnection.Location = new global::System.Drawing.Point(6, 258);
			this.btn_testConnection.Name = "btn_testConnection";
			this.btn_testConnection.Size = new global::System.Drawing.Size(152, 24);
			this.btn_testConnection.TabIndex = 31;
			this.btn_testConnection.Text = "Test &Connection ...";
			this.btn_testConnection.Click += new global::System.EventHandler(this.btn_testConnection_Click);
			this.txt_databaseName.Location = new global::System.Drawing.Point(12, 114);
			this.txt_databaseName.Name = "txt_databaseName";
			this.txt_databaseName.Size = new global::System.Drawing.Size(256, 22);
			this.txt_databaseName.TabIndex = 28;
			this.label7.AutoSize = true;
			this.label7.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label7.Location = new global::System.Drawing.Point(6, 96);
			this.label7.Name = "label7";
			this.label7.Size = new global::System.Drawing.Size(194, 14);
			this.label7.TabIndex = 27;
			this.label7.Text = "3. Enter the name of the database:";
			this.label7.TextAlign = global::System.Drawing.ContentAlignment.BottomLeft;
			this.label6.AutoSize = true;
			this.label6.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label6.Location = new global::System.Drawing.Point(6, 144);
			this.label6.Name = "label6";
			this.label6.Size = new global::System.Drawing.Size(294, 14);
			this.label6.TabIndex = 29;
			this.label6.Text = "4. Enter information to logon to the database server:";
			this.label6.TextAlign = global::System.Drawing.ContentAlignment.BottomLeft;
			this.panel1.Controls.Add(this.p_usernamePassword);
			this.panel1.Controls.Add(this.chk_useIntegratedSecurity);
			this.panel1.Location = new global::System.Drawing.Point(6, 162);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new global::System.Windows.Forms.Padding(1);
			this.panel1.Size = new global::System.Drawing.Size(352, 88);
			this.panel1.TabIndex = 30;
			this.p_usernamePassword.Controls.Add(this.txt_password);
			this.p_usernamePassword.Controls.Add(this.txt_username);
			this.p_usernamePassword.Controls.Add(this.label4);
			this.p_usernamePassword.Controls.Add(this.label5);
			this.p_usernamePassword.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.p_usernamePassword.Enabled = false;
			this.p_usernamePassword.Location = new global::System.Drawing.Point(1, 25);
			this.p_usernamePassword.Name = "p_usernamePassword";
			this.p_usernamePassword.Size = new global::System.Drawing.Size(350, 62);
			this.p_usernamePassword.TabIndex = 15;
			this.txt_password.Location = new global::System.Drawing.Point(80, 32);
			this.txt_password.Name = "txt_password";
			this.txt_password.PasswordChar = '*';
			this.txt_password.Size = new global::System.Drawing.Size(256, 22);
			this.txt_password.TabIndex = 19;
			this.txt_username.Location = new global::System.Drawing.Point(80, 8);
			this.txt_username.Name = "txt_username";
			this.txt_username.Size = new global::System.Drawing.Size(256, 22);
			this.txt_username.TabIndex = 17;
			this.label4.AutoSize = true;
			this.label4.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label4.Location = new global::System.Drawing.Point(8, 8);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(67, 14);
			this.label4.TabIndex = 16;
			this.label4.Text = "Username:";
			this.label4.TextAlign = global::System.Drawing.ContentAlignment.BottomLeft;
			this.label5.AutoSize = true;
			this.label5.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label5.Location = new global::System.Drawing.Point(8, 32);
			this.label5.Name = "label5";
			this.label5.Size = new global::System.Drawing.Size(66, 14);
			this.label5.TabIndex = 18;
			this.label5.Text = "Password:";
			this.label5.TextAlign = global::System.Drawing.ContentAlignment.BottomLeft;
			this.chk_useIntegratedSecurity.Checked = true;
			this.chk_useIntegratedSecurity.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.chk_useIntegratedSecurity.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.chk_useIntegratedSecurity.Location = new global::System.Drawing.Point(1, 1);
			this.chk_useIntegratedSecurity.Name = "chk_useIntegratedSecurity";
			this.chk_useIntegratedSecurity.Size = new global::System.Drawing.Size(350, 24);
			this.chk_useIntegratedSecurity.TabIndex = 14;
			this.chk_useIntegratedSecurity.Text = "Use Integrated Security";
			this.btn_findServer.Location = new global::System.Drawing.Point(366, 66);
			this.btn_findServer.Name = "btn_findServer";
			this.btn_findServer.Size = new global::System.Drawing.Size(120, 24);
			this.btn_findServer.TabIndex = 26;
			this.btn_findServer.Text = "&Refresh List";
			this.btn_findServer.Click += new global::System.EventHandler(this.btn_findServer_Click);
			this.label3.AutoSize = true;
			this.label3.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label3.Location = new global::System.Drawing.Point(6, 6);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(249, 14);
			this.label3.TabIndex = 22;
			this.label3.Text = "1. Select the type of database you are using:";
			this.label3.TextAlign = global::System.Drawing.ContentAlignment.BottomLeft;
			this.label2.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label2.Location = new global::System.Drawing.Point(6, 48);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(320, 16);
			this.label2.TabIndex = 24;
			this.label2.Text = "2. Select or enter a database server name or IP:";
			this.label2.TextAlign = global::System.Drawing.ContentAlignment.BottomLeft;
			this.mainMenu1.MenuItems.AddRange(new global::System.Windows.Forms.MenuItem[]
			{
				this.menuItem1
			});
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new global::System.Windows.Forms.MenuItem[]
			{
				this.MENU_viewConnectionString,
				this.menuItem3,
				this.menuItem4,
				this.menuItem5
			});
			this.menuItem1.Text = "File";
			this.MENU_viewConnectionString.Index = 0;
			this.MENU_viewConnectionString.Text = "View Connection String";
			this.MENU_viewConnectionString.Click += new global::System.EventHandler(this.MENU_viewConnectionString_Click);
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "Print";
			this.menuItem4.Index = 2;
			this.menuItem4.Text = "-";
			this.menuItem5.Index = 3;
			this.menuItem5.Text = "E&xit";
			this.menuItem5.Click += new global::System.EventHandler(this.menuItem5_Click);
			this.cmb_server.AccessibleRole = global::System.Windows.Forms.AccessibleRole.ComboBox;
			this.cmb_server.AllowUserToEnterAnyText = true;
			this.cmb_server.AutoCompleteEnabled = true;
			this.cmb_server.ChildLookupGroupId = 0;
			this.cmb_server.GotoNextItemOnDoubleClick = false;
			this.cmb_server.Location = new global::System.Drawing.Point(6, 66);
			this.cmb_server.LookupGroupId = 0;
			this.cmb_server.Name = "cmb_server";
			this.cmb_server.Size = new global::System.Drawing.Size(352, 24);
			this.cmb_server.TabIndex = 33;
			this.cmb_server.TryToSelectOnFocusLeave = true;
			this.cmb_databaseTypes.AccessibleRole = global::System.Windows.Forms.AccessibleRole.ComboBox;
			this.cmb_databaseTypes.AllowUserToEnterAnyText = true;
			this.cmb_databaseTypes.AutoCompleteEnabled = true;
			this.cmb_databaseTypes.ChildLookupGroupId = 0;
			this.cmb_databaseTypes.Enabled = false;
			this.cmb_databaseTypes.GotoNextItemOnDoubleClick = false;
			this.cmb_databaseTypes.Items.AddRange(new object[]
			{
				"SQL Server"
			});
			this.cmb_databaseTypes.Location = new global::System.Drawing.Point(6, 24);
			this.cmb_databaseTypes.LookupGroupId = 0;
			this.cmb_databaseTypes.Name = "cmb_databaseTypes";
			this.cmb_databaseTypes.Size = new global::System.Drawing.Size(352, 24);
			this.cmb_databaseTypes.TabIndex = 32;
			this.cmb_databaseTypes.TryToSelectOnFocusLeave = true;
			this.label11.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label11.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label11.Location = new global::System.Drawing.Point(0, 68);
			this.label11.Name = "label11";
			this.label11.Size = new global::System.Drawing.Size(487, 18);
			this.label11.TabIndex = 34;
			this.label11.Text = "SQL Update Command:";
			this.label11.TextAlign = global::System.Drawing.ContentAlignment.BottomLeft;
			this.toolTip1.SetToolTip(this.label11, "SQL command to update the remote database when a row has been dealt with");
			this.textBox1.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.textBox1.Location = new global::System.Drawing.Point(0, 30);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new global::System.Drawing.Size(487, 22);
			this.textBox1.TabIndex = 35;
			this.textBox2.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.textBox2.Location = new global::System.Drawing.Point(0, 86);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new global::System.Drawing.Size(487, 22);
			this.textBox2.TabIndex = 36;
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.tabControl1.Location = new global::System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new global::System.Drawing.Size(495, 315);
			this.tabControl1.TabIndex = 37;
			this.tabPage1.Controls.Add(this.cmb_server);
			this.tabPage1.Controls.Add(this.cmb_databaseTypes);
			this.tabPage1.Controls.Add(this.btn_findServer);
			this.tabPage1.Controls.Add(this.label3);
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Controls.Add(this.btn_testConnection);
			this.tabPage1.Controls.Add(this.txt_databaseName);
			this.tabPage1.Controls.Add(this.label7);
			this.tabPage1.Controls.Add(this.label6);
			this.tabPage1.Controls.Add(this.panel1);
			this.tabPage1.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.tabPage1.Location = new global::System.Drawing.Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new global::System.Drawing.Size(487, 286);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Connection to Remote Database";
			this.tabPage2.Controls.Add(this.textBox2);
			this.tabPage2.Controls.Add(this.label11);
			this.tabPage2.Controls.Add(this.label12);
			this.tabPage2.Controls.Add(this.textBox1);
			this.tabPage2.Controls.Add(this.label1);
			this.tabPage2.Controls.Add(this.label13);
			this.tabPage2.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.tabPage2.Location = new global::System.Drawing.Point(4, 25);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new global::System.Drawing.Size(487, 149);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "SQL Commands";
			this.label12.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label12.Location = new global::System.Drawing.Point(0, 52);
			this.label12.Name = "label12";
			this.label12.Size = new global::System.Drawing.Size(487, 16);
			this.label12.TabIndex = 37;
			this.label13.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label13.Location = new global::System.Drawing.Point(0, 0);
			this.label13.Name = "label13";
			this.label13.Size = new global::System.Drawing.Size(487, 12);
			this.label13.TabIndex = 38;
			this.imageList1.ImageStream = (global::System.Windows.Forms.ImageListStreamer)componentResourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = global::System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "");
			this.imageList1.Images.SetKeyName(1, "");
			this.imageList1.Images.SetKeyName(2, "");
			this.imageList1.Images.SetKeyName(3, "");
			this.imageList1.Images.SetKeyName(4, "");
			this.imageList1.Images.SetKeyName(5, "");
			this.imageList1.Images.SetKeyName(6, "");
			this.imageList1.Images.SetKeyName(7, "");
			this.imageList1.Images.SetKeyName(8, "");
			this.toolStrip1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip1.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_save,
				this.btn_cancel
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(0, 315);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(495, 39);
			this.toolStrip1.TabIndex = 39;
			this.toolStrip1.Text = "toolStrip1";
			this.btn_save.Image = global::AutoComboBox.Properties.Resources.check2;
			this.btn_save.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_save.Name = "btn_save";
			this.btn_save.Size = new global::System.Drawing.Size(64, 36);
			this.btn_save.Text = "&Ok";
			this.btn_save.Click += new global::System.EventHandler(this.btn_save_Click);
			this.btn_cancel.Image = global::AutoComboBox.Properties.Resources.delete2;
			this.btn_cancel.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(93, 36);
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
			this.AutoScaleBaseSize = new global::System.Drawing.Size(5, 13);
			base.ClientSize = new global::System.Drawing.Size(495, 354);
			base.Controls.Add(this.tabControl1);
			base.Controls.Add(this.toolStrip1);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Menu = this.mainMenu1;
			base.Name = "ImportSettingsDialog";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Import Settings";
			this.panel1.ResumeLayout(false);
			this.p_usernamePassword.ResumeLayout(false);
			this.p_usernamePassword.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000395 RID: 917
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000396 RID: 918
		private global::System.Windows.Forms.Button btn_testConnection;

		// Token: 0x04000397 RID: 919
		private global::System.Windows.Forms.TextBox txt_databaseName;

		// Token: 0x04000398 RID: 920
		private global::System.Windows.Forms.Label label7;

		// Token: 0x04000399 RID: 921
		private global::System.Windows.Forms.Label label6;

		// Token: 0x0400039A RID: 922
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x0400039B RID: 923
		private global::System.Windows.Forms.Panel p_usernamePassword;

		// Token: 0x0400039C RID: 924
		private global::System.Windows.Forms.TextBox txt_password;

		// Token: 0x0400039D RID: 925
		private global::System.Windows.Forms.TextBox txt_username;

		// Token: 0x0400039E RID: 926
		private global::System.Windows.Forms.Label label4;

		// Token: 0x0400039F RID: 927
		private global::System.Windows.Forms.Label label5;

		// Token: 0x040003A0 RID: 928
		private global::System.Windows.Forms.CheckBox chk_useIntegratedSecurity;

		// Token: 0x040003A1 RID: 929
		private global::System.Windows.Forms.Button btn_findServer;

		// Token: 0x040003A2 RID: 930
		private global::System.Windows.Forms.Label label3;

		// Token: 0x040003A3 RID: 931
		private global::System.Windows.Forms.Label label2;

		// Token: 0x040003A4 RID: 932
		private global::System.Windows.Forms.MainMenu mainMenu1;

		// Token: 0x040003A5 RID: 933
		private global::System.Windows.Forms.MenuItem menuItem1;

		// Token: 0x040003A6 RID: 934
		private global::System.Windows.Forms.MenuItem menuItem3;

		// Token: 0x040003A7 RID: 935
		private global::System.Windows.Forms.MenuItem menuItem4;

		// Token: 0x040003A8 RID: 936
		private global::System.Windows.Forms.MenuItem menuItem5;

		// Token: 0x040003A9 RID: 937
		private global::AutoComboBox.AutoComboBox cmb_server;

		// Token: 0x040003AA RID: 938
		private global::AutoComboBox.AutoComboBox cmb_databaseTypes;

		// Token: 0x040003AB RID: 939
		private global::System.Windows.Forms.Label label11;

		// Token: 0x040003AC RID: 940
		private global::System.Windows.Forms.ToolTip toolTip1;

		// Token: 0x040003AD RID: 941
		private global::System.Windows.Forms.TextBox textBox1;

		// Token: 0x040003AE RID: 942
		private global::System.Windows.Forms.TextBox textBox2;

		// Token: 0x040003AF RID: 943
		private global::System.Windows.Forms.TabControl tabControl1;

		// Token: 0x040003B0 RID: 944
		private global::System.Windows.Forms.TabPage tabPage1;

		// Token: 0x040003B1 RID: 945
		private global::System.Windows.Forms.TabPage tabPage2;

		// Token: 0x040003B2 RID: 946
		private global::System.Windows.Forms.Label label12;

		// Token: 0x040003B3 RID: 947
		private global::System.Windows.Forms.Label label13;

		// Token: 0x040003B4 RID: 948
		private global::System.Windows.Forms.ImageList imageList1;

		// Token: 0x040003B5 RID: 949
		private global::System.Windows.Forms.MenuItem MENU_viewConnectionString;

		// Token: 0x040003B6 RID: 950
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x040003B7 RID: 951
		private global::System.Windows.Forms.ToolStripButton btn_save;

		// Token: 0x040003B8 RID: 952
		private global::System.Windows.Forms.ToolStripButton btn_cancel;

		// Token: 0x040003B9 RID: 953
		private global::System.ComponentModel.IContainer components;
	}
}

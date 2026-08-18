namespace AutoComboBox
{
	// Token: 0x02000105 RID: 261
	public partial class PasswordListManager : global::System.Windows.Forms.Form
	{
		// Token: 0x06000A44 RID: 2628 RVA: 0x0004F550 File Offset: 0x0004E550
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

		// Token: 0x06000A45 RID: 2629 RVA: 0x0004F58C File Offset: 0x0004E58C
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::AutoComboBox.PasswordListManager));
			this.listView1 = new global::System.Windows.Forms.ListView();
			this.ch_username = new global::System.Windows.Forms.ColumnHeader();
			this.imageList1 = new global::System.Windows.Forms.ImageList(this.components);
			this.label1 = new global::System.Windows.Forms.Label();
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.btn_addUsername = new global::System.Windows.Forms.ToolStripButton();
			this.btn_deleteUsername = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_close = new global::System.Windows.Forms.ToolStripButton();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.listView1.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.ch_username
			});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.Location = new global::System.Drawing.Point(6, 24);
			this.listView1.Name = "listView1";
			this.listView1.Size = new global::System.Drawing.Size(600, 114);
			this.listView1.TabIndex = 0;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = global::System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new global::System.EventHandler(this.listView1_DoubleClick);
			this.ch_username.Text = "Username";
			this.ch_username.Width = 567;
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
			this.label1.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new global::System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(610, 18);
			this.label1.TabIndex = 7;
			this.label1.Text = "Double-click a username to change the password for that username.";
			this.toolStrip1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip1.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_addUsername,
				this.btn_deleteUsername,
				this.toolStripSeparator1,
				this.btn_close
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(0, 150);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(610, 39);
			this.toolStrip1.TabIndex = 8;
			this.toolStrip1.Text = "toolStrip1";
			this.btn_addUsername.Image = global::AutoComboBox.Properties.Resources.add;
			this.btn_addUsername.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_addUsername.Name = "btn_addUsername";
			this.btn_addUsername.Size = new global::System.Drawing.Size(110, 36);
			this.btn_addUsername.Text = "&Add alias";
			this.btn_addUsername.Visible = false;
			this.btn_addUsername.Click += new global::System.EventHandler(this.btn_addUsername_Click);
			this.btn_deleteUsername.Image = global::AutoComboBox.Properties.Resources.delete;
			this.btn_deleteUsername.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_deleteUsername.Name = "btn_deleteUsername";
			this.btn_deleteUsername.Size = new global::System.Drawing.Size(127, 36);
			this.btn_deleteUsername.Text = "&Delete alias";
			this.btn_deleteUsername.Click += new global::System.EventHandler(this.btn_deleteUsername_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new global::System.Drawing.Size(6, 39);
			this.btn_close.Image = global::AutoComboBox.Properties.Resources.delete2;
			this.btn_close.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_close.Name = "btn_close";
			this.btn_close.Size = new global::System.Drawing.Size(85, 36);
			this.btn_close.Text = "&Close";
			this.btn_close.Click += new global::System.EventHandler(this.btn_close_Click);
			this.AutoScaleBaseSize = new global::System.Drawing.Size(6, 15);
			base.ClientSize = new global::System.Drawing.Size(610, 189);
			base.Controls.Add(this.toolStrip1);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.listView1);
			this.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "PasswordListManager";
			this.Text = "Password List Manager";
			base.Load += new global::System.EventHandler(this.PasswordListManager_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400078A RID: 1930
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400078B RID: 1931
		private global::System.Windows.Forms.ListView listView1;

		// Token: 0x0400078C RID: 1932
		private global::System.Windows.Forms.ColumnHeader ch_username;

		// Token: 0x0400078D RID: 1933
		private global::System.Windows.Forms.ImageList imageList1;

		// Token: 0x04000792 RID: 1938
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000793 RID: 1939
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x04000794 RID: 1940
		private global::System.Windows.Forms.ToolStripButton btn_addUsername;

		// Token: 0x04000795 RID: 1941
		private global::System.Windows.Forms.ToolStripButton btn_deleteUsername;

		// Token: 0x04000796 RID: 1942
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

		// Token: 0x04000797 RID: 1943
		private global::System.Windows.Forms.ToolStripButton btn_close;
	}
}

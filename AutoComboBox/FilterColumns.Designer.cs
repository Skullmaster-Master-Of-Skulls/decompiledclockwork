namespace AutoComboBox
{
	// Token: 0x02000104 RID: 260
	public partial class FilterColumns : global::System.Windows.Forms.Form
	{
		// Token: 0x06000A38 RID: 2616 RVA: 0x0004E9B8 File Offset: 0x0004D9B8
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

		// Token: 0x06000A39 RID: 2617 RVA: 0x0004E9F4 File Offset: 0x0004D9F4
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::AutoComboBox.FilterColumns));
			this.listView1 = new global::System.Windows.Forms.ListView();
			this.columnHeader1 = new global::System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new global::System.Windows.Forms.ColumnHeader();
			this.label1 = new global::System.Windows.Forms.Label();
			this.imageList1 = new global::System.Windows.Forms.ImageList(this.components);
			this.cm_lv = new global::System.Windows.Forms.ContextMenu();
			this.MENU_lv_clearChecks = new global::System.Windows.Forms.MenuItem();
			this.MENU_lv_checkAll = new global::System.Windows.Forms.MenuItem();
			this.p_tableName = new global::System.Windows.Forms.Panel();
			this.txt_tableName = new global::System.Windows.Forms.TextBox();
			this.label2 = new global::System.Windows.Forms.Label();
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.btn_OK = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_uncheckAll = new global::System.Windows.Forms.ToolStripButton();
			this.btn_checkAll = new global::System.Windows.Forms.ToolStripButton();
			this.p_tableName.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.listView1.CheckBoxes = true;
			this.listView1.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.columnHeader1,
				this.columnHeader2
			});
			this.listView1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.HeaderStyle = global::System.Windows.Forms.ColumnHeaderStyle.None;
			this.listView1.HideSelection = false;
			this.listView1.Location = new global::System.Drawing.Point(0, 72);
			this.listView1.Name = "listView1";
			this.listView1.Size = new global::System.Drawing.Size(526, 228);
			this.listView1.TabIndex = 0;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = global::System.Windows.Forms.View.Details;
			this.listView1.SizeChanged += new global::System.EventHandler(this.listView1_SizeChanged);
			this.listView1.ItemCheck += new global::System.Windows.Forms.ItemCheckEventHandler(this.listView1_ItemCheck);
			this.columnHeader1.Width = 27;
			this.columnHeader2.Width = 465;
			this.label1.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new global::System.Drawing.Point(0, 48);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(526, 24);
			this.label1.TabIndex = 1;
			this.label1.Text = "Please check the columns to export:";
			this.imageList1.ImageStream = (global::System.Windows.Forms.ImageListStreamer)componentResourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = global::System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "");
			this.imageList1.Images.SetKeyName(1, "");
			this.imageList1.Images.SetKeyName(2, "");
			this.imageList1.Images.SetKeyName(3, "");
			this.imageList1.Images.SetKeyName(4, "");
			this.imageList1.Images.SetKeyName(5, "");
			this.cm_lv.MenuItems.AddRange(new global::System.Windows.Forms.MenuItem[]
			{
				this.MENU_lv_clearChecks,
				this.MENU_lv_checkAll
			});
			this.MENU_lv_clearChecks.Index = 0;
			this.MENU_lv_clearChecks.Text = "&Clear Checks";
			this.MENU_lv_clearChecks.Click += new global::System.EventHandler(this.MENU_lv_clearChecks_Click);
			this.MENU_lv_checkAll.Index = 1;
			this.MENU_lv_checkAll.Text = "Check &All";
			this.MENU_lv_checkAll.Click += new global::System.EventHandler(this.MENU_lv_checkAll_Click);
			this.p_tableName.Controls.Add(this.txt_tableName);
			this.p_tableName.Controls.Add(this.label2);
			this.p_tableName.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.p_tableName.Location = new global::System.Drawing.Point(0, 0);
			this.p_tableName.Name = "p_tableName";
			this.p_tableName.Size = new global::System.Drawing.Size(526, 48);
			this.p_tableName.TabIndex = 7;
			this.p_tableName.Visible = false;
			this.txt_tableName.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.txt_tableName.Location = new global::System.Drawing.Point(0, 18);
			this.txt_tableName.Name = "txt_tableName";
			this.txt_tableName.Size = new global::System.Drawing.Size(526, 26);
			this.txt_tableName.TabIndex = 5;
			this.txt_tableName.Text = "table1";
			this.label2.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label2.Location = new global::System.Drawing.Point(0, 0);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(526, 18);
			this.label2.TabIndex = 2;
			this.label2.Text = "Enter the name of the new table to create:";
			this.toolStrip1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip1.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_OK,
				this.toolStripSeparator1,
				this.btn_uncheckAll,
				this.btn_checkAll
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(0, 300);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(526, 39);
			this.toolStrip1.TabIndex = 8;
			this.toolStrip1.Text = "toolStrip1";
			this.btn_OK.Image = global::AutoComboBox.Properties.Resources.check2;
			this.btn_OK.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_OK.Name = "btn_OK";
			this.btn_OK.Size = new global::System.Drawing.Size(64, 36);
			this.btn_OK.Text = "&Ok";
			this.btn_OK.Click += new global::System.EventHandler(this.btn_OK_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new global::System.Drawing.Size(6, 39);
			this.btn_uncheckAll.Image = global::AutoComboBox.Properties.Resources.clipboard_empty;
			this.btn_uncheckAll.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_uncheckAll.Name = "btn_uncheckAll";
			this.btn_uncheckAll.Size = new global::System.Drawing.Size(128, 36);
			this.btn_uncheckAll.Text = "&Un-check all";
			this.btn_uncheckAll.Click += new global::System.EventHandler(this.btn_uncheckAll_Click);
			this.btn_checkAll.Image = global::AutoComboBox.Properties.Resources.clipboard;
			this.btn_checkAll.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_checkAll.Name = "btn_checkAll";
			this.btn_checkAll.Size = new global::System.Drawing.Size(108, 36);
			this.btn_checkAll.Text = "&Check all";
			this.btn_checkAll.Click += new global::System.EventHandler(this.btn_checkAll_Click);
			this.AutoScaleBaseSize = new global::System.Drawing.Size(8, 19);
			base.ClientSize = new global::System.Drawing.Size(526, 339);
			base.Controls.Add(this.listView1);
			base.Controls.Add(this.toolStrip1);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.p_tableName);
			this.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "FilterColumns";
			this.Text = "Export";
			base.Load += new global::System.EventHandler(this.FilterColumns_Load);
			this.p_tableName.ResumeLayout(false);
			this.p_tableName.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000778 RID: 1912
		public global::System.Windows.Forms.ListView listView1;

		// Token: 0x04000779 RID: 1913
		private global::System.Windows.Forms.ColumnHeader columnHeader1;

		// Token: 0x0400077A RID: 1914
		private global::System.Windows.Forms.ColumnHeader columnHeader2;

		// Token: 0x0400077B RID: 1915
		private global::System.Windows.Forms.Label label1;

		// Token: 0x0400077C RID: 1916
		private global::System.Windows.Forms.ImageList imageList1;

		// Token: 0x0400077D RID: 1917
		private global::System.Windows.Forms.ContextMenu cm_lv;

		// Token: 0x0400077E RID: 1918
		private global::System.Windows.Forms.MenuItem MENU_lv_clearChecks;

		// Token: 0x0400077F RID: 1919
		private global::System.Windows.Forms.MenuItem MENU_lv_checkAll;

		// Token: 0x04000780 RID: 1920
		private global::System.Windows.Forms.Panel p_tableName;

		// Token: 0x04000781 RID: 1921
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000782 RID: 1922
		public global::System.Windows.Forms.TextBox txt_tableName;

		// Token: 0x04000783 RID: 1923
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x04000784 RID: 1924
		private global::System.Windows.Forms.ToolStripButton btn_OK;

		// Token: 0x04000785 RID: 1925
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

		// Token: 0x04000786 RID: 1926
		private global::System.Windows.Forms.ToolStripButton btn_uncheckAll;

		// Token: 0x04000787 RID: 1927
		private global::System.Windows.Forms.ToolStripButton btn_checkAll;

		// Token: 0x04000788 RID: 1928
		private global::System.ComponentModel.IContainer components;
	}
}

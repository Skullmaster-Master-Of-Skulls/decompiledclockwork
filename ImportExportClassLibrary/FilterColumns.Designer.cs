namespace ImportExportClassLibrary
{
	// Token: 0x0200004F RID: 79
	public partial class FilterColumns : global::System.Windows.Forms.Form
	{
		// Token: 0x06000317 RID: 791 RVA: 0x00020089 File Offset: 0x0001F089
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x000200A8 File Offset: 0x0001F0A8
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::ImportExportClassLibrary.FilterColumns));
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
			this.btn_checkAll = new global::System.Windows.Forms.ToolStripButton();
			this.btn_checkNone = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_save = new global::System.Windows.Forms.ToolStripButton();
			this.btn_cancel = new global::System.Windows.Forms.ToolStripButton();
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
			this.imageList1.Images.SetKeyName(6, "");
			this.imageList1.Images.SetKeyName(7, "");
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
				this.btn_checkAll,
				this.btn_checkNone,
				this.toolStripSeparator1,
				this.btn_save,
				this.btn_cancel
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(0, 300);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(526, 39);
			this.toolStrip1.TabIndex = 8;
			this.toolStrip1.Text = "toolStrip1";
			this.btn_checkAll.Image = global::ImportExportClassLibrary.Properties.Resources.document_check;
			this.btn_checkAll.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_checkAll.Name = "btn_checkAll";
			this.btn_checkAll.Size = new global::System.Drawing.Size(108, 36);
			this.btn_checkAll.Text = "Check &all";
			this.btn_checkAll.Click += new global::System.EventHandler(this.btn_checkAll_Click);
			this.btn_checkNone.Image = global::ImportExportClassLibrary.Properties.Resources.document_plain;
			this.btn_checkNone.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_checkNone.Name = "btn_checkNone";
			this.btn_checkNone.Size = new global::System.Drawing.Size(127, 36);
			this.btn_checkNone.Text = "Check &none";
			this.btn_checkNone.Click += new global::System.EventHandler(this.btn_checkNone_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new global::System.Drawing.Size(6, 39);
			this.btn_save.Image = global::ImportExportClassLibrary.Properties.Resources.check2;
			this.btn_save.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_save.Name = "btn_save";
			this.btn_save.Size = new global::System.Drawing.Size(64, 36);
			this.btn_save.Text = "&Ok";
			this.btn_save.Click += new global::System.EventHandler(this.btn_save_Click);
			this.btn_cancel.Image = global::ImportExportClassLibrary.Properties.Resources.delete2;
			this.btn_cancel.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(93, 36);
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
			this.AutoScaleBaseSize = new global::System.Drawing.Size(8, 19);
			base.ClientSize = new global::System.Drawing.Size(526, 339);
			base.Controls.Add(this.listView1);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.p_tableName);
			base.Controls.Add(this.toolStrip1);
			this.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "FilterColumns";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Export";
			base.Load += new global::System.EventHandler(this.FilterColumns_Load);
			this.p_tableName.ResumeLayout(false);
			this.p_tableName.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040001AE RID: 430
		public global::System.Windows.Forms.ListView listView1;

		// Token: 0x040001AF RID: 431
		private global::System.Windows.Forms.ColumnHeader columnHeader1;

		// Token: 0x040001B0 RID: 432
		private global::System.Windows.Forms.ColumnHeader columnHeader2;

		// Token: 0x040001B1 RID: 433
		private global::System.Windows.Forms.Label label1;

		// Token: 0x040001B2 RID: 434
		private global::System.Windows.Forms.ImageList imageList1;

		// Token: 0x040001B3 RID: 435
		private global::System.Windows.Forms.ContextMenu cm_lv;

		// Token: 0x040001B4 RID: 436
		private global::System.Windows.Forms.MenuItem MENU_lv_clearChecks;

		// Token: 0x040001B5 RID: 437
		private global::System.Windows.Forms.MenuItem MENU_lv_checkAll;

		// Token: 0x040001B6 RID: 438
		private global::System.Windows.Forms.Panel p_tableName;

		// Token: 0x040001B7 RID: 439
		private global::System.Windows.Forms.Label label2;

		// Token: 0x040001B8 RID: 440
		public global::System.Windows.Forms.TextBox txt_tableName;

		// Token: 0x040001B9 RID: 441
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x040001BA RID: 442
		private global::System.Windows.Forms.ToolStripButton btn_save;

		// Token: 0x040001BB RID: 443
		private global::System.Windows.Forms.ToolStripButton btn_cancel;

		// Token: 0x040001BC RID: 444
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

		// Token: 0x040001BD RID: 445
		private global::System.Windows.Forms.ToolStripButton btn_checkAll;

		// Token: 0x040001BE RID: 446
		private global::System.Windows.Forms.ToolStripButton btn_checkNone;

		// Token: 0x040001BF RID: 447
		private global::System.ComponentModel.IContainer components;
	}
}

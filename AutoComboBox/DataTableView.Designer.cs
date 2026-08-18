namespace AutoComboBox
{
	// Token: 0x020000BA RID: 186
	public partial class DataTableView : global::System.Windows.Forms.Form
	{
		// Token: 0x060006FB RID: 1787 RVA: 0x00038A60 File Offset: 0x00037A60
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

		// Token: 0x060006FC RID: 1788 RVA: 0x00038A9C File Offset: 0x00037A9C
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::AutoComboBox.DataTableView));
			this.imageList1 = new global::System.Windows.Forms.ImageList(this.components);
			this.btn_fakeCancel = new global::System.Windows.Forms.Button();
			this.btn_fakeOK = new global::System.Windows.Forms.Button();
			this.label1 = new global::System.Windows.Forms.Label();
			this.contextMenu1 = new global::System.Windows.Forms.ContextMenu();
			this.MENU_GenerateSQLToUpdate = new global::System.Windows.Forms.MenuItem();
			this.dataGrid1 = new global::AutoComboBox.MyDataGrid();
			this.contextMenuStrip1 = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.viewRowDataToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.btn_properties = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolStripButton1 = new global::System.Windows.Forms.ToolStripButton();
			this.btn_import = new global::System.Windows.Forms.ToolStripDropDownButton();
			this.btn_importFromXml = new global::System.Windows.Forms.ToolStripMenuItem();
			this.btn_export = new global::System.Windows.Forms.ToolStripDropDownButton();
			this.btn_exportToXml = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_ok = new global::System.Windows.Forms.ToolStripButton();
			this.btn_close = new global::System.Windows.Forms.ToolStripButton();
			this.lbl_left = new global::System.Windows.Forms.Label();
			((global::System.ComponentModel.ISupportInitialize)this.dataGrid1).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
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
			this.imageList1.Images.SetKeyName(9, "");
			this.imageList1.Images.SetKeyName(10, "");
			this.imageList1.Images.SetKeyName(11, "");
			this.imageList1.Images.SetKeyName(12, "");
			this.imageList1.Images.SetKeyName(13, "");
			this.imageList1.Images.SetKeyName(14, "");
			this.imageList1.Images.SetKeyName(15, "");
			this.imageList1.Images.SetKeyName(16, "");
			this.imageList1.Images.SetKeyName(17, "");
			this.btn_fakeCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btn_fakeCancel.Location = new global::System.Drawing.Point(100, 0);
			this.btn_fakeCancel.Name = "btn_fakeCancel";
			this.btn_fakeCancel.Size = new global::System.Drawing.Size(0, 0);
			this.btn_fakeCancel.TabIndex = 7;
			this.btn_fakeCancel.Text = "button1";
			this.btn_fakeCancel.Click += new global::System.EventHandler(this.btn_fakeCancel_Click);
			this.btn_fakeOK.Location = new global::System.Drawing.Point(263, 238);
			this.btn_fakeOK.Name = "btn_fakeOK";
			this.btn_fakeOK.Size = new global::System.Drawing.Size(0, 0);
			this.btn_fakeOK.TabIndex = 8;
			this.btn_fakeOK.Text = "button1";
			this.btn_fakeOK.Click += new global::System.EventHandler(this.btn_fakeOK_Click);
			this.label1.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new global::System.Drawing.Point(54, 0);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(688, 31);
			this.label1.TabIndex = 9;
			this.label1.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.contextMenu1.MenuItems.AddRange(new global::System.Windows.Forms.MenuItem[]
			{
				this.MENU_GenerateSQLToUpdate
			});
			this.MENU_GenerateSQLToUpdate.Index = 0;
			this.MENU_GenerateSQLToUpdate.Text = "&Generate SQL to update";
			this.MENU_GenerateSQLToUpdate.Click += new global::System.EventHandler(this.MENU_GenerateSQLToUpdate_Click);
			this.dataGrid1.ColumnOrders = "";
			this.dataGrid1.ContextMenuStrip = this.contextMenuStrip1;
			this.dataGrid1.DataMember = "";
			this.dataGrid1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.dataGrid1.HeaderForeColor = global::System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Location = new global::System.Drawing.Point(54, 31);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.ReadOnly = true;
			this.dataGrid1.SelectedIndices = (global::System.Collections.ArrayList)componentResourceManager.GetObject("dataGrid1.SelectedIndices");
			this.dataGrid1.Size = new global::System.Drawing.Size(688, 421);
			this.dataGrid1.TabIndex = 11;
			this.contextMenuStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.viewRowDataToolStripMenuItem
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new global::System.Drawing.Size(149, 26);
			this.viewRowDataToolStripMenuItem.Name = "viewRowDataToolStripMenuItem";
			this.viewRowDataToolStripMenuItem.Size = new global::System.Drawing.Size(148, 22);
			this.viewRowDataToolStripMenuItem.Text = "&View row data";
			this.viewRowDataToolStripMenuItem.Click += new global::System.EventHandler(this.viewRowDataToolStripMenuItem_Click);
			this.toolStrip1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip1.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_properties,
				this.toolStripSeparator1,
				this.toolStripButton1,
				this.btn_import,
				this.btn_export,
				this.toolStripSeparator2,
				this.btn_ok,
				this.btn_close
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(54, 452);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(688, 39);
			this.toolStrip1.TabIndex = 12;
			this.toolStrip1.Text = "toolStrip1";
			this.btn_properties.Image = global::AutoComboBox.Properties.Resources.news_view;
			this.btn_properties.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_properties.Name = "btn_properties";
			this.btn_properties.Size = new global::System.Drawing.Size(117, 36);
			this.btn_properties.Text = "&Properties";
			this.btn_properties.Click += new global::System.EventHandler(this.btn_properties_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new global::System.Drawing.Size(6, 39);
			this.toolStripButton1.Image = global::AutoComboBox.Properties.Resources.printer;
			this.toolStripButton1.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new global::System.Drawing.Size(76, 36);
			this.toolStripButton1.Text = "&Print";
			this.toolStripButton1.Visible = false;
			this.btn_import.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_importFromXml
			});
			this.btn_import.Image = global::AutoComboBox.Properties.Resources.import2;
			this.btn_import.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_import.Name = "btn_import";
			this.btn_import.Size = new global::System.Drawing.Size(96, 36);
			this.btn_import.Text = "&Import";
			this.btn_importFromXml.Name = "btn_importFromXml";
			this.btn_importFromXml.Size = new global::System.Drawing.Size(181, 22);
			this.btn_importFromXml.Text = "Import from &xml";
			this.btn_importFromXml.Click += new global::System.EventHandler(this.btn_importFromXml_Click);
			this.btn_export.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_exportToXml
			});
			this.btn_export.Image = global::AutoComboBox.Properties.Resources.export2;
			this.btn_export.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_export.Name = "btn_export";
			this.btn_export.Size = new global::System.Drawing.Size(98, 36);
			this.btn_export.Text = "&Export";
			this.btn_exportToXml.Name = "btn_exportToXml";
			this.btn_exportToXml.Size = new global::System.Drawing.Size(165, 22);
			this.btn_exportToXml.Text = "Export to &xml";
			this.btn_exportToXml.Click += new global::System.EventHandler(this.btn_exportToXml_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new global::System.Drawing.Size(6, 39);
			this.btn_ok.Image = global::AutoComboBox.Properties.Resources.check2;
			this.btn_ok.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_ok.Name = "btn_ok";
			this.btn_ok.Size = new global::System.Drawing.Size(64, 36);
			this.btn_ok.Text = "&Ok";
			this.btn_ok.Click += new global::System.EventHandler(this.btn_ok_Click);
			this.btn_close.Image = global::AutoComboBox.Properties.Resources.delete2;
			this.btn_close.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_close.Name = "btn_close";
			this.btn_close.Size = new global::System.Drawing.Size(85, 36);
			this.btn_close.Text = "&Close";
			this.btn_close.Click += new global::System.EventHandler(this.btn_close_Click);
			this.lbl_left.Dock = global::System.Windows.Forms.DockStyle.Left;
			this.lbl_left.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("lbl_left.Image");
			this.lbl_left.ImageAlign = global::System.Drawing.ContentAlignment.TopLeft;
			this.lbl_left.Location = new global::System.Drawing.Point(0, 0);
			this.lbl_left.Name = "lbl_left";
			this.lbl_left.Size = new global::System.Drawing.Size(54, 491);
			this.lbl_left.TabIndex = 10;
			this.lbl_left.Visible = false;
			base.AcceptButton = this.btn_fakeOK;
			this.AutoScaleBaseSize = new global::System.Drawing.Size(5, 13);
			base.CancelButton = this.btn_fakeCancel;
			base.ClientSize = new global::System.Drawing.Size(742, 491);
			base.Controls.Add(this.dataGrid1);
			base.Controls.Add(this.toolStrip1);
			base.Controls.Add(this.btn_fakeOK);
			base.Controls.Add(this.btn_fakeCancel);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.lbl_left);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "DataTableView";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Data View";
			base.Load += new global::System.EventHandler(this.DataTableView_Load);
			((global::System.ComponentModel.ISupportInitialize)this.dataGrid1).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400056F RID: 1391
		private global::System.Windows.Forms.ImageList imageList1;

		// Token: 0x04000570 RID: 1392
		private global::System.Windows.Forms.Button btn_fakeCancel;

		// Token: 0x04000571 RID: 1393
		private global::System.Windows.Forms.Button btn_fakeOK;

		// Token: 0x04000572 RID: 1394
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000573 RID: 1395
		private global::System.Windows.Forms.Label lbl_left;

		// Token: 0x04000574 RID: 1396
		private global::System.Windows.Forms.ContextMenu contextMenu1;

		// Token: 0x04000575 RID: 1397
		private global::System.Windows.Forms.MenuItem MENU_GenerateSQLToUpdate;

		// Token: 0x04000576 RID: 1398
		public global::AutoComboBox.MyDataGrid dataGrid1;

		// Token: 0x04000577 RID: 1399
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x04000578 RID: 1400
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator2;

		// Token: 0x04000579 RID: 1401
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

		// Token: 0x0400057A RID: 1402
		private global::System.Windows.Forms.ToolStripButton btn_properties;

		// Token: 0x0400057B RID: 1403
		private global::System.Windows.Forms.ToolStripDropDownButton btn_import;

		// Token: 0x0400057C RID: 1404
		private global::System.Windows.Forms.ToolStripMenuItem btn_importFromXml;

		// Token: 0x0400057D RID: 1405
		private global::System.Windows.Forms.ToolStripDropDownButton btn_export;

		// Token: 0x0400057E RID: 1406
		private global::System.Windows.Forms.ToolStripMenuItem btn_exportToXml;

		// Token: 0x0400057F RID: 1407
		private global::System.Windows.Forms.ToolStripButton btn_ok;

		// Token: 0x04000580 RID: 1408
		private global::System.Windows.Forms.ToolStripButton btn_close;

		// Token: 0x04000581 RID: 1409
		private global::System.Windows.Forms.ToolStripButton toolStripButton1;

		// Token: 0x04000582 RID: 1410
		private global::System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

		// Token: 0x04000583 RID: 1411
		private global::System.Windows.Forms.ToolStripMenuItem viewRowDataToolStripMenuItem;

		// Token: 0x04000584 RID: 1412
		private global::System.ComponentModel.IContainer components;
	}
}

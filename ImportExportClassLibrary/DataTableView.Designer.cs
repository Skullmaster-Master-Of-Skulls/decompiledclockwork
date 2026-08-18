namespace ImportExportClassLibrary
{
	// Token: 0x0200003D RID: 61
	public partial class DataTableView : global::System.Windows.Forms.Form
	{
		// Token: 0x0600021B RID: 539 RVA: 0x000161FC File Offset: 0x000151FC
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0001621C File Offset: 0x0001521C
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::ImportExportClassLibrary.DataTableView));
			this.dataGrid1 = new global::AutoComboBox.MyDataGrid();
			this.imageList1 = new global::System.Windows.Forms.ImageList(this.components);
			this.btn_fakeCancel = new global::System.Windows.Forms.Button();
			this.btn_fakeOK = new global::System.Windows.Forms.Button();
			this.label1 = new global::System.Windows.Forms.Label();
			this.panelEx1 = new global::DevComponents.DotNetBar.PanelEx();
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.toolStripSeparator1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator2 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_properties = new global::System.Windows.Forms.ToolStripButton();
			this.btn_print = new global::System.Windows.Forms.ToolStripButton();
			this.btn_import = new global::System.Windows.Forms.ToolStripDropDownButton();
			this.btn_importFromXml = new global::System.Windows.Forms.ToolStripMenuItem();
			this.btn_export = new global::System.Windows.Forms.ToolStripDropDownButton();
			this.btn_exportToExcel = new global::System.Windows.Forms.ToolStripMenuItem();
			this.btn_exportToTabDelimiteredText = new global::System.Windows.Forms.ToolStripMenuItem();
			this.btn_exportToDelimiteredText = new global::System.Windows.Forms.ToolStripMenuItem();
			this.btn_exportToFormattedText = new global::System.Windows.Forms.ToolStripMenuItem();
			this.btn_exportToXml = new global::System.Windows.Forms.ToolStripMenuItem();
			this.btn_ok = new global::System.Windows.Forms.ToolStripButton();
			this.btn_cancel = new global::System.Windows.Forms.ToolStripButton();
			((global::System.ComponentModel.ISupportInitialize)this.dataGrid1).BeginInit();
			this.panelEx1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.dataGrid1.ColumnOrders = "";
			this.dataGrid1.DataMember = "";
			this.dataGrid1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.dataGrid1.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.dataGrid1.HeaderForeColor = global::System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Location = new global::System.Drawing.Point(0, 54);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.ReadOnly = true;
			this.dataGrid1.SelectedIndices = (global::System.Collections.ArrayList)componentResourceManager.GetObject("dataGrid1.SelectedIndices");
			this.dataGrid1.Size = new global::System.Drawing.Size(742, 398);
			this.dataGrid1.TabIndex = 0;
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
			this.label1.BackColor = global::System.Drawing.SystemColors.Control;
			this.label1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.label1.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label1.Location = new global::System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(742, 54);
			this.label1.TabIndex = 9;
			this.label1.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.panelEx1.AntiAlias = true;
			this.panelEx1.CanvasColor = global::System.Drawing.SystemColors.Control;
			this.panelEx1.Controls.Add(this.label1);
			this.panelEx1.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.panelEx1.Location = new global::System.Drawing.Point(0, 0);
			this.panelEx1.Name = "panelEx1";
			this.panelEx1.Size = new global::System.Drawing.Size(742, 54);
			this.panelEx1.Style.Alignment = global::System.Drawing.StringAlignment.Center;
			this.panelEx1.Style.BackColor1.ColorSchemePart = 30;
			this.panelEx1.Style.BackColor2.ColorSchemePart = 31;
			this.panelEx1.Style.Border = 1;
			this.panelEx1.Style.BorderColor.ColorSchemePart = 53;
			this.panelEx1.Style.ForeColor.ColorSchemePart = 33;
			this.panelEx1.Style.GradientAngle = 90;
			this.panelEx1.TabIndex = 10;
			this.panelEx1.Text = "panelEx1";
			this.toolStrip1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip1.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_properties,
				this.toolStripSeparator1,
				this.btn_print,
				this.btn_import,
				this.btn_export,
				this.toolStripSeparator2,
				this.btn_ok,
				this.btn_cancel
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(0, 452);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(742, 39);
			this.toolStrip1.TabIndex = 11;
			this.toolStrip1.Text = "toolStrip1";
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new global::System.Drawing.Size(6, 39);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new global::System.Drawing.Size(6, 39);
			this.btn_properties.Image = global::ImportExportClassLibrary.Properties.Resources.news_view;
			this.btn_properties.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_properties.Name = "btn_properties";
			this.btn_properties.Size = new global::System.Drawing.Size(117, 36);
			this.btn_properties.Text = "P&roperties";
			this.btn_properties.Click += new global::System.EventHandler(this.btn_properties_Click);
			this.btn_print.Image = global::ImportExportClassLibrary.Properties.Resources.printer;
			this.btn_print.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_print.Name = "btn_print";
			this.btn_print.Size = new global::System.Drawing.Size(76, 36);
			this.btn_print.Text = "&Print";
			this.btn_print.Click += new global::System.EventHandler(this.btn_print_Click);
			this.btn_import.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_importFromXml
			});
			this.btn_import.Image = global::ImportExportClassLibrary.Properties.Resources.import2;
			this.btn_import.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_import.Name = "btn_import";
			this.btn_import.Size = new global::System.Drawing.Size(96, 36);
			this.btn_import.Text = "&Import";
			this.btn_importFromXml.Name = "btn_importFromXml";
			this.btn_importFromXml.Size = new global::System.Drawing.Size(193, 22);
			this.btn_importFromXml.Text = "Import from &xml";
			this.btn_importFromXml.Click += new global::System.EventHandler(this.btn_importFromXml_Click);
			this.btn_export.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_exportToExcel,
				this.btn_exportToTabDelimiteredText,
				this.btn_exportToDelimiteredText,
				this.btn_exportToFormattedText,
				this.btn_exportToXml
			});
			this.btn_export.Image = global::ImportExportClassLibrary.Properties.Resources.export2;
			this.btn_export.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_export.Name = "btn_export";
			this.btn_export.Size = new global::System.Drawing.Size(98, 36);
			this.btn_export.Text = "&Export";
			this.btn_exportToExcel.Name = "btn_exportToExcel";
			this.btn_exportToExcel.Size = new global::System.Drawing.Size(319, 22);
			this.btn_exportToExcel.Text = "Export to &Excel";
			this.btn_exportToExcel.Click += new global::System.EventHandler(this.btn_exportToExcel_Click);
			this.btn_exportToTabDelimiteredText.Name = "btn_exportToTabDelimiteredText";
			this.btn_exportToTabDelimiteredText.Size = new global::System.Drawing.Size(319, 22);
			this.btn_exportToTabDelimiteredText.Text = "Export to &tab delimitered text (.txt)";
			this.btn_exportToTabDelimiteredText.Click += new global::System.EventHandler(this.btn_exportToTabDelimiteredText_Click);
			this.btn_exportToDelimiteredText.Name = "btn_exportToDelimiteredText";
			this.btn_exportToDelimiteredText.Size = new global::System.Drawing.Size(319, 22);
			this.btn_exportToDelimiteredText.Text = "Export to &delimitered text (.csv)";
			this.btn_exportToDelimiteredText.Click += new global::System.EventHandler(this.btn_exportToDelimiteredText_Click);
			this.btn_exportToFormattedText.Name = "btn_exportToFormattedText";
			this.btn_exportToFormattedText.Size = new global::System.Drawing.Size(319, 22);
			this.btn_exportToFormattedText.Text = "Export to &formatted text";
			this.btn_exportToFormattedText.Click += new global::System.EventHandler(this.exportToformattedTextToolStripMenuItem_Click);
			this.btn_exportToXml.Name = "btn_exportToXml";
			this.btn_exportToXml.Size = new global::System.Drawing.Size(319, 22);
			this.btn_exportToXml.Text = "Export to &xml";
			this.btn_exportToXml.Click += new global::System.EventHandler(this.btn_exportToXml_Click);
			this.btn_ok.Image = global::ImportExportClassLibrary.Properties.Resources.check2;
			this.btn_ok.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_ok.Name = "btn_ok";
			this.btn_ok.Size = new global::System.Drawing.Size(64, 36);
			this.btn_ok.Text = "&Ok";
			this.btn_ok.Click += new global::System.EventHandler(this.btn_ok_Click);
			this.btn_cancel.Image = global::ImportExportClassLibrary.Properties.Resources.delete2;
			this.btn_cancel.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(93, 36);
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
			base.AcceptButton = this.btn_fakeOK;
			this.AutoScaleBaseSize = new global::System.Drawing.Size(5, 13);
			base.CancelButton = this.btn_fakeCancel;
			base.ClientSize = new global::System.Drawing.Size(742, 491);
			base.Controls.Add(this.dataGrid1);
			base.Controls.Add(this.toolStrip1);
			base.Controls.Add(this.panelEx1);
			base.Controls.Add(this.btn_fakeOK);
			base.Controls.Add(this.btn_fakeCancel);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "DataTableView";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Data View";
			base.Load += new global::System.EventHandler(this.DataTableView_Load);
			((global::System.ComponentModel.ISupportInitialize)this.dataGrid1).EndInit();
			this.panelEx1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400010B RID: 267
		public global::AutoComboBox.MyDataGrid dataGrid1;

		// Token: 0x0400010C RID: 268
		private global::System.Windows.Forms.ImageList imageList1;

		// Token: 0x0400010D RID: 269
		private global::System.Windows.Forms.Button btn_fakeCancel;

		// Token: 0x0400010E RID: 270
		private global::System.Windows.Forms.Button btn_fakeOK;

		// Token: 0x0400010F RID: 271
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000110 RID: 272
		private global::DevComponents.DotNetBar.PanelEx panelEx1;

		// Token: 0x04000111 RID: 273
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x04000112 RID: 274
		private global::System.Windows.Forms.ToolStripButton btn_properties;

		// Token: 0x04000113 RID: 275
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

		// Token: 0x04000114 RID: 276
		private global::System.Windows.Forms.ToolStripButton btn_print;

		// Token: 0x04000115 RID: 277
		private global::System.Windows.Forms.ToolStripDropDownButton btn_import;

		// Token: 0x04000116 RID: 278
		private global::System.Windows.Forms.ToolStripDropDownButton btn_export;

		// Token: 0x04000117 RID: 279
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator2;

		// Token: 0x04000118 RID: 280
		private global::System.Windows.Forms.ToolStripButton btn_ok;

		// Token: 0x04000119 RID: 281
		private global::System.Windows.Forms.ToolStripButton btn_cancel;

		// Token: 0x0400011A RID: 282
		private global::System.Windows.Forms.ToolStripMenuItem btn_importFromXml;

		// Token: 0x0400011B RID: 283
		private global::System.Windows.Forms.ToolStripMenuItem btn_exportToFormattedText;

		// Token: 0x0400011C RID: 284
		private global::System.Windows.Forms.ToolStripMenuItem btn_exportToXml;

		// Token: 0x0400011D RID: 285
		private global::System.Windows.Forms.ToolStripMenuItem btn_exportToExcel;

		// Token: 0x0400011E RID: 286
		private global::System.Windows.Forms.ToolStripMenuItem btn_exportToTabDelimiteredText;

		// Token: 0x0400011F RID: 287
		private global::System.Windows.Forms.ToolStripMenuItem btn_exportToDelimiteredText;

		// Token: 0x04000120 RID: 288
		private global::System.ComponentModel.IContainer components;
	}
}

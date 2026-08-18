namespace ReportFunctions
{
	// Token: 0x02000044 RID: 68
	public partial class VariablesInput : global::System.Windows.Forms.Form
	{
		// Token: 0x060003F5 RID: 1013 RVA: 0x00045094 File Offset: 0x00044094
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.DisposePanel(this.p_custom);
				if (this.components != null)
				{
					this.components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x000450DC File Offset: 0x000440DC
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::ReportFunctions.VariablesInput));
			this.label1 = new global::System.Windows.Forms.Label();
			this.p_data = new global::AutoComboBox.MyPanel();
			this.imageList1 = new global::System.Windows.Forms.ImageList(this.components);
			this.btn_fakeCancel = new global::System.Windows.Forms.Button();
			this.p_custom = new global::System.Windows.Forms.Panel();
			this.webBrowser1 = new global::System.Windows.Forms.WebBrowser();
			this.cm_customCheckboxes = new global::System.Windows.Forms.ContextMenu();
			this.MENU_customCheckSelectAll = new global::System.Windows.Forms.MenuItem();
			this.MENU_customChecksClearAll = new global::System.Windows.Forms.MenuItem();
			this.btn_fakeOK = new global::System.Windows.Forms.Button();
			this.imageList3 = new global::System.Windows.Forms.ImageList(this.components);
			this.imageList2 = new global::System.Windows.Forms.ImageList(this.components);
			this.ts_custom = new global::System.Windows.Forms.ToolStrip();
			this.btn_customSelectAll = new global::System.Windows.Forms.ToolStripButton();
			this.btn_selectNone = new global::System.Windows.Forms.ToolStripButton();
			this.toolStrip2 = new global::System.Windows.Forms.ToolStrip();
			this.btn_runReport = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_cancel = new global::System.Windows.Forms.ToolStripButton();
			this.expandableSplitter1 = new global::DevComponents.DotNetBar.ExpandableSplitter();
			this.p_right = new global::System.Windows.Forms.Panel();
			this.p_custom.SuspendLayout();
			this.ts_custom.SuspendLayout();
			this.toolStrip2.SuspendLayout();
			this.p_right.SuspendLayout();
			base.SuspendLayout();
			this.label1.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new global::System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(457, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "Please specify the report parameters below:";
			this.label1.TextAlign = global::System.Drawing.ContentAlignment.BottomLeft;
			this.p_data.AutoScroll = true;
			this.p_data.BackColor = global::System.Drawing.SystemColors.Control;
			this.p_data.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.p_data.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.p_data.Location = new global::System.Drawing.Point(0, 20);
			this.p_data.Name = "p_data";
			this.p_data.Size = new global::System.Drawing.Size(457, 427);
			this.p_data.TabIndex = 1;
			this.imageList1.ImageStream = (global::System.Windows.Forms.ImageListStreamer)componentResourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = global::System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "");
			this.imageList1.Images.SetKeyName(1, "");
			this.imageList1.Images.SetKeyName(2, "");
			this.imageList1.Images.SetKeyName(3, "");
			this.btn_fakeCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btn_fakeCancel.Location = new global::System.Drawing.Point(180, 0);
			this.btn_fakeCancel.Name = "btn_fakeCancel";
			this.btn_fakeCancel.Size = new global::System.Drawing.Size(0, 0);
			this.btn_fakeCancel.TabIndex = 4;
			this.btn_fakeCancel.Text = "button1";
			this.btn_fakeCancel.Click += new global::System.EventHandler(this.btn_fakeCancel_Click);
			this.p_custom.AutoScroll = true;
			this.p_custom.BackColor = global::System.Drawing.SystemColors.ControlLight;
			this.p_custom.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.p_custom.Controls.Add(this.webBrowser1);
			this.p_custom.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.p_custom.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.p_custom.Location = new global::System.Drawing.Point(0, 0);
			this.p_custom.Name = "p_custom";
			this.p_custom.Padding = new global::System.Windows.Forms.Padding(3);
			this.p_custom.Size = new global::System.Drawing.Size(275, 447);
			this.p_custom.TabIndex = 5;
			this.webBrowser1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.webBrowser1.Location = new global::System.Drawing.Point(3, 3);
			this.webBrowser1.MinimumSize = new global::System.Drawing.Size(20, 20);
			this.webBrowser1.Name = "webBrowser1";
			this.webBrowser1.Size = new global::System.Drawing.Size(265, 437);
			this.webBrowser1.TabIndex = 0;
			this.webBrowser1.Navigating += new global::System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webBrowser1_Navigating);
			this.cm_customCheckboxes.MenuItems.AddRange(new global::System.Windows.Forms.MenuItem[]
			{
				this.MENU_customCheckSelectAll,
				this.MENU_customChecksClearAll
			});
			this.MENU_customCheckSelectAll.Index = 0;
			this.MENU_customCheckSelectAll.Text = "Select &all";
			this.MENU_customCheckSelectAll.Click += new global::System.EventHandler(this.MENU_customCheckSelectAll_Click);
			this.MENU_customChecksClearAll.Index = 1;
			this.MENU_customChecksClearAll.Text = "Select &none";
			this.MENU_customChecksClearAll.Click += new global::System.EventHandler(this.MENU_customChecksClearAll_Click);
			this.btn_fakeOK.Location = new global::System.Drawing.Point(108, 0);
			this.btn_fakeOK.Name = "btn_fakeOK";
			this.btn_fakeOK.Size = new global::System.Drawing.Size(0, 0);
			this.btn_fakeOK.TabIndex = 6;
			this.btn_fakeOK.Text = "button1";
			this.btn_fakeOK.Click += new global::System.EventHandler(this.btn_fakeOK_Click);
			this.imageList3.ImageStream = (global::System.Windows.Forms.ImageListStreamer)componentResourceManager.GetObject("imageList3.ImageStream");
			this.imageList3.TransparentColor = global::System.Drawing.Color.Transparent;
			this.imageList3.Images.SetKeyName(0, "");
			this.imageList3.Images.SetKeyName(1, "");
			this.imageList3.Images.SetKeyName(2, "");
			this.imageList3.Images.SetKeyName(3, "");
			this.imageList2.ImageStream = (global::System.Windows.Forms.ImageListStreamer)componentResourceManager.GetObject("imageList2.ImageStream");
			this.imageList2.TransparentColor = global::System.Drawing.Color.Transparent;
			this.imageList2.Images.SetKeyName(0, "");
			this.imageList2.Images.SetKeyName(1, "");
			this.imageList2.Images.SetKeyName(2, "");
			this.imageList2.Images.SetKeyName(3, "");
			this.ts_custom.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.ts_custom.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.ts_custom.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.ts_custom.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.ts_custom.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_customSelectAll,
				this.btn_selectNone
			});
			this.ts_custom.Location = new global::System.Drawing.Point(0, 447);
			this.ts_custom.Name = "ts_custom";
			this.ts_custom.Size = new global::System.Drawing.Size(275, 39);
			this.ts_custom.TabIndex = 6;
			this.ts_custom.TabStop = true;
			this.btn_customSelectAll.Image = global::ReportFunctions.Properties.Resources.document_check;
			this.btn_customSelectAll.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_customSelectAll.Name = "btn_customSelectAll";
			this.btn_customSelectAll.Size = new global::System.Drawing.Size(86, 36);
			this.btn_customSelectAll.Text = "Select &all";
			this.btn_customSelectAll.Click += new global::System.EventHandler(this.btn_customSelectAll_Click);
			this.btn_selectNone.Image = global::ReportFunctions.Properties.Resources.document_plain;
			this.btn_selectNone.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_selectNone.Name = "btn_selectNone";
			this.btn_selectNone.Size = new global::System.Drawing.Size(100, 36);
			this.btn_selectNone.Text = "Select &none";
			this.btn_selectNone.Click += new global::System.EventHandler(this.btn_selectNone_Click);
			this.toolStrip2.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip2.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip2.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip2.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip2.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_runReport,
				this.toolStripSeparator1,
				this.btn_cancel
			});
			this.toolStrip2.Location = new global::System.Drawing.Point(0, 447);
			this.toolStrip2.Name = "toolStrip2";
			this.toolStrip2.Size = new global::System.Drawing.Size(467, 39);
			this.toolStrip2.TabIndex = 9;
			this.toolStrip2.TabStop = true;
			this.btn_runReport.Image = global::ReportFunctions.Properties.Resources.check2;
			this.btn_runReport.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_runReport.Name = "btn_runReport";
			this.btn_runReport.Size = new global::System.Drawing.Size(116, 36);
			this.btn_runReport.Text = "&Run report";
			this.btn_runReport.Click += new global::System.EventHandler(this.btn_runReport_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new global::System.Drawing.Size(6, 39);
			this.btn_cancel.Image = global::ReportFunctions.Properties.Resources.delete2;
			this.btn_cancel.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(93, 36);
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
			this.expandableSplitter1.BackColor2 = global::System.Drawing.Color.FromArgb(0, 45, 150);
			this.expandableSplitter1.BackColor2SchemePart = 53;
			this.expandableSplitter1.BackColorSchemePart = 51;
			this.expandableSplitter1.Dock = global::System.Windows.Forms.DockStyle.Right;
			this.expandableSplitter1.ExpandableControl = this.p_right;
			this.expandableSplitter1.ExpandFillColor = global::System.Drawing.Color.FromArgb(0, 45, 150);
			this.expandableSplitter1.ExpandFillColorSchemePart = 53;
			this.expandableSplitter1.ExpandLineColor = global::System.Drawing.SystemColors.ControlText;
			this.expandableSplitter1.ExpandLineColorSchemePart = 40;
			this.expandableSplitter1.GripDarkColor = global::System.Drawing.SystemColors.ControlText;
			this.expandableSplitter1.GripDarkColorSchemePart = 40;
			this.expandableSplitter1.GripLightColor = global::System.Drawing.Color.FromArgb(223, 237, 254);
			this.expandableSplitter1.GripLightColorSchemePart = 0;
			this.expandableSplitter1.HotBackColor = global::System.Drawing.Color.FromArgb(254, 142, 75);
			this.expandableSplitter1.HotBackColor2 = global::System.Drawing.Color.FromArgb(255, 207, 139);
			this.expandableSplitter1.HotBackColor2SchemePart = 35;
			this.expandableSplitter1.HotBackColorSchemePart = 34;
			this.expandableSplitter1.HotExpandFillColor = global::System.Drawing.Color.FromArgb(0, 45, 150);
			this.expandableSplitter1.HotExpandFillColorSchemePart = 53;
			this.expandableSplitter1.HotExpandLineColor = global::System.Drawing.SystemColors.ControlText;
			this.expandableSplitter1.HotExpandLineColorSchemePart = 40;
			this.expandableSplitter1.HotGripDarkColor = global::System.Drawing.Color.FromArgb(0, 45, 150);
			this.expandableSplitter1.HotGripDarkColorSchemePart = 53;
			this.expandableSplitter1.HotGripLightColor = global::System.Drawing.Color.FromArgb(223, 237, 254);
			this.expandableSplitter1.HotGripLightColorSchemePart = 0;
			this.expandableSplitter1.Location = new global::System.Drawing.Point(457, 0);
			this.expandableSplitter1.Name = "expandableSplitter1";
			this.expandableSplitter1.Size = new global::System.Drawing.Size(10, 447);
			this.expandableSplitter1.TabIndex = 10;
			this.expandableSplitter1.TabStop = false;
			this.p_right.Controls.Add(this.p_custom);
			this.p_right.Controls.Add(this.ts_custom);
			this.p_right.Dock = global::System.Windows.Forms.DockStyle.Right;
			this.p_right.Location = new global::System.Drawing.Point(467, 0);
			this.p_right.Name = "p_right";
			this.p_right.Size = new global::System.Drawing.Size(275, 486);
			this.p_right.TabIndex = 11;
			this.AutoScaleBaseSize = new global::System.Drawing.Size(6, 15);
			base.ClientSize = new global::System.Drawing.Size(742, 486);
			base.Controls.Add(this.p_data);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.expandableSplitter1);
			base.Controls.Add(this.btn_fakeOK);
			base.Controls.Add(this.btn_fakeCancel);
			base.Controls.Add(this.toolStrip2);
			base.Controls.Add(this.p_right);
			this.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "VariablesInput";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Report Parameters";
			base.Load += new global::System.EventHandler(this.VariablesInput_Load);
			base.Closing += new global::System.ComponentModel.CancelEventHandler(this.VariablesInput_Closing);
			base.KeyUp += new global::System.Windows.Forms.KeyEventHandler(this.VariablesInput_KeyUp);
			this.p_custom.ResumeLayout(false);
			this.ts_custom.ResumeLayout(false);
			this.ts_custom.PerformLayout();
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			this.p_right.ResumeLayout(false);
			this.p_right.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040001FA RID: 506
		private global::System.Windows.Forms.Label label1;

		// Token: 0x040001FB RID: 507
		private global::AutoComboBox.MyPanel p_data;

		// Token: 0x040001FC RID: 508
		private global::System.Windows.Forms.ImageList imageList1;

		// Token: 0x040001FD RID: 509
		private global::System.Windows.Forms.Button btn_fakeCancel;

		// Token: 0x040001FE RID: 510
		private global::System.Windows.Forms.Panel p_custom;

		// Token: 0x040001FF RID: 511
		private global::System.Windows.Forms.ContextMenu cm_customCheckboxes;

		// Token: 0x04000200 RID: 512
		private global::System.Windows.Forms.MenuItem MENU_customCheckSelectAll;

		// Token: 0x04000201 RID: 513
		private global::System.Windows.Forms.MenuItem MENU_customChecksClearAll;

		// Token: 0x04000202 RID: 514
		private global::System.Windows.Forms.Button btn_fakeOK;

		// Token: 0x04000203 RID: 515
		private global::System.Windows.Forms.ImageList imageList2;

		// Token: 0x04000204 RID: 516
		private global::System.Windows.Forms.ImageList imageList3;

		// Token: 0x04000205 RID: 517
		private global::System.Windows.Forms.ToolStrip ts_custom;

		// Token: 0x04000206 RID: 518
		private global::System.Windows.Forms.ToolStripButton btn_customSelectAll;

		// Token: 0x04000207 RID: 519
		private global::System.Windows.Forms.ToolStripButton btn_selectNone;

		// Token: 0x04000208 RID: 520
		private global::System.Windows.Forms.ToolStrip toolStrip2;

		// Token: 0x04000209 RID: 521
		private global::System.Windows.Forms.ToolStripButton btn_runReport;

		// Token: 0x0400020A RID: 522
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

		// Token: 0x0400020B RID: 523
		private global::System.Windows.Forms.ToolStripButton btn_cancel;

		// Token: 0x0400020C RID: 524
		private global::System.Windows.Forms.WebBrowser webBrowser1;

		// Token: 0x0400020D RID: 525
		private global::DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;

		// Token: 0x0400020E RID: 526
		private global::System.Windows.Forms.Panel p_right;

		// Token: 0x0400020F RID: 527
		private global::System.ComponentModel.IContainer components;
	}
}

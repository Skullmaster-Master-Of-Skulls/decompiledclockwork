namespace AutoComboBox
{
	// Token: 0x020000E9 RID: 233
	public partial class InputListView : global::System.Windows.Forms.Form
	{
		// Token: 0x06000917 RID: 2327 RVA: 0x00045FE4 File Offset: 0x00044FE4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.lv != null)
				{
					this.lv.DrawItem -= new global::System.Windows.Forms.DrawItemEventHandler(this.lv_DrawItem);
				}
				if (this.components != null)
				{
					this.components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x00046048 File Offset: 0x00045048
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::AutoComboBox.InputListView));
			this.columnHeader1 = new global::System.Windows.Forms.ColumnHeader();
			this.lbl_caption = new global::System.Windows.Forms.Label();
			this.lv = new global::AutoComboBox.ListViewEx();
			this.imageList1 = new global::System.Windows.Forms.ImageList(this.components);
			this.btn_fakeCancel = new global::System.Windows.Forms.Button();
			this.p_caption = new global::System.Windows.Forms.Panel();
			this.btn_fakeOk = new global::System.Windows.Forms.Button();
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.btn_export = new global::System.Windows.Forms.ToolStripDropDownButton();
			this.btn_exportToFormattedText = new global::System.Windows.Forms.ToolStripMenuItem();
			this.btn_exportToTabDelimiteredText = new global::System.Windows.Forms.ToolStripMenuItem();
			this.btn_exportToDelimiteredText = new global::System.Windows.Forms.ToolStripMenuItem();
			this.btn_exportToAccess = new global::System.Windows.Forms.ToolStripMenuItem();
			this.btn_exportToExcel = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_ok = new global::System.Windows.Forms.ToolStripButton();
			this.btn_cancel = new global::System.Windows.Forms.ToolStripButton();
			this.chk = new global::DevComponents.DotNetBar.Controls.CheckBoxX();
			this.p_caption.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.lbl_caption.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.lbl_caption.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lbl_caption.Location = new global::System.Drawing.Point(0, 4);
			this.lbl_caption.Name = "lbl_caption";
			this.lbl_caption.Size = new global::System.Drawing.Size(634, 24);
			this.lbl_caption.TabIndex = 0;
			this.lbl_caption.Text = "label1";
			this.lbl_caption.TextChanged += new global::System.EventHandler(this.lbl_caption_TextChanged);
			this.lv.BackColourSelected = global::System.Drawing.Color.LightBlue;
			this.lv.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.lv.DrawMode = global::System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.lv.EnterTriggersDoubleClickEvent = false;
			this.lv.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lv.FullRowSelect = true;
			this.lv.GridLines = true;
			this.lv.HideSelection = false;
			this.lv.IsFileList = false;
			this.lv.ItemHeight = 22;
			this.lv.Location = new global::System.Drawing.Point(2, 73);
			this.lv.Name = "lv";
			this.lv.Size = new global::System.Drawing.Size(638, 300);
			this.lv.TabIndex = 1;
			this.lv.Tag2 = null;
			this.lv.UseCompatibleStateImageBehavior = false;
			this.lv.View = global::System.Windows.Forms.View.Details;
			this.lv.SizeChanged += new global::System.EventHandler(this.lv_SizeChanged);
			this.lv.MouseUp += new global::System.Windows.Forms.MouseEventHandler(this.lv_MouseUp);
			this.lv.MeasureItem += new global::System.Windows.Forms.MeasureItemEventHandler(this.lv_MeasureItem);
			this.lv.DrawItem += new global::System.Windows.Forms.DrawItemEventHandler(this.lv_DrawItem);
			this.lv.DoubleClick += new global::System.EventHandler(this.lv_DoubleClick);
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
			this.btn_fakeCancel.Location = new global::System.Drawing.Point(104, 0);
			this.btn_fakeCancel.Name = "btn_fakeCancel";
			this.btn_fakeCancel.Size = new global::System.Drawing.Size(0, 0);
			this.btn_fakeCancel.TabIndex = 7;
			this.btn_fakeCancel.Text = "button1";
			this.btn_fakeCancel.Click += new global::System.EventHandler(this.btn_fakeCancel_Click);
			this.p_caption.Controls.Add(this.lbl_caption);
			this.p_caption.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.p_caption.Location = new global::System.Drawing.Point(2, 2);
			this.p_caption.Name = "p_caption";
			this.p_caption.Padding = new global::System.Windows.Forms.Padding(0, 4, 4, 4);
			this.p_caption.Size = new global::System.Drawing.Size(638, 32);
			this.p_caption.TabIndex = 8;
			this.btn_fakeOk.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btn_fakeOk.Location = new global::System.Drawing.Point(321, 207);
			this.btn_fakeOk.Name = "btn_fakeOk";
			this.btn_fakeOk.Size = new global::System.Drawing.Size(0, 0);
			this.btn_fakeOk.TabIndex = 9;
			this.btn_fakeOk.Text = "button1";
			this.btn_fakeOk.Click += new global::System.EventHandler(this.btn_fakeOk_Click);
			this.toolStrip1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip1.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_export,
				this.toolStripSeparator1,
				this.btn_ok,
				this.btn_cancel
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(2, 373);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(638, 39);
			this.toolStrip1.TabIndex = 10;
			this.toolStrip1.TabStop = true;
			this.btn_export.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_exportToFormattedText,
				this.btn_exportToTabDelimiteredText,
				this.btn_exportToDelimiteredText,
				this.btn_exportToAccess,
				this.btn_exportToExcel
			});
			this.btn_export.Image = global::AutoComboBox.Properties.Resources.export2;
			this.btn_export.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_export.Name = "btn_export";
			this.btn_export.Size = new global::System.Drawing.Size(98, 36);
			this.btn_export.Text = "E&xport";
			this.btn_exportToFormattedText.Name = "btn_exportToFormattedText";
			this.btn_exportToFormattedText.Size = new global::System.Drawing.Size(307, 22);
			this.btn_exportToFormattedText.Text = "Export to &formatted text (.txt)";
			this.btn_exportToFormattedText.Click += new global::System.EventHandler(this.btn_exportToFormattedText_Click);
			this.btn_exportToTabDelimiteredText.Name = "btn_exportToTabDelimiteredText";
			this.btn_exportToTabDelimiteredText.Size = new global::System.Drawing.Size(307, 22);
			this.btn_exportToTabDelimiteredText.Text = "Export to &tab delimitered text (.txt)";
			this.btn_exportToTabDelimiteredText.Click += new global::System.EventHandler(this.btn_exportToTabDelimiteredText_Click);
			this.btn_exportToDelimiteredText.Name = "btn_exportToDelimiteredText";
			this.btn_exportToDelimiteredText.Size = new global::System.Drawing.Size(307, 22);
			this.btn_exportToDelimiteredText.Text = "Export to &delimitered text (.csv)";
			this.btn_exportToDelimiteredText.Click += new global::System.EventHandler(this.btn_exportToDelimiteredText_Click);
			this.btn_exportToAccess.Name = "btn_exportToAccess";
			this.btn_exportToAccess.Size = new global::System.Drawing.Size(307, 22);
			this.btn_exportToAccess.Text = "Export to &Access";
			this.btn_exportToAccess.Click += new global::System.EventHandler(this.btn_exportToAccess_Click);
			this.btn_exportToExcel.Name = "btn_exportToExcel";
			this.btn_exportToExcel.Size = new global::System.Drawing.Size(307, 22);
			this.btn_exportToExcel.Text = "Export to &Excel";
			this.btn_exportToExcel.Click += new global::System.EventHandler(this.btn_exportToExcel_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new global::System.Drawing.Size(6, 39);
			this.btn_ok.Image = global::AutoComboBox.Properties.Resources.check2;
			this.btn_ok.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_ok.Name = "btn_ok";
			this.btn_ok.Size = new global::System.Drawing.Size(64, 36);
			this.btn_ok.Text = "&Ok";
			this.btn_ok.Click += new global::System.EventHandler(this.btn_ok_Click);
			this.btn_cancel.Image = global::AutoComboBox.Properties.Resources.delete2;
			this.btn_cancel.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(93, 36);
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
			this.chk.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.chk.Location = new global::System.Drawing.Point(2, 34);
			this.chk.Name = "chk";
			this.chk.Size = new global::System.Drawing.Size(638, 39);
			this.chk.TabIndex = 11;
			this.chk.Text = "Email this letter";
			this.chk.Visible = false;
			base.AcceptButton = this.btn_fakeOk;
			this.AutoScaleBaseSize = new global::System.Drawing.Size(9, 22);
			base.CancelButton = this.btn_fakeCancel;
			base.ClientSize = new global::System.Drawing.Size(642, 414);
			base.Controls.Add(this.lv);
			base.Controls.Add(this.chk);
			base.Controls.Add(this.toolStrip1);
			base.Controls.Add(this.btn_fakeOk);
			base.Controls.Add(this.p_caption);
			base.Controls.Add(this.btn_fakeCancel);
			this.Font = new global::System.Drawing.Font("Arial", 14.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "InputListView";
			base.Padding = new global::System.Windows.Forms.Padding(2);
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "List";
			base.Load += new global::System.EventHandler(this.InputListView_Load);
			base.SizeChanged += new global::System.EventHandler(this.InputListView_SizeChanged);
			base.KeyPress += new global::System.Windows.Forms.KeyPressEventHandler(this.InputListView_KeyPress);
			base.KeyUp += new global::System.Windows.Forms.KeyEventHandler(this.InputListView_KeyUp);
			this.p_caption.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400068F RID: 1679
		private global::System.Windows.Forms.ColumnHeader columnHeader1;

		// Token: 0x04000690 RID: 1680
		private global::System.Windows.Forms.Label lbl_caption;

		// Token: 0x04000691 RID: 1681
		private global::AutoComboBox.ListViewEx lv;

		// Token: 0x04000692 RID: 1682
		private global::System.Windows.Forms.ImageList imageList1;

		// Token: 0x04000693 RID: 1683
		private global::System.Windows.Forms.Button btn_fakeCancel;

		// Token: 0x04000694 RID: 1684
		private global::System.Windows.Forms.Panel p_caption;

		// Token: 0x04000695 RID: 1685
		private global::System.Windows.Forms.Button btn_fakeOk;

		// Token: 0x04000696 RID: 1686
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x04000697 RID: 1687
		private global::System.Windows.Forms.ToolStripDropDownButton btn_export;

		// Token: 0x04000698 RID: 1688
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

		// Token: 0x04000699 RID: 1689
		private global::System.Windows.Forms.ToolStripButton btn_ok;

		// Token: 0x0400069A RID: 1690
		private global::System.Windows.Forms.ToolStripButton btn_cancel;

		// Token: 0x0400069B RID: 1691
		private global::System.Windows.Forms.ToolStripMenuItem btn_exportToFormattedText;

		// Token: 0x0400069C RID: 1692
		private global::System.Windows.Forms.ToolStripMenuItem btn_exportToTabDelimiteredText;

		// Token: 0x0400069D RID: 1693
		private global::System.Windows.Forms.ToolStripMenuItem btn_exportToDelimiteredText;

		// Token: 0x0400069E RID: 1694
		private global::System.Windows.Forms.ToolStripMenuItem btn_exportToAccess;

		// Token: 0x0400069F RID: 1695
		private global::System.Windows.Forms.ToolStripMenuItem btn_exportToExcel;

		// Token: 0x040006A0 RID: 1696
		private global::DevComponents.DotNetBar.Controls.CheckBoxX chk;

		// Token: 0x040006A1 RID: 1697
		private global::System.ComponentModel.IContainer components;
	}
}

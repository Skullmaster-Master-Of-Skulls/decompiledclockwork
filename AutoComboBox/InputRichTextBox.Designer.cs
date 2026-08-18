namespace AutoComboBox
{
	// Token: 0x0200007C RID: 124
	public partial class InputRichTextBox : global::System.Windows.Forms.Form
	{
		// Token: 0x060004E3 RID: 1251 RVA: 0x0002738C File Offset: 0x0002638C
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

		// Token: 0x060004E4 RID: 1252 RVA: 0x000273C8 File Offset: 0x000263C8
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::AutoComboBox.InputRichTextBox));
			this.rtf = new global::AutoComboBox.RichTextBoxPrintCtrl();
			this.label1 = new global::System.Windows.Forms.Label();
			this.imageList1 = new global::System.Windows.Forms.ImageList(this.components);
			this.txt = new global::System.Windows.Forms.TextBox();
			this.expandableSplitter1 = new global::DevComponents.DotNetBar.ExpandableSplitter();
			this.label2 = new global::System.Windows.Forms.Label();
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.btn_OK = new global::System.Windows.Forms.ToolStripButton();
			this.btn_cancel = new global::System.Windows.Forms.ToolStripButton();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.rtf.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.rtf.HiglightColor = global::AutoComboBox.RtfColor.White;
			this.rtf.Location = new global::System.Drawing.Point(0, 24);
			this.rtf.Name = "rtf";
			this.rtf.Size = new global::System.Drawing.Size(592, 262);
			this.rtf.TabIndex = 0;
			this.rtf.Text = "";
			this.rtf.TextColor = global::AutoComboBox.RtfColor.Black;
			this.label1.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new global::System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(592, 24);
			this.label1.TabIndex = 6;
			this.label1.Text = "Please enter a value:";
			this.label1.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.imageList1.ImageStream = (global::System.Windows.Forms.ImageListStreamer)componentResourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = global::System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "");
			this.imageList1.Images.SetKeyName(1, "");
			this.imageList1.Images.SetKeyName(2, "");
			this.imageList1.Images.SetKeyName(3, "");
			this.imageList1.Images.SetKeyName(4, "");
			this.imageList1.Images.SetKeyName(5, "");
			this.txt.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.txt.Location = new global::System.Drawing.Point(0, 320);
			this.txt.Multiline = true;
			this.txt.Name = "txt";
			this.txt.Size = new global::System.Drawing.Size(592, 55);
			this.txt.TabIndex = 8;
			this.expandableSplitter1.BackColor2 = global::System.Drawing.Color.FromArgb(0, 45, 150);
			this.expandableSplitter1.BackColor2SchemePart = 53;
			this.expandableSplitter1.BackColorSchemePart = 51;
			this.expandableSplitter1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.expandableSplitter1.ExpandFillColor = global::System.Drawing.Color.FromArgb(0, 45, 150);
			this.expandableSplitter1.ExpandFillColorSchemePart = 53;
			this.expandableSplitter1.ExpandLineColor = global::System.Drawing.SystemColors.ControlText;
			this.expandableSplitter1.ExpandLineColorSchemePart = 40;
			this.expandableSplitter1.GripDarkColor = global::System.Drawing.SystemColors.ControlText;
			this.expandableSplitter1.GripDarkColorSchemePart = 40;
			this.expandableSplitter1.GripLightColor = global::System.Drawing.Color.FromArgb(216, 236, 248);
			this.expandableSplitter1.GripLightColorSchemePart = 0;
			this.expandableSplitter1.HotBackColor = global::System.Drawing.Color.FromArgb(248, 140, 72);
			this.expandableSplitter1.HotBackColor2 = global::System.Drawing.Color.FromArgb(248, 204, 136);
			this.expandableSplitter1.HotBackColor2SchemePart = 35;
			this.expandableSplitter1.HotBackColorSchemePart = 34;
			this.expandableSplitter1.HotExpandFillColor = global::System.Drawing.Color.FromArgb(0, 45, 150);
			this.expandableSplitter1.HotExpandFillColorSchemePart = 53;
			this.expandableSplitter1.HotExpandLineColor = global::System.Drawing.SystemColors.ControlText;
			this.expandableSplitter1.HotExpandLineColorSchemePart = 40;
			this.expandableSplitter1.HotGripDarkColor = global::System.Drawing.Color.FromArgb(0, 45, 150);
			this.expandableSplitter1.HotGripDarkColorSchemePart = 53;
			this.expandableSplitter1.HotGripLightColor = global::System.Drawing.Color.FromArgb(216, 236, 248);
			this.expandableSplitter1.HotGripLightColorSchemePart = 0;
			this.expandableSplitter1.Location = new global::System.Drawing.Point(0, 286);
			this.expandableSplitter1.Name = "expandableSplitter1";
			this.expandableSplitter1.Size = new global::System.Drawing.Size(592, 16);
			this.expandableSplitter1.TabIndex = 9;
			this.expandableSplitter1.TabStop = false;
			this.label2.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.label2.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label2.Location = new global::System.Drawing.Point(0, 302);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(592, 18);
			this.label2.TabIndex = 10;
			this.label2.Text = "Enter your reply:";
			this.label2.TextAlign = global::System.Drawing.ContentAlignment.BottomLeft;
			this.toolStrip1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip1.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_OK,
				this.btn_cancel
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(0, 375);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(592, 39);
			this.toolStrip1.TabIndex = 11;
			this.toolStrip1.Text = "toolStrip1";
			this.btn_OK.Image = global::AutoComboBox.Properties.Resources.check2;
			this.btn_OK.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_OK.Name = "btn_OK";
			this.btn_OK.Size = new global::System.Drawing.Size(64, 36);
			this.btn_OK.Text = "&Ok";
			this.btn_OK.Click += new global::System.EventHandler(this.btn_OK_Click);
			this.btn_cancel.Image = global::AutoComboBox.Properties.Resources.delete2;
			this.btn_cancel.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(93, 36);
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
			this.AutoScaleBaseSize = new global::System.Drawing.Size(8, 19);
			base.ClientSize = new global::System.Drawing.Size(592, 414);
			base.Controls.Add(this.rtf);
			base.Controls.Add(this.expandableSplitter1);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.txt);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.toolStrip1);
			this.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "InputRichTextBox";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Text";
			base.Load += new global::System.EventHandler(this.InputRichTextBox_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400040A RID: 1034
		public global::AutoComboBox.RichTextBoxPrintCtrl rtf;

		// Token: 0x0400040B RID: 1035
		private global::System.Windows.Forms.Label label1;

		// Token: 0x0400040C RID: 1036
		private global::System.Windows.Forms.ImageList imageList1;

		// Token: 0x0400040D RID: 1037
		private global::DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;

		// Token: 0x0400040E RID: 1038
		private global::System.Windows.Forms.Label label2;

		// Token: 0x0400040F RID: 1039
		private global::System.Windows.Forms.TextBox txt;

		// Token: 0x04000410 RID: 1040
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x04000411 RID: 1041
		private global::System.Windows.Forms.ToolStripButton btn_OK;

		// Token: 0x04000412 RID: 1042
		private global::System.Windows.Forms.ToolStripButton btn_cancel;

		// Token: 0x04000413 RID: 1043
		private global::System.ComponentModel.IContainer components;
	}
}

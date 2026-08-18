namespace ReportFunctions
{
	// Token: 0x02000035 RID: 53
	public partial class StringEdit : global::System.Windows.Forms.Form
	{
		// Token: 0x06000321 RID: 801 RVA: 0x0003DEB4 File Offset: 0x0003CEB4
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

		// Token: 0x06000322 RID: 802 RVA: 0x0003DEF0 File Offset: 0x0003CEF0
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::ReportFunctions.StringEdit));
			this.imageList1 = new global::System.Windows.Forms.ImageList(this.components);
			this.label1 = new global::System.Windows.Forms.Label();
			this.textBox1 = new global::System.Windows.Forms.TextBox();
			this.btn_fakeCancel = new global::System.Windows.Forms.Button();
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.btn_OK = new global::System.Windows.Forms.ToolStripButton();
			this.btn_cancel = new global::System.Windows.Forms.ToolStripButton();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.imageList1.ColorDepth = global::System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList1.ImageSize = new global::System.Drawing.Size(32, 32);
			this.imageList1.TransparentColor = global::System.Drawing.Color.Transparent;
			this.label1.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new global::System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(580, 24);
			this.label1.TabIndex = 1;
			this.label1.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.textBox1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.textBox1.Location = new global::System.Drawing.Point(0, 24);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new global::System.Drawing.Size(580, 348);
			this.textBox1.TabIndex = 2;
			this.btn_fakeCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btn_fakeCancel.Location = new global::System.Drawing.Point(90, 0);
			this.btn_fakeCancel.Name = "btn_fakeCancel";
			this.btn_fakeCancel.Size = new global::System.Drawing.Size(0, 0);
			this.btn_fakeCancel.TabIndex = 3;
			this.btn_fakeCancel.Text = "button1";
			this.btn_fakeCancel.Click += new global::System.EventHandler(this.btn_fakeCancel_Click);
			this.toolStrip1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip1.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_OK,
				this.btn_cancel
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(0, 372);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(580, 39);
			this.toolStrip1.TabIndex = 4;
			this.toolStrip1.Text = "toolStrip1";
			this.btn_OK.Image = global::ReportFunctions.Properties.Resources.check2;
			this.btn_OK.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_OK.Name = "btn_OK";
			this.btn_OK.Size = new global::System.Drawing.Size(64, 36);
			this.btn_OK.Text = "&Ok";
			this.btn_OK.Click += new global::System.EventHandler(this.btn_OK_Click);
			this.btn_cancel.Image = global::ReportFunctions.Properties.Resources.delete2;
			this.btn_cancel.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(93, 36);
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
			this.AutoScaleBaseSize = new global::System.Drawing.Size(5, 13);
			base.CancelButton = this.btn_fakeCancel;
			base.ClientSize = new global::System.Drawing.Size(580, 411);
			base.Controls.Add(this.textBox1);
			base.Controls.Add(this.toolStrip1);
			base.Controls.Add(this.btn_fakeCancel);
			base.Controls.Add(this.label1);
			this.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "StringEdit";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Input";
			base.Load += new global::System.EventHandler(this.StringEdit_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000184 RID: 388
		private global::System.Windows.Forms.ImageList imageList1;

		// Token: 0x04000185 RID: 389
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000186 RID: 390
		private global::System.Windows.Forms.TextBox textBox1;

		// Token: 0x04000187 RID: 391
		private global::System.Windows.Forms.Button btn_fakeCancel;

		// Token: 0x04000188 RID: 392
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x04000189 RID: 393
		private global::System.Windows.Forms.ToolStripButton btn_OK;

		// Token: 0x0400018A RID: 394
		private global::System.Windows.Forms.ToolStripButton btn_cancel;

		// Token: 0x0400018B RID: 395
		private global::System.ComponentModel.IContainer components;
	}
}

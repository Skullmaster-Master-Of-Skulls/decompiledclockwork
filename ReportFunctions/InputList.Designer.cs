namespace ReportFunctions
{
	// Token: 0x0200004F RID: 79
	public partial class InputList : global::System.Windows.Forms.Form
	{
		// Token: 0x06000469 RID: 1129 RVA: 0x0004E884 File Offset: 0x0004D884
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

		// Token: 0x0600046A RID: 1130 RVA: 0x0004E8C0 File Offset: 0x0004D8C0
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::ReportFunctions.InputList));
			this.imageList1 = new global::System.Windows.Forms.ImageList(this.components);
			this.listBox1 = new global::System.Windows.Forms.ListBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.btn_fake = new global::System.Windows.Forms.Button();
			this.btn_fakeAccept = new global::System.Windows.Forms.Button();
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.btn_ok = new global::System.Windows.Forms.ToolStripButton();
			this.btn_cancel = new global::System.Windows.Forms.ToolStripButton();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.imageList1.ImageStream = (global::System.Windows.Forms.ImageListStreamer)componentResourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = global::System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "");
			this.imageList1.Images.SetKeyName(1, "");
			this.imageList1.Images.SetKeyName(2, "");
			this.imageList1.Images.SetKeyName(3, "");
			this.listBox1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.listBox1.ItemHeight = 16;
			this.listBox1.Location = new global::System.Drawing.Point(0, 56);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new global::System.Drawing.Size(566, 244);
			this.listBox1.TabIndex = 2;
			this.listBox1.DoubleClick += new global::System.EventHandler(this.listBox1_DoubleClick);
			this.label1.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new global::System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(566, 56);
			this.label1.TabIndex = 3;
			this.label1.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_fake.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btn_fake.Location = new global::System.Drawing.Point(126, 0);
			this.btn_fake.Name = "btn_fake";
			this.btn_fake.Size = new global::System.Drawing.Size(0, 0);
			this.btn_fake.TabIndex = 4;
			this.btn_fake.Text = "button1";
			this.btn_fake.Click += new global::System.EventHandler(this.btn_fake_Click);
			this.btn_fakeAccept.Location = new global::System.Drawing.Point(88, 0);
			this.btn_fakeAccept.Name = "btn_fakeAccept";
			this.btn_fakeAccept.Size = new global::System.Drawing.Size(0, 0);
			this.btn_fakeAccept.TabIndex = 5;
			this.btn_fakeAccept.Text = "button1";
			this.btn_fakeAccept.Click += new global::System.EventHandler(this.btn_fakeAccept_Click);
			this.toolStrip1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip1.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_ok,
				this.btn_cancel
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(0, 309);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(566, 39);
			this.toolStrip1.TabIndex = 6;
			this.toolStrip1.TabStop = true;
			this.btn_ok.Image = global::ReportFunctions.Properties.Resources.check2;
			this.btn_ok.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_ok.Name = "btn_ok";
			this.btn_ok.Size = new global::System.Drawing.Size(64, 36);
			this.btn_ok.Text = "&Ok";
			this.btn_ok.Click += new global::System.EventHandler(this.btn_ok_Click);
			this.btn_cancel.Image = global::ReportFunctions.Properties.Resources.delete2;
			this.btn_cancel.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(93, 36);
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
			base.AcceptButton = this.btn_fakeAccept;
			this.AutoScaleBaseSize = new global::System.Drawing.Size(6, 15);
			base.CancelButton = this.btn_fake;
			base.ClientSize = new global::System.Drawing.Size(566, 348);
			base.Controls.Add(this.listBox1);
			base.Controls.Add(this.toolStrip1);
			base.Controls.Add(this.btn_fakeAccept);
			base.Controls.Add(this.btn_fake);
			base.Controls.Add(this.label1);
			this.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			base.KeyPreview = true;
			base.Name = "InputList";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "InputList";
			base.Load += new global::System.EventHandler(this.InputList_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000268 RID: 616
		private global::System.Windows.Forms.ImageList imageList1;

		// Token: 0x04000269 RID: 617
		public global::System.Windows.Forms.ListBox listBox1;

		// Token: 0x0400026A RID: 618
		private global::System.Windows.Forms.Label label1;

		// Token: 0x0400026B RID: 619
		private global::System.Windows.Forms.Button btn_fake;

		// Token: 0x0400026C RID: 620
		private global::System.Windows.Forms.Button btn_fakeAccept;

		// Token: 0x0400026D RID: 621
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x0400026E RID: 622
		private global::System.Windows.Forms.ToolStripButton btn_ok;

		// Token: 0x0400026F RID: 623
		private global::System.Windows.Forms.ToolStripButton btn_cancel;

		// Token: 0x04000270 RID: 624
		private global::System.ComponentModel.IContainer components;
	}
}

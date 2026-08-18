namespace AutoComboBox
{
	// Token: 0x020000EA RID: 234
	public partial class ExportTypeChooser : global::System.Windows.Forms.Form
	{
		// Token: 0x06000943 RID: 2371 RVA: 0x00048244 File Offset: 0x00047244
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

		// Token: 0x06000944 RID: 2372 RVA: 0x00048280 File Offset: 0x00047280
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::AutoComboBox.ExportTypeChooser));
			this.imageList1 = new global::System.Windows.Forms.ImageList(this.components);
			this.listBox1 = new global::System.Windows.Forms.ListBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.btn_fakeCancel = new global::System.Windows.Forms.Button();
			this.btn_fakeOK = new global::System.Windows.Forms.Button();
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.btn_save = new global::System.Windows.Forms.ToolStripButton();
			this.btn_cancel = new global::System.Windows.Forms.ToolStripButton();
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
			this.listBox1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.listBox1.ItemHeight = 16;
			this.listBox1.Items.AddRange(new object[]
			{
				"Excel",
				"Access",
				"Text, Delimitered",
				"Text, Formatted"
			});
			this.listBox1.Location = new global::System.Drawing.Point(0, 18);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new global::System.Drawing.Size(273, 84);
			this.listBox1.TabIndex = 40;
			this.listBox1.DoubleClick += new global::System.EventHandler(this.listBox1_DoubleClick);
			this.label1.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new global::System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(273, 18);
			this.label1.TabIndex = 41;
			this.label1.Text = "Please choose the type of export:";
			this.btn_fakeCancel.Location = new global::System.Drawing.Point(192, 6);
			this.btn_fakeCancel.Name = "btn_fakeCancel";
			this.btn_fakeCancel.Size = new global::System.Drawing.Size(0, 0);
			this.btn_fakeCancel.TabIndex = 42;
			this.btn_fakeCancel.Text = "button1";
			this.btn_fakeCancel.Click += new global::System.EventHandler(this.btn_fakeCancel_Click);
			this.btn_fakeOK.Location = new global::System.Drawing.Point(228, 0);
			this.btn_fakeOK.Name = "btn_fakeOK";
			this.btn_fakeOK.Size = new global::System.Drawing.Size(0, 0);
			this.btn_fakeOK.TabIndex = 43;
			this.btn_fakeOK.Text = "button1";
			this.btn_fakeOK.Click += new global::System.EventHandler(this.btn_fakeOK_Click);
			this.toolStrip1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip1.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_save,
				this.btn_cancel
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(0, 106);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(273, 39);
			this.toolStrip1.TabIndex = 44;
			this.toolStrip1.Text = "toolStrip1";
			this.btn_save.Image = global::AutoComboBox.Properties.Resources.check2;
			this.btn_save.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_save.Name = "btn_save";
			this.btn_save.Size = new global::System.Drawing.Size(64, 36);
			this.btn_save.Text = "&Ok";
			this.btn_save.Click += new global::System.EventHandler(this.btn_save_Click);
			this.btn_cancel.Image = global::AutoComboBox.Properties.Resources.delete2;
			this.btn_cancel.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(93, 36);
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
			this.AutoScaleBaseSize = new global::System.Drawing.Size(6, 15);
			base.ClientSize = new global::System.Drawing.Size(273, 145);
			base.Controls.Add(this.listBox1);
			base.Controls.Add(this.toolStrip1);
			base.Controls.Add(this.btn_fakeOK);
			base.Controls.Add(this.btn_fakeCancel);
			base.Controls.Add(this.label1);
			this.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "ExportTypeChooser";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Choose the Type of Export";
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040006B1 RID: 1713
		private global::System.Windows.Forms.ImageList imageList1;

		// Token: 0x040006B2 RID: 1714
		public global::System.Windows.Forms.ListBox listBox1;

		// Token: 0x040006B3 RID: 1715
		private global::System.Windows.Forms.Label label1;

		// Token: 0x040006B4 RID: 1716
		private global::System.Windows.Forms.Button btn_fakeCancel;

		// Token: 0x040006B5 RID: 1717
		private global::System.Windows.Forms.Button btn_fakeOK;

		// Token: 0x040006B6 RID: 1718
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x040006B7 RID: 1719
		private global::System.Windows.Forms.ToolStripButton btn_save;

		// Token: 0x040006B8 RID: 1720
		private global::System.Windows.Forms.ToolStripButton btn_cancel;

		// Token: 0x040006B9 RID: 1721
		private global::System.ComponentModel.IContainer components;
	}
}

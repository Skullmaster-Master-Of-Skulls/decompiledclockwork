namespace AutoComboBox
{
	// Token: 0x0200001E RID: 30
	public partial class InputList : global::System.Windows.Forms.Form
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x00008E2C File Offset: 0x00007E2C
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

		// Token: 0x060000C4 RID: 196 RVA: 0x00008E68 File Offset: 0x00007E68
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::AutoComboBox.InputList));
			this.imageList1 = new global::System.Windows.Forms.ImageList(this.components);
			this.listBox1 = new global::System.Windows.Forms.ListBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.btn_fake = new global::System.Windows.Forms.Button();
			this.btn_fakeAccept = new global::System.Windows.Forms.Button();
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.btn_print = new global::System.Windows.Forms.ToolStripButton();
			this.btn_splitUpDown = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_up = new global::System.Windows.Forms.ToolStripButton();
			this.btn_down = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_selectAll = new global::System.Windows.Forms.ToolStripButton();
			this.btn_selectNone = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new global::System.Windows.Forms.ToolStripSeparator();
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
			this.imageList1.Images.SetKeyName(18, "");
			this.imageList1.Images.SetKeyName(19, "");
			this.imageList1.Images.SetKeyName(20, "");
			this.imageList1.Images.SetKeyName(21, "");
			this.listBox1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.listBox1.Font = new global::System.Drawing.Font("Arial", 14.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.listBox1.ItemHeight = 22;
			this.listBox1.Location = new global::System.Drawing.Point(0, 56);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new global::System.Drawing.Size(872, 312);
			this.listBox1.TabIndex = 2;
			this.listBox1.DoubleClick += new global::System.EventHandler(this.listBox1_DoubleClick);
			this.listBox1.KeyUp += new global::System.Windows.Forms.KeyEventHandler(this.listBox1_KeyUp);
			this.listBox1.KeyDown += new global::System.Windows.Forms.KeyEventHandler(this.listBox1_KeyDown);
			this.label1.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label1.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label1.Location = new global::System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(872, 56);
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
			this.toolStrip1.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_print,
				this.btn_splitUpDown,
				this.btn_up,
				this.btn_down,
				this.toolStripSeparator2,
				this.btn_selectAll,
				this.btn_selectNone,
				this.toolStripSeparator3,
				this.btn_ok,
				this.btn_cancel
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(0, 387);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(872, 39);
			this.toolStrip1.TabIndex = 6;
			this.toolStrip1.Text = "toolStrip1";
			this.btn_print.Image = global::AutoComboBox.Properties.Resources.printer;
			this.btn_print.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_print.Name = "btn_print";
			this.btn_print.Size = new global::System.Drawing.Size(99, 36);
			this.btn_print.Text = "&Print list";
			this.btn_print.Click += new global::System.EventHandler(this.btn_print_Click);
			this.btn_splitUpDown.Name = "btn_splitUpDown";
			this.btn_splitUpDown.Size = new global::System.Drawing.Size(6, 39);
			this.btn_up.Image = global::AutoComboBox.Properties.Resources.nav_up_blue;
			this.btn_up.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_up.Name = "btn_up";
			this.btn_up.Size = new global::System.Drawing.Size(64, 36);
			this.btn_up.Text = "&Up";
			this.btn_up.Click += new global::System.EventHandler(this.btn_up_Click);
			this.btn_down.Image = global::AutoComboBox.Properties.Resources.nav_down_blue;
			this.btn_down.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_down.Name = "btn_down";
			this.btn_down.Size = new global::System.Drawing.Size(84, 36);
			this.btn_down.Text = "&Down";
			this.btn_down.Click += new global::System.EventHandler(this.btn_down_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new global::System.Drawing.Size(6, 39);
			this.btn_selectAll.Image = global::AutoComboBox.Properties.Resources.document_ok;
			this.btn_selectAll.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_selectAll.Name = "btn_selectAll";
			this.btn_selectAll.Size = new global::System.Drawing.Size(107, 36);
			this.btn_selectAll.Text = "Select &all";
			this.btn_selectAll.Click += new global::System.EventHandler(this.btn_selectAll_Click);
			this.btn_selectNone.Image = global::AutoComboBox.Properties.Resources.document_plain;
			this.btn_selectNone.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_selectNone.Name = "btn_selectNone";
			this.btn_selectNone.Size = new global::System.Drawing.Size(126, 36);
			this.btn_selectNone.Text = "Select &none";
			this.btn_selectNone.Click += new global::System.EventHandler(this.btn_selectNone_Click);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new global::System.Drawing.Size(6, 39);
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
			this.AutoScaleBaseSize = new global::System.Drawing.Size(8, 19);
			base.CancelButton = this.btn_fake;
			base.ClientSize = new global::System.Drawing.Size(872, 426);
			base.Controls.Add(this.listBox1);
			base.Controls.Add(this.toolStrip1);
			base.Controls.Add(this.btn_fakeAccept);
			base.Controls.Add(this.btn_fake);
			base.Controls.Add(this.label1);
			this.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			base.KeyPreview = true;
			base.Name = "InputList";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select from List";
			base.Load += new global::System.EventHandler(this.InputList_Load);
			base.KeyUp += new global::System.Windows.Forms.KeyEventHandler(this.InputList_KeyUp);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400012E RID: 302
		private global::System.Windows.Forms.ImageList imageList1;

		// Token: 0x0400012F RID: 303
		public global::System.Windows.Forms.ListBox listBox1;

		// Token: 0x04000130 RID: 304
		public global::System.Windows.Forms.Label label1;

		// Token: 0x04000131 RID: 305
		private global::System.Windows.Forms.Button btn_fake;

		// Token: 0x04000132 RID: 306
		private global::System.Windows.Forms.Button btn_fakeAccept;

		// Token: 0x04000133 RID: 307
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x04000134 RID: 308
		private global::System.Windows.Forms.ToolStripButton btn_print;

		// Token: 0x04000135 RID: 309
		private global::System.Windows.Forms.ToolStripSeparator btn_splitUpDown;

		// Token: 0x04000136 RID: 310
		private global::System.Windows.Forms.ToolStripButton btn_up;

		// Token: 0x04000137 RID: 311
		private global::System.Windows.Forms.ToolStripButton btn_down;

		// Token: 0x04000138 RID: 312
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator2;

		// Token: 0x04000139 RID: 313
		private global::System.Windows.Forms.ToolStripButton btn_selectAll;

		// Token: 0x0400013A RID: 314
		private global::System.Windows.Forms.ToolStripButton btn_selectNone;

		// Token: 0x0400013B RID: 315
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator3;

		// Token: 0x0400013C RID: 316
		private global::System.Windows.Forms.ToolStripButton btn_ok;

		// Token: 0x0400013D RID: 317
		private global::System.Windows.Forms.ToolStripButton btn_cancel;

		// Token: 0x0400013E RID: 318
		private global::System.ComponentModel.IContainer components;
	}
}

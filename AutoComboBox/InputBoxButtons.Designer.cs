namespace AutoComboBox
{
	// Token: 0x020000AB RID: 171
	public partial class InputBoxButtons : global::System.Windows.Forms.Form
	{
		// Token: 0x06000664 RID: 1636 RVA: 0x000331A8 File Offset: 0x000321A8
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

		// Token: 0x06000665 RID: 1637 RVA: 0x000331E4 File Offset: 0x000321E4
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.Resources.ResourceManager resourceManager = new global::System.Resources.ResourceManager(typeof(global::AutoComboBox.InputBoxButtons));
			this.button1 = new global::System.Windows.Forms.Button();
			this.button2 = new global::System.Windows.Forms.Button();
			this.btn_cancel = new global::System.Windows.Forms.Button();
			this.label1 = new global::System.Windows.Forms.Label();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.btn_fakeCancel = new global::System.Windows.Forms.Button();
			this.lbl_image = new global::System.Windows.Forms.Label();
			this.imageList1 = new global::System.Windows.Forms.ImageList(this.components);
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.button1.FlatStyle = global::System.Windows.Forms.FlatStyle.System;
			this.button1.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.button1.Location = new global::System.Drawing.Point(0, 6);
			this.button1.Name = "button1";
			this.button1.Size = new global::System.Drawing.Size(132, 36);
			this.button1.TabIndex = 0;
			this.button1.Text = "button1";
			this.button1.Click += new global::System.EventHandler(this.button1_Click);
			this.button2.FlatStyle = global::System.Windows.Forms.FlatStyle.System;
			this.button2.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.button2.Location = new global::System.Drawing.Point(150, 6);
			this.button2.Name = "button2";
			this.button2.Size = new global::System.Drawing.Size(132, 36);
			this.button2.TabIndex = 1;
			this.button2.Text = "button2";
			this.button2.Click += new global::System.EventHandler(this.button2_Click_1);
			this.btn_cancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btn_cancel.FlatStyle = global::System.Windows.Forms.FlatStyle.System;
			this.btn_cancel.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_cancel.Location = new global::System.Drawing.Point(354, 6);
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(96, 36);
			this.btn_cancel.TabIndex = 2;
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
			this.label1.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label1.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label1.Location = new global::System.Drawing.Point(66, 6);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(396, 42);
			this.label1.TabIndex = 3;
			this.label1.Text = "label1";
			this.label1.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.label1.TextChanged += new global::System.EventHandler(this.label1_TextChanged);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Controls.Add(this.button2);
			this.panel1.Controls.Add(this.btn_cancel);
			this.panel1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new global::System.Drawing.Point(6, 58);
			this.panel1.Name = "panel1";
			this.panel1.Size = new global::System.Drawing.Size(456, 48);
			this.panel1.TabIndex = 4;
			this.btn_fakeCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btn_fakeCancel.Location = new global::System.Drawing.Point(174, 0);
			this.btn_fakeCancel.Name = "btn_fakeCancel";
			this.btn_fakeCancel.Size = new global::System.Drawing.Size(0, 0);
			this.btn_fakeCancel.TabIndex = 5;
			this.btn_fakeCancel.Click += new global::System.EventHandler(this.btn_fakeCancel_Click);
			this.lbl_image.Dock = global::System.Windows.Forms.DockStyle.Left;
			this.lbl_image.ImageAlign = global::System.Drawing.ContentAlignment.TopCenter;
			this.lbl_image.ImageList = this.imageList1;
			this.lbl_image.Location = new global::System.Drawing.Point(6, 6);
			this.lbl_image.Name = "lbl_image";
			this.lbl_image.Size = new global::System.Drawing.Size(60, 52);
			this.lbl_image.TabIndex = 6;
			this.imageList1.ColorDepth = global::System.Windows.Forms.ColorDepth.Depth16Bit;
			this.imageList1.ImageSize = new global::System.Drawing.Size(48, 48);
			this.imageList1.ImageStream = (global::System.Windows.Forms.ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = global::System.Drawing.Color.Transparent;
			this.AutoScaleBaseSize = new global::System.Drawing.Size(9, 22);
			base.CancelButton = this.btn_fakeCancel;
			base.ClientSize = new global::System.Drawing.Size(462, 106);
			base.Controls.Add(this.btn_fakeCancel);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.lbl_image);
			base.Controls.Add(this.panel1);
			base.DockPadding.Left = 6;
			base.DockPadding.Top = 6;
			this.Font = new global::System.Drawing.Font("Arial", 14.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Icon = (global::System.Drawing.Icon)resourceManager.GetObject("$this.Icon");
			base.Name = "InputBoxButtons";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MyDialog";
			base.Load += new global::System.EventHandler(this.MyDialog_Load);
			this.panel1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x04000501 RID: 1281
		public global::System.Windows.Forms.Button button1;

		// Token: 0x04000502 RID: 1282
		public global::System.Windows.Forms.Button button2;

		// Token: 0x04000503 RID: 1283
		private global::System.Windows.Forms.Button btn_cancel;

		// Token: 0x04000504 RID: 1284
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000505 RID: 1285
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x04000506 RID: 1286
		private global::System.Windows.Forms.Button btn_fakeCancel;

		// Token: 0x04000507 RID: 1287
		private global::System.Windows.Forms.Label lbl_image;

		// Token: 0x04000508 RID: 1288
		private global::System.Windows.Forms.ImageList imageList1;

		// Token: 0x04000509 RID: 1289
		private global::System.ComponentModel.IContainer components;
	}
}

namespace AutoComboBox
{
	// Token: 0x020000C7 RID: 199
	public partial class InputBox : global::System.Windows.Forms.Form
	{
		// Token: 0x0600079E RID: 1950 RVA: 0x0003C780 File Offset: 0x0003B780
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

		// Token: 0x0600079F RID: 1951 RVA: 0x0003C7BC File Offset: 0x0003B7BC
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::AutoComboBox.InputBox));
			this.imageList1 = new global::System.Windows.Forms.ImageList(this.components);
			this.label1 = new global::System.Windows.Forms.Label();
			this.btn_cancelFake = new global::System.Windows.Forms.Button();
			this.panel2 = new global::System.Windows.Forms.Panel();
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.btn_ok = new global::System.Windows.Forms.ToolStripButton();
			this.btn_cancel = new global::System.Windows.Forms.ToolStripButton();
			this.btn_split = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_encrypt = new global::System.Windows.Forms.ToolStripButton();
			this.label2 = new global::System.Windows.Forms.Label();
			this.textBox2 = new global::System.Windows.Forms.TextBox();
			this.btn_colour = new global::System.Windows.Forms.Button();
			this.textBox1 = new global::System.Windows.Forms.TextBox();
			this.panel2.SuspendLayout();
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
			this.label1.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new global::System.Drawing.Point(3, 3);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(420, 24);
			this.label1.TabIndex = 1;
			this.label1.Text = "Please enter a value:";
			this.label1.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_cancelFake.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btn_cancelFake.Location = new global::System.Drawing.Point(40, 0);
			this.btn_cancelFake.Name = "btn_cancelFake";
			this.btn_cancelFake.Size = new global::System.Drawing.Size(0, 0);
			this.btn_cancelFake.TabIndex = 4;
			this.btn_cancelFake.Click += new global::System.EventHandler(this.btn_cancelFake_Click);
			this.panel2.Controls.Add(this.toolStrip1);
			this.panel2.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new global::System.Drawing.Point(3, 76);
			this.panel2.Name = "panel2";
			this.panel2.Padding = new global::System.Windows.Forms.Padding(1, 5, 1, 1);
			this.panel2.Size = new global::System.Drawing.Size(420, 43);
			this.panel2.TabIndex = 5;
			this.toolStrip1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip1.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_ok,
				this.btn_cancel,
				this.btn_split,
				this.btn_encrypt
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(1, 3);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(418, 39);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.TabStop = true;
			this.btn_ok.Image = global::AutoComboBox.Properties.Resources.check2;
			this.btn_ok.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_ok.Name = "btn_ok";
			this.btn_ok.Size = new global::System.Drawing.Size(131, 36);
			this.btn_ok.Text = "Ok (ALT + &s)";
			this.btn_ok.Click += new global::System.EventHandler(this.btn_ok_Click);
			this.btn_cancel.Image = global::AutoComboBox.Properties.Resources.delete2;
			this.btn_cancel.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(93, 36);
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
			this.btn_split.Name = "btn_split";
			this.btn_split.Size = new global::System.Drawing.Size(6, 39);
			this.btn_encrypt.CheckOnClick = true;
			this.btn_encrypt.Image = global::AutoComboBox.Properties.Resources.key1;
			this.btn_encrypt.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_encrypt.Name = "btn_encrypt";
			this.btn_encrypt.Size = new global::System.Drawing.Size(96, 36);
			this.btn_encrypt.Text = "&Encrypt";
			this.btn_encrypt.Visible = false;
			this.label2.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label2.Location = new global::System.Drawing.Point(3, 27);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(420, 24);
			this.label2.TabIndex = 6;
			this.label2.Text = "Please enter a value:";
			this.label2.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.label2.Visible = false;
			this.textBox2.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.textBox2.Location = new global::System.Drawing.Point(3, 51);
			this.textBox2.Multiline = true;
			this.textBox2.Name = "textBox2";
			this.textBox2.PasswordChar = '*';
			this.textBox2.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
			this.textBox2.Size = new global::System.Drawing.Size(345, 25);
			this.textBox2.TabIndex = 7;
			this.textBox2.Visible = false;
			this.btn_colour.BackColor = global::System.Drawing.Color.Black;
			this.btn_colour.Dock = global::System.Windows.Forms.DockStyle.Right;
			this.btn_colour.Location = new global::System.Drawing.Point(348, 51);
			this.btn_colour.Name = "btn_colour";
			this.btn_colour.Size = new global::System.Drawing.Size(75, 25);
			this.btn_colour.TabIndex = 8;
			this.btn_colour.UseVisualStyleBackColor = false;
			this.btn_colour.Visible = false;
			this.btn_colour.Click += new global::System.EventHandler(this.btn_colour_Click);
			this.textBox1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.textBox1.Location = new global::System.Drawing.Point(3, 76);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new global::System.Drawing.Size(345, 0);
			this.textBox1.TabIndex = 10;
			this.AutoScaleBaseSize = new global::System.Drawing.Size(6, 15);
			base.CancelButton = this.btn_cancelFake;
			base.ClientSize = new global::System.Drawing.Size(426, 122);
			base.ControlBox = false;
			base.Controls.Add(this.textBox1);
			base.Controls.Add(this.textBox2);
			base.Controls.Add(this.btn_colour);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.btn_cancelFake);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.panel2);
			this.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			base.KeyPreview = true;
			base.Name = "InputBox";
			base.Padding = new global::System.Windows.Forms.Padding(3);
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Input";
			base.Load += new global::System.EventHandler(this.InputBox_Load);
			base.KeyUp += new global::System.Windows.Forms.KeyEventHandler(this.InputBox_KeyUp);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040005C5 RID: 1477
		private global::System.Windows.Forms.Label label1;

		// Token: 0x040005C6 RID: 1478
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040005C7 RID: 1479
		private global::System.Windows.Forms.Button btn_cancelFake;

		// Token: 0x040005C8 RID: 1480
		private global::System.Windows.Forms.ImageList imageList1;

		// Token: 0x040005C9 RID: 1481
		private global::System.Windows.Forms.Panel panel2;

		// Token: 0x040005CB RID: 1483
		private global::System.Windows.Forms.Label label2;

		// Token: 0x040005CC RID: 1484
		private global::System.Windows.Forms.TextBox textBox2;

		// Token: 0x040005CD RID: 1485
		private global::System.Windows.Forms.Button btn_colour;

		// Token: 0x040005CE RID: 1486
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x040005CF RID: 1487
		private global::System.Windows.Forms.ToolStripButton btn_ok;

		// Token: 0x040005D0 RID: 1488
		private global::System.Windows.Forms.ToolStripButton btn_cancel;

		// Token: 0x040005D1 RID: 1489
		private global::System.Windows.Forms.ToolStripButton btn_encrypt;

		// Token: 0x040005D2 RID: 1490
		private global::System.Windows.Forms.ToolStripSeparator btn_split;

		// Token: 0x040005D3 RID: 1491
		private global::System.Windows.Forms.TextBox textBox1;
	}
}

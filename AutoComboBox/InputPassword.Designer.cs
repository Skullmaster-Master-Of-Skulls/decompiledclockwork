namespace AutoComboBox
{
	// Token: 0x02000096 RID: 150
	public partial class InputPassword : global::System.Windows.Forms.Form
	{
		// Token: 0x060005C8 RID: 1480 RVA: 0x0002FCD4 File Offset: 0x0002ECD4
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

		// Token: 0x060005C9 RID: 1481 RVA: 0x0002FD10 File Offset: 0x0002ED10
		private void InitializeComponent()
		{
			this.label1 = new global::System.Windows.Forms.Label();
			this.txt_pwd1 = new global::System.Windows.Forms.TextBox();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.label2 = new global::System.Windows.Forms.Label();
			this.panel2 = new global::System.Windows.Forms.Panel();
			this.txt_pwd2 = new global::System.Windows.Forms.TextBox();
			this.label3 = new global::System.Windows.Forms.Label();
			this.panel3 = new global::System.Windows.Forms.Panel();
			this.btn_ok = new global::System.Windows.Forms.Button();
			this.label4 = new global::System.Windows.Forms.Label();
			this.btn_cancel = new global::System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			base.SuspendLayout();
			this.label1.Dock = global::System.Windows.Forms.DockStyle.Left;
			this.label1.Location = new global::System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(264, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "Please enter the new password:";
			this.label1.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.txt_pwd1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.txt_pwd1.Location = new global::System.Drawing.Point(272, 8);
			this.txt_pwd1.Name = "txt_pwd1";
			this.txt_pwd1.PasswordChar = '*';
			this.txt_pwd1.Size = new global::System.Drawing.Size(434, 22);
			this.txt_pwd1.TabIndex = 2;
			this.panel1.Controls.Add(this.txt_pwd1);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new global::System.Drawing.Point(0, 48);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new global::System.Windows.Forms.Padding(8);
			this.panel1.Size = new global::System.Drawing.Size(714, 40);
			this.panel1.TabIndex = 3;
			this.label2.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label2.Font = new global::System.Drawing.Font("Arial", 14.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label2.Location = new global::System.Drawing.Point(0, 0);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(714, 48);
			this.label2.TabIndex = 4;
			this.label2.Text = "Please enter the new password:";
			this.label2.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.panel2.Controls.Add(this.txt_pwd2);
			this.panel2.Controls.Add(this.label3);
			this.panel2.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new global::System.Drawing.Point(0, 88);
			this.panel2.Name = "panel2";
			this.panel2.Padding = new global::System.Windows.Forms.Padding(8);
			this.panel2.Size = new global::System.Drawing.Size(714, 40);
			this.panel2.TabIndex = 5;
			this.txt_pwd2.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.txt_pwd2.Location = new global::System.Drawing.Point(272, 8);
			this.txt_pwd2.Name = "txt_pwd2";
			this.txt_pwd2.PasswordChar = '*';
			this.txt_pwd2.Size = new global::System.Drawing.Size(434, 22);
			this.txt_pwd2.TabIndex = 2;
			this.label3.Dock = global::System.Windows.Forms.DockStyle.Left;
			this.label3.Location = new global::System.Drawing.Point(8, 8);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(264, 24);
			this.label3.TabIndex = 0;
			this.label3.Text = "Please enter the new password again:";
			this.label3.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.panel3.Controls.Add(this.btn_ok);
			this.panel3.Controls.Add(this.label4);
			this.panel3.Controls.Add(this.btn_cancel);
			this.panel3.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.panel3.Location = new global::System.Drawing.Point(0, 140);
			this.panel3.Name = "panel3";
			this.panel3.Padding = new global::System.Windows.Forms.Padding(4);
			this.panel3.Size = new global::System.Drawing.Size(714, 48);
			this.panel3.TabIndex = 6;
			this.btn_ok.Dock = global::System.Windows.Forms.DockStyle.Right;
			this.btn_ok.Font = new global::System.Drawing.Font("Arial", 14.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_ok.Location = new global::System.Drawing.Point(422, 4);
			this.btn_ok.Name = "btn_ok";
			this.btn_ok.Size = new global::System.Drawing.Size(136, 40);
			this.btn_ok.TabIndex = 2;
			this.btn_ok.Text = "&Ok";
			this.btn_ok.Click += new global::System.EventHandler(this.btn_ok_Click);
			this.label4.Dock = global::System.Windows.Forms.DockStyle.Right;
			this.label4.Location = new global::System.Drawing.Point(558, 4);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(16, 40);
			this.label4.TabIndex = 1;
			this.btn_cancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btn_cancel.Dock = global::System.Windows.Forms.DockStyle.Right;
			this.btn_cancel.Font = new global::System.Drawing.Font("Arial", 14.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_cancel.Location = new global::System.Drawing.Point(574, 4);
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(136, 40);
			this.btn_cancel.TabIndex = 0;
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
			base.AcceptButton = this.btn_ok;
			this.AutoScaleBaseSize = new global::System.Drawing.Size(6, 15);
			base.CancelButton = this.btn_cancel;
			base.ClientSize = new global::System.Drawing.Size(714, 188);
			base.Controls.Add(this.panel3);
			base.Controls.Add(this.panel2);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.label2);
			this.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "InputPassword";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "InputPassword";
			base.Load += new global::System.EventHandler(this.InputPassword_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel3.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x040004AF RID: 1199
		private global::System.Windows.Forms.Label label1;

		// Token: 0x040004B0 RID: 1200
		private global::System.Windows.Forms.TextBox txt_pwd1;

		// Token: 0x040004B1 RID: 1201
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x040004B2 RID: 1202
		private global::System.Windows.Forms.Label label2;

		// Token: 0x040004B3 RID: 1203
		private global::System.Windows.Forms.Panel panel2;

		// Token: 0x040004B4 RID: 1204
		private global::System.Windows.Forms.TextBox txt_pwd2;

		// Token: 0x040004B5 RID: 1205
		private global::System.Windows.Forms.Label label3;

		// Token: 0x040004B6 RID: 1206
		private global::System.Windows.Forms.Panel panel3;

		// Token: 0x040004B7 RID: 1207
		private global::System.Windows.Forms.Button btn_cancel;

		// Token: 0x040004B8 RID: 1208
		private global::System.Windows.Forms.Label label4;

		// Token: 0x040004B9 RID: 1209
		private global::System.Windows.Forms.Button btn_ok;

		// Token: 0x040004BA RID: 1210
		private global::System.ComponentModel.Container components = null;
	}
}

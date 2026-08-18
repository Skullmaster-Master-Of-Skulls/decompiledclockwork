namespace ClockWorkAPI
{
	// Token: 0x02000075 RID: 117
	public partial class InputPassword : global::System.Windows.Forms.Form
	{
		// Token: 0x0600060D RID: 1549 RVA: 0x00020004 File Offset: 0x0001F004
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

		// Token: 0x0600060E RID: 1550 RVA: 0x00020040 File Offset: 0x0001F040
		private void InitializeComponent()
		{
			this.txt = new global::System.Windows.Forms.TextBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.btn_ok = new global::System.Windows.Forms.Button();
			this.btn_cancel = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.txt.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.txt.Location = new global::System.Drawing.Point(8, 24);
			this.txt.Name = "txt";
			this.txt.PasswordChar = '*';
			this.txt.Size = new global::System.Drawing.Size(352, 22);
			this.txt.TabIndex = 0;
			this.txt.Text = "";
			this.label1.Location = new global::System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(176, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Please enter your password:";
			this.btn_ok.Location = new global::System.Drawing.Point(152, 64);
			this.btn_ok.Name = "btn_ok";
			this.btn_ok.Size = new global::System.Drawing.Size(96, 24);
			this.btn_ok.TabIndex = 2;
			this.btn_ok.Text = "&Ok";
			this.btn_ok.Click += new global::System.EventHandler(this.btn_ok_Click);
			this.btn_cancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btn_cancel.Location = new global::System.Drawing.Point(264, 64);
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(96, 24);
			this.btn_cancel.TabIndex = 3;
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
			base.AcceptButton = this.btn_ok;
			this.AutoScaleBaseSize = new global::System.Drawing.Size(5, 13);
			base.CancelButton = this.btn_cancel;
			base.ClientSize = new global::System.Drawing.Size(368, 94);
			base.Controls.Add(this.btn_cancel);
			base.Controls.Add(this.btn_ok);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.txt);
			base.Name = "InputPassword";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Input Password";
			base.ResumeLayout(false);
		}

		// Token: 0x0400030C RID: 780
		private global::System.Windows.Forms.Label label1;

		// Token: 0x0400030D RID: 781
		private global::System.Windows.Forms.TextBox txt;

		// Token: 0x0400030E RID: 782
		private global::System.Windows.Forms.Button btn_ok;

		// Token: 0x0400030F RID: 783
		private global::System.Windows.Forms.Button btn_cancel;

		// Token: 0x04000310 RID: 784
		private global::System.ComponentModel.Container components = null;
	}
}

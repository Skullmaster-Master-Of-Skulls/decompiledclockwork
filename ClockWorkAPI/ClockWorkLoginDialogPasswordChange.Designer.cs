namespace ClockWorkAPI
{
	// Token: 0x02000018 RID: 24
	public partial class ClockWorkLoginDialogPasswordChange : global::System.Windows.Forms.Form
	{
		// Token: 0x060000CF RID: 207 RVA: 0x00005F74 File Offset: 0x00004F74
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

		// Token: 0x060000D0 RID: 208 RVA: 0x00005FB0 File Offset: 0x00004FB0
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::ClockWorkAPI.ClockWorkLoginDialogPasswordChange));
			this.btn_ok = new global::System.Windows.Forms.Button();
			this.btn_cancel = new global::System.Windows.Forms.Button();
			this.txt_newpwd2 = new global::System.Windows.Forms.TextBox();
			this.txt_newpwd1 = new global::System.Windows.Forms.TextBox();
			this.txt_oldpwd = new global::System.Windows.Forms.TextBox();
			this.txt_username = new global::System.Windows.Forms.TextBox();
			this.label4 = new global::System.Windows.Forms.Label();
			this.label3 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.label1 = new global::System.Windows.Forms.Label();
			base.SuspendLayout();
			this.btn_ok.AccessibleDescription = "Ok";
			this.btn_ok.AccessibleName = "Ok";
			this.btn_ok.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_ok.Location = new global::System.Drawing.Point(147, 152);
			this.btn_ok.Name = "btn_ok";
			this.btn_ok.Size = new global::System.Drawing.Size(80, 32);
			this.btn_ok.TabIndex = 9;
			this.btn_ok.Text = "&Ok";
			this.btn_ok.Click += new global::System.EventHandler(this.btn_ok_Click);
			this.btn_cancel.AccessibleDescription = "Cancel";
			this.btn_cancel.AccessibleName = "Cancel";
			this.btn_cancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btn_cancel.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_cancel.Location = new global::System.Drawing.Point(251, 152);
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(80, 32);
			this.btn_cancel.TabIndex = 10;
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
			this.txt_newpwd2.AccessibleDescription = "Confirm new password";
			this.txt_newpwd2.AccessibleName = "Confirm new password";
			this.txt_newpwd2.Location = new global::System.Drawing.Point(145, 112);
			this.txt_newpwd2.Name = "txt_newpwd2";
			this.txt_newpwd2.PasswordChar = '*';
			this.txt_newpwd2.Size = new global::System.Drawing.Size(184, 20);
			this.txt_newpwd2.TabIndex = 8;
			this.txt_newpwd2.KeyDown += new global::System.Windows.Forms.KeyEventHandler(this.txt_newpwd2_KeyDown);
			this.txt_newpwd1.AccessibleDescription = "New password";
			this.txt_newpwd1.AccessibleName = "New password";
			this.txt_newpwd1.Location = new global::System.Drawing.Point(145, 80);
			this.txt_newpwd1.Name = "txt_newpwd1";
			this.txt_newpwd1.PasswordChar = '*';
			this.txt_newpwd1.Size = new global::System.Drawing.Size(184, 20);
			this.txt_newpwd1.TabIndex = 6;
			this.txt_oldpwd.AccessibleDescription = "Old password";
			this.txt_oldpwd.AccessibleName = "Old password";
			this.txt_oldpwd.Location = new global::System.Drawing.Point(145, 40);
			this.txt_oldpwd.Name = "txt_oldpwd";
			this.txt_oldpwd.PasswordChar = '*';
			this.txt_oldpwd.Size = new global::System.Drawing.Size(184, 20);
			this.txt_oldpwd.TabIndex = 4;
			this.txt_username.AccessibleDescription = "Username";
			this.txt_username.AccessibleName = "Username";
			this.txt_username.CharacterCasing = global::System.Windows.Forms.CharacterCasing.Upper;
			this.txt_username.Location = new global::System.Drawing.Point(145, 8);
			this.txt_username.Name = "txt_username";
			this.txt_username.Size = new global::System.Drawing.Size(184, 20);
			this.txt_username.TabIndex = 2;
			this.label4.Location = new global::System.Drawing.Point(8, 115);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(131, 32);
			this.label4.TabIndex = 7;
			this.label4.Text = "Confirm new password";
			this.label3.Location = new global::System.Drawing.Point(8, 83);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(88, 16);
			this.label3.TabIndex = 5;
			this.label3.Text = "New password:";
			this.label2.Location = new global::System.Drawing.Point(8, 43);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(88, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Old password:";
			this.label1.Location = new global::System.Drawing.Point(8, 10);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(88, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Username:";
			base.AcceptButton = this.btn_ok;
			this.AutoScaleBaseSize = new global::System.Drawing.Size(5, 13);
			base.CancelButton = this.btn_cancel;
			base.ClientSize = new global::System.Drawing.Size(344, 196);
			base.Controls.Add(this.btn_ok);
			base.Controls.Add(this.btn_cancel);
			base.Controls.Add(this.txt_newpwd2);
			base.Controls.Add(this.txt_newpwd1);
			base.Controls.Add(this.txt_oldpwd);
			base.Controls.Add(this.txt_username);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "ClockWorkLoginDialogPasswordChange";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Change your password";
			base.Load += new global::System.EventHandler(this.ClockWorkLoginDialogPasswordChange_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000072 RID: 114
		private global::System.Windows.Forms.Button btn_ok;

		// Token: 0x04000073 RID: 115
		private global::System.Windows.Forms.Button btn_cancel;

		// Token: 0x04000074 RID: 116
		private global::System.Windows.Forms.TextBox txt_newpwd2;

		// Token: 0x04000075 RID: 117
		private global::System.Windows.Forms.TextBox txt_newpwd1;

		// Token: 0x04000076 RID: 118
		private global::System.Windows.Forms.TextBox txt_oldpwd;

		// Token: 0x04000077 RID: 119
		private global::System.Windows.Forms.TextBox txt_username;

		// Token: 0x04000078 RID: 120
		private global::System.Windows.Forms.Label label4;

		// Token: 0x04000079 RID: 121
		private global::System.Windows.Forms.Label label3;

		// Token: 0x0400007A RID: 122
		private global::System.Windows.Forms.Label label2;

		// Token: 0x0400007B RID: 123
		private global::System.Windows.Forms.Label label1;

		// Token: 0x0400007C RID: 124
		private global::System.ComponentModel.Container components = null;
	}
}

namespace ClockWorkAPI
{
	// Token: 0x0200007B RID: 123
	public partial class ClockWorkLoginDialog : global::System.Windows.Forms.Form
	{
		// Token: 0x06000643 RID: 1603 RVA: 0x00022CB8 File Offset: 0x00021CB8
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

		// Token: 0x06000644 RID: 1604 RVA: 0x00022CF4 File Offset: 0x00021CF4
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::ClockWorkAPI.ClockWorkLoginDialog));
			this.imageList1 = new global::System.Windows.Forms.ImageList(this.components);
			this.label1 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.txt_user = new global::System.Windows.Forms.TextBox();
			this.chk_rememberPassword = new global::System.Windows.Forms.CheckBox();
			this.link_help = new global::System.Windows.Forms.LinkLabel();
			this.lbl_message = new global::System.Windows.Forms.Label();
			this.btn_fake = new global::System.Windows.Forms.Button();
			this.btn_fakeCancel = new global::System.Windows.Forms.Button();
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.btn_save = new global::System.Windows.Forms.ToolStripButton();
			this.btn_cancel = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_changePassword = new global::System.Windows.Forms.ToolStripButton();
			this.lbl_banner = new global::System.Windows.Forms.Label();
			this.balloonTip_capsLock = new global::DevComponents.DotNetBar.BalloonTip();
			this.txt_pass = new global::System.Windows.Forms.TextBox();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.imageList1.ColorDepth = global::System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList1.ImageSize = new global::System.Drawing.Size(16, 16);
			this.imageList1.TransparentColor = global::System.Drawing.Color.Transparent;
			this.label1.Location = new global::System.Drawing.Point(8, 118);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(80, 32);
			this.label1.TabIndex = 5;
			this.label1.Text = "User name:";
			this.label1.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.label2.Location = new global::System.Drawing.Point(8, 158);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(80, 32);
			this.label2.TabIndex = 6;
			this.label2.Text = "Password:";
			this.label2.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.txt_user.AccessibleDescription = "Username";
			this.txt_user.AccessibleName = "Username";
			this.txt_user.Font = new global::System.Drawing.Font("Arial", 15.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.txt_user.Location = new global::System.Drawing.Point(88, 120);
			this.txt_user.Name = "txt_user";
			this.txt_user.Size = new global::System.Drawing.Size(304, 32);
			this.txt_user.TabIndex = 7;
			this.txt_user.KeyUp += new global::System.Windows.Forms.KeyEventHandler(this.txt_user_KeyUp);
			this.txt_user.Enter += new global::System.EventHandler(this.txt_user_Enter);
			this.chk_rememberPassword.AccessibleDescription = "Remember my password";
			this.chk_rememberPassword.AccessibleName = "Remember my password";
			this.chk_rememberPassword.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.chk_rememberPassword.Location = new global::System.Drawing.Point(96, 192);
			this.chk_rememberPassword.Name = "chk_rememberPassword";
			this.chk_rememberPassword.Size = new global::System.Drawing.Size(216, 24);
			this.chk_rememberPassword.TabIndex = 10;
			this.chk_rememberPassword.Text = "&Remember my password";
			this.chk_rememberPassword.Visible = false;
			this.link_help.AccessibleDescription = "Help";
			this.link_help.AccessibleName = "Help";
			this.link_help.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.link_help.Location = new global::System.Drawing.Point(366, 192);
			this.link_help.Name = "link_help";
			this.link_help.Size = new global::System.Drawing.Size(32, 24);
			this.link_help.TabIndex = 11;
			this.link_help.TabStop = true;
			this.link_help.Text = "help";
			this.link_help.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.link_help.Visible = false;
			this.link_help.LinkClicked += new global::System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_help_LinkClicked);
			this.lbl_message.AccessibleDescription = "Login has failed";
			this.lbl_message.AccessibleName = "Login has failed";
			this.lbl_message.BackColor = global::System.Drawing.Color.Black;
			this.lbl_message.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.lbl_message.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lbl_message.ForeColor = global::System.Drawing.Color.White;
			this.lbl_message.Location = new global::System.Drawing.Point(8, 216);
			this.lbl_message.Name = "lbl_message";
			this.lbl_message.Size = new global::System.Drawing.Size(384, 24);
			this.lbl_message.TabIndex = 12;
			this.lbl_message.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.lbl_message.Visible = false;
			this.lbl_message.Enter += new global::System.EventHandler(this.lbl_message_Enter);
			this.btn_fake.Location = new global::System.Drawing.Point(-50, 192);
			this.btn_fake.Name = "btn_fake";
			this.btn_fake.Size = new global::System.Drawing.Size(0, 0);
			this.btn_fake.TabIndex = 13;
			this.btn_fake.TabStop = false;
			this.btn_fake.Click += new global::System.EventHandler(this.btn_fake_Click);
			this.btn_fakeCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btn_fakeCancel.Location = new global::System.Drawing.Point(-82, 192);
			this.btn_fakeCancel.Name = "btn_fakeCancel";
			this.btn_fakeCancel.Size = new global::System.Drawing.Size(0, 0);
			this.btn_fakeCancel.TabIndex = 14;
			this.btn_fakeCancel.TabStop = false;
			this.btn_fakeCancel.Click += new global::System.EventHandler(this.btn_fakeCancel_Click);
			this.toolStrip1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip1.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_save,
				this.btn_cancel,
				this.toolStripSeparator1,
				this.btn_changePassword
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(0, 225);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Padding = new global::System.Windows.Forms.Padding(0, 2, 1, 2);
			this.toolStrip1.Size = new global::System.Drawing.Size(402, 43);
			this.toolStrip1.TabIndex = 15;
			this.toolStrip1.TabStop = true;
			this.toolStrip1.Text = "Toolbar";
			this.btn_save.Image = global::ClockWorkAPI.Properties.Resources.check2;
			this.btn_save.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_save.Name = "btn_save";
			this.btn_save.Size = new global::System.Drawing.Size(89, 36);
			this.btn_save.Text = "&Login";
			this.btn_save.Click += new global::System.EventHandler(this.btn_save_Click);
			this.btn_cancel.Image = global::ClockWorkAPI.Properties.Resources.delete2;
			this.btn_cancel.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(98, 36);
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new global::System.Drawing.Size(6, 39);
			this.btn_changePassword.Image = global::ClockWorkAPI.Properties.Resources.key1;
			this.btn_changePassword.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_changePassword.Name = "btn_changePassword";
			this.btn_changePassword.Size = new global::System.Drawing.Size(185, 36);
			this.btn_changePassword.Text = "Change &password";
			this.btn_changePassword.Click += new global::System.EventHandler(this.btn_changePassword_Click);
			this.lbl_banner.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.lbl_banner.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.lbl_banner.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("lbl_banner.Image");
			this.lbl_banner.Location = new global::System.Drawing.Point(0, 0);
			this.lbl_banner.Name = "lbl_banner";
			this.lbl_banner.Size = new global::System.Drawing.Size(402, 100);
			this.lbl_banner.TabIndex = 16;
			this.lbl_banner.DoubleClick += new global::System.EventHandler(this.lbl_banner_DoubleClick);
			this.lbl_banner.Click += new global::System.EventHandler(this.lbl_banner_Click);
			this.balloonTip_capsLock.AntiAlias = true;
			this.balloonTip_capsLock.Enabled = false;
			this.balloonTip_capsLock.BalloonDisplaying += new global::System.EventHandler(this.balloonTip_capsLock_BalloonDisplaying);
			this.txt_pass.AccessibleDescription = "Password";
			this.txt_pass.AccessibleName = "Password";
			this.txt_pass.BackColor = global::System.Drawing.SystemColors.Window;
			this.balloonTip_capsLock.SetBalloonCaption(this.txt_pass, "Caps Lock On");
			this.balloonTip_capsLock.SetBalloonText(this.txt_pass, "Warning: Caps lock is on.");
			this.txt_pass.Font = new global::System.Drawing.Font("Marlett", 15.75f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 2);
			this.txt_pass.Location = new global::System.Drawing.Point(88, 160);
			this.txt_pass.Name = "txt_pass";
			this.txt_pass.PasswordChar = 'g';
			this.txt_pass.Size = new global::System.Drawing.Size(304, 28);
			this.txt_pass.TabIndex = 8;
			this.txt_pass.KeyUp += new global::System.Windows.Forms.KeyEventHandler(this.txt_pass_KeyUp);
			this.txt_pass.Enter += new global::System.EventHandler(this.txt_pass_Enter);
			base.AcceptButton = this.btn_fake;
			this.AutoScaleBaseSize = new global::System.Drawing.Size(6, 15);
			base.CancelButton = this.btn_fakeCancel;
			base.ClientSize = new global::System.Drawing.Size(402, 268);
			base.Controls.Add(this.lbl_message);
			base.Controls.Add(this.lbl_banner);
			base.Controls.Add(this.toolStrip1);
			base.Controls.Add(this.btn_fakeCancel);
			base.Controls.Add(this.btn_fake);
			base.Controls.Add(this.link_help);
			base.Controls.Add(this.chk_rememberPassword);
			base.Controls.Add(this.txt_pass);
			base.Controls.Add(this.txt_user);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			this.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "ClockWorkLoginDialog";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ClockWork Login";
			base.Load += new global::System.EventHandler(this.ClockWorkLoginDialog_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400032B RID: 811
		private global::System.Windows.Forms.Label label1;

		// Token: 0x0400032C RID: 812
		private global::System.Windows.Forms.Label label2;

		// Token: 0x0400032D RID: 813
		private global::System.Windows.Forms.TextBox txt_user;

		// Token: 0x0400032E RID: 814
		private global::System.Windows.Forms.TextBox txt_pass;

		// Token: 0x0400032F RID: 815
		private global::System.Windows.Forms.ImageList imageList1;

		// Token: 0x04000330 RID: 816
		private global::System.Windows.Forms.CheckBox chk_rememberPassword;

		// Token: 0x04000331 RID: 817
		private global::System.Windows.Forms.LinkLabel link_help;

		// Token: 0x04000332 RID: 818
		private global::System.Windows.Forms.Label lbl_message;

		// Token: 0x04000333 RID: 819
		private global::System.Windows.Forms.Button btn_fake;

		// Token: 0x04000334 RID: 820
		private global::System.Windows.Forms.Button btn_fakeCancel;

		// Token: 0x04000335 RID: 821
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x04000336 RID: 822
		private global::System.Windows.Forms.ToolStripButton btn_save;

		// Token: 0x04000337 RID: 823
		private global::System.Windows.Forms.ToolStripButton btn_cancel;

		// Token: 0x04000338 RID: 824
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

		// Token: 0x04000339 RID: 825
		private global::System.Windows.Forms.ToolStripButton btn_changePassword;

		// Token: 0x0400033A RID: 826
		private global::System.Windows.Forms.Label lbl_banner;

		// Token: 0x0400033B RID: 827
		private global::DevComponents.DotNetBar.BalloonTip balloonTip_capsLock;

		// Token: 0x0400033C RID: 828
		private global::System.ComponentModel.IContainer components;
	}
}

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ClockWorkAPI
{
	// Token: 0x02000018 RID: 24
	public partial class ClockWorkLoginDialogPasswordChange : Form
	{
		// Token: 0x060000CE RID: 206 RVA: 0x00005F59 File Offset: 0x00004F59
		public ClockWorkLoginDialogPasswordChange()
		{
			this.InitializeComponent();
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000667D File Offset: 0x0000567D
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00006690 File Offset: 0x00005690
		private void btn_ok_Click(object sender, EventArgs e)
		{
			if (this.txt_newpwd1.Text.CompareTo(this.txt_newpwd2.Text) == 0)
			{
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
			else
			{
				MessageBox.Show("The 'New password' doesn't match the 'New password (again)'!  Please correct this and click 'ok' again.");
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x000066E4 File Offset: 0x000056E4
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00006701 File Offset: 0x00005701
		public string oldUsername
		{
			get
			{
				return this.txt_username.Text;
			}
			set
			{
				this.txt_username.Text = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00006714 File Offset: 0x00005714
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00006731 File Offset: 0x00005731
		public string oldPassword
		{
			get
			{
				return this.txt_oldpwd.Text;
			}
			set
			{
				this.txt_oldpwd.Text = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00006744 File Offset: 0x00005744
		public string newPassword
		{
			get
			{
				return this.txt_newpwd1.Text;
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00006764 File Offset: 0x00005764
		private void ClockWorkLoginDialogPasswordChange_Load(object sender, EventArgs e)
		{
			if (this.txt_username.Text.Length > 0)
			{
				base.ActiveControl = this.txt_oldpwd;
			}
			else
			{
				base.ActiveControl = this.txt_username;
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000067A8 File Offset: 0x000057A8
		private void txt_newpwd2_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.btn_ok_Click(this.btn_ok, new EventArgs());
			}
		}
	}
}

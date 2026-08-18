using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox
{
	// Token: 0x02000096 RID: 150
	public partial class InputPassword : Form
	{
		// Token: 0x060005C7 RID: 1479 RVA: 0x0002FCB9 File Offset: 0x0002ECB9
		public InputPassword()
		{
			this.InitializeComponent();
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x000304B6 File Offset: 0x0002F4B6
		private void InputPassword_Load(object sender, EventArgs e)
		{
			base.ActiveControl = this.txt_pwd1;
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x000304C8 File Offset: 0x0002F4C8
		public string Password
		{
			get
			{
				return this.txt_pwd1.Text;
			}
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x000304E8 File Offset: 0x0002F4E8
		private void btn_ok_Click(object sender, EventArgs e)
		{
			string text = this.txt_pwd1.Text;
			string text2 = this.txt_pwd2.Text;
			if (text.Trim().Length < 1)
			{
				MessageBox.Show("Please enter at least one numeric or alpha character.");
			}
			else if (text.CompareTo(text2) != 0)
			{
				MessageBox.Show("Your passwords don't match.");
			}
			else
			{
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00030560 File Offset: 0x0002F560
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}
	}
}

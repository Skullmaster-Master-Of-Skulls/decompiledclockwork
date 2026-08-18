using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ClockWorkAPI
{
	// Token: 0x02000075 RID: 117
	public partial class InputPassword : Form
	{
		// Token: 0x0600060C RID: 1548 RVA: 0x0001FFEA File Offset: 0x0001EFEA
		public InputPassword()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00020303 File Offset: 0x0001F303
		private void btn_ok_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00020315 File Offset: 0x0001F315
		public void SetCaption(string caption)
		{
			this.label1.Text = caption;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00020325 File Offset: 0x0001F325
		public void SetPasswordChar(char c)
		{
			this.txt.PasswordChar = c;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00020335 File Offset: 0x0001F335
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00020340 File Offset: 0x0001F340
		public string GetPassword()
		{
			return this.txt.Text;
		}
	}
}

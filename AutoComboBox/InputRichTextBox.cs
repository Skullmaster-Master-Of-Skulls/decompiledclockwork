using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AutoComboBox.Properties;
using DevComponents.DotNetBar;

namespace AutoComboBox
{
	// Token: 0x0200007C RID: 124
	public partial class InputRichTextBox : Form
	{
		// Token: 0x060004E2 RID: 1250 RVA: 0x00027364 File Offset: 0x00026364
		public InputRichTextBox(string title, string caption)
		{
			this.InitializeComponent();
			this.Text = title;
			this.label1.Text = caption;
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00027C0B File Offset: 0x00026C0B
		private void InputRichTextBox_Load(object sender, EventArgs e)
		{
			base.ActiveControl = this.txt;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00027C1B File Offset: 0x00026C1B
		private void btn_OK_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00027C1E File Offset: 0x00026C1E
		private void btn_cancel_Click(object sender, EventArgs e)
		{
		}
	}
}

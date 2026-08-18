using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox
{
	// Token: 0x0200007D RID: 125
	public partial class CustomProgressBar : Form
	{
		// Token: 0x060004E8 RID: 1256 RVA: 0x00027C21 File Offset: 0x00026C21
		public CustomProgressBar(bool ShowCancelButton)
		{
			this.InitializeComponent();
			this.btn_cancel.Visible = ShowCancelButton;
		}
	}
}

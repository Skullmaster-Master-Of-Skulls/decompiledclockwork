using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AutoComboBox.Properties;

namespace AutoComboBox.InputDialogControls
{
	// Token: 0x020000CF RID: 207
	public partial class frmSaveClose : Form
	{
		// Token: 0x060007E2 RID: 2018 RVA: 0x0003E82B File Offset: 0x0003D82B
		public frmSaveClose()
		{
			this.InitializeComponent();
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0003E844 File Offset: 0x0003D844
		private void btn_ok_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0003E856 File Offset: 0x0003D856
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0003E860 File Offset: 0x0003D860
		public static DialogResult ShowForm(IWin32Window owner, string title, Control ctrl)
		{
			frmSaveClose frmSaveClose = new frmSaveClose();
			frmSaveClose.Controls.Add(ctrl);
			ctrl.Dock = DockStyle.Fill;
			ctrl.BringToFront();
			DialogResult result = frmSaveClose.ShowDialog(owner);
			frmSaveClose.Controls.Remove(ctrl);
			return result;
		}
	}
}

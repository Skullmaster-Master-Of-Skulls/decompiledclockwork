using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AutoComboBox.Properties;

namespace AutoComboBox
{
	// Token: 0x020000EA RID: 234
	public partial class ExportTypeChooser : Form
	{
		// Token: 0x06000942 RID: 2370 RVA: 0x00048231 File Offset: 0x00047231
		public ExportTypeChooser()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x000488C9 File Offset: 0x000478C9
		private void btn_fakeCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x000488D3 File Offset: 0x000478D3
		private void btn_fakeOK_Click(object sender, EventArgs e)
		{
			this.OK();
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x000488E0 File Offset: 0x000478E0
		public ExportTypeChooser.ExportType SelectedExportType
		{
			get
			{
				ExportTypeChooser.ExportType result;
				switch (this.listBox1.SelectedIndex)
				{
				case 0:
					result = ExportTypeChooser.ExportType.Excel;
					break;
				case 1:
					result = ExportTypeChooser.ExportType.Access;
					break;
				case 2:
					result = ExportTypeChooser.ExportType.DelimiteredText;
					break;
				case 3:
					result = ExportTypeChooser.ExportType.FormattedText;
					break;
				default:
					result = ExportTypeChooser.ExportType.Unknown;
					break;
				}
				return result;
			}
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00048928 File Offset: 0x00047928
		private void OK()
		{
			if (this.listBox1.SelectedIndex >= 0)
			{
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00048959 File Offset: 0x00047959
		private void listBox1_DoubleClick(object sender, EventArgs e)
		{
			this.OK();
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00048963 File Offset: 0x00047963
		private void btn_save_Click(object sender, EventArgs e)
		{
			this.OK();
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0004896D File Offset: 0x0004796D
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x020000EB RID: 235
		public enum ExportType
		{
			// Token: 0x040006BB RID: 1723
			Unknown = -1,
			// Token: 0x040006BC RID: 1724
			Excel,
			// Token: 0x040006BD RID: 1725
			Access,
			// Token: 0x040006BE RID: 1726
			DelimiteredText,
			// Token: 0x040006BF RID: 1727
			FormattedText
		}
	}
}

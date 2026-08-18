using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DynamicScreens.AdminTools
{
	// Token: 0x0200001C RID: 28
	public partial class ScreenTypeChooser : Form
	{
		// Token: 0x060001C3 RID: 451 RVA: 0x00017931 File Offset: 0x00016931
		public ScreenTypeChooser()
		{
			this.InitializeComponent();
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000186A7 File Offset: 0x000176A7
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000186B1 File Offset: 0x000176B1
		private void btn_ok_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x000186C4 File Offset: 0x000176C4
		public int SelectedScreenType
		{
			get
			{
				int result;
				if (this.rbtn_perStudent.Checked)
				{
					result = 0;
				}
				else if (this.rbtn_perAppointment.Checked)
				{
					result = 1;
				}
				else if (this.rbtn_anonymous.Checked)
				{
					result = 2;
				}
				else if (this.rbnt_survey.Checked)
				{
					result = 3;
				}
				else if (this.rbtn_staffPA.Checked)
				{
					result = 20;
				}
				else if (this.rb_infoPM.Checked)
				{
					result = 25;
				}
				else if (this.rbtn_instructorPerDate.Checked)
				{
					result = 30;
				}
				else
				{
					result = -1;
				}
				return result;
			}
		}
	}
}

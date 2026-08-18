using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AutoComboBox.MyControls;

namespace AutoComboBox.InputDialogControls
{
	// Token: 0x020000CE RID: 206
	public partial class DateRangeInputAcc : Form
	{
		// Token: 0x060007DA RID: 2010 RVA: 0x0003E417 File Offset: 0x0003D417
		public DateRangeInputAcc()
		{
			this.InitializeComponent();
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0003E430 File Offset: 0x0003D430
		public DateRangeInputAcc(string title, DateTime defaultDate)
		{
			this.InitializeComponent();
			this.Text = title;
			this.dtp_date.Date = defaultDate;
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0003E45E File Offset: 0x0003D45E
		private void btn_select_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0003E470 File Offset: 0x0003D470
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0003E47A File Offset: 0x0003D47A
		private void DateRangeInputAcc_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x0003E480 File Offset: 0x0003D480
		public DateTime SelectedDate
		{
			get
			{
				return this.dtp_date.Date;
			}
		}
	}
}

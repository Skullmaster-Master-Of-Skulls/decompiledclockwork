using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AutoComboBox.Properties;

namespace AutoComboBox
{
	// Token: 0x020000EC RID: 236
	public partial class DateRangeInput : Form
	{
		// Token: 0x0600094C RID: 2380 RVA: 0x00048977 File Offset: 0x00047977
		public DateRangeInput(DateTime currentStartDate, DateTime currentEndDate)
		{
			this.InitializeComponent();
			this.dtp_startDate.Value = currentStartDate;
			this.dtp_endDate.Value = currentEndDate;
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x000489A4 File Offset: 0x000479A4
		public DateRangeInput(string caption, string title, DateTime currentStartDate, DateTime currentEndDate)
		{
			this.InitializeComponent();
			this.dtp_startDate.Value = currentStartDate;
			this.dtp_endDate.Value = currentEndDate;
			this.label1.Text = caption;
			this.Text = title;
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x00049854 File Offset: 0x00048854
		// (set) Token: 0x06000951 RID: 2385 RVA: 0x000498A7 File Offset: 0x000488A7
		public DateTime StartDate
		{
			get
			{
				return new DateTime(this.dtp_startDate.Value.Year, this.dtp_startDate.Value.Month, this.dtp_startDate.Value.Day, 0, 0, 0);
			}
			set
			{
				this.dtp_startDate.Value = value;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000952 RID: 2386 RVA: 0x000498B8 File Offset: 0x000488B8
		// (set) Token: 0x06000953 RID: 2387 RVA: 0x0004990B File Offset: 0x0004890B
		public DateTime EndDate
		{
			get
			{
				return new DateTime(this.dtp_endDate.Value.Year, this.dtp_endDate.Value.Month, this.dtp_endDate.Value.Day, 0, 0, 0);
			}
			set
			{
				this.dtp_endDate.Value = value;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x0004991C File Offset: 0x0004891C
		public DateTime StartDateTime
		{
			get
			{
				return this.dtp_startDate.Value;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x0004993C File Offset: 0x0004893C
		public DateTime EndDateTime
		{
			get
			{
				return this.dtp_endDate.Value;
			}
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0004995C File Offset: 0x0004895C
		public void ShowEndDateTimePicker()
		{
			this.dtp_endDate.Visible = true;
			this.lbl_endTime.Visible = true;
			this.btn_set1month.Visible = true;
			this.btn_set1week.Visible = true;
			this.btn_setToCurrentTerm.Visible = true;
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x000499AC File Offset: 0x000489AC
		// (set) Token: 0x06000958 RID: 2392 RVA: 0x000499C9 File Offset: 0x000489C9
		public string StartDateCustomFormat
		{
			get
			{
				return this.dtp_startDate.CustomFormat;
			}
			set
			{
				this.dtp_startDate.CustomFormat = value;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x000499DC File Offset: 0x000489DC
		// (set) Token: 0x0600095A RID: 2394 RVA: 0x000499F9 File Offset: 0x000489F9
		public string EndDateCustomFormat
		{
			get
			{
				return this.dtp_endDate.CustomFormat;
			}
			set
			{
				this.dtp_endDate.CustomFormat = value;
			}
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00049A09 File Offset: 0x00048A09
		private void DateRangeInput_Load(object sender, EventArgs e)
		{
			base.ActiveControl = this.dtp_startDate;
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x00049A19 File Offset: 0x00048A19
		private void btn_fakeOK_Click(object sender, EventArgs e)
		{
			this.btn_ok_Click(this.btn_ok, null);
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x00049A2A File Offset: 0x00048A2A
		private void btn_fakeCancel_Click(object sender, EventArgs e)
		{
			this.btn_cancel_Click(this.btn_cancel, null);
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00049A3B File Offset: 0x00048A3B
		private void dtp_startDate_KeyPress(object sender, KeyPressEventArgs e)
		{
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x00049A3E File Offset: 0x00048A3E
		private void btn_ok_Click(object sender, EventArgs e)
		{
			base.ActiveControl = null;
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x00049A58 File Offset: 0x00048A58
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x00049A62 File Offset: 0x00048A62
		private void btn_fakeAccept_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x00049A68 File Offset: 0x00048A68
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			bool result;
			if (keyData == Keys.Return && base.ActiveControl != null)
			{
				base.SelectNextControl(base.ActiveControl, true, true, true, true);
				this.btn_ok_Click(this.btn_ok, null);
				result = true;
			}
			else
			{
				result = base.ProcessCmdKey(ref msg, keyData);
			}
			return result;
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x00049ABC File Offset: 0x00048ABC
		private void dtp_startDate_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				e.SuppressKeyPress = true;
				e.Handled = true;
				this.btn_ok_Click(this.btn_ok, null);
			}
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x00049AFC File Offset: 0x00048AFC
		private void dateTimeInput1_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.btn_ok_Click(this.btn_ok, null);
				e.Handled = true;
			}
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x00049B34 File Offset: 0x00048B34
		private void btn_set1week_Click(object sender, EventArgs e)
		{
			this.dtp_endDate.Value = this.dtp_startDate.Value.AddDays(7.0);
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00049B6C File Offset: 0x00048B6C
		private void btn_set1month_Click(object sender, EventArgs e)
		{
			this.dtp_endDate.Value = this.dtp_startDate.Value.AddMonths(1);
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x00049B9C File Offset: 0x00048B9C
		private void btn_setToCurrentTerm_Click(object sender, EventArgs e)
		{
			int month = DateTime.Now.Month;
			int year = DateTime.Now.Year;
			if (month < 5)
			{
				this.dtp_startDate.Value = new DateTime(year, 1, 1);
				this.dtp_endDate.Value = new DateTime(year, 4, 30);
			}
			else if (month < 9)
			{
				this.dtp_startDate.Value = new DateTime(year, 5, 1);
				this.dtp_endDate.Value = new DateTime(year, 8, 30);
			}
			else
			{
				this.dtp_startDate.Value = new DateTime(year, 9, 1);
				this.dtp_endDate.Value = new DateTime(year, 12, 31);
			}
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x00049C64 File Offset: 0x00048C64
		private void btn_goForward_Click(object sender, EventArgs e)
		{
			TimeSpan timeSpan = this.dtp_endDate.Value - this.dtp_startDate.Value;
			this.dtp_startDate.Value = this.dtp_startDate.Value.AddDays(7.0);
			this.dtp_endDate.Value = this.dtp_startDate.Value.AddDays(timeSpan.TotalDays);
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00049CDC File Offset: 0x00048CDC
		private void btn_goBackward_Click(object sender, EventArgs e)
		{
			TimeSpan timeSpan = this.dtp_endDate.Value - this.dtp_startDate.Value;
			this.dtp_startDate.Value = this.dtp_startDate.Value.AddDays(-7.0);
			this.dtp_endDate.Value = this.dtp_startDate.Value.AddDays(timeSpan.TotalDays);
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x00049D54 File Offset: 0x00048D54
		private void btn_goToToday_Click(object sender, EventArgs e)
		{
			TimeSpan timeSpan = this.dtp_endDate.Value - this.dtp_startDate.Value;
			this.dtp_startDate.Value = DateTime.Now.Date;
			this.dtp_endDate.Value = this.dtp_startDate.Value.AddDays(timeSpan.TotalDays);
		}
	}
}

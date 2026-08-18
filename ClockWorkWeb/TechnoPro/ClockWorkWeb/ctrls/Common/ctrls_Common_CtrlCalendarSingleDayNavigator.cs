using System;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TechnoPro.Common.UI.Web.Entity.Web.EventArgs;

namespace TechnoPro.ClockWorkWeb.ctrls.Common
{
	// Token: 0x02000150 RID: 336
	public class ctrls_Common_CtrlCalendarSingleDayNavigator : UserControl
	{
		// Token: 0x06000A45 RID: 2629 RVA: 0x000476C8 File Offset: 0x000458C8
		protected void Page_Load(object sender, EventArgs e)
		{
			bool isPostBack = this.Page.IsPostBack;
			if (isPostBack)
			{
				string text = (this.hf_date.Value ?? "").Trim();
				bool flag = text.Length > 0;
				if (flag)
				{
					this.dtp_date.Value = text;
				}
			}
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0004771C File Offset: 0x0004591C
		public void SetSelectedDate(DateTime? date)
		{
			this.dtp_date.Value = (((date != null) ? date.GetValueOrDefault().ToString("MM/dd/yyyy") : null) ?? "");
			this.hf_date.Value = ((date != null) ? date.Value.ToString("yyyy-MM-dd") : "");
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x00047790 File Offset: 0x00045990
		public DateTime? SelectedDate
		{
			get
			{
				string text = (this.hf_date.Value ?? "").Trim();
				bool flag = text.Length < 1;
				if (flag)
				{
					text = this.dtp_date.Value;
				}
				DateTime value;
				bool flag2 = string.IsNullOrWhiteSpace(text) || !DateTime.TryParse(text, out value);
				DateTime? result;
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = new DateTime?(value);
				}
				return result;
			}
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00047808 File Offset: 0x00045A08
		protected override void OnLoad(EventArgs e)
		{
			bool flag = this.SelectedDate == null;
			if (flag)
			{
				this.SetSelectedDate(new DateTime?(DateTime.Now.Date.AddDays(1.0)));
			}
			base.OnLoad(e);
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x00047860 File Offset: 0x00045A60
		protected void btn_next_Click(object sender, EventArgs e)
		{
			DateTime? selectedDate = this.SelectedDate;
			DateTime value = (selectedDate != null) ? selectedDate.GetValueOrDefault().AddDays(1.0) : DateTime.Now.Date.AddDays(1.0);
			this.SetSelectedDate(new DateTime?(value));
			this.UpdateCalendar();
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x000478CC File Offset: 0x00045ACC
		protected void btn_prev_Click(object sender, EventArgs e)
		{
			DateTime? selectedDate = this.SelectedDate;
			DateTime value = (selectedDate != null) ? selectedDate.GetValueOrDefault().AddDays(-1.0) : DateTime.Now.Date.AddDays(1.0);
			this.SetSelectedDate(new DateTime?(value));
			this.UpdateCalendar();
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x00047937 File Offset: 0x00045B37
		protected void dt_SelectedDateChanged(object sender, EventArgs e)
		{
			this.UpdateCalendar();
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x00047937 File Offset: 0x00045B37
		protected void btn_refresh_Click(object sender, EventArgs e)
		{
			this.UpdateCalendar();
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00047944 File Offset: 0x00045B44
		private void UpdateCalendar()
		{
			DateTime dt = this.SelectedDate ?? DateTime.Now.Date.AddDays(1.0);
			this.FireDateChanged(dt);
		}

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06000A4E RID: 2638 RVA: 0x00047994 File Offset: 0x00045B94
		// (remove) Token: 0x06000A4F RID: 2639 RVA: 0x000479CC File Offset: 0x00045BCC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<DateArgs> DateChanged;

		// Token: 0x06000A50 RID: 2640 RVA: 0x00047A01 File Offset: 0x00045C01
		private void FireDateChanged(DateTime dt)
		{
			EventHandler<DateArgs> dateChanged = this.DateChanged;
			if (dateChanged != null)
			{
				dateChanged(this, new DateArgs
				{
					Date = dt
				});
			}
		}

		// Token: 0x040007E9 RID: 2025
		protected ImageButton btn_prev;

		// Token: 0x040007EA RID: 2026
		protected ImageButton btn_next;

		// Token: 0x040007EB RID: 2027
		protected HtmlInputText dtp_date;

		// Token: 0x040007EC RID: 2028
		protected LinkButton btn_refresh;

		// Token: 0x040007ED RID: 2029
		protected HiddenField hf_date;
	}
}

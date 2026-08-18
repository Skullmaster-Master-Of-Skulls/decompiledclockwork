using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001A25 RID: 6693
	public class SchedulerInfo : ISchedulerInfo
	{
		// Token: 0x17004EA9 RID: 20137
		// (get) Token: 0x060103D3 RID: 66515 RVA: 0x003A0EF7 File Offset: 0x0039F0F7
		// (set) Token: 0x060103D4 RID: 66516 RVA: 0x003A0EFF File Offset: 0x0039F0FF
		public DateTime ViewStart { get; set; }

		// Token: 0x17004EAA RID: 20138
		// (get) Token: 0x060103D5 RID: 66517 RVA: 0x003A0F08 File Offset: 0x0039F108
		// (set) Token: 0x060103D6 RID: 66518 RVA: 0x003A0F10 File Offset: 0x0039F110
		public DateTime ViewEnd { get; set; }

		// Token: 0x17004EAB RID: 20139
		// (get) Token: 0x060103D7 RID: 66519 RVA: 0x003A0F19 File Offset: 0x0039F119
		// (set) Token: 0x060103D8 RID: 66520 RVA: 0x003A0F21 File Offset: 0x0039F121
		public bool EnableDescriptionField { get; set; }

		// Token: 0x17004EAC RID: 20140
		// (get) Token: 0x060103D9 RID: 66521 RVA: 0x003A0F2A File Offset: 0x0039F12A
		// (set) Token: 0x060103DA RID: 66522 RVA: 0x003A0F32 File Offset: 0x0039F132
		public int MinutesPerRow { get; set; }

		// Token: 0x17004EAD RID: 20141
		// (get) Token: 0x060103DB RID: 66523 RVA: 0x003A0F3B File Offset: 0x0039F13B
		// (set) Token: 0x060103DC RID: 66524 RVA: 0x003A0F43 File Offset: 0x0039F143
		public int TimeZoneOffset { get; set; }

		// Token: 0x17004EAE RID: 20142
		// (get) Token: 0x060103DD RID: 66525 RVA: 0x003A0F4C File Offset: 0x0039F14C
		// (set) Token: 0x060103DE RID: 66526 RVA: 0x003A0F54 File Offset: 0x0039F154
		public int VisibleAppointmentsPerDay { get; set; }

		// Token: 0x17004EAF RID: 20143
		// (get) Token: 0x060103DF RID: 66527 RVA: 0x003A0F5D File Offset: 0x0039F15D
		// (set) Token: 0x060103E0 RID: 66528 RVA: 0x003A0F65 File Offset: 0x0039F165
		public AppointmentUpdateMode UpdateMode { get; set; }

		// Token: 0x060103E1 RID: 66529 RVA: 0x003A0F6E File Offset: 0x0039F16E
		public SchedulerInfo()
		{
			this.UpdateMode = AppointmentUpdateMode.Batch;
		}

		// Token: 0x060103E2 RID: 66530 RVA: 0x003A0F80 File Offset: 0x0039F180
		public SchedulerInfo(ISchedulerInfo baseInfo)
		{
			this.ViewStart = baseInfo.ViewStart;
			this.ViewEnd = baseInfo.ViewEnd;
			this.TimeZoneOffset = baseInfo.TimeZoneOffset;
			this.MinutesPerRow = baseInfo.MinutesPerRow;
			this.EnableDescriptionField = baseInfo.EnableDescriptionField;
			this.VisibleAppointmentsPerDay = baseInfo.VisibleAppointmentsPerDay;
			this.UpdateMode = baseInfo.UpdateMode;
		}

		// Token: 0x060103E3 RID: 66531 RVA: 0x003A0FE8 File Offset: 0x0039F1E8
		internal SchedulerInfo(RadScheduler scheduler)
		{
			this.ViewStart = scheduler.VisibleRangeStart;
			this.ViewEnd = scheduler.VisibleRangeEnd;
			this.EnableDescriptionField = scheduler.EnableDescriptionField;
			this.MinutesPerRow = scheduler.MinutesPerRow;
			this.TimeZoneOffset = (int)scheduler.TimeZoneOffset.TotalMilliseconds;
			this.UpdateMode = scheduler.WebServiceSettings.UpdateMode;
			if (scheduler.SelectedView == SchedulerViewType.MonthView)
			{
				this.VisibleAppointmentsPerDay = scheduler.MonthView.VisibleAppointmentsPerDay;
			}
			if (scheduler.SelectedView == SchedulerViewType.YearView)
			{
				this.VisibleAppointmentsPerDay = 1;
			}
		}
	}
}

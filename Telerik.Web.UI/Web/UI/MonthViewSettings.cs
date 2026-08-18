using System;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001A45 RID: 6725
	public class MonthViewSettings : GroupableViewSettings
	{
		// Token: 0x060104F6 RID: 66806 RVA: 0x003A4485 File Offset: 0x003A2685
		internal MonthViewSettings(IScheduler scheduler, StateBag ownerViewState) : base(scheduler, "MonthViewSettings", ownerViewState)
		{
		}

		// Token: 0x17004F23 RID: 20259
		// (get) Token: 0x060104F7 RID: 66807 RVA: 0x003A4494 File Offset: 0x003A2694
		// (set) Token: 0x060104F8 RID: 66808 RVA: 0x003A44B4 File Offset: 0x003A26B4
		[DefaultValue("MMM, yyyy")]
		[Description("The RadScheduler's header date format string in Month View (e.g. \"MMM, yyyy\").")]
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		public string HeaderDateFormat
		{
			get
			{
				return (string)(base.ViewState["HeaderDateFormat"] ?? "MMM, yyyy");
			}
			set
			{
				base.ViewState["HeaderDateFormat"] = value;
			}
		}

		// Token: 0x17004F24 RID: 20260
		// (get) Token: 0x060104F9 RID: 66809 RVA: 0x003A44C7 File Offset: 0x003A26C7
		// (set) Token: 0x060104FA RID: 66810 RVA: 0x003A44E7 File Offset: 0x003A26E7
		[DefaultValue("ddd")]
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		[Description("The column header date format string in Month View (e.g. \"ddd\").")]
		public string ColumnHeaderDateFormat
		{
			get
			{
				return (string)(base.ViewState["ColumnHeaderDateFormat"] ?? "ddd");
			}
			set
			{
				base.ViewState["ColumnHeaderDateFormat"] = value;
			}
		}

		// Token: 0x17004F25 RID: 20261
		// (get) Token: 0x060104FB RID: 66811 RVA: 0x003A44FA File Offset: 0x003A26FA
		// (set) Token: 0x060104FC RID: 66812 RVA: 0x003A451A File Offset: 0x003A271A
		[DefaultValue("dd")]
		[NotifyParentProperty(true)]
		[Description("The day header date format string in Month View (e.g. \"dd\").")]
		[Category("Appearance")]
		public string DayHeaderDateFormat
		{
			get
			{
				return (string)(base.ViewState["DayHeaderDateFormat"] ?? "dd");
			}
			set
			{
				base.ViewState["DayHeaderDateFormat"] = value;
			}
		}

		// Token: 0x17004F26 RID: 20262
		// (get) Token: 0x060104FD RID: 66813 RVA: 0x003A452D File Offset: 0x003A272D
		// (set) Token: 0x060104FE RID: 66814 RVA: 0x003A454D File Offset: 0x003A274D
		[DefaultValue("d MMM")]
		[Description("The first day of month header date format in Month View (e.g. \"d MMM\").")]
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		public string FirstDayHeaderDateFormat
		{
			get
			{
				return (string)(base.ViewState["FirstDayHeaderDateFormat"] ?? "d MMM");
			}
			set
			{
				base.ViewState["FirstDayHeaderDateFormat"] = value;
			}
		}

		// Token: 0x17004F27 RID: 20263
		// (get) Token: 0x060104FF RID: 66815 RVA: 0x003A4560 File Offset: 0x003A2760
		// (set) Token: 0x06010500 RID: 66816 RVA: 0x003A4581 File Offset: 0x003A2781
		[Category("Behavior")]
		[Description("The number of visible appointments per day in month view.")]
		[DefaultValue(2)]
		public int VisibleAppointmentsPerDay
		{
			get
			{
				return (int)(base.ViewState["VisibleAppointmentsPerDay"] ?? 2);
			}
			set
			{
				base.ViewState["VisibleAppointmentsPerDay"] = value;
				base.Owner.NotifyDataPropertyChanged();
			}
		}

		// Token: 0x17004F28 RID: 20264
		// (get) Token: 0x06010501 RID: 66817 RVA: 0x003A45A4 File Offset: 0x003A27A4
		// (set) Token: 0x06010502 RID: 66818 RVA: 0x003A45C5 File Offset: 0x003A27C5
		[Category("Behavior")]
		[Description("A value indicating whether the height of each row should be adjusted to match the height of its content.")]
		[DefaultValue(false)]
		public bool AdaptiveRowHeight
		{
			get
			{
				return (bool)(base.ViewState["AdaptiveRowHeight"] ?? false);
			}
			set
			{
				base.ViewState["AdaptiveRowHeight"] = value;
				base.Owner.NotifyDataPropertyChanged();
			}
		}

		// Token: 0x17004F29 RID: 20265
		// (get) Token: 0x06010503 RID: 66819 RVA: 0x003A45E8 File Offset: 0x003A27E8
		// (set) Token: 0x06010504 RID: 66820 RVA: 0x003A4609 File Offset: 0x003A2809
		[DefaultValue(4)]
		[Category("Behavior")]
		[Description("The minimum row height (including date header). Ignored when AdaptiveRowHeight is set to true.")]
		public int MinimumRowHeight
		{
			get
			{
				return (int)(base.ViewState["MinimumRowHeight"] ?? 4);
			}
			set
			{
				base.ViewState["MinimumRowHeight"] = value;
				base.Owner.NotifyDataPropertyChanged();
			}
		}

		// Token: 0x06010505 RID: 66821 RVA: 0x003A462C File Offset: 0x003A282C
		internal override JavaScriptConverter GetConverter()
		{
			return new MonthViewSettingsConverter();
		}
	}
}

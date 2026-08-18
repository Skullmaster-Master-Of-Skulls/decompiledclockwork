using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001A41 RID: 6721
	public abstract class BaseMultiDayViewSettings : GroupableViewSettings
	{
		// Token: 0x060104C1 RID: 66753 RVA: 0x003A3F00 File Offset: 0x003A2100
		internal BaseMultiDayViewSettings(IScheduler owner, string keyPrefix, StateBag ownerViewState) : base(owner, keyPrefix, ownerViewState)
		{
		}

		// Token: 0x17004F08 RID: 20232
		// (get) Token: 0x060104C2 RID: 66754
		// (set) Token: 0x060104C3 RID: 66755
		public abstract string HeaderDateFormat { get; set; }

		// Token: 0x17004F09 RID: 20233
		// (get) Token: 0x060104C4 RID: 66756 RVA: 0x003A3F0B File Offset: 0x003A210B
		// (set) Token: 0x060104C5 RID: 66757 RVA: 0x003A3F30 File Offset: 0x003A2130
		[NotifyParentProperty(true)]
		[Description("The start of the day")]
		[DefaultValue(typeof(TimeSpan), "08:00:00")]
		[Category("Behavior")]
		public TimeSpan DayStartTime
		{
			get
			{
				return (TimeSpan)(base.ViewState["StartTime"] ?? RadScheduler.Defaults.DayStartTime);
			}
			set
			{
				base.ViewState["StartTime"] = value;
				base.Owner.NotifyDataPropertyChanged();
			}
		}

		// Token: 0x17004F0A RID: 20234
		// (get) Token: 0x060104C6 RID: 66758 RVA: 0x003A3F53 File Offset: 0x003A2153
		// (set) Token: 0x060104C7 RID: 66759 RVA: 0x003A3F78 File Offset: 0x003A2178
		[DefaultValue(typeof(TimeSpan), "18:00:00")]
		[NotifyParentProperty(true)]
		[Description("The end of the day")]
		[Category("Behavior")]
		public TimeSpan DayEndTime
		{
			get
			{
				return (TimeSpan)(base.ViewState["EndTime"] ?? RadScheduler.Defaults.DayEndTime);
			}
			set
			{
				TimeSpan timeSpan = value;
				if (timeSpan == TimeSpan.FromHours(0.0))
				{
					timeSpan = TimeSpan.FromHours(24.0);
				}
				base.ViewState["EndTime"] = timeSpan;
				base.Owner.NotifyDataPropertyChanged();
			}
		}

		// Token: 0x17004F0B RID: 20235
		// (get) Token: 0x060104C8 RID: 66760 RVA: 0x003A3FCD File Offset: 0x003A21CD
		// (set) Token: 0x060104C9 RID: 66761 RVA: 0x003A3FF2 File Offset: 0x003A21F2
		[NotifyParentProperty(true)]
		[Description("The start of the business day")]
		[DefaultValue(typeof(TimeSpan), "8:00")]
		[Category("Behavior")]
		public TimeSpan WorkDayStartTime
		{
			get
			{
				return (TimeSpan)(base.ViewState["WorkDayStartTime"] ?? RadScheduler.Defaults.WorkDayStartTime);
			}
			set
			{
				base.ViewState["WorkDayStartTime"] = value;
			}
		}

		// Token: 0x17004F0C RID: 20236
		// (get) Token: 0x060104CA RID: 66762 RVA: 0x003A400A File Offset: 0x003A220A
		// (set) Token: 0x060104CB RID: 66763 RVA: 0x003A402F File Offset: 0x003A222F
		[Category("Behavior")]
		[DefaultValue(typeof(TimeSpan), "17:00")]
		[Description("The end of the business day")]
		[NotifyParentProperty(true)]
		public TimeSpan WorkDayEndTime
		{
			get
			{
				return (TimeSpan)(base.ViewState["WorkDayEndTime"] ?? RadScheduler.Defaults.WorkDayEndTime);
			}
			set
			{
				base.ViewState["WorkDayEndTime"] = value;
			}
		}

		// Token: 0x17004F0D RID: 20237
		// (get) Token: 0x060104CC RID: 66764 RVA: 0x003A4047 File Offset: 0x003A2247
		// (set) Token: 0x060104CD RID: 66765 RVA: 0x003A4068 File Offset: 0x003A2268
		[Category("Appearance")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		[Description("Controls the visibility of the hours column")]
		public bool ShowHoursColumn
		{
			get
			{
				return (bool)(base.ViewState["ShowHoursColumn"] ?? true);
			}
			set
			{
				base.ViewState["ShowHoursColumn"] = value;
			}
		}

		// Token: 0x17004F0E RID: 20238
		// (get) Token: 0x060104CE RID: 66766 RVA: 0x003A4080 File Offset: 0x003A2280
		// (set) Token: 0x060104CF RID: 66767 RVA: 0x003A40A1 File Offset: 0x003A22A1
		[Category("Appearance")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		[Description("Controls whether to render indicator for hidden appointments")]
		public bool ShowHiddenAppointmentsIndicator
		{
			get
			{
				return (bool)(base.ViewState["ShowHiddenAppointmentsIndicator"] ?? true);
			}
			set
			{
				base.ViewState["ShowHiddenAppointmentsIndicator"] = value;
			}
		}

		// Token: 0x17004F0F RID: 20239
		// (get) Token: 0x060104D0 RID: 66768 RVA: 0x003A40B9 File Offset: 0x003A22B9
		// (set) Token: 0x060104D1 RID: 66769 RVA: 0x003A40DA File Offset: 0x003A22DA
		[DefaultValue(true)]
		[Category("Behavior")]
		[Description("Specifies whether to show an empty area at the end of each AllDay time slot that can be used to insert appointments.")]
		[NotifyParentProperty(true)]
		public bool ShowAllDayInsertArea
		{
			get
			{
				return (bool)(base.ViewState["ShowAllDayInsertArea"] ?? true);
			}
			set
			{
				base.ViewState["ShowAllDayInsertArea"] = value;
			}
		}

		// Token: 0x17004F10 RID: 20240
		// (get) Token: 0x060104D2 RID: 66770 RVA: 0x003A40F2 File Offset: 0x003A22F2
		// (set) Token: 0x060104D3 RID: 66771 RVA: 0x003A4113 File Offset: 0x003A2313
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[Description("Specifies whether to show an empty area at the end of each time slot that can be used to insert appointments.")]
		[DefaultValue(true)]
		public bool ShowInsertArea
		{
			get
			{
				return (bool)(base.ViewState["ShowInsertArea"] ?? true);
			}
			set
			{
				base.ViewState["ShowInsertArea"] = value;
			}
		}

		// Token: 0x17004F11 RID: 20241
		// (get) Token: 0x060104D4 RID: 66772 RVA: 0x003A412B File Offset: 0x003A232B
		// (set) Token: 0x060104D5 RID: 66773 RVA: 0x003A414C File Offset: 0x003A234C
		[Description("Gets or sets a value indicating whether the appointment start and end time should be rendered exactly")]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[DefaultValue(false)]
		public bool EnableExactTimeRendering
		{
			get
			{
				return (bool)(base.ViewState["EnableExactTimeRendering"] ?? false);
			}
			set
			{
				base.ViewState["EnableExactTimeRendering"] = value;
			}
		}

		// Token: 0x17004F12 RID: 20242
		// (get) Token: 0x060104D6 RID: 66774 RVA: 0x003A4164 File Offset: 0x003A2364
		protected internal TimeSpan DayStartTimeResolved
		{
			get
			{
				if (base.ViewState["StartTime"] == null)
				{
					return base.Owner.DayStartTime;
				}
				return this.DayStartTime;
			}
		}

		// Token: 0x17004F13 RID: 20243
		// (get) Token: 0x060104D7 RID: 66775 RVA: 0x003A418A File Offset: 0x003A238A
		protected internal TimeSpan DayEndTimeResolved
		{
			get
			{
				if (base.ViewState["EndTime"] == null)
				{
					return base.Owner.DayEndTime;
				}
				return this.DayEndTime;
			}
		}

		// Token: 0x17004F14 RID: 20244
		// (get) Token: 0x060104D8 RID: 66776 RVA: 0x003A41B0 File Offset: 0x003A23B0
		protected internal TimeSpan WorkDayStartTimeResolved
		{
			get
			{
				if (base.ViewState["WorkDayStartTime"] == null)
				{
					return base.Owner.WorkDayStartTime;
				}
				return this.WorkDayStartTime;
			}
		}

		// Token: 0x17004F15 RID: 20245
		// (get) Token: 0x060104D9 RID: 66777 RVA: 0x003A41D6 File Offset: 0x003A23D6
		protected internal TimeSpan WorkDayEndTimeResolved
		{
			get
			{
				if (base.ViewState["WorkDayEndTime"] == null)
				{
					return base.Owner.WorkDayEndTime;
				}
				return this.WorkDayEndTime;
			}
		}

		// Token: 0x17004F16 RID: 20246
		// (get) Token: 0x060104DA RID: 66778 RVA: 0x003A41FC File Offset: 0x003A23FC
		protected internal bool ShowHoursColumnResolved
		{
			get
			{
				if (base.ViewState["ShowHoursColumn"] == null)
				{
					return base.Owner.ShowHoursColumn;
				}
				return this.ShowHoursColumn;
			}
		}

		// Token: 0x17004F17 RID: 20247
		// (get) Token: 0x060104DB RID: 66779 RVA: 0x003A4222 File Offset: 0x003A2422
		protected internal bool EnableExactTimeRenderingResolved
		{
			get
			{
				return (bool)(base.ViewState["EnableExactTimeRendering"] ?? base.Owner.EnableExactTimeRendering);
			}
		}

		// Token: 0x17004F18 RID: 20248
		// (get) Token: 0x060104DC RID: 66780 RVA: 0x003A424D File Offset: 0x003A244D
		internal TimeSpan EffectiveDayStartTime
		{
			get
			{
				if (!base.Owner.ShowFullTime)
				{
					return this.DayStartTimeResolved;
				}
				return TimeSpan.Zero;
			}
		}

		// Token: 0x17004F19 RID: 20249
		// (get) Token: 0x060104DD RID: 66781 RVA: 0x003A4268 File Offset: 0x003A2468
		internal TimeSpan EffectiveDayEndTime
		{
			get
			{
				TimeSpan t = base.Owner.ShowFullTime ? TimeSpan.FromHours(24.0) : this.DayEndTimeResolved;
				int num = (int)Math.Ceiling((t - this.EffectiveDayStartTime).TotalMinutes / (double)base.Owner.MinutesPerRow);
				return this.EffectiveDayStartTime.Add(TimeSpan.FromMinutes((double)(num * base.Owner.MinutesPerRow)));
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.Script.Serialization;
using Telerik.Web.UI.Scheduling;

namespace Telerik.Web.UI.Scheduler.Views.Week
{
	// Token: 0x02001A4C RID: 6732
	internal abstract class ModelBase : SchedulerModel
	{
		// Token: 0x17004F3E RID: 20286
		// (get) Token: 0x06010537 RID: 66871 RVA: 0x003A504E File Offset: 0x003A324E
		// (set) Token: 0x06010538 RID: 66872 RVA: 0x003A5056 File Offset: 0x003A3256
		public override IScheduler Owner { get; protected set; }

		// Token: 0x17004F3F RID: 20287
		// (get) Token: 0x06010539 RID: 66873 RVA: 0x003A5060 File Offset: 0x003A3260
		public override DateTime SelectedDate
		{
			get
			{
				return this.Owner.SelectedDate.Date;
			}
		}

		// Token: 0x17004F40 RID: 20288
		// (get) Token: 0x0601053A RID: 66874 RVA: 0x003A5080 File Offset: 0x003A3280
		public override DateTime NextPeriodDate
		{
			get
			{
				return this.SelectedDate.AddDays(7.0);
			}
		}

		// Token: 0x17004F41 RID: 20289
		// (get) Token: 0x0601053B RID: 66875 RVA: 0x003A50A4 File Offset: 0x003A32A4
		public override DateTime PreviousPeriodDate
		{
			get
			{
				return this.SelectedDate.AddDays(-7.0);
			}
		}

		// Token: 0x17004F42 RID: 20290
		// (get) Token: 0x0601053C RID: 66876 RVA: 0x003A50C8 File Offset: 0x003A32C8
		public override string CssClass
		{
			get
			{
				return "rsWeekView";
			}
		}

		// Token: 0x17004F43 RID: 20291
		// (get) Token: 0x0601053D RID: 66877 RVA: 0x003A50CF File Offset: 0x003A32CF
		public override bool EnableExactTimeRendering
		{
			get
			{
				return this.Owner.WeekView.EnableExactTimeRenderingResolved;
			}
		}

		// Token: 0x17004F44 RID: 20292
		// (get) Token: 0x0601053E RID: 66878 RVA: 0x003A50E1 File Offset: 0x003A32E1
		// (set) Token: 0x0601053F RID: 66879 RVA: 0x003A50E9 File Offset: 0x003A32E9
		public override AppointmentCollection Appointments { get; protected set; }

		// Token: 0x17004F45 RID: 20293
		// (get) Token: 0x06010540 RID: 66880 RVA: 0x003A50F2 File Offset: 0x003A32F2
		// (set) Token: 0x06010541 RID: 66881 RVA: 0x003A50FA File Offset: 0x003A32FA
		public override DateTime VisibleRangeStart { get; protected set; }

		// Token: 0x17004F46 RID: 20294
		// (get) Token: 0x06010542 RID: 66882 RVA: 0x003A5103 File Offset: 0x003A3303
		// (set) Token: 0x06010543 RID: 66883 RVA: 0x003A510B File Offset: 0x003A330B
		public override DateTime VisibleRangeEnd { get; protected set; }

		// Token: 0x17004F47 RID: 20295
		// (get) Token: 0x06010544 RID: 66884 RVA: 0x003A5114 File Offset: 0x003A3314
		// (set) Token: 0x06010545 RID: 66885 RVA: 0x003A511C File Offset: 0x003A331C
		public int NumberOfDays { get; protected set; }

		// Token: 0x17004F48 RID: 20296
		// (get) Token: 0x06010546 RID: 66886 RVA: 0x003A5125 File Offset: 0x003A3325
		// (set) Token: 0x06010547 RID: 66887 RVA: 0x003A512D File Offset: 0x003A332D
		protected TimeSpan DayDuration { get; set; }

		// Token: 0x17004F49 RID: 20297
		// (get) Token: 0x06010548 RID: 66888 RVA: 0x003A5136 File Offset: 0x003A3336
		public override bool ReadOnly
		{
			get
			{
				return this.Owner.WeekView.ReadOnlyResolved;
			}
		}

		// Token: 0x17004F4A RID: 20298
		// (get) Token: 0x06010549 RID: 66889 RVA: 0x003A5148 File Offset: 0x003A3348
		public virtual TimeSpan EffectiveDayStartTime
		{
			get
			{
				return this.Owner.WeekView.EffectiveDayStartTime;
			}
		}

		// Token: 0x17004F4B RID: 20299
		// (get) Token: 0x0601054A RID: 66890 RVA: 0x003A515A File Offset: 0x003A335A
		public virtual TimeSpan EffectiveDayEndTime
		{
			get
			{
				return this.Owner.WeekView.EffectiveDayEndTime;
			}
		}

		// Token: 0x0601054B RID: 66891 RVA: 0x003A516C File Offset: 0x003A336C
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		protected ModelBase(IScheduler owner)
		{
			this.Owner = owner;
			this.Appointments = new AppointmentCollection();
			this.NumberOfDays = this.GetNumberOfDays();
			this.VisibleRangeStart = this.GetVisibleStart();
			this.VisibleRangeEnd = this.GetVisibleEnd();
			this.DayDuration = this.GetDayDuration();
		}

		// Token: 0x0601054C RID: 66892 RVA: 0x003A51C1 File Offset: 0x003A33C1
		public virtual int GetNumberOfDays()
		{
			return DateHelper.GetWeekLength(this.SelectedDate, this.Owner.FirstDayOfWeek, this.Owner.LastDayOfWeek);
		}

		// Token: 0x0601054D RID: 66893 RVA: 0x003A51E4 File Offset: 0x003A33E4
		public virtual DateTime GetVisibleStart()
		{
			DateTime date = DateHelper.GetStartOfWeek(this.SelectedDate, this.Owner.FirstDayOfWeek);
			if (!this.Owner.ShowAllDayRow)
			{
				date = date.Add(this.EffectiveDayStartTime);
			}
			return this.Owner.DisplayToUtc(date);
		}

		// Token: 0x0601054E RID: 66894 RVA: 0x003A5230 File Offset: 0x003A3430
		public virtual DateTime GetVisibleEnd()
		{
			DateTime date = DateHelper.GetEndOfWeek(this.SelectedDate, this.Owner.FirstDayOfWeek, this.NumberOfDays - 1);
			if (!this.Owner.ShowAllDayRow)
			{
				date = date.Add(this.EffectiveDayEndTime);
			}
			else
			{
				date = date.AddHours(24.0);
			}
			return this.Owner.DisplayToUtc(date);
		}

		// Token: 0x0601054F RID: 66895 RVA: 0x003A5296 File Offset: 0x003A3496
		public virtual TimeSpan GetDayDuration()
		{
			return this.EffectiveDayEndTime - this.EffectiveDayStartTime;
		}

		// Token: 0x06010550 RID: 66896 RVA: 0x003A52AC File Offset: 0x003A34AC
		public IList<DayInterval> GetVisibleDays()
		{
			DateTime date = this.Owner.UtcToDisplay(this.VisibleRangeStart);
			if (this.Owner.ShowAllDayRow)
			{
				date = date.Add(this.EffectiveDayStartTime);
			}
			DateTime date2 = date.Add(this.DayDuration);
			List<DayInterval> list = new List<DayInterval>();
			while (date2.AddMinutes((double)(-(double)this.Owner.MinutesPerRow)) <= this.Owner.UtcToDisplay(this.VisibleRangeEnd))
			{
				DayInterval item = new DayInterval(this.Owner.DisplayToUtc(date), this.Owner.DisplayToUtc(date2));
				list.Add(item);
				date = date.AddDays(1.0);
				date2 = date2.AddDays(1.0);
			}
			return list;
		}

		// Token: 0x06010551 RID: 66897 RVA: 0x003A5374 File Offset: 0x003A3574
		public override void DescribeModelData(string propertyName, JavaScriptSerializer serializer, IScriptDescriptor descriptor)
		{
			base.DescribeModelData(propertyName, serializer, descriptor);
			JavaScriptConverter javaScriptConverter = new WeekViewDataConverter();
			serializer.RegisterConverters(new JavaScriptConverter[]
			{
				javaScriptConverter
			});
			IDictionary<string, object> dictionary = javaScriptConverter.Serialize(this, serializer);
			if (dictionary.Count > 0)
			{
				descriptor.AddProperty(propertyName, serializer.Serialize(this));
			}
		}

		// Token: 0x06010552 RID: 66898 RVA: 0x003A53C4 File Offset: 0x003A35C4
		public override IList<RadMenuItem> GetTimeSlotContextMenuItems()
		{
			IList<RadMenuItem> timeSlotContextMenuItems = base.GetTimeSlotContextMenuItems();
			timeSlotContextMenuItems.Add(new RadMenuItem
			{
				Text = (this.Owner.ShowFullTime ? this.Owner.Localization.ShowBusinessHours : this.Owner.Localization.Show24Hours),
				Value = "CommandShow24Hours"
			});
			return timeSlotContextMenuItems;
		}

		// Token: 0x06010553 RID: 66899 RVA: 0x003A5444 File Offset: 0x003A3644
		public override Dictionary<string, ContextMenuAction> GetTimeSlotContextMenuCommands()
		{
			Dictionary<string, ContextMenuAction> timeSlotContextMenuCommands = base.GetTimeSlotContextMenuCommands();
			timeSlotContextMenuCommands.Add("CommandShow24Hours", delegate(ISchedulerModel model, SchedulerPostBackEvent postBackEvent)
			{
				model.Owner.ShowFullTime = !model.Owner.ShowFullTime;
			});
			return timeSlotContextMenuCommands;
		}

		// Token: 0x04004978 RID: 18808
		private const int FullWeekLength = 7;
	}
}

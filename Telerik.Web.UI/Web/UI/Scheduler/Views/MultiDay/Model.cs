using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Web.UI;
using Telerik.Web.UI.Scheduler.Views.Week;

namespace Telerik.Web.UI.Scheduler.Views.MultiDay
{
	// Token: 0x02001A6B RID: 6763
	internal class Model : Model
	{
		// Token: 0x17004F99 RID: 20377
		// (get) Token: 0x06010648 RID: 67144 RVA: 0x003A95C8 File Offset: 0x003A77C8
		public override DateTime NextPeriodDate
		{
			get
			{
				return this.SelectedDate.AddDays((double)base.NumberOfDays);
			}
		}

		// Token: 0x17004F9A RID: 20378
		// (get) Token: 0x06010649 RID: 67145 RVA: 0x003A95EC File Offset: 0x003A77EC
		public override DateTime PreviousPeriodDate
		{
			get
			{
				return this.SelectedDate.AddDays((double)(-(double)base.NumberOfDays));
			}
		}

		// Token: 0x17004F9B RID: 20379
		// (get) Token: 0x0601064A RID: 67146 RVA: 0x003A960F File Offset: 0x003A780F
		public override bool ReadOnly
		{
			get
			{
				return this.Owner.MultiDayView.ReadOnlyResolved;
			}
		}

		// Token: 0x17004F9C RID: 20380
		// (get) Token: 0x0601064B RID: 67147 RVA: 0x003A9621 File Offset: 0x003A7821
		public override string CssClass
		{
			get
			{
				return base.CssClass + " rsMultiDayView";
			}
		}

		// Token: 0x17004F9D RID: 20381
		// (get) Token: 0x0601064C RID: 67148 RVA: 0x003A9633 File Offset: 0x003A7833
		public override bool EnableExactTimeRendering
		{
			get
			{
				return this.Owner.MultiDayView.EnableExactTimeRenderingResolved;
			}
		}

		// Token: 0x17004F9E RID: 20382
		// (get) Token: 0x0601064D RID: 67149 RVA: 0x003A9645 File Offset: 0x003A7845
		public override TimeSpan EffectiveDayStartTime
		{
			get
			{
				return this.Owner.MultiDayView.EffectiveDayStartTime;
			}
		}

		// Token: 0x17004F9F RID: 20383
		// (get) Token: 0x0601064E RID: 67150 RVA: 0x003A9657 File Offset: 0x003A7857
		public override TimeSpan EffectiveDayEndTime
		{
			get
			{
				return this.Owner.MultiDayView.EffectiveDayEndTime;
			}
		}

		// Token: 0x0601064F RID: 67151 RVA: 0x003A9669 File Offset: 0x003A7869
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Model(IScheduler owner) : base(owner, owner.MultiDayView.WorkDayStartTimeResolved, owner.MultiDayView.WorkDayEndTimeResolved)
		{
		}

		// Token: 0x06010650 RID: 67152 RVA: 0x003A9688 File Offset: 0x003A7888
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Model(IScheduler owner, IWeekTimeSlotFactory timeSlotFactory) : base(owner, timeSlotFactory, owner.MultiDayView.WorkDayStartTimeResolved, owner.MultiDayView.WorkDayEndTimeResolved)
		{
		}

		// Token: 0x06010651 RID: 67153 RVA: 0x003A96A8 File Offset: 0x003A78A8
		public override ISchedulerRenderer GetRenderer()
		{
			return new Renderer(new View(this));
		}

		// Token: 0x06010652 RID: 67154 RVA: 0x003A96B8 File Offset: 0x003A78B8
		public override IEnumerable<ScriptReference> GetScriptReferences()
		{
			return new List<ScriptReference>(base.GetScriptReferences())
			{
				new ScriptReference("Telerik.Web.UI.Scheduler.Views.MultiDay.Model.js", Assembly.GetExecutingAssembly().FullName)
			};
		}

		// Token: 0x06010653 RID: 67155 RVA: 0x003A96EC File Offset: 0x003A78EC
		public override int GetNumberOfDays()
		{
			return this.Owner.MultiDayView.NumberOfDays;
		}

		// Token: 0x06010654 RID: 67156 RVA: 0x003A9700 File Offset: 0x003A7900
		public override DateTime GetVisibleStart()
		{
			DateTime date = this.SelectedDate;
			if (!this.Owner.ShowAllDayRow)
			{
				date = this.SelectedDate.Add(this.EffectiveDayStartTime);
			}
			return this.Owner.DisplayToUtc(date);
		}

		// Token: 0x06010655 RID: 67157 RVA: 0x003A9744 File Offset: 0x003A7944
		public override DateTime GetVisibleEnd()
		{
			DateTime date = this.SelectedDate.AddDays((double)(base.NumberOfDays - 1));
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
	}
}

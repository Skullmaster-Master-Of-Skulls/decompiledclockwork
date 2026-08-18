using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Web.UI;
using Telerik.Web.UI.Scheduler.Views.Week;

namespace Telerik.Web.UI.Scheduler.Views.Day
{
	// Token: 0x02001A76 RID: 6774
	internal class Model : Model
	{
		// Token: 0x17004FB5 RID: 20405
		// (get) Token: 0x0601069D RID: 67229 RVA: 0x003AAE80 File Offset: 0x003A9080
		public override DateTime NextPeriodDate
		{
			get
			{
				return this.SelectedDate.AddDays((double)base.NumberOfDays);
			}
		}

		// Token: 0x17004FB6 RID: 20406
		// (get) Token: 0x0601069E RID: 67230 RVA: 0x003AAEA4 File Offset: 0x003A90A4
		public override DateTime PreviousPeriodDate
		{
			get
			{
				return this.SelectedDate.AddDays((double)(-(double)base.NumberOfDays));
			}
		}

		// Token: 0x17004FB7 RID: 20407
		// (get) Token: 0x0601069F RID: 67231 RVA: 0x003AAEC7 File Offset: 0x003A90C7
		public override TimeSpan EffectiveDayStartTime
		{
			get
			{
				return this.Owner.DayView.EffectiveDayStartTime;
			}
		}

		// Token: 0x17004FB8 RID: 20408
		// (get) Token: 0x060106A0 RID: 67232 RVA: 0x003AAED9 File Offset: 0x003A90D9
		public override TimeSpan EffectiveDayEndTime
		{
			get
			{
				return this.Owner.DayView.EffectiveDayEndTime;
			}
		}

		// Token: 0x17004FB9 RID: 20409
		// (get) Token: 0x060106A1 RID: 67233 RVA: 0x003AAEEB File Offset: 0x003A90EB
		public override bool ReadOnly
		{
			get
			{
				return this.Owner.DayView.ReadOnlyResolved;
			}
		}

		// Token: 0x17004FBA RID: 20410
		// (get) Token: 0x060106A2 RID: 67234 RVA: 0x003AAEFD File Offset: 0x003A90FD
		public override string CssClass
		{
			get
			{
				return "rsDayView";
			}
		}

		// Token: 0x17004FBB RID: 20411
		// (get) Token: 0x060106A3 RID: 67235 RVA: 0x003AAF04 File Offset: 0x003A9104
		public override bool EnableExactTimeRendering
		{
			get
			{
				return this.Owner.DayView.EnableExactTimeRenderingResolved;
			}
		}

		// Token: 0x060106A4 RID: 67236 RVA: 0x003AAF16 File Offset: 0x003A9116
		public override ISchedulerRenderer GetRenderer()
		{
			return new Renderer(new View(this));
		}

		// Token: 0x060106A5 RID: 67237 RVA: 0x003AAF23 File Offset: 0x003A9123
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Model(IScheduler owner) : base(owner, owner.DayView.WorkDayStartTimeResolved, owner.DayView.WorkDayEndTimeResolved)
		{
		}

		// Token: 0x060106A6 RID: 67238 RVA: 0x003AAF42 File Offset: 0x003A9142
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Model(IScheduler owner, IWeekTimeSlotFactory timeSlotFactory) : base(owner, timeSlotFactory, owner.DayView.WorkDayStartTimeResolved, owner.DayView.WorkDayEndTimeResolved)
		{
		}

		// Token: 0x060106A7 RID: 67239 RVA: 0x003AAF64 File Offset: 0x003A9164
		public override IEnumerable<ScriptReference> GetScriptReferences()
		{
			return new List<ScriptReference>(base.GetScriptReferences())
			{
				new ScriptReference("Telerik.Web.UI.Scheduler.Views.MultiDay.Model.js", Assembly.GetExecutingAssembly().FullName),
				new ScriptReference("Telerik.Web.UI.Scheduler.Views.Day.Model.js", Assembly.GetExecutingAssembly().FullName)
			};
		}

		// Token: 0x060106A8 RID: 67240 RVA: 0x003AAFB2 File Offset: 0x003A91B2
		public override int GetNumberOfDays()
		{
			return 1;
		}

		// Token: 0x060106A9 RID: 67241 RVA: 0x003AAFB8 File Offset: 0x003A91B8
		public override DateTime GetVisibleStart()
		{
			DateTime date = this.SelectedDate;
			if (!this.Owner.ShowAllDayRow)
			{
				date = this.SelectedDate.Add(this.EffectiveDayStartTime);
			}
			return this.Owner.DisplayToUtc(date);
		}

		// Token: 0x060106AA RID: 67242 RVA: 0x003AAFFC File Offset: 0x003A91FC
		public override DateTime GetVisibleEnd()
		{
			DateTime date = this.SelectedDate;
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

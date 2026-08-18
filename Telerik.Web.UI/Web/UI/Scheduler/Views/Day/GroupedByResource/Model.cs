using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Web.UI;
using Telerik.Web.UI.Scheduler.Views.Week;
using Telerik.Web.UI.Scheduler.Views.Week.GroupedByResource;

namespace Telerik.Web.UI.Scheduler.Views.Day.GroupedByResource
{
	// Token: 0x02001A4E RID: 6734
	internal class Model : Telerik.Web.UI.Scheduler.Views.Week.GroupedByResource.Model
	{
		// Token: 0x17004F4F RID: 20303
		// (get) Token: 0x06010565 RID: 66917 RVA: 0x003A591C File Offset: 0x003A3B1C
		public override DateTime NextPeriodDate
		{
			get
			{
				return this.SelectedDate.AddDays((double)base.NumberOfDays);
			}
		}

		// Token: 0x17004F50 RID: 20304
		// (get) Token: 0x06010566 RID: 66918 RVA: 0x003A5940 File Offset: 0x003A3B40
		public override DateTime PreviousPeriodDate
		{
			get
			{
				return this.SelectedDate.AddDays((double)(-(double)base.NumberOfDays));
			}
		}

		// Token: 0x17004F51 RID: 20305
		// (get) Token: 0x06010567 RID: 66919 RVA: 0x003A5963 File Offset: 0x003A3B63
		public override TimeSpan EffectiveDayStartTime
		{
			get
			{
				return this.Owner.DayView.EffectiveDayStartTime;
			}
		}

		// Token: 0x17004F52 RID: 20306
		// (get) Token: 0x06010568 RID: 66920 RVA: 0x003A5975 File Offset: 0x003A3B75
		public override bool ReadOnly
		{
			get
			{
				return this.Owner.DayView.ReadOnlyResolved;
			}
		}

		// Token: 0x17004F53 RID: 20307
		// (get) Token: 0x06010569 RID: 66921 RVA: 0x003A5987 File Offset: 0x003A3B87
		public override string CssClass
		{
			get
			{
				return "rsDayView";
			}
		}

		// Token: 0x17004F54 RID: 20308
		// (get) Token: 0x0601056A RID: 66922 RVA: 0x003A598E File Offset: 0x003A3B8E
		public override bool EnableExactTimeRendering
		{
			get
			{
				return this.Owner.DayView.EnableExactTimeRenderingResolved;
			}
		}

		// Token: 0x0601056B RID: 66923 RVA: 0x003A59A0 File Offset: 0x003A3BA0
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Model(IScheduler owner, string groupingResourceName) : base(owner, groupingResourceName)
		{
			base.NumberOfDays = 1;
			this.VisibleRangeStart = this.Owner.DisplayToUtc(this.SelectedDate);
			this.VisibleRangeEnd = this.Owner.DisplayToUtc(this.SelectedDate);
			if (!this.Owner.ShowAllDayRow)
			{
				this.VisibleRangeStart = this.VisibleRangeStart.Add(this.Owner.DayView.EffectiveDayStartTime);
				this.VisibleRangeEnd = this.VisibleRangeEnd.Add(this.Owner.DayView.EffectiveDayEndTime);
				return;
			}
			this.VisibleRangeEnd = this.VisibleRangeEnd.AddHours(24.0);
		}

		// Token: 0x0601056C RID: 66924 RVA: 0x003A5A60 File Offset: 0x003A3C60
		public override ISchedulerRenderer GetRenderer()
		{
			Telerik.Web.UI.Scheduler.Views.Week.View view;
			if (this.Owner.DayView.GroupingDirectionResolved == GroupingDirection.Vertical)
			{
				view = new VerticalView(this);
			}
			else
			{
				view = new HorizontalView(this);
			}
			return new Telerik.Web.UI.Scheduler.Views.Day.GroupedByResource.Renderer(view, this.Owner.DayView.GroupingDirectionResolved);
		}

		// Token: 0x0601056D RID: 66925 RVA: 0x003A5AA8 File Offset: 0x003A3CA8
		public override IEnumerable<ScriptReference> GetScriptReferences()
		{
			return new List<ScriptReference>(base.GetScriptReferences())
			{
				new ScriptReference("Telerik.Web.UI.Scheduler.Views.MultiDay.Model.js", Assembly.GetExecutingAssembly().FullName),
				new ScriptReference("Telerik.Web.UI.Scheduler.Views.Day.Model.js", Assembly.GetExecutingAssembly().FullName),
				new ScriptReference("Telerik.Web.UI.Scheduler.Views.Day.GroupedByResource.Model.js", Assembly.GetExecutingAssembly().FullName)
			};
		}

		// Token: 0x0601056E RID: 66926 RVA: 0x003A5B10 File Offset: 0x003A3D10
		protected override Telerik.Web.UI.Scheduler.Views.Week.Model CreateModel(IWeekTimeSlotFactory slotFactory)
		{
			return new Telerik.Web.UI.Scheduler.Views.Day.Model(this.Owner, slotFactory);
		}
	}
}

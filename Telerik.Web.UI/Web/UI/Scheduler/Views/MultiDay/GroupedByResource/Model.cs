using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Web.UI;
using Telerik.Web.UI.Scheduler.Views.Week;
using Telerik.Web.UI.Scheduler.Views.Week.GroupedByResource;

namespace Telerik.Web.UI.Scheduler.Views.MultiDay.GroupedByResource
{
	// Token: 0x02001A68 RID: 6760
	internal class Model : Telerik.Web.UI.Scheduler.Views.Week.GroupedByResource.Model
	{
		// Token: 0x17004F8C RID: 20364
		// (get) Token: 0x0601061E RID: 67102 RVA: 0x003A892C File Offset: 0x003A6B2C
		public override DateTime NextPeriodDate
		{
			get
			{
				return this.SelectedDate.AddDays((double)base.NumberOfDays);
			}
		}

		// Token: 0x17004F8D RID: 20365
		// (get) Token: 0x0601061F RID: 67103 RVA: 0x003A8950 File Offset: 0x003A6B50
		public override DateTime PreviousPeriodDate
		{
			get
			{
				return this.SelectedDate.AddDays((double)(-(double)base.NumberOfDays));
			}
		}

		// Token: 0x17004F8E RID: 20366
		// (get) Token: 0x06010620 RID: 67104 RVA: 0x003A8973 File Offset: 0x003A6B73
		public override bool ReadOnly
		{
			get
			{
				return this.Owner.MultiDayView.ReadOnlyResolved;
			}
		}

		// Token: 0x17004F8F RID: 20367
		// (get) Token: 0x06010621 RID: 67105 RVA: 0x003A8985 File Offset: 0x003A6B85
		public override string CssClass
		{
			get
			{
				return base.CssClass + " rsMultiDayView";
			}
		}

		// Token: 0x17004F90 RID: 20368
		// (get) Token: 0x06010622 RID: 67106 RVA: 0x003A8997 File Offset: 0x003A6B97
		public override bool EnableExactTimeRendering
		{
			get
			{
				return this.Owner.MultiDayView.EnableExactTimeRenderingResolved;
			}
		}

		// Token: 0x06010623 RID: 67107 RVA: 0x003A89AC File Offset: 0x003A6BAC
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Model(IScheduler owner, string groupingResourceName) : base(owner, groupingResourceName)
		{
			base.NumberOfDays = this.Owner.MultiDayView.NumberOfDays;
			this.VisibleRangeStart = this.Owner.DisplayToUtc(this.SelectedDate);
			this.VisibleRangeEnd = this.Owner.DisplayToUtc(this.SelectedDate).AddDays((double)(base.NumberOfDays - 1));
			if (!this.Owner.ShowAllDayRow)
			{
				this.VisibleRangeStart = this.VisibleRangeStart.Add(this.Owner.DayView.EffectiveDayStartTime);
				this.VisibleRangeEnd = this.VisibleRangeEnd.Add(this.Owner.DayView.EffectiveDayEndTime);
				return;
			}
			this.VisibleRangeEnd = this.VisibleRangeEnd.AddHours(24.0);
		}

		// Token: 0x06010624 RID: 67108 RVA: 0x003A8A8C File Offset: 0x003A6C8C
		public override IEnumerable<ScriptReference> GetScriptReferences()
		{
			return new List<ScriptReference>(base.GetScriptReferences())
			{
				new ScriptReference("Telerik.Web.UI.Scheduler.Views.MultiDay.Model.js", Assembly.GetExecutingAssembly().FullName),
				new ScriptReference("Telerik.Web.UI.Scheduler.Views.MultiDay.GroupedByResource.Model.js", Assembly.GetExecutingAssembly().FullName)
			};
		}

		// Token: 0x06010625 RID: 67109 RVA: 0x003A8ADC File Offset: 0x003A6CDC
		public override ISchedulerRenderer GetRenderer()
		{
			Telerik.Web.UI.Scheduler.Views.Week.View view;
			if (this.Owner.MultiDayView.GroupingDirectionResolved == GroupingDirection.Vertical)
			{
				view = new VerticalView(this);
			}
			else
			{
				view = new HorizontalView(this);
			}
			return new Telerik.Web.UI.Scheduler.Views.Week.GroupedByResource.Renderer(view, this.Owner.MultiDayView.GroupingDirectionResolved);
		}

		// Token: 0x06010626 RID: 67110 RVA: 0x003A8B22 File Offset: 0x003A6D22
		protected override Telerik.Web.UI.Scheduler.Views.Week.Model CreateModel(IWeekTimeSlotFactory slotFactory)
		{
			return new Telerik.Web.UI.Scheduler.Views.MultiDay.Model(this.Owner, slotFactory);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Web.UI;
using Telerik.Web.UI.Scheduler.Views.Week;
using Telerik.Web.UI.Scheduler.Views.Week.GroupedByDate;

namespace Telerik.Web.UI.Scheduler.Views.MultiDay.GroupedByDate
{
	// Token: 0x02001A63 RID: 6755
	internal class Model : Telerik.Web.UI.Scheduler.Views.Week.GroupedByDate.Model
	{
		// Token: 0x17004F83 RID: 20355
		// (get) Token: 0x06010603 RID: 67075 RVA: 0x003A822C File Offset: 0x003A642C
		public override DateTime NextPeriodDate
		{
			get
			{
				return this.SelectedDate.AddDays((double)base.NumberOfDays);
			}
		}

		// Token: 0x17004F84 RID: 20356
		// (get) Token: 0x06010604 RID: 67076 RVA: 0x003A8250 File Offset: 0x003A6450
		public override DateTime PreviousPeriodDate
		{
			get
			{
				return this.SelectedDate.AddDays((double)(-(double)base.NumberOfDays));
			}
		}

		// Token: 0x17004F85 RID: 20357
		// (get) Token: 0x06010605 RID: 67077 RVA: 0x003A8273 File Offset: 0x003A6473
		public override bool ReadOnly
		{
			get
			{
				return this.Owner.MultiDayView.ReadOnlyResolved;
			}
		}

		// Token: 0x17004F86 RID: 20358
		// (get) Token: 0x06010606 RID: 67078 RVA: 0x003A8285 File Offset: 0x003A6485
		public override string CssClass
		{
			get
			{
				return base.CssClass + " rsMultiDayView";
			}
		}

		// Token: 0x17004F87 RID: 20359
		// (get) Token: 0x06010607 RID: 67079 RVA: 0x003A8297 File Offset: 0x003A6497
		public override bool EnableExactTimeRendering
		{
			get
			{
				return this.Owner.MultiDayView.EnableExactTimeRenderingResolved;
			}
		}

		// Token: 0x06010608 RID: 67080 RVA: 0x003A82AC File Offset: 0x003A64AC
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Model(IScheduler owner, string groupingResourceName) : base(owner, groupingResourceName)
		{
			base.NumberOfDays = this.Owner.MultiDayView.NumberOfDays;
			this.VisibleRangeStart = this.Owner.DisplayToUtc(this.SelectedDate);
			this.VisibleRangeEnd = this.Owner.DisplayToUtc(this.SelectedDate).AddDays((double)(base.NumberOfDays - 1));
			if (!this.Owner.ShowAllDayRow)
			{
				this.VisibleRangeStart = this.VisibleRangeStart.Add(this.Owner.MultiDayView.EffectiveDayStartTime);
				this.VisibleRangeEnd = this.VisibleRangeEnd.Add(this.Owner.MultiDayView.EffectiveDayEndTime);
				return;
			}
			this.VisibleRangeEnd = this.VisibleRangeEnd.AddHours(24.0);
		}

		// Token: 0x06010609 RID: 67081 RVA: 0x003A838C File Offset: 0x003A658C
		public override IEnumerable<ScriptReference> GetScriptReferences()
		{
			List<ScriptReference> list = new List<ScriptReference>(base.GetScriptReferences());
			string fullName = Assembly.GetExecutingAssembly().FullName;
			list.Add(new ScriptReference("Telerik.Web.UI.Scheduler.Views.MultiDay.Model.js", fullName));
			list.Add(new ScriptReference("Telerik.Web.UI.Scheduler.Views.MultiDay.GroupedByDate.Model.js", fullName));
			return list;
		}

		// Token: 0x0601060A RID: 67082 RVA: 0x003A83D4 File Offset: 0x003A65D4
		public override ISchedulerRenderer GetRenderer()
		{
			View view;
			if (this.Owner.MultiDayView.GroupingDirectionResolved == GroupingDirection.Vertical)
			{
				view = new VerticalView(this);
			}
			else
			{
				view = new HorizontalView(this);
			}
			return new Telerik.Web.UI.Scheduler.Views.Week.GroupedByDate.Renderer(view, this.Owner.MultiDayView);
		}

		// Token: 0x0601060B RID: 67083 RVA: 0x003A8418 File Offset: 0x003A6618
		protected override Telerik.Web.UI.Scheduler.Views.Week.Model CreateModel(IWeekTimeSlotFactory slotFactory)
		{
			return new Telerik.Web.UI.Scheduler.Views.MultiDay.Model(this.Owner, slotFactory)
			{
				AppointmentFilter = new Telerik.Web.UI.Scheduler.Views.Week.GroupedByDate.AppointmentFilter()
			};
		}
	}
}

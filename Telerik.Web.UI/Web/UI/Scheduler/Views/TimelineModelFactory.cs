using System;
using Telerik.Web.UI.Scheduler.Views.Timeline;
using Telerik.Web.UI.Scheduler.Views.Timeline.GroupedByDate;
using Telerik.Web.UI.Scheduler.Views.Timeline.GroupedByResource;

namespace Telerik.Web.UI.Scheduler.Views
{
	// Token: 0x02001A83 RID: 6787
	internal class TimelineModelFactory : ModelFactory
	{
		// Token: 0x060106F8 RID: 67320 RVA: 0x003AC458 File Offset: 0x003AA658
		public TimelineModelFactory(IScheduler owner) : base(owner)
		{
		}

		// Token: 0x060106F9 RID: 67321 RVA: 0x003AC464 File Offset: 0x003AA664
		public override ISchedulerModel CreateModel()
		{
			if (!base.EnableGrouping)
			{
				return new Telerik.Web.UI.Scheduler.Views.Timeline.Model(base.Owner);
			}
			if (!base.GroupByDate)
			{
				return new Telerik.Web.UI.Scheduler.Views.Timeline.GroupedByResource.Model(base.Owner, base.GroupingResourceName);
			}
			return new Telerik.Web.UI.Scheduler.Views.Timeline.GroupedByDate.Model(base.Owner, base.GroupingResourceName);
		}

		// Token: 0x17004FD0 RID: 20432
		// (get) Token: 0x060106FA RID: 67322 RVA: 0x003AC4B0 File Offset: 0x003AA6B0
		protected override string GroupBy
		{
			get
			{
				return base.Owner.TimelineView.GroupByResolved;
			}
		}
	}
}

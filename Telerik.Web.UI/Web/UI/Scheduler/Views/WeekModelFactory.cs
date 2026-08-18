using System;
using Telerik.Web.UI.Scheduler.Views.Week;
using Telerik.Web.UI.Scheduler.Views.Week.GroupedByDate;
using Telerik.Web.UI.Scheduler.Views.Week.GroupedByResource;

namespace Telerik.Web.UI.Scheduler.Views
{
	// Token: 0x02001A9A RID: 6810
	internal class WeekModelFactory : ModelFactory
	{
		// Token: 0x17004FFB RID: 20475
		// (get) Token: 0x06010793 RID: 67475 RVA: 0x003AEBE4 File Offset: 0x003ACDE4
		protected override string GroupBy
		{
			get
			{
				return base.Owner.WeekView.GroupByResolved;
			}
		}

		// Token: 0x06010794 RID: 67476 RVA: 0x003AEBF6 File Offset: 0x003ACDF6
		public WeekModelFactory(IScheduler owner) : base(owner)
		{
		}

		// Token: 0x06010795 RID: 67477 RVA: 0x003AEC00 File Offset: 0x003ACE00
		public override ISchedulerModel CreateModel()
		{
			if (!base.EnableGrouping)
			{
				return new Telerik.Web.UI.Scheduler.Views.Week.Model(base.Owner);
			}
			if (!base.GroupByDate)
			{
				return new Telerik.Web.UI.Scheduler.Views.Week.GroupedByResource.Model(base.Owner, base.GroupingResourceName);
			}
			return new Telerik.Web.UI.Scheduler.Views.Week.GroupedByDate.Model(base.Owner, base.GroupingResourceName);
		}
	}
}

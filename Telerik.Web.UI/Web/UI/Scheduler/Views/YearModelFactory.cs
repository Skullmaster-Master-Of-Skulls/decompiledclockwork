using System;
using Telerik.Web.UI.Scheduler.Views.Year;
using Telerik.Web.UI.Scheduler.Views.Year.GroupedByResource;

namespace Telerik.Web.UI.Scheduler.Views
{
	// Token: 0x02000849 RID: 2121
	internal class YearModelFactory : ModelFactory
	{
		// Token: 0x06004E52 RID: 20050 RVA: 0x000F58F0 File Offset: 0x000F3AF0
		public YearModelFactory(IScheduler owner) : base(owner)
		{
		}

		// Token: 0x06004E53 RID: 20051 RVA: 0x000F58F9 File Offset: 0x000F3AF9
		public override ISchedulerModel CreateModel()
		{
			if (!base.EnableGrouping)
			{
				return new Telerik.Web.UI.Scheduler.Views.Year.Model(base.Owner);
			}
			if (base.GroupByDate)
			{
				throw new InvalidOperationException("Date grouped YearView is not supported");
			}
			return new Telerik.Web.UI.Scheduler.Views.Year.GroupedByResource.Model(base.Owner, base.GroupingResourceName);
		}

		// Token: 0x17001999 RID: 6553
		// (get) Token: 0x06004E54 RID: 20052 RVA: 0x000F5933 File Offset: 0x000F3B33
		protected override string GroupBy
		{
			get
			{
				return base.Owner.YearView.GroupByResolved;
			}
		}
	}
}

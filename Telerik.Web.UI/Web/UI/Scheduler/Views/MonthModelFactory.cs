using System;
using Telerik.Web.UI.Scheduler.Views.Month;
using Telerik.Web.UI.Scheduler.Views.Month.GroupedByDate;
using Telerik.Web.UI.Scheduler.Views.Month.GroupedByResource;

namespace Telerik.Web.UI.Scheduler.Views
{
	// Token: 0x02001A52 RID: 6738
	internal class MonthModelFactory : ModelFactory
	{
		// Token: 0x06010578 RID: 66936 RVA: 0x003A5BD2 File Offset: 0x003A3DD2
		public MonthModelFactory(IScheduler owner) : base(owner)
		{
		}

		// Token: 0x06010579 RID: 66937 RVA: 0x003A5BDC File Offset: 0x003A3DDC
		public override ISchedulerModel CreateModel()
		{
			if (!base.EnableGrouping)
			{
				return new Telerik.Web.UI.Scheduler.Views.Month.Model(base.Owner);
			}
			if (base.GroupByDate)
			{
				return new Telerik.Web.UI.Scheduler.Views.Month.GroupedByDate.Model(base.Owner, base.GroupingResourceName);
			}
			return new Telerik.Web.UI.Scheduler.Views.Month.GroupedByResource.Model(base.Owner, base.GroupingResourceName);
		}

		// Token: 0x17004F59 RID: 20313
		// (get) Token: 0x0601057A RID: 66938 RVA: 0x003A5C28 File Offset: 0x003A3E28
		protected override string GroupBy
		{
			get
			{
				return base.Owner.MonthView.GroupByResolved;
			}
		}
	}
}

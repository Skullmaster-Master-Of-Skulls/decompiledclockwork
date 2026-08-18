using System;
using Telerik.Web.UI.Scheduler.Views.MultiDay;
using Telerik.Web.UI.Scheduler.Views.MultiDay.GroupedByDate;
using Telerik.Web.UI.Scheduler.Views.MultiDay.GroupedByResource;

namespace Telerik.Web.UI.Scheduler.Views
{
	// Token: 0x02001A4B RID: 6731
	internal class MultiDayModelFactory : ModelFactory
	{
		// Token: 0x06010534 RID: 66868 RVA: 0x003A4FE5 File Offset: 0x003A31E5
		public MultiDayModelFactory(IScheduler owner) : base(owner)
		{
		}

		// Token: 0x06010535 RID: 66869 RVA: 0x003A4FF0 File Offset: 0x003A31F0
		public override ISchedulerModel CreateModel()
		{
			if (!base.EnableGrouping)
			{
				return new Telerik.Web.UI.Scheduler.Views.MultiDay.Model(base.Owner);
			}
			if (base.GroupByDate)
			{
				return new Telerik.Web.UI.Scheduler.Views.MultiDay.GroupedByDate.Model(base.Owner, base.GroupingResourceName);
			}
			return new Telerik.Web.UI.Scheduler.Views.MultiDay.GroupedByResource.Model(base.Owner, base.GroupingResourceName);
		}

		// Token: 0x17004F3D RID: 20285
		// (get) Token: 0x06010536 RID: 66870 RVA: 0x003A503C File Offset: 0x003A323C
		protected override string GroupBy
		{
			get
			{
				return base.Owner.MultiDayView.GroupByResolved;
			}
		}
	}
}

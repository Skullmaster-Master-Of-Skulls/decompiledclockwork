using System;
using Telerik.Web.UI.Scheduler.Views.Day;
using Telerik.Web.UI.Scheduler.Views.Day.GroupedByDate;
using Telerik.Web.UI.Scheduler.Views.Day.GroupedByResource;

namespace Telerik.Web.UI.Scheduler.Views
{
	// Token: 0x02001A51 RID: 6737
	internal class DayModelFactory : ModelFactory
	{
		// Token: 0x06010575 RID: 66933 RVA: 0x003A5B6A File Offset: 0x003A3D6A
		public DayModelFactory(IScheduler owner) : base(owner)
		{
		}

		// Token: 0x06010576 RID: 66934 RVA: 0x003A5B74 File Offset: 0x003A3D74
		public override ISchedulerModel CreateModel()
		{
			if (!base.EnableGrouping)
			{
				return new Telerik.Web.UI.Scheduler.Views.Day.Model(base.Owner);
			}
			if (base.GroupByDate)
			{
				return new Telerik.Web.UI.Scheduler.Views.Day.GroupedByDate.Model(base.Owner, base.GroupingResourceName);
			}
			return new Telerik.Web.UI.Scheduler.Views.Day.GroupedByResource.Model(base.Owner, base.GroupingResourceName);
		}

		// Token: 0x17004F58 RID: 20312
		// (get) Token: 0x06010577 RID: 66935 RVA: 0x003A5BC0 File Offset: 0x003A3DC0
		protected override string GroupBy
		{
			get
			{
				return base.Owner.DayView.GroupByResolved;
			}
		}
	}
}

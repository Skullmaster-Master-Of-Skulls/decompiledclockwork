using System;
using Telerik.Web.UI.Scheduler.Views.Agenda;
using Telerik.Web.UI.Scheduler.Views.Agenda.GroupedByDate;
using Telerik.Web.UI.Scheduler.Views.Agenda.GroupedByResource;

namespace Telerik.Web.UI.Scheduler.Views
{
	// Token: 0x02000846 RID: 2118
	internal class AgendaModelFactory : ModelFactory
	{
		// Token: 0x17001995 RID: 6549
		// (get) Token: 0x06004E3D RID: 20029 RVA: 0x000F51F2 File Offset: 0x000F33F2
		protected override string GroupBy
		{
			get
			{
				return base.Owner.AgendaView.GroupByResolved;
			}
		}

		// Token: 0x06004E3E RID: 20030 RVA: 0x000F5204 File Offset: 0x000F3404
		public AgendaModelFactory(IScheduler owner) : base(owner)
		{
		}

		// Token: 0x06004E3F RID: 20031 RVA: 0x000F5210 File Offset: 0x000F3410
		public override ISchedulerModel CreateModel()
		{
			if (!base.EnableGrouping)
			{
				return new Telerik.Web.UI.Scheduler.Views.Agenda.Model(base.Owner);
			}
			if (base.GroupByDate)
			{
				return new Telerik.Web.UI.Scheduler.Views.Agenda.GroupedByDate.Model(base.Owner, base.GroupingResourceName);
			}
			return new Telerik.Web.UI.Scheduler.Views.Agenda.GroupedByResource.Model(base.Owner, base.GroupingResourceName);
		}
	}
}

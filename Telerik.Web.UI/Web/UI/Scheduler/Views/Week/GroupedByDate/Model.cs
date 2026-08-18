using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;
using Telerik.Web.UI.Scheduler.Views.Week.GroupedByResource;

namespace Telerik.Web.UI.Scheduler.Views.Week.GroupedByDate
{
	// Token: 0x02001A62 RID: 6754
	internal class Model : Model
	{
		// Token: 0x060105FE RID: 67070 RVA: 0x003A817F File Offset: 0x003A637F
		public Model(IScheduler owner) : base(owner, string.Empty)
		{
		}

		// Token: 0x060105FF RID: 67071 RVA: 0x003A818D File Offset: 0x003A638D
		public Model(IScheduler owner, string groupingResourceName) : base(owner, groupingResourceName)
		{
		}

		// Token: 0x06010600 RID: 67072 RVA: 0x003A8198 File Offset: 0x003A6398
		public override ISchedulerRenderer GetRenderer()
		{
			View view;
			if (this.Owner.WeekView.GroupingDirectionResolved == GroupingDirection.Vertical)
			{
				view = new VerticalView(this);
			}
			else
			{
				view = new HorizontalView(this);
			}
			return new Renderer(view);
		}

		// Token: 0x06010601 RID: 67073 RVA: 0x003A81D0 File Offset: 0x003A63D0
		public override IEnumerable<ScriptReference> GetScriptReferences()
		{
			List<ScriptReference> list = new List<ScriptReference>(base.GetScriptReferences());
			string fullName = Assembly.GetExecutingAssembly().FullName;
			list.Add(new ScriptReference("Telerik.Web.UI.Scheduler.Views.Week.GroupedByDate.Model.js", fullName));
			return list;
		}

		// Token: 0x06010602 RID: 67074 RVA: 0x003A8208 File Offset: 0x003A6408
		protected override Model CreateModel(IWeekTimeSlotFactory slotFactory)
		{
			Model model = base.CreateModel(slotFactory);
			model.AppointmentFilter = new AppointmentFilter();
			return model;
		}
	}
}

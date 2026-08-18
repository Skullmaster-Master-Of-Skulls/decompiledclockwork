using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;
using Telerik.Web.UI.Scheduler.Views.Month.GroupedByResource;

namespace Telerik.Web.UI.Scheduler.Views.Month.GroupedByDate
{
	// Token: 0x02001A5C RID: 6748
	internal class Model : Model
	{
		// Token: 0x060105E2 RID: 67042 RVA: 0x003A7AB6 File Offset: 0x003A5CB6
		public Model(IScheduler owner, string groupingResourceName) : base(owner, groupingResourceName)
		{
		}

		// Token: 0x060105E3 RID: 67043 RVA: 0x003A7AC0 File Offset: 0x003A5CC0
		public override ISchedulerRenderer GetRenderer()
		{
			if (this.Owner.MonthView.GroupingDirectionResolved == GroupingDirection.Vertical)
			{
				return new VerticalRenderer(new VerticalView(this));
			}
			return new Renderer(new HorizontalView(this));
		}

		// Token: 0x060105E4 RID: 67044 RVA: 0x003A7AEC File Offset: 0x003A5CEC
		public override void HandleResize(Appointment appointment, ISchedulerTimeSlot sourceSlot, DateTime appointmentStart, DateTime appointmentEnd, bool editSeries)
		{
		}

		// Token: 0x060105E5 RID: 67045 RVA: 0x003A7AF0 File Offset: 0x003A5CF0
		public override IEnumerable<ScriptReference> GetScriptReferences()
		{
			List<ScriptReference> list = new List<ScriptReference>();
			list.AddRange(base.GetScriptReferences());
			list.Add(new ScriptReference("Telerik.Web.UI.Scheduler.Views.Month.GroupedByDate.Model.js", Assembly.GetExecutingAssembly().FullName));
			return list;
		}
	}
}

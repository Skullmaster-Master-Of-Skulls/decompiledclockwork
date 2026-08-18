using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;
using Telerik.Web.UI.Scheduler.Views.Agenda.GroupedByResource;

namespace Telerik.Web.UI.Scheduler.Views.Agenda.GroupedByDate
{
	// Token: 0x02000830 RID: 2096
	internal class Model : Model
	{
		// Token: 0x06004DAE RID: 19886 RVA: 0x000F391A File Offset: 0x000F1B1A
		public Model(IScheduler owner, string groupingResourceName) : base(owner, groupingResourceName)
		{
		}

		// Token: 0x06004DAF RID: 19887 RVA: 0x000F3924 File Offset: 0x000F1B24
		public override ISchedulerRenderer GetRenderer()
		{
			View view;
			if (this.Owner.AgendaView.GroupingDirectionResolved == GroupingDirection.Vertical)
			{
				view = new VerticalView(this);
			}
			else
			{
				view = new HorizontalView(this);
			}
			return new Renderer(view);
		}

		// Token: 0x06004DB0 RID: 19888 RVA: 0x000F395C File Offset: 0x000F1B5C
		public override IEnumerable<ScriptReference> GetScriptReferences()
		{
			List<ScriptReference> list = new List<ScriptReference>();
			list.AddRange(base.GetScriptReferences());
			list.Add(new ScriptReference("Telerik.Web.UI.Scheduler.Views.Agenda.GroupedByDate.Model.js", Assembly.GetExecutingAssembly().FullName));
			return list;
		}
	}
}

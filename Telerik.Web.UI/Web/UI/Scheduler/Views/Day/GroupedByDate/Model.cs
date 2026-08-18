using System;
using Telerik.Web.UI.Scheduler.Views.Day.GroupedByResource;

namespace Telerik.Web.UI.Scheduler.Views.Day.GroupedByDate
{
	// Token: 0x02001A4F RID: 6735
	internal class Model : Model
	{
		// Token: 0x0601056F RID: 66927 RVA: 0x003A5B1E File Offset: 0x003A3D1E
		public Model(IScheduler owner, string groupingResourceName) : base(owner, groupingResourceName)
		{
		}

		// Token: 0x06010570 RID: 66928 RVA: 0x003A5B28 File Offset: 0x003A3D28
		public override ISchedulerRenderer GetRenderer()
		{
			HorizontalView view = new HorizontalView(this);
			return new Renderer(view, GroupingDirection.Horizontal);
		}

		// Token: 0x17004F55 RID: 20309
		// (get) Token: 0x06010571 RID: 66929 RVA: 0x003A5B43 File Offset: 0x003A3D43
		public override string CssClass
		{
			get
			{
				return "rsDayView";
			}
		}
	}
}

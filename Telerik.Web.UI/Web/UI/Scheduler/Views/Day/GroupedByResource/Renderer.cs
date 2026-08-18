using System;
using System.Web.UI;
using Telerik.Web.UI.Scheduler.Views.Week;
using Telerik.Web.UI.Scheduler.Views.Week.GroupedByResource;

namespace Telerik.Web.UI.Scheduler.Views.Day.GroupedByResource
{
	// Token: 0x02001A72 RID: 6770
	internal class Renderer : Telerik.Web.UI.Scheduler.Views.Week.GroupedByResource.Renderer
	{
		// Token: 0x17004FB0 RID: 20400
		// (get) Token: 0x06010689 RID: 67209 RVA: 0x003AAA6C File Offset: 0x003A8C6C
		protected override string ContentTableCssClass
		{
			get
			{
				return "rsContentTable";
			}
		}

		// Token: 0x0601068A RID: 67210 RVA: 0x003AAA73 File Offset: 0x003A8C73
		public Renderer(Telerik.Web.UI.Scheduler.Views.Week.View view) : base(view)
		{
		}

		// Token: 0x0601068B RID: 67211 RVA: 0x003AAA7C File Offset: 0x003A8C7C
		public Renderer(Telerik.Web.UI.Scheduler.Views.Week.View view, GroupingDirection groupingDirection) : base(view, groupingDirection)
		{
		}

		// Token: 0x0601068C RID: 67212 RVA: 0x003AAA88 File Offset: 0x003A8C88
		protected override void CreateNavigationPane(Control container)
		{
			string dateLabel = string.Format("{0}", this.Owner.UtcToDisplay(base.Model.VisibleRangeStart).Date.ToString(this.Owner.DayView.HeaderDateFormat, this.Owner.Culture));
			container.Controls.Add(base.GetHeaderFactory(dateLabel, this.Owner).CreateHeaderControl());
		}
	}
}

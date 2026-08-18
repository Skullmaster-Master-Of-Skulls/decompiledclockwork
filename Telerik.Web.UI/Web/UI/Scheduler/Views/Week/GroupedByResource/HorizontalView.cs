using System;

namespace Telerik.Web.UI.Scheduler.Views.Week.GroupedByResource
{
	// Token: 0x02001A5F RID: 6751
	internal class HorizontalView : View
	{
		// Token: 0x060105F5 RID: 67061 RVA: 0x003A7EC8 File Offset: 0x003A60C8
		public HorizontalView(Model model) : base(model)
		{
		}

		// Token: 0x060105F6 RID: 67062 RVA: 0x003A7ED4 File Offset: 0x003A60D4
		protected override void InitializeColumnHeaders()
		{
			foreach (Resource resource in base.Model.Resources)
			{
				if (this.Owner.WeekView.ShowResourceHeadersResolved)
				{
					ViewHeader viewHeader = new ViewHeader();
					viewHeader.Text = resource.Text;
					viewHeader.ClassName = "rsMainHeader";
					viewHeader.Resource = resource;
					if (this.Owner.WeekView.ShowDateHeadersResolved)
					{
						viewHeader.SubHeaders = base.CreateDateHeaders();
					}
					base.ColumnHeaders.Add(viewHeader);
				}
				else if (this.Owner.WeekView.ShowDateHeadersResolved)
				{
					foreach (ViewHeader item in base.CreateDateHeaders())
					{
						base.ColumnHeaders.Add(item);
					}
				}
			}
		}
	}
}

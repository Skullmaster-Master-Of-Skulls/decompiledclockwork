using System;
using Telerik.Web.UI.Scheduler.Views.Week.GroupedByResource;

namespace Telerik.Web.UI.Scheduler.Views.Day.GroupedByResource
{
	// Token: 0x02001A6F RID: 6767
	internal class HorizontalView : HorizontalView
	{
		// Token: 0x17004FA5 RID: 20389
		// (get) Token: 0x06010663 RID: 67171 RVA: 0x003A9AE0 File Offset: 0x003A7CE0
		internal override BaseMultiDayViewSettings EffectiveViewSettings
		{
			get
			{
				return this.Owner.DayView;
			}
		}

		// Token: 0x06010664 RID: 67172 RVA: 0x003A9AED File Offset: 0x003A7CED
		public HorizontalView(Model model) : base(model)
		{
		}

		// Token: 0x06010665 RID: 67173 RVA: 0x003A9AF8 File Offset: 0x003A7CF8
		protected override void InitializeColumnHeaders()
		{
			if (!this.Owner.DayView.ShowResourceHeadersResolved)
			{
				return;
			}
			foreach (Resource resource in base.Model.Resources)
			{
				ViewHeader viewHeader = new ViewHeader();
				viewHeader.Text = resource.Text;
				viewHeader.Resource = resource;
				viewHeader.ClassName = "rsMainHeader rsLastCell";
				base.ColumnHeaders.Add(viewHeader);
			}
		}
	}
}

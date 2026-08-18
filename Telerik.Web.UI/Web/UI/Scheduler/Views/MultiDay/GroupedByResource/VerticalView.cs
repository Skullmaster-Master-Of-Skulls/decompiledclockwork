using System;
using Telerik.Web.UI.Scheduler.Views.Week.GroupedByResource;

namespace Telerik.Web.UI.Scheduler.Views.MultiDay.GroupedByResource
{
	// Token: 0x02001A69 RID: 6761
	internal class VerticalView : VerticalView
	{
		// Token: 0x17004F91 RID: 20369
		// (get) Token: 0x06010627 RID: 67111 RVA: 0x003A8B30 File Offset: 0x003A6D30
		internal override BaseMultiDayViewSettings EffectiveViewSettings
		{
			get
			{
				return this.Owner.MultiDayView;
			}
		}

		// Token: 0x17004F92 RID: 20370
		// (get) Token: 0x06010628 RID: 67112 RVA: 0x003A8B3D File Offset: 0x003A6D3D
		public override WeekViewSettings EffectiveWeekViewSettings
		{
			get
			{
				return this.Owner.MultiDayView;
			}
		}

		// Token: 0x06010629 RID: 67113 RVA: 0x003A8B4A File Offset: 0x003A6D4A
		public VerticalView(Model model) : base(model)
		{
		}
	}
}

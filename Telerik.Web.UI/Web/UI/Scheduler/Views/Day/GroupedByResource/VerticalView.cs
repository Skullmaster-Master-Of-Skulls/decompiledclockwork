using System;
using Telerik.Web.UI.Scheduler.Views.Week.GroupedByResource;

namespace Telerik.Web.UI.Scheduler.Views.Day.GroupedByResource
{
	// Token: 0x02001A6E RID: 6766
	internal class VerticalView : VerticalView
	{
		// Token: 0x17004FA4 RID: 20388
		// (get) Token: 0x06010661 RID: 67169 RVA: 0x003A9ACA File Offset: 0x003A7CCA
		internal override BaseMultiDayViewSettings EffectiveViewSettings
		{
			get
			{
				return this.Owner.DayView;
			}
		}

		// Token: 0x06010662 RID: 67170 RVA: 0x003A9AD7 File Offset: 0x003A7CD7
		public VerticalView(Model model) : base(model)
		{
		}
	}
}

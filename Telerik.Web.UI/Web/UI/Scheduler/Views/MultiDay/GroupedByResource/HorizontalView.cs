using System;
using Telerik.Web.UI.Scheduler.Views.Week.GroupedByResource;

namespace Telerik.Web.UI.Scheduler.Views.MultiDay.GroupedByResource
{
	// Token: 0x02001A67 RID: 6759
	internal class HorizontalView : HorizontalView
	{
		// Token: 0x17004F8A RID: 20362
		// (get) Token: 0x0601061B RID: 67099 RVA: 0x003A8907 File Offset: 0x003A6B07
		internal override BaseMultiDayViewSettings EffectiveViewSettings
		{
			get
			{
				return this.Owner.MultiDayView;
			}
		}

		// Token: 0x17004F8B RID: 20363
		// (get) Token: 0x0601061C RID: 67100 RVA: 0x003A8914 File Offset: 0x003A6B14
		public override WeekViewSettings EffectiveWeekViewSettings
		{
			get
			{
				return this.Owner.MultiDayView;
			}
		}

		// Token: 0x0601061D RID: 67101 RVA: 0x003A8921 File Offset: 0x003A6B21
		public HorizontalView(Model model) : base(model)
		{
		}
	}
}

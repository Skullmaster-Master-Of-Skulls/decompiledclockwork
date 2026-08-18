using System;
using Telerik.Web.UI.Scheduler.Views.Week.GroupedByDate;

namespace Telerik.Web.UI.Scheduler.Views.MultiDay.GroupedByDate
{
	// Token: 0x02001A66 RID: 6758
	internal class VerticalView : VerticalView
	{
		// Token: 0x17004F88 RID: 20360
		// (get) Token: 0x06010618 RID: 67096 RVA: 0x003A88E4 File Offset: 0x003A6AE4
		internal override BaseMultiDayViewSettings EffectiveViewSettings
		{
			get
			{
				return this.Owner.MultiDayView;
			}
		}

		// Token: 0x17004F89 RID: 20361
		// (get) Token: 0x06010619 RID: 67097 RVA: 0x003A88F1 File Offset: 0x003A6AF1
		public override WeekViewSettings EffectiveWeekViewSettings
		{
			get
			{
				return this.Owner.MultiDayView;
			}
		}

		// Token: 0x0601061A RID: 67098 RVA: 0x003A88FE File Offset: 0x003A6AFE
		public VerticalView(Model model) : base(model)
		{
		}
	}
}

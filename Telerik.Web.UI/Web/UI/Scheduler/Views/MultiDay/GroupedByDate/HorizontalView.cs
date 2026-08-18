using System;
using Telerik.Web.UI.Scheduler.Views.Week.GroupedByDate;

namespace Telerik.Web.UI.Scheduler.Views.MultiDay.GroupedByDate
{
	// Token: 0x02001A61 RID: 6753
	internal class HorizontalView : HorizontalView
	{
		// Token: 0x17004F81 RID: 20353
		// (get) Token: 0x060105FB RID: 67067 RVA: 0x003A815C File Offset: 0x003A635C
		internal override BaseMultiDayViewSettings EffectiveViewSettings
		{
			get
			{
				return this.Owner.MultiDayView;
			}
		}

		// Token: 0x17004F82 RID: 20354
		// (get) Token: 0x060105FC RID: 67068 RVA: 0x003A8169 File Offset: 0x003A6369
		public override WeekViewSettings EffectiveWeekViewSettings
		{
			get
			{
				return this.Owner.MultiDayView;
			}
		}

		// Token: 0x060105FD RID: 67069 RVA: 0x003A8176 File Offset: 0x003A6376
		public HorizontalView(Model model) : base(model)
		{
		}
	}
}

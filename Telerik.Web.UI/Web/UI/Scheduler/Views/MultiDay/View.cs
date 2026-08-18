using System;
using Telerik.Web.UI.Scheduler.Views.Week;

namespace Telerik.Web.UI.Scheduler.Views.MultiDay
{
	// Token: 0x02001A6C RID: 6764
	internal class View : View
	{
		// Token: 0x17004FA0 RID: 20384
		// (get) Token: 0x06010656 RID: 67158 RVA: 0x003A97A3 File Offset: 0x003A79A3
		internal override BaseMultiDayViewSettings EffectiveViewSettings
		{
			get
			{
				return this.Owner.MultiDayView;
			}
		}

		// Token: 0x17004FA1 RID: 20385
		// (get) Token: 0x06010657 RID: 67159 RVA: 0x003A97B0 File Offset: 0x003A79B0
		public override WeekViewSettings EffectiveWeekViewSettings
		{
			get
			{
				return this.Owner.MultiDayView;
			}
		}

		// Token: 0x06010658 RID: 67160 RVA: 0x003A97BD File Offset: 0x003A79BD
		public View(ModelBase model) : base(model)
		{
		}
	}
}

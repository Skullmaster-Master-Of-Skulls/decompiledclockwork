using System;
using Telerik.Web.UI.Scheduler.Views.Week;

namespace Telerik.Web.UI.Scheduler.Views.Day
{
	// Token: 0x02001A75 RID: 6773
	internal class View : View
	{
		// Token: 0x17004FB4 RID: 20404
		// (get) Token: 0x0601069A RID: 67226 RVA: 0x003AAE66 File Offset: 0x003A9066
		internal override BaseMultiDayViewSettings EffectiveViewSettings
		{
			get
			{
				return this.Owner.DayView;
			}
		}

		// Token: 0x0601069B RID: 67227 RVA: 0x003AAE73 File Offset: 0x003A9073
		public View(ModelBase model) : base(model)
		{
		}

		// Token: 0x0601069C RID: 67228 RVA: 0x003AAE7C File Offset: 0x003A907C
		protected override void InitializeColumnHeaders()
		{
		}
	}
}

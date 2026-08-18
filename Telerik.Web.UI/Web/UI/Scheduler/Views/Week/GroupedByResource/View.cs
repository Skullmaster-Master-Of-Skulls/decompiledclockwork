using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Week.GroupedByResource
{
	// Token: 0x02001A5E RID: 6750
	internal abstract class View : View
	{
		// Token: 0x17004F7F RID: 20351
		// (get) Token: 0x060105F2 RID: 67058 RVA: 0x003A7E3C File Offset: 0x003A603C
		public new Model Model
		{
			get
			{
				return this._model;
			}
		}

		// Token: 0x060105F3 RID: 67059 RVA: 0x003A7E44 File Offset: 0x003A6044
		public View(Model model) : base(model)
		{
			this._model = model;
		}

		// Token: 0x060105F4 RID: 67060 RVA: 0x003A7E54 File Offset: 0x003A6054
		protected static int GetMaxAllDayDepth(Model week)
		{
			List<ISchedulerTimeSlot> list = new List<ISchedulerTimeSlot>(week.AllDaySlots.Count);
			foreach (TimeSlot timeSlot in week.AllDaySlots)
			{
				TimeSlot item = (TimeSlot)timeSlot;
				list.Add(item);
			}
			AllDayLayout allDayLayout = new AllDayLayout(list, false);
			return allDayLayout.ActualRowCount;
		}

		// Token: 0x04004996 RID: 18838
		private readonly Model _model;
	}
}

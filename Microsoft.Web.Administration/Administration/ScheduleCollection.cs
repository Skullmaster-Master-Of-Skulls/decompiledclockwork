using System;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000067 RID: 103
	public sealed class ScheduleCollection : ConfigurationElementCollectionBase<Schedule>
	{
		// Token: 0x060002A8 RID: 680 RVA: 0x000073B3 File Offset: 0x000063B3
		internal ScheduleCollection()
		{
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x000073BC File Offset: 0x000063BC
		public Schedule Add(TimeSpan scheduleTime)
		{
			Schedule schedule = base.CreateElement();
			schedule.Time = scheduleTime;
			return base.Add(schedule);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x000073DE File Offset: 0x000063DE
		protected override Schedule CreateNewElement(string elementTagName)
		{
			return new Schedule();
		}
	}
}

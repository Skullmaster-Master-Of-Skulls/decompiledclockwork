using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduling
{
	// Token: 0x02000F96 RID: 3990
	internal class AppointmentsPerDayCounter
	{
		// Token: 0x17003043 RID: 12355
		// (get) Token: 0x06009893 RID: 39059 RVA: 0x0022163A File Offset: 0x0021F83A
		// (set) Token: 0x06009894 RID: 39060 RVA: 0x00221642 File Offset: 0x0021F842
		private Dictionary<DateTime, int> AppointmentsCount { get; set; }

		// Token: 0x17003044 RID: 12356
		public int this[DateTime day]
		{
			get
			{
				if (!this.AppointmentsCount.ContainsKey(day))
				{
					return 0;
				}
				return this.AppointmentsCount[day];
			}
		}

		// Token: 0x06009896 RID: 39062 RVA: 0x00221669 File Offset: 0x0021F869
		public AppointmentsPerDayCounter()
		{
			this.AppointmentsCount = new Dictionary<DateTime, int>();
		}

		// Token: 0x06009897 RID: 39063 RVA: 0x0022167C File Offset: 0x0021F87C
		public bool RegisterAppointment(DateTime start, TimeSpan duration, ISchedulerInfo schedulerInfo)
		{
			bool result = false;
			for (int i = 0; i < (int)Math.Ceiling(duration.TotalDays); i++)
			{
				DateTime key = start.Date.AddDays((double)i);
				int num;
				this.AppointmentsCount.TryGetValue(key, out num);
				if (num <= schedulerInfo.VisibleAppointmentsPerDay)
				{
					result = true;
				}
				this.AppointmentsCount[key] = num + 1;
			}
			return result;
		}
	}
}

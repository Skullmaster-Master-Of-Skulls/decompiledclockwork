using System;
using System.Collections;

namespace ClockWorkAPI
{
	// Token: 0x02000061 RID: 97
	public class AvailabilityDayOfWeekCollection : CollectionBase
	{
		// Token: 0x06000576 RID: 1398 RVA: 0x0001CBF0 File Offset: 0x0001BBF0
		public int Add(AvailabilityDayOfWeek availabilityDayOfWeek)
		{
			return base.List.Add(availabilityDayOfWeek);
		}
	}
}

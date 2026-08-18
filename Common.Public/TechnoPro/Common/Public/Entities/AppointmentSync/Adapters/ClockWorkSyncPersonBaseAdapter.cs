using System;

namespace TechnoPro.Common.Public.Entities.AppointmentSync.Adapters
{
	// Token: 0x020004EE RID: 1262
	public static class ClockWorkSyncPersonBaseAdapter
	{
		// Token: 0x06002627 RID: 9767 RVA: 0x00028B88 File Offset: 0x00026D88
		public static string GetName(this ClockWorkSyncPersonBase Person)
		{
			return string.Format("{0} {1}", Person.FirstName, Person.LastName);
		}
	}
}

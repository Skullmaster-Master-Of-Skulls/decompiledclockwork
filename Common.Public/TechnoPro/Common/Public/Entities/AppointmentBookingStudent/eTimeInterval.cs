using System;

namespace TechnoPro.Common.Public.Entities.AppointmentBookingStudent
{
	// Token: 0x02000568 RID: 1384
	[Serializable]
	public enum eTimeInterval
	{
		// Token: 0x04001F52 RID: 8018
		[TimeInterval(true)]
		Days = 1,
		// Token: 0x04001F53 RID: 8019
		[TimeInterval(true)]
		WeekDays,
		// Token: 0x04001F54 RID: 8020
		[TimeInterval(true)]
		Months,
		// Token: 0x04001F55 RID: 8021
		[TimeInterval(true)]
		Years,
		// Token: 0x04001F56 RID: 8022
		[TimeInterval(false)]
		Minutes,
		// Token: 0x04001F57 RID: 8023
		[TimeInterval(false)]
		Hours
	}
}

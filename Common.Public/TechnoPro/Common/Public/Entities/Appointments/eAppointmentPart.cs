using System;

namespace TechnoPro.Common.Public.Entities.Appointments
{
	// Token: 0x020004B6 RID: 1206
	[Flags]
	[Serializable]
	public enum eAppointmentPart
	{
		// Token: 0x04001B3C RID: 6972
		None = 0,
		// Token: 0x04001B3D RID: 6973
		DateTimeAndDuration = 1,
		// Token: 0x04001B3E RID: 6974
		RecurringGroupCode = 2
	}
}

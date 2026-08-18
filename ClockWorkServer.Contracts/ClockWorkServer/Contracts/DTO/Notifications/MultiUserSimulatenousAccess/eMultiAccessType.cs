using System;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notifications.MultiUserSimulatenousAccess
{
	// Token: 0x02000413 RID: 1043
	[Serializable]
	public enum eMultiAccessType
	{
		// Token: 0x0400078F RID: 1935
		Unknown,
		// Token: 0x04000790 RID: 1936
		Appointment,
		// Token: 0x04000791 RID: 1937
		PerStudentForm,
		// Token: 0x04000792 RID: 1938
		PerDateForm,
		// Token: 0x04000793 RID: 1939
		Accommodations
	}
}

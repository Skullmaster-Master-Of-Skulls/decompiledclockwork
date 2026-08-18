using System;

namespace TechnoPro.Common.Public.Entities.AppointmentSync
{
	// Token: 0x020004D8 RID: 1240
	public enum eClockWorkExternalApplicationSyncActionType
	{
		// Token: 0x04001BB7 RID: 7095
		Unknown,
		// Token: 0x04001BB8 RID: 7096
		DoNothing,
		// Token: 0x04001BB9 RID: 7097
		CreateClockWorkAppointment,
		// Token: 0x04001BBA RID: 7098
		CreateExternalAppointment,
		// Token: 0x04001BBB RID: 7099
		UpdateClockWorkAppointment,
		// Token: 0x04001BBC RID: 7100
		UpdateExternalAppointment,
		// Token: 0x04001BBD RID: 7101
		DeleteClockWorkAppointment,
		// Token: 0x04001BBE RID: 7102
		DeleteExternalAppointment
	}
}

using System;

namespace TechnoPro.Common.Public.Entities.AppointmentsRecurring
{
	// Token: 0x02000550 RID: 1360
	[Serializable]
	public enum eRecurringInstanceSetPropertyModifyBehaviour
	{
		// Token: 0x04001EF7 RID: 7927
		Default,
		// Token: 0x04001EF8 RID: 7928
		ApplyChangeToAllAppointmentsInSet,
		// Token: 0x04001EF9 RID: 7929
		ApplyChangeToPrimaryAppointmentOnly
	}
}

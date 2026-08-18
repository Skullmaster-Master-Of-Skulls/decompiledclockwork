using System;

namespace TechnoPro.Common.ClientManager.Notifications.AppointmentNotifications
{
	// Token: 0x02000023 RID: 35
	public enum eAppNotificationMessageCode
	{
		// Token: 0x04000064 RID: 100
		Unknown,
		// Token: 0x04000065 RID: 101
		AppointmentCreateStarted,
		// Token: 0x04000066 RID: 102
		AppointmentCreateEnded,
		// Token: 0x04000067 RID: 103
		AppointmentModifyStarted,
		// Token: 0x04000068 RID: 104
		AppointmentModifyEnded,
		// Token: 0x04000069 RID: 105
		AppointmentDeleted,
		// Token: 0x0400006A RID: 106
		CalendarRefreshRequired,
		// Token: 0x0400006B RID: 107
		CheckIfAlreadyBookingNewAppSlotRequested,
		// Token: 0x0400006C RID: 108
		ImAlreadyBookingNewAppSlotNotify_TellOtherClockWorks,
		// Token: 0x0400006D RID: 109
		ImAlreadyBookingNewAppSlotNotify_NotifyInternal,
		// Token: 0x0400006E RID: 110
		NotifyStudentIsWaiting
	}
}

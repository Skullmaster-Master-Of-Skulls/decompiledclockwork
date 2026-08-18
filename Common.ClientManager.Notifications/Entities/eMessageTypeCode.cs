using System;

namespace TechnoPro.Common.ClientManager.Notifications.Entities
{
	// Token: 0x0200000F RID: 15
	public enum eMessageTypeCode
	{
		// Token: 0x04000028 RID: 40
		Unknown,
		// Token: 0x04000029 RID: 41
		AppointmentCreated = 1001,
		// Token: 0x0400002A RID: 42
		AppointmentModified,
		// Token: 0x0400002B RID: 43
		AppointmentDeleted,
		// Token: 0x0400002C RID: 44
		StudentCreated,
		// Token: 0x0400002D RID: 45
		StudentModified,
		// Token: 0x0400002E RID: 46
		StudentDeleted,
		// Token: 0x0400002F RID: 47
		AskIfAnyoneCreatingNewAppointment,
		// Token: 0x04000030 RID: 48
		RespondToAskIfAnyoneCreatingNewAppointment,
		// Token: 0x04000031 RID: 49
		TestExamCreated = 2000,
		// Token: 0x04000032 RID: 50
		TestExamModified,
		// Token: 0x04000033 RID: 51
		TestExamDeleted,
		// Token: 0x04000034 RID: 52
		TaskCreated = 3000,
		// Token: 0x04000035 RID: 53
		TaskModified,
		// Token: 0x04000036 RID: 54
		TaskDeleted,
		// Token: 0x04000037 RID: 55
		TaskGroupCreated = 3050,
		// Token: 0x04000038 RID: 56
		TaskGroupModified,
		// Token: 0x04000039 RID: 57
		TaskGroupDeleted,
		// Token: 0x0400003A RID: 58
		StudentIsWaiting = 4000,
		// Token: 0x0400003B RID: 59
		AlternateFormat_ToBeApprovedStudentMediaRequestListChanged = 5000,
		// Token: 0x0400003C RID: 60
		AlternateFormat_InProgressStudentMediaRequestListChanged,
		// Token: 0x0400003D RID: 61
		AlternateFormat_CompletedStudentMediaRequestListChanged,
		// Token: 0x0400003E RID: 62
		AlternateFormat_InProgressMediaJobListChanged,
		// Token: 0x0400003F RID: 63
		AlternateFormat_CompletedMediaJobListChanged,
		// Token: 0x04000040 RID: 64
		AlternateFormat_CancelledMediaJobListChanged,
		// Token: 0x04000041 RID: 65
		AlternateFormat_PublisherListChanged,
		// Token: 0x04000042 RID: 66
		AlternateFormat_MediaContentListChanged,
		// Token: 0x04000043 RID: 67
		MultiAccess_UserIsBroadcastingThatHeJustEnteredData = 5500,
		// Token: 0x04000044 RID: 68
		MultiAccess_UserIsRespondingThatHeIsAlreadyEditingThisData
	}
}

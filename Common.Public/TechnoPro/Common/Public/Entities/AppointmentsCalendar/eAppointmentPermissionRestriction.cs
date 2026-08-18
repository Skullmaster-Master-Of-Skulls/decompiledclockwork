using System;

namespace TechnoPro.Common.Public.Entities.AppointmentsCalendar
{
	// Token: 0x0200055C RID: 1372
	[Serializable]
	public enum eAppointmentPermissionRestriction
	{
		// Token: 0x04001F19 RID: 7961
		[AppointmentPermissionRestriction]
		Unknown,
		// Token: 0x04001F1A RID: 7962
		[AppointmentPermissionRestriction(eAppointmentPermissionRestrictionResult.NotAllowedToView, "Appointment is marked as private")]
		PrivateNotInAttendeesOrBooker,
		// Token: 0x04001F1B RID: 7963
		[AppointmentPermissionRestriction(eAppointmentPermissionRestrictionResult.NotAllowedToModifyOrDelete, "Appointment is marked as locked for editing")]
		LockedNotInAttendeesOrBooker,
		// Token: 0x04001F1C RID: 7964
		[AppointmentPermissionRestriction(eAppointmentPermissionRestrictionResult.NotAllowedToView, "Not allowed to view students in appointments")]
		UserNotAllowedToViewStudents,
		// Token: 0x04001F1D RID: 7965
		[AppointmentPermissionRestriction(eAppointmentPermissionRestrictionResult.NotAllowedToModifyOrDelete, "External appointment (eg Outlook) can't be modified")]
		ExternalAppointmentReadOnly,
		// Token: 0x04001F1E RID: 7966
		[AppointmentPermissionRestriction(eAppointmentPermissionRestrictionResult.NotAllowedToModifyOrDelete, "Not allowed to modify appointments")]
		UserNotAllowedToModifyAppointments,
		// Token: 0x04001F1F RID: 7967
		[AppointmentPermissionRestriction(eAppointmentPermissionRestrictionResult.NotAllowedToModifyOrDelete, "Not allowed to modify appointments with no appointment type")]
		UserNotAllowedToModifyAppointmentsWithNoAppType,
		// Token: 0x04001F20 RID: 7968
		[AppointmentPermissionRestriction(eAppointmentPermissionRestrictionResult.NotAllowedToModifyOrDelete, "Not allowed to modify appointment because of cutoff permission")]
		UserNotAllowedToModifyAppointmentsCutoffPassed,
		// Token: 0x04001F21 RID: 7969
		[AppointmentPermissionRestriction(eAppointmentPermissionRestrictionResult.NotAllowedToDelete, "Not allowed to delete appointments")]
		UserNotAllowedToDeleteAppointments,
		// Token: 0x04001F22 RID: 7970
		[AppointmentPermissionRestriction(eAppointmentPermissionRestrictionResult.NotAllowedToDelete, "Not allowed to delete appointments you didn't create")]
		UserNotAllowedToDeleteAppointmentsTheyDidntCreate,
		// Token: 0x04001F23 RID: 7971
		[AppointmentPermissionRestriction(eAppointmentPermissionRestrictionResult.NotAllowedToView, "Not allowed to view others schedule details")]
		UserNotAllowedToViewOthersSchedules
	}
}

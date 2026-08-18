using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar;

namespace TechnoPro.Common.ClientManager.ICore.AppointmentsCalendar
{
	// Token: 0x02000094 RID: 148
	public interface IAppointmentClientManager : IWebService
	{
		// Token: 0x0600047B RID: 1147
		int CreateAppointment(AppointmentDTO Appointment);

		// Token: 0x0600047C RID: 1148
		void UpdateAppointment(AppointmentDTO Appointment);

		// Token: 0x0600047D RID: 1149
		void DeleteAppointment(int AppointmentId);

		// Token: 0x0600047E RID: 1150
		void DeleteAppointmentWithoutFiringNotifications(int AppointmentId);

		// Token: 0x0600047F RID: 1151
		IList<AppointmentDTO> LoadAppointments(List<int> PersonIds, List<int> AppTypeIds, bool HideCancelled, bool LoadPerStudentDataIcons, bool LoadPerAnonymousDataIcons, DateTime StartDateTime, DateTime EndDateTime);

		// Token: 0x06000480 RID: 1152
		AppointmentDTO LoadAppointment(int AppointmentId);

		// Token: 0x06000481 RID: 1153
		LoadAppointmentExtendedInfoResp LoadAppointmentExtendedInfo(int AppointmentId);

		// Token: 0x06000482 RID: 1154
		IList<AppointmentDTO> LoadAppointmentsWithSpecialPermissions(IList<int> PersonIds, IList<int> AppTypeIds, bool HideCancelled, DateTime StartDateTime, int NumDays, out IDictionary<int, IList<eAppointmentPermissionRestriction>> permissionRestrictions);

		// Token: 0x06000483 RID: 1155
		void CancelAppointment(int AppointmentId, AppCancelInfoDTO CancelInfo);

		// Token: 0x06000484 RID: 1156
		void UnCancelAppointment(int AppointmentId);

		// Token: 0x06000485 RID: 1157
		void MarkAppointmentTentative(int AppointmentId);

		// Token: 0x06000486 RID: 1158
		void UnMarkAppointmentTentative(int AppointmentId);

		// Token: 0x06000487 RID: 1159
		int RecoverDeletedAppointment(int AppointmentId);

		// Token: 0x06000488 RID: 1160
		void MergeAllAppointments(int PersonIdNew, int PersonIdOld);

		// Token: 0x06000489 RID: 1161
		AppointmentDTO LoadDeletedAppointmentById(int AppointmentId);

		// Token: 0x0600048A RID: 1162
		void UpdateAppointmentParts(AppointmentDTO Appointment, eAppointmentPart PartsToUpdate);

		// Token: 0x0600048B RID: 1163
		void InsertOrUpdateAppointmentMemo(int AppointmentId, string MemoText);

		// Token: 0x0600048C RID: 1164
		int GetNumberOfAppointmentsWithAppType(int appTypeId);

		// Token: 0x0600048D RID: 1165
		void SwapAppointmentTypeForAllAppointments(int appTypeIdToReplace, int appTypeIdToKeep);

		// Token: 0x0600048E RID: 1166
		IList<BaseBasicAppointmentDTO> FreeTimeSearch(FreeTimeSearchContextDTO FreeTimeSearchContext);

		// Token: 0x0600048F RID: 1167
		AppointmentsWithAvailabilityAndTimetableDTO LoadAppointmentsAndAvailability(AppointmentLoadOptionsDTO LoadOptions);

		// Token: 0x06000490 RID: 1168
		Task<AppointmentsWithAvailabilityAndTimetableDTO> LoadAppointmentsAndAvailabilityAsync(AppointmentLoadOptionsDTO LoadOptions);

		// Token: 0x06000491 RID: 1169
		IList<BaseBasicAppointmentDTO> LoadBasicAppointmentInformationByUserAndDateRange(int PersonId, DateTime StartDate, DateTime EndDate, bool HideCancelled);

		// Token: 0x06000492 RID: 1170
		DatesAndAppointmentsWithAvailabilityAndTimetable LoadAppointmentsFromDatabase(LoadAppointmentsFromDatabaseParameters loadAppsParams);

		// Token: 0x06000493 RID: 1171
		Task<DatesAndAppointmentsWithAvailabilityAndTimetable> LoadAppointmentsFromDatabaseAsync(LoadAppointmentsFromDatabaseParameters loadAppsParams);

		// Token: 0x06000494 RID: 1172
		AppointmentsWithAvailabilityAndTimetableDTO LoadAppointmentsFromDatabase(IList<int> appTypeIds, DateTime sd, DateTime ed, IList<int> pids, bool hideCancelledAppointments, bool perStudentShowIconsForFilledOutPerStudentScreensOnAppointments, bool anonymousShowIconsForFilledOutAnonymousScreensOnAppointments, IList<int> studentPersonIdsForTimetableLoad);

		// Token: 0x06000495 RID: 1173
		Task<AppointmentsWithAvailabilityAndTimetableDTO> LoadAppointmentsFromDatabaseAsync(IList<int> appTypeIds, DateTime sd, DateTime ed, IList<int> pids, bool hideCancelledAppointments, bool perStudentShowIconsForFilledOutPerStudentScreensOnAppointments, bool anonymousShowIconsForFilledOutAnonymousScreensOnAppointments, IList<int> studentPersonIdsForTimetableLoad);

		// Token: 0x06000496 RID: 1174
		void UpdateAppointmentDateAndTime(int AppointmentId, DateTime NewStartDateTime, DateTime NewEndDateTime);

		// Token: 0x06000497 RID: 1175
		AppointmentDTO LoadAppointmentWithSpecialPermissions(int appointmentId, out IList<eAppointmentPermissionRestriction> permissionRestrictions);

		// Token: 0x06000498 RID: 1176
		void CancelAttendeeAppointment(int appointmentId, int personId, AppCancelInfoDTO CancelInfo);
	}
}

using System;
using System.Collections.Generic;
using System.Data.Common;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsRecurring;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.Appointments
{
	// Token: 0x020000AC RID: 172
	public interface IBaseAppointmentDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600047A RID: 1146
		int CreateBaseExtendedAppointment(BaseExtendedAppointment basicAppointment, DbTransaction transaction = null);

		// Token: 0x0600047B RID: 1147
		void UpdateBaseBasicAppointment(BaseBasicAppointment basicAppointment, DbTransaction transaction = null);

		// Token: 0x0600047C RID: 1148
		void UpdateBaseExtendedAppointment(BaseExtendedAppointment basicAppointment, DbTransaction transaction = null);

		// Token: 0x0600047D RID: 1149
		void UpdateBaseExtendedAppointment(BaseExtendedAppointment basicAppointment, RecurringInstanceSetModifyBehaviour ModifyBehaivour, DbTransaction transaction = null);

		// Token: 0x0600047E RID: 1150
		void DeleteAppointment(int AppointmentId, DbTransaction transaction = null);

		// Token: 0x0600047F RID: 1151
		void UpdateDateAndTime(int appId, DateTime startDateTime, DateTime endDateTime, DbTransaction transaction = null);

		// Token: 0x06000480 RID: 1152
		void UpdateAppointmentCancelledValue(int appId, bool cancelledValue, AppCancelInfo cancelInfo, DbTransaction transaction = null);

		// Token: 0x06000481 RID: 1153
		void UpdateAppointmentAppCodeValue(int appId, int appCodeValue, DbTransaction transaction = null);

		// Token: 0x06000482 RID: 1154
		BaseBasicAppointment LoadBaseBasicAppointmentById(int appointmentId);

		// Token: 0x06000483 RID: 1155
		T LoadBaseExtendedAppointmentById<T>(int appointmentId) where T : BaseExtendedAppointment;

		// Token: 0x06000484 RID: 1156
		IList<T> LoadBaseExtendedAppointmentsByDateRangeAndAppType<T>(DateTime StartDateTime, DateTime EndDateTime, IList<int> AppTypeIds) where T : BaseExtendedAppointment;

		// Token: 0x06000485 RID: 1157
		IList<T> LoadBaseExtendedAppointmentsByDateRangeAndPersonIds<T>(DateTime StartDate, DateTime EndDate, IList<int> PersonIds) where T : BaseExtendedAppointment;

		// Token: 0x06000486 RID: 1158
		IList<T> LoadBaseExtendedAppointmentsByDateRangeAndPersonIdsAndAppTypes<T>(DateTime StartDate, DateTime EndDate, IList<int> PersonIds, IList<int> AppTypeIds, bool HideCancelled) where T : BaseExtendedAppointment;

		// Token: 0x06000487 RID: 1159
		int InsertOrUpdateAppointmentRoom(int appId, int roomId, DbTransaction transaction = null);

		// Token: 0x06000488 RID: 1160
		void DeleteAppointmentRoom(int appId, DbTransaction transaction = null);

		// Token: 0x06000489 RID: 1161
		int FindMatchingExistingAppointment(BaseExtendedAppointment Appointment);

		// Token: 0x0600048A RID: 1162
		int CreateBaseBasicAppointment(BaseBasicAppointment basicAppointment, DbTransaction transaction = null);

		// Token: 0x0600048B RID: 1163
		IList<PersonBase> LoadDistinctUsersAStudentHasHadAtLeastOneAppointmentWith(int StudentPersonId, IList<int> StaffGroupIds);

		// Token: 0x0600048C RID: 1164
		void InsertOrUpdateAppointmentMemo(int AppointmentId, string MemoText, DbTransaction transaction = null);

		// Token: 0x0600048D RID: 1165
		IList<T> LoadBaseExtendedAppointmentsByGroupCode<T>(int GroupCode) where T : BaseExtendedAppointment;

		// Token: 0x0600048E RID: 1166
		IList<T> LoadBaseExtendedAppointmentsByAppointmentIds<T>(IList<int> AppointmentIds, IList<int> allowedAppTypeIds) where T : BaseExtendedAppointment;

		// Token: 0x0600048F RID: 1167
		IList<T> LoadBaseExtendedAppointmentsByPersonId<T>(int PersonId) where T : BaseExtendedAppointment;

		// Token: 0x06000490 RID: 1168
		IList<T> LoadBaseExtendedAppointmentsByDateRange<T>(DateTime StartDateTime, DateTime EndDateTime, bool ShowCancelled = false) where T : BaseExtendedAppointment;

		// Token: 0x06000491 RID: 1169
		void DeleteMemo(int AppointmentId, DbTransaction transaction = null);

		// Token: 0x06000492 RID: 1170
		void DeleteCancelledReason(int AppointmentId, DbTransaction transaction = null);

		// Token: 0x06000493 RID: 1171
		void DeleteIcons(int AppointmentId, DbTransaction transaction = null);

		// Token: 0x06000494 RID: 1172
		void DeleteAttendees(int AppointmentId, DbTransaction transaction = null);

		// Token: 0x06000495 RID: 1173
		void DeleteAppointmentWorkshopInfo(int AppointmentId, DbTransaction transaction = null);

		// Token: 0x06000496 RID: 1174
		void DeleteTestExamInfo(int AppointmentId, DbTransaction transaction = null);

		// Token: 0x06000497 RID: 1175
		void DeleteAppData(int AppointmentId, DbTransaction transaction = null);

		// Token: 0x06000498 RID: 1176
		void DeleteMainAppointment(int AppointmentId, DbTransaction transaction = null);

		// Token: 0x06000499 RID: 1177
		IList<BaseBasicAppointment> LoadBaseBasicAppointmentsByPersonAndDateRange(int PersonId, bool hideCancelled, DateTime StartDate, DateTime EndDate);

		// Token: 0x0600049A RID: 1178
		void UpdateAppointmentExternalId(int appId, int externalId);

		// Token: 0x0600049B RID: 1179
		int LoadAppointmentExternalId(int appId);

		// Token: 0x0600049C RID: 1180
		int CreateBaseExtendedAppointmentEnsureUsersNotDoubleBooked(BaseExtendedAppointment extAppointment, int[] PidsToEnsureNotDoubleBooked, DbTransaction transaction = null);
	}
}

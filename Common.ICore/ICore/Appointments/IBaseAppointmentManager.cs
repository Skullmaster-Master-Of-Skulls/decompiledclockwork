using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsRecurring;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.ICore.Appointments
{
	// Token: 0x020000E6 RID: 230
	public interface IBaseAppointmentManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000730 RID: 1840
		int CreateBaseExtendedAppointment(bool runInTransaction, BaseExtendedAppointment basicAppointment);

		// Token: 0x06000731 RID: 1841
		void UpdateBaseBasicAppointment(bool runInTransaction, BaseBasicAppointment basicAppointment);

		// Token: 0x06000732 RID: 1842
		void UpdateBaseExtendedAppointment(bool runInTransaction, BaseExtendedAppointment basicAppointment);

		// Token: 0x06000733 RID: 1843
		void UpdateBaseExtendedAppointment(bool runInTransaction, BaseExtendedAppointment basicAppointment, RecurringInstanceSetModifyBehaviour ModifyBehaivour);

		// Token: 0x06000734 RID: 1844
		void DeleteAppointment(bool runInTransaction, int AppointmentId);

		// Token: 0x06000735 RID: 1845
		void UpdateDateAndTime(bool runInTransaction, int appId, DateTime startDateTime, DateTime endDateTime);

		// Token: 0x06000736 RID: 1846
		void UpdateAppointmentCancelledValue(bool runInTransaction, int appId, bool cancelledValue, AppCancelInfo cancelInfo);

		// Token: 0x06000737 RID: 1847
		void UpdateAppointmentAppCodeValue(bool runInTransaction, int appId, int appCodeValue);

		// Token: 0x06000738 RID: 1848
		BaseBasicAppointment LoadBaseBasicAppointmentById(int appointmentId);

		// Token: 0x06000739 RID: 1849
		T LoadBaseExtendedAppointmentById<T>(int appointmentId) where T : BaseExtendedAppointment;

		// Token: 0x0600073A RID: 1850
		IList<T> LoadBaseExtendedAppointmentsByDateRangeAndAppType<T>(DateTime StartDateTime, DateTime EndDateTime, IList<int> AppTypeIds) where T : BaseExtendedAppointment;

		// Token: 0x0600073B RID: 1851
		IList<T> LoadBaseExtendedAppointmentsByDateRangeAndPersonIds<T>(DateTime StartDate, DateTime EndDate, IList<int> PersonIds) where T : BaseExtendedAppointment;

		// Token: 0x0600073C RID: 1852
		int InsertOrUpdateAppointmentRoom(bool runInTransaction, int appId, int roomId);

		// Token: 0x0600073D RID: 1853
		void DeleteAppointmentRoom(bool runInTransaction, int appId);

		// Token: 0x0600073E RID: 1854
		int FindMatchingExistingAppointment(BaseExtendedAppointment Appointment);

		// Token: 0x0600073F RID: 1855
		void UpdateAppointmentParts(bool runInTransaction, BaseBasicAppointment Appointment, eAppointmentPart PartsToUpdate);

		// Token: 0x06000740 RID: 1856
		int CreateBaseBasicAppointment(bool runInTransaction, BaseBasicAppointment basicAppointment);

		// Token: 0x06000741 RID: 1857
		IList<PersonBase> LoadDistinctUsersAStudentHasHadAtLeastOneAppointmentWith(int StudentPersonId, IList<int> StaffGroupIds);

		// Token: 0x06000742 RID: 1858
		void InsertOrUpdateAppointmentMemo(bool runInTransaction, int AppointmentId, string MemoText);

		// Token: 0x06000743 RID: 1859
		IList<T> LoadBaseExtendedAppointmentsByGroupCode<T>(int GroupCode) where T : BaseExtendedAppointment;

		// Token: 0x06000744 RID: 1860
		IList<T> LoadBaseExtendedAppointmentsByAppointmentIds<T>(IList<int> AppointmentIds) where T : BaseExtendedAppointment;

		// Token: 0x06000745 RID: 1861
		IList<T> LoadBaseExtendedAppointmentsByPersonId<T>(int PersonId) where T : BaseExtendedAppointment;

		// Token: 0x06000746 RID: 1862
		IList<BaseBasicAppointment> FreeTimeSearch(FreeTimeSearchContext Context);

		// Token: 0x06000747 RID: 1863
		IList<T> LoadBaseExtendedAppointmentsByDateRange<T>(DateTime StartDate, int NumDays, bool ShowCancelled = false) where T : BaseExtendedAppointment;

		// Token: 0x06000748 RID: 1864
		IList<BaseBasicAppointment> LoadBaseBasicAppointmentsByPersonAndDateRange(int PersonId, bool hideCancelled, DateTime StartDate, DateTime EndDate);

		// Token: 0x06000749 RID: 1865
		void UpdateAppointmentExternalId(int appId, int externalId);
	}
}

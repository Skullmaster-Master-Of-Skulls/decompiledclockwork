using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar;

namespace TechnoPro.Common.ICore.AppointmentsCalendar
{
	// Token: 0x020000EC RID: 236
	public interface IAppointmentManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600075B RID: 1883
		int CreateAppointment(bool runInTransaction, Appointment Appointment);

		// Token: 0x0600075C RID: 1884
		void UpdateAppointment(bool runInTransaction, Appointment Appointment);

		// Token: 0x0600075D RID: 1885
		void DeleteAppointment(bool runInTransaction, int AppointmentId);

		// Token: 0x0600075E RID: 1886
		List<Appointment> LoadAppointments(List<int> PersonIds, List<int> AppTypeIds, bool HideCancelled, bool LoadPerStudentDataIcons, bool LoadPerAnonymousDataIcons, DateTime StartDateTime, DateTime EndDateTime);

		// Token: 0x0600075F RID: 1887
		Appointment LoadAppointment(int AppointmentId);

		// Token: 0x06000760 RID: 1888
		void CancelAppointment(bool runInTransaction, int AppointmentId, AppCancelInfo CancelInfo);

		// Token: 0x06000761 RID: 1889
		void UnCancelAppointment(bool runInTransaction, int AppointmentId);

		// Token: 0x06000762 RID: 1890
		void MarkAppointmentTentative(bool runInTransaction, int AppointmentId);

		// Token: 0x06000763 RID: 1891
		void UnMarkAppointmentTentative(bool runInTransaction, int AppointmentId);

		// Token: 0x06000764 RID: 1892
		int RecoverDeletedAppointment(bool runInTransaction, int AppointmentId);

		// Token: 0x06000765 RID: 1893
		void MergeAllAppointments(bool runInTransaction, int PersonIdNew, int PersonIdOld);

		// Token: 0x06000766 RID: 1894
		Appointment LoadDeletedAppointmentById(int AppointmentId);

		// Token: 0x06000767 RID: 1895
		IList<Appointment> LoadAllAppointmentsInADay(DateTime DayToLoadAppointmentsFor, bool ShowCancelled = false, int NumDaysToLoadAppointmentsFor = 1, int[] AppTypeIds = null);

		// Token: 0x06000768 RID: 1896
		AppointmentsWithAvailabilityAndTimetable LoadAppointmentsAndAvailability(AppointmentLoadOptions LoadOptions);

		// Token: 0x06000769 RID: 1897
		Task<AppointmentsWithAvailabilityAndTimetable> LoadAppointmentsAndAvailabilityAsync(AppointmentLoadOptions LoadOptions);

		// Token: 0x0600076A RID: 1898
		int GetNumberOfNonCancelledAppointments(int PersonId, DateTime StartDate, DateTime? EndDate, params int[] AppTypeIdsToCheck);

		// Token: 0x0600076B RID: 1899
		int GetNumberOfConsecutiveNoshows(int PersonId, DateTime StartDate, int MaxNumberOfNoShowsToCheckFor, params int[] AppTypeIdsToCheck);

		// Token: 0x0600076C RID: 1900
		int LoadAppointmentOrganizerPersonId(int appointmentId);

		// Token: 0x0600076D RID: 1901
		int CreateAppointmentEnsureUsersNotDoubleBooked(bool RunInTransaction, Appointment Appointment, int[] PidsToEnsureNotDoubleBooked);

		// Token: 0x0600076E RID: 1902
		int GetNumberOfAppointmentsWithAppType(int appTypeId);

		// Token: 0x0600076F RID: 1903
		void SwapAppointmentTypeForAllAppointments(int appTypeIdToReplace, int appTypeIdToKeep);

		// Token: 0x06000770 RID: 1904
		IList<Appointment> LoadAppointmentsWithSpecialPermissions(List<int> PersonIds, List<int> AppTypeIds, bool HideCancelled, DateTime StartDateTime, DateTime EndDateTime, out IDictionary<int, IList<eAppointmentPermissionRestriction>> permissionRestrictions);

		// Token: 0x06000771 RID: 1905
		Appointment LoadAppointmentWithSpecialPermissions(int appointmentId, out IList<eAppointmentPermissionRestriction> permissionRestrictions);

		// Token: 0x06000772 RID: 1906
		IDictionary<int, IList<AppointmentBasicSlot>> LoadUncancelledBookedSlots(IList<int> personIds, DateTime startDate, int numDays);

		// Token: 0x06000773 RID: 1907
		void CancelAttendeeAppointment(int appointmentId, int personId, AppCancelInfo CancelInfo);

		// Token: 0x06000774 RID: 1908
		int GetNumberOfNonCancelledAppointments(int PersonId, DateTime StartDate, DateTime? EndDate, bool excludeTestsExams, params int[] AppTypeIdsToCheck);
	}
}

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar;

namespace TechnoPro.Common.DAO.AppointmentsCalendar
{
	// Token: 0x020000C7 RID: 199
	public interface IAppointmentDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000569 RID: 1385
		Appointment LoadDeletedAppointmentById(int AppointmentId);

		// Token: 0x0600056A RID: 1386
		void UpdateAppointment(Appointment Appointment, DbTransaction transaction = null);

		// Token: 0x0600056B RID: 1387
		int CreateAppointment(Appointment Appointment, DbTransaction transaction = null);

		// Token: 0x0600056C RID: 1388
		List<Appointment> LoadAppointments(List<int> PersonIds, List<int> AppTypeIds, bool HideCancelled, bool LoadPerStudentDataIcons, bool LoadPerAnonymousDataIcons, DateTime StartDateTime, DateTime EndDateTime);

		// Token: 0x0600056D RID: 1389
		Task<IList<Appointment>> LoadAppointmentsAsync(List<int> PersonIds, List<int> AppTypeIds, bool HideCancelled, bool LoadPerStudentDataIcons, bool LoadPerAnonymousDataIcons, DateTime StartDateTime, DateTime EndDateTime);

		// Token: 0x0600056E RID: 1390
		Appointment LoadAppointment(int AppointmentId);

		// Token: 0x0600056F RID: 1391
		void CancelAppointment(int AppointmentId, AppCancelInfo CancelInfo, DbTransaction transaction = null);

		// Token: 0x06000570 RID: 1392
		void UnCancelAppointment(int AppointmentId, DbTransaction transaction = null);

		// Token: 0x06000571 RID: 1393
		void MarkAppointmentTentative(int AppointmentId, DbTransaction transaction = null);

		// Token: 0x06000572 RID: 1394
		void UnMarkAppointmentTentative(int AppointmentId, DbTransaction transaction = null);

		// Token: 0x06000573 RID: 1395
		void UpdateAttendeeNoShow(int AppointmentId, int PersonId, bool newNoShow, DbTransaction transaction = null);

		// Token: 0x06000574 RID: 1396
		int RecoverDeletedAppointment(int AppointmentId, DbTransaction transaction = null);

		// Token: 0x06000575 RID: 1397
		void MergeAllAppointments(int PersonIdNew, int PersonIdOld);

		// Token: 0x06000576 RID: 1398
		IList<Appointment> LoadAllAppointmentsInADay(DateTime DayToLoadAppointmentsFor, bool ShowCancelled = false, int NumDaysToLoadAppointmentsFor = 1, int[] AppTypeIds = null);

		// Token: 0x06000577 RID: 1399
		int GetNumberOfNonCancelledAppointments(int PersonId, DateTime StartDate, DateTime? EndDate, bool excludeTestsExams, params int[] AppTypeIdsToCheck);

		// Token: 0x06000578 RID: 1400
		int GetNumberOfConsecutiveNoshows(int PersonId, DateTime StartDate, int MaxNumberOfNoShowsToCheckFor, params int[] AppTypeIdsToCheck);

		// Token: 0x06000579 RID: 1401
		int CreateAppointmentEnsureUsersNotDoubleBooked(Appointment app, int[] PidsToEnsureNotDoubleBooked, DbTransaction transaction = null);

		// Token: 0x0600057A RID: 1402
		int GetNumberOfAppointmentsWithAppType(int appTypeId);

		// Token: 0x0600057B RID: 1403
		void SwapAppointmentTypeForAllAppointments(int appTypeIdToReplace, int appTypeIdToKeep);

		// Token: 0x0600057C RID: 1404
		IDictionary<int, IList<AppointmentBasicSlot>> LoadUncancelledBookedSlots(IList<int> personIds, DateTime startDate, int numDays);
	}
}

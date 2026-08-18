using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsList;
using TechnoPro.Common.Public.Entities.AvailabilitySchedule2;
using TechnoPro.Common.Public.Entities.MailMergeEntities.DocumentForPrint;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.ICore.AppointmentsList
{
	// Token: 0x020000D1 RID: 209
	public interface IListAppointmentManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600066F RID: 1647
		void CreateListAppointment(bool runInTransaction, ListAppointment Appointment);

		// Token: 0x06000670 RID: 1648
		void CancelListAppointment(bool runInTransaction, int AppointmentId);

		// Token: 0x06000671 RID: 1649
		void UnCancelListAppointment(bool runInTransaction, int AppointmentId);

		// Token: 0x06000672 RID: 1650
		void MarkListAppointmentAsTentative(bool runInTransaction, int Appointmentid);

		// Token: 0x06000673 RID: 1651
		void UnMarkListAppointmentAsTentative(bool runInTransaction, int Appointmentid);

		// Token: 0x06000674 RID: 1652
		void DeleteListAppointment(bool runInTransaction, int AppointmentId);

		// Token: 0x06000675 RID: 1653
		void UpdateListAppointment(bool runInTransaction, ListAppointment Appointment);

		// Token: 0x06000676 RID: 1654
		void CreateAvailabilities(List<Availability2Item> Availabilities);

		// Token: 0x06000677 RID: 1655
		void DeleteAvailability(List<int> AvailabilityIds);

		// Token: 0x06000678 RID: 1656
		void UpdateAvailability(List<Availability2Item> Availabilities);

		// Token: 0x06000679 RID: 1657
		List<Availability2Item> LoadOverlappingAvailabilities(int PersonId, DateTime StartDateTime, DateTime EndDateTime);

		// Token: 0x0600067A RID: 1658
		List<Availability2Item> FreeTimeSearch(List<int> PersonIds, DateTime StartDateTime, DateTime EndDateTime);

		// Token: 0x0600067B RID: 1659
		IList<ClosedDay> LoadClosedDays(IList<int> PersonIds, DateTime StartDate, DateTime EndDate);

		// Token: 0x0600067C RID: 1660
		ClosedDay IsDayClosed(int PersonId, DateTime Date);

		// Token: 0x0600067D RID: 1661
		void CreateClosedDay(IList<ClosedDay> ClosedDays);

		// Token: 0x0600067E RID: 1662
		void DeleteClosedDay(int PersonId, DateTime Date);

		// Token: 0x0600067F RID: 1663
		IList<DocumentPrintItem> GenerateMedicalCalendarDocumentPrintItems(DateTime StartDate, int NumDays, IList<PersonBase> Staff, bool HideCancelled);

		// Token: 0x06000680 RID: 1664
		IList<Availability2Item> LoadAvailability(IList<int> PersonIds, DateTime StartDate, int NumDays);

		// Token: 0x06000681 RID: 1665
		IList<ListAppointment> LoadAppointments(IList<int> PersonIds, DateTime StartDate, int NumDays, bool LoadIsStudentsFirstAppointment);

		// Token: 0x06000682 RID: 1666
		IList<ListAppointmentOrAvailability> LoadAppointmentsWithAvailability(IList<int> PersonIds, DateTime StartDate, int NumDays, bool LoadIsStudentsFirstAppointment, bool HideCancelledAppointments);

		// Token: 0x06000683 RID: 1667
		ListAppointment LoadAppointmentById(int AppointmentId, bool LoadIsStudentsFirstAppointment);

		// Token: 0x06000684 RID: 1668
		void MarkIn(bool runInTransaction, int AppointmentId, bool newIn);

		// Token: 0x06000685 RID: 1669
		void MarkNoShow(bool runInTransaction, int AppointmentId, bool newNoShow);

		// Token: 0x06000686 RID: 1670
		void MarkConfirmed(bool runInTransaction, int AppointmentId, bool newConfirmed);

		// Token: 0x06000687 RID: 1671
		Dictionary<DateTime, eAvailabilityCode> LoadSingleDayAvailabilityStatusesByUser(int PersonId, DateTime StartDate, int NumDays);

		// Token: 0x06000688 RID: 1672
		Availability2Item LoadAvailabilityById(int Availability2ItemId);

		// Token: 0x06000689 RID: 1673
		void FixAvailabilityAppointmentMappings(DateTime StartDate, DateTime EndDate);

		// Token: 0x0600068A RID: 1674
		IList<ListAppointment> LoadAllAppointmentsInADay(DateTime DayToLoadAppointmentsFor, bool ShowCancelled = false, int NumDaysToLoadAppointmentsFor = 1);

		// Token: 0x0600068B RID: 1675
		IList<Availability2Marker> LoadAvailability2Markers();

		// Token: 0x0600068C RID: 1676
		int CreateAvailability2Marker(Availability2Marker Marker);

		// Token: 0x0600068D RID: 1677
		void DeleteAvailability2Marker(int Availability2MarkerId);

		// Token: 0x0600068E RID: 1678
		void UpdateAvailability2Marker(Availability2Marker Marker);
	}
}

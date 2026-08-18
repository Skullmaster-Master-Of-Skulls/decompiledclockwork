using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsList;
using TechnoPro.Common.Public.Entities.AvailabilitySchedule2;

namespace TechnoPro.Common.DAO.AppointmentsList
{
	// Token: 0x020000C3 RID: 195
	public interface IListAppointmentDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000545 RID: 1349
		int CreateListAppointment(ListAppointment Appointment);

		// Token: 0x06000546 RID: 1350
		void UpdateListAppointment(ListAppointment Appointment);

		// Token: 0x06000547 RID: 1351
		List<Availability2Item> FreeTimeSearch(List<int> PersonIds, DateTime StartDateTime, DateTime EndDateTime);

		// Token: 0x06000548 RID: 1352
		List<Availability2Item> LoadOverlappingAvailabilities(int PersonId, DateTime StartDateTime, DateTime EndDateTime);

		// Token: 0x06000549 RID: 1353
		IList<ClosedDay> LoadClosedDays(IList<int> PersonIds, DateTime StartDate, DateTime EndDate);

		// Token: 0x0600054A RID: 1354
		void CreateClosedDay(IList<ClosedDay> ClosedDays);

		// Token: 0x0600054B RID: 1355
		void DeleteClosedDay(int PersonId, DateTime Date);

		// Token: 0x0600054C RID: 1356
		void CreateAvailabilities(List<Availability2Item> Availabilities);

		// Token: 0x0600054D RID: 1357
		void DeleteAvailability(List<int> AvailabilityIds);

		// Token: 0x0600054E RID: 1358
		void UpdateAvailability(List<Availability2Item> Availabilities);

		// Token: 0x0600054F RID: 1359
		IList<Availability2Item> LoadAvailability(IList<int> PersonIds, DateTime StartDate, int NumDays);

		// Token: 0x06000550 RID: 1360
		IList<ListAppointment> LoadAppointments(IList<int> PersonIds, DateTime StartDate, int NumDays, bool LoadIsStudentsFirstAppointment);

		// Token: 0x06000551 RID: 1361
		IList<ListAppointment> LoadAppointments(IList<int> PersonIds, DateTime StartDate, int NumDays, bool LoadIsStudentsFirstAppointment, bool HideCancelledAppointments);

		// Token: 0x06000552 RID: 1362
		ListAppointment LoadAppointmentById(int AppointmentId, bool LoadIsStudentsFirstAppointment);

		// Token: 0x06000553 RID: 1363
		Dictionary<DateTime, eAvailabilityCode> LoadSingleDayAvailabilityStatusesByUser(int PersonId, DateTime StartDate, int NumDays);

		// Token: 0x06000554 RID: 1364
		Availability2Item LoadAvailabilityById(int Availability2ItemId);

		// Token: 0x06000555 RID: 1365
		IList<Availability2ItemWithAppointmentId> LoadUniqueAvailabilitiesForAllPeopleWithAppointmentIds(DateTime StartDate, DateTime EndDate);

		// Token: 0x06000556 RID: 1366
		void MarkAvailabilityWithAppointment(int availability2itemid, int appointmentId);

		// Token: 0x06000557 RID: 1367
		IList<ListAppointment> LoadAllAppointments(DateTime StartDate, int NumDays, bool ShowCancelled = false);

		// Token: 0x06000558 RID: 1368
		IList<Availability2Marker> LoadAvailability2Markers();

		// Token: 0x06000559 RID: 1369
		int CreateAvailability2Marker(Availability2Marker Marker);

		// Token: 0x0600055A RID: 1370
		void DeleteAvailability2Marker(int Availability2MarkerId);

		// Token: 0x0600055B RID: 1371
		void UpdateAvailability2Marker(Availability2Marker Marker);

		// Token: 0x0600055C RID: 1372
		IList<Availability2Item> LoadOverlappingAvailabilitiesWithAppointment(ListAppointment app);
	}
}

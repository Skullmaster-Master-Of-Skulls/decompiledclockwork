using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule2;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentsList;

namespace TechnoPro.Common.ClientManager.ICore.AppointmentsList
{
	// Token: 0x02000092 RID: 146
	public interface IListAppointmentClientManager : IWebService
	{
		// Token: 0x06000458 RID: 1112
		int CreateListAppointment(ListAppointmentDTO Appointment);

		// Token: 0x06000459 RID: 1113
		void CancelListAppointment(int AppointmentId);

		// Token: 0x0600045A RID: 1114
		void UnCancelListAppointment(int AppointmentId);

		// Token: 0x0600045B RID: 1115
		void MarkListAppointmentAsTentative(int Appointmentid);

		// Token: 0x0600045C RID: 1116
		void UnMarkListAppointmentAsTentative(int Appointmentid);

		// Token: 0x0600045D RID: 1117
		void DeleteListAppointment(int AppointmentId);

		// Token: 0x0600045E RID: 1118
		void UpdateListAppointment(ListAppointmentDTO Appointment);

		// Token: 0x0600045F RID: 1119
		void CreateAvailabilities(List<Availability2ItemDTO> Availabilities);

		// Token: 0x06000460 RID: 1120
		void DeleteAvailability(List<int> AvailabilityIds);

		// Token: 0x06000461 RID: 1121
		void UpdateAvailability(List<Availability2ItemDTO> Availabilities);

		// Token: 0x06000462 RID: 1122
		IList<Availability2ItemDTO> LoadOverlappingAvailabilities(int PersonId, DateTime StartDateTime, DateTime EndDateTime);

		// Token: 0x06000463 RID: 1123
		IList<Availability2ItemDTO> FreeTimeSearch(List<int> PersonIds, DateTime StartDateTime, DateTime EndDateTime);

		// Token: 0x06000464 RID: 1124
		IList<ClosedDayDTO> LoadClosedDays(IList<int> PersonIds, DateTime StartDate, DateTime EndDate);

		// Token: 0x06000465 RID: 1125
		ClosedDayDTO IsDayClosed(int PersonId, DateTime Date);

		// Token: 0x06000466 RID: 1126
		void CreateClosedDay(IList<ClosedDayDTO> ClosedDays);

		// Token: 0x06000467 RID: 1127
		void DeleteClosedDay(int PersonId, DateTime Date);

		// Token: 0x06000468 RID: 1128
		IList<Availability2ItemDTO> LoadAvailability(IList<int> PersonIds, DateTime StartDate, int NumDays);

		// Token: 0x06000469 RID: 1129
		IList<ListAppointmentDTO> LoadAppointments(IList<int> PersonIds, DateTime StartDate, int NumDays, bool LoadIsStudentsFirstAppointment);

		// Token: 0x0600046A RID: 1130
		IList<ListAppointmentOrAvailabilityDTO> LoadAppointmentsWithAvailability(IList<int> PersonIds, DateTime StartDate, int NumDays, bool LoadIsStudentsFirstAppointment, bool HideCancelledAppointments);

		// Token: 0x0600046B RID: 1131
		BinaryFileDTO PrintMedicalCalendar(DateTime StartDate, int NumDays, IList<PersonBaseDTO> Staff, eFileFormatDTO OutputFormat, bool HideCancelled);

		// Token: 0x0600046C RID: 1132
		ListAppointmentDTO LoadAppointmentById(int AppointmentId, bool LoadIsStudentsFirstAppointment = false);

		// Token: 0x0600046D RID: 1133
		void MarkIn(int AppointmentId, bool newIn);

		// Token: 0x0600046E RID: 1134
		void MarkNoShow(int AppointmentId, bool newNoShow);

		// Token: 0x0600046F RID: 1135
		void MarkConfirmed(int AppointmentId, bool newConfirmed);

		// Token: 0x06000470 RID: 1136
		Dictionary<DateTime, eAvailabilityCode> LoadSingleDayAvailabilityStatusesByUser(int PersonId, DateTime StartDate, int NumDays);

		// Token: 0x06000471 RID: 1137
		void InsertOrUpdateAppointmentMemo(int AppointmentId, string MemoText);

		// Token: 0x06000472 RID: 1138
		void FixAvailabilityAppointmentMappings(DateTime StartDate, DateTime EndDate);

		// Token: 0x06000473 RID: 1139
		IList<Availability2MarkerDTO> LoadAvailability2Markers();

		// Token: 0x06000474 RID: 1140
		int CreateAvailability2Marker(Availability2MarkerDTO Marker);

		// Token: 0x06000475 RID: 1141
		void DeleteAvailability2Marker(int Availability2MarkerId);

		// Token: 0x06000476 RID: 1142
		void UpdateAvailability2Marker(Availability2MarkerDTO Marker);
	}
}

using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Appointments
{
	// Token: 0x0200007B RID: 123
	public interface IAppointmentAttendeeClientManager : IWebService
	{
		// Token: 0x06000385 RID: 901
		IList<AttendeeDTO> LoadAttendeesByAppointmentId(int appointmentId);

		// Token: 0x06000386 RID: 902
		AttendeeDTO LoadAttendeeById(int appointmentId, int personId);

		// Token: 0x06000387 RID: 903
		AttendeeDTO LoadAttendeeById(int attendeeId);

		// Token: 0x06000388 RID: 904
		void DeleteAttendee(int appointmentId, int personId);

		// Token: 0x06000389 RID: 905
		void DeleteAttendee(int attendeeId);

		// Token: 0x0600038A RID: 906
		int InsertOrUpdateAppointmentAttendee(int appointmentId, AttendeeDTO attendee);

		// Token: 0x0600038B RID: 907
		void InsertOrUpdateAppointmentAttendees(int appointmentId, IList<AttendeeDTO> attendees);

		// Token: 0x0600038C RID: 908
		void RemoveAttendeesNotInList(int appointmentId, IList<int> personIds);

		// Token: 0x0600038D RID: 909
		void UpdateNoShowValue(int appointmentId, int personId, bool noShowValue);

		// Token: 0x0600038E RID: 910
		void UpdateNoShowValue(int attendeeId, bool noShowValue);

		// Token: 0x0600038F RID: 911
		void UpdateMiscCodeValue(int appointmentId, int personId, int misccodeValue);

		// Token: 0x06000390 RID: 912
		void UpdateMiscCodeValue(int attendeeId, int misccodeValue);

		// Token: 0x06000391 RID: 913
		void SwapAttendee(int AppointmentId, int OldPersonId, int NewPersonId);

		// Token: 0x06000392 RID: 914
		void UpdateNoShowValue(int appointmentId, IList<int> personIds, bool noShowValue);

		// Token: 0x06000393 RID: 915
		bool IsAttendeeDoubleBooked(int PersonId, DateTime StartDateTime, DateTime EndDateTime, int AppointmentIdToSkip);

		// Token: 0x06000394 RID: 916
		IList<int> GetDoubleBookedAttendees(IList<int> PersonIdsToCheck, DateTime StartDateTime, DateTime EndDateTime, int AppointmentIdToSkip);

		// Token: 0x06000395 RID: 917
		IList<int> TryToRemoveAttendees(int appointmentId, params int[] personIds);

		// Token: 0x06000396 RID: 918
		IList<int> TryToRemoveAttendees(IList<int> attendeeIds);

		// Token: 0x06000397 RID: 919
		Dictionary<int, List<AttendeeDTO>> LoadAttendeesByAppointmentIds(IList<int> appointmentIds);
	}
}

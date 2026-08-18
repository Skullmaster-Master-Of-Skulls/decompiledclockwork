using System;
using System.Collections.Generic;
using System.Data.Common;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.DAO.Appointments
{
	// Token: 0x020000A3 RID: 163
	public interface IAppointmentAttendeeDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000433 RID: 1075
		IList<Attendee> LoadAttendeesByAppointmentId(int appointmentId);

		// Token: 0x06000434 RID: 1076
		Attendee LoadAttendeeById(int appointmentId, int personId);

		// Token: 0x06000435 RID: 1077
		Attendee LoadAttendeeById(int attendeeId);

		// Token: 0x06000436 RID: 1078
		void InsertOrUpdateAppointmentAttendees(int appointmentId, IList<Attendee> attendees, DbTransaction transaction = null);

		// Token: 0x06000437 RID: 1079
		int InsertOrUpdateAppointmentAttendee(int appointmentId, Attendee attendee, DbTransaction transaction = null);

		// Token: 0x06000438 RID: 1080
		void DeleteAttendee(int appointmentId, int personId, DbTransaction transaction = null);

		// Token: 0x06000439 RID: 1081
		int DeleteAttendee(int attendeeId, DbTransaction transaction = null);

		// Token: 0x0600043A RID: 1082
		void RemoveAttendeesNotInList(int appointmentId, IList<int> personIds, DbTransaction transaction = null);

		// Token: 0x0600043B RID: 1083
		void UpdateNoShowValue(int appointmentId, int personId, bool noShowValue, DbTransaction transaction = null);

		// Token: 0x0600043C RID: 1084
		int UpdateNoShowValue(int attendeeId, bool noShowValue, DbTransaction transaction = null);

		// Token: 0x0600043D RID: 1085
		void UpdateMiscCodeValue(int appointmentId, int personId, int misccodeValue, DbTransaction transaction = null);

		// Token: 0x0600043E RID: 1086
		int UpdateMiscCodeValue(int attendeeId, int misccodeValue, DbTransaction transaction = null);

		// Token: 0x0600043F RID: 1087
		void SwapAttendee(int AppointmentId, int OldPersonId, int NewPersonId, DbTransaction transaction = null);

		// Token: 0x06000440 RID: 1088
		IList<AttendeeWithAppointmentId> LoadAttendeesWhoHaveNoShowedInThePast(DateTime minimumDateToCheckFrom, int SkipAppointmentsWithThisIconId = -1, int[] AppTypeIds = null);

		// Token: 0x06000441 RID: 1089
		IList<int> GetDoubleBookedAttendees(IList<int> PersonIdsToCheck, DateTime StartDateTime, DateTime EndDateTime, int AppointmentIdToSkip);

		// Token: 0x06000442 RID: 1090
		int LoadAppointmentIdByAttendee(int AttendeeId);

		// Token: 0x06000443 RID: 1091
		bool CheckIfDoubleBooked(int PersonId, DateTime StartDateTime, DateTime EndDateTime, params int[] AppTypeIds);

		// Token: 0x06000444 RID: 1092
		IList<int> TryToRemoveAttendees(int appointmentId, params int[] attendeeIds);

		// Token: 0x06000445 RID: 1093
		IList<int> TryToRemoveAttendees(IList<int> attendeeId);

		// Token: 0x06000446 RID: 1094
		IDictionary<int, IList<Attendee>> LoadAttendeesByAppointmentIds(IList<int> appointmentIds);
	}
}

using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.ICore.Appointments
{
	// Token: 0x020000DF RID: 223
	public interface IAppointmentAttendeeManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060006E3 RID: 1763
		IList<Attendee> LoadAttendeesByAppointmentId(int appointmentId);

		// Token: 0x060006E4 RID: 1764
		Attendee LoadAttendeeById(int appointmentId, int personId);

		// Token: 0x060006E5 RID: 1765
		Attendee LoadAttendeeById(int attendeeId);

		// Token: 0x060006E6 RID: 1766
		void DeleteAttendee(bool runInTransaction, int appointmentId, int personId);

		// Token: 0x060006E7 RID: 1767
		void DeleteAttendee(bool runInTransaction, int attendeeId);

		// Token: 0x060006E8 RID: 1768
		int InsertOrUpdateAppointmentAttendee(bool runInTransaction, int appointmentId, Attendee attendee);

		// Token: 0x060006E9 RID: 1769
		void InsertOrUpdateAppointmentAttendees(bool runInTransaction, int appointmentId, IList<Attendee> attendees);

		// Token: 0x060006EA RID: 1770
		void RemoveAttendeesNotInList(bool runInTransaction, int appointmentId, IList<int> personIds);

		// Token: 0x060006EB RID: 1771
		void UpdateNoShowValue(bool runInTransaction, int appointmentId, int personId, bool noShowValue);

		// Token: 0x060006EC RID: 1772
		void UpdateNoShowValue(bool runInTransaction, int attendeeId, bool noShowValue);

		// Token: 0x060006ED RID: 1773
		void UpdateMiscCodeValue(bool runInTransaction, int appointmentId, int personId, int misccodeValue);

		// Token: 0x060006EE RID: 1774
		void UpdateMiscCodeValue(bool runInTransaction, int attendeeId, int misccodeValue);

		// Token: 0x060006EF RID: 1775
		void SwapAttendee(bool runInTransaction, int AppointmentId, int OldPersonId, int NewPersonId);

		// Token: 0x060006F0 RID: 1776
		IList<AttendeeWithAppointmentId> LoadAttendeesWhoHaveNoShowedInThePast(DateTime? minimumDateToCheckFrom = null, int SkipAppointmentsWithThisIconId = -1, int[] AppTypeIds = null);

		// Token: 0x060006F1 RID: 1777
		bool IsAttendeeDoubleBooked(int PersonId, DateTime StartDateTime, DateTime EndDateTime, int AppointmentIdToSkip);

		// Token: 0x060006F2 RID: 1778
		IList<int> GetDoubleBookedAttendees(IList<int> PersonIdsToCheck, DateTime StartDateTime, DateTime EndDateTime, int AppointmentIdToSkip);

		// Token: 0x060006F3 RID: 1779
		bool CheckIfDoubleBooked(int PersonId, DateTime StartDateTime, DateTime EndDateTime, params int[] AppTypeIds);

		// Token: 0x060006F4 RID: 1780
		IList<int> TryToRemoveAttendees(int appointmentId, params int[] attendeeIds);

		// Token: 0x060006F5 RID: 1781
		IList<int> TryToRemoveAttendees(IList<int> attendeeIds);

		// Token: 0x060006F6 RID: 1782
		IDictionary<int, IList<Attendee>> LoadAttendeesByAppointmentIds(IList<int> appointmentIds);
	}
}

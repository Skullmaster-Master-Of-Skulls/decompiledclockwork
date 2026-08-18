using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent.BookingRequest;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar.StudentAppointmentBooking;

namespace TechnoPro.Common.ICore.AppointmentsCalendar
{
	// Token: 0x020000EA RID: 234
	public interface IAppointmentBookingStudentManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600074F RID: 1871
		AppointmentBookingRes ValidateBookStudentAppointment(int studentPersonId, DateTime? date, TimeSpan? startTime, TimeSpan? endTime);

		// Token: 0x06000750 RID: 1872
		AppointmentBookingRes TryToBookStudentAppointment(int studentPersonId, string channelId, int availabilityGroupId, string calendarTitle, DateTime start, DateTime end);

		// Token: 0x06000751 RID: 1873
		bool IsStudentBannedFromOnlineAppointmentBooking(int PersonId);

		// Token: 0x06000752 RID: 1874
		DateTime? MarkStudentBannedFromOnlineAppointmentBooking(int PersonId);

		// Token: 0x06000753 RID: 1875
		IList<ChannelCalendarWithAvailability> LoadAvailabilityForChannel(int studentPersonId, string channelId, string optionalCalendarName, DateTime startDate, int numDays);

		// Token: 0x06000754 RID: 1876
		IList<Channel> GetActiveChannelsForStudent(int studentPersonId);
	}
}

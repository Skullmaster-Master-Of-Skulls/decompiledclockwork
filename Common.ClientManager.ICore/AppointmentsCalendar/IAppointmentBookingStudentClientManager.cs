using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent.BookingRequest;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.StudentAppointmentBooking;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.AppointmentsCalendar
{
	// Token: 0x02000093 RID: 147
	public interface IAppointmentBookingStudentClientManager : IWebService
	{
		// Token: 0x06000477 RID: 1143
		IList<ChannelCalendarWithAvailabilityDTO> LoadAvailabilityForChannel(int studentPersonId, string channelId, string optionalCalendarName, DateTime startDate, int numDays);

		// Token: 0x06000478 RID: 1144
		IList<ChannelDTO> GetActiveChannelsForStudent(int studentPersonId);

		// Token: 0x06000479 RID: 1145
		AppointmentBookingResDTO ValidateBookStudentAppointment(int studentPersonId, DateTime? date, TimeSpan? startTime, TimeSpan? endTime);

		// Token: 0x0600047A RID: 1146
		AppointmentBookingResDTO TryToBookStudentAppointment(int studentPersonId, string channelId, int availabilityGroupId, string calendarTitle, DateTime start, DateTime end);
	}
}

using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent.BookingRequest;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.StudentAppointmentBooking;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AppointmentsCalendar;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.AppointmentsCalendar
{
	// Token: 0x02000099 RID: 153
	public class AppointmentBookingStudentClientManager : IAppointmentBookingStudentClientManager, IWebService
	{
		// Token: 0x060005A0 RID: 1440 RVA: 0x00018DF4 File Offset: 0x00016FF4
		public IList<ChannelDTO> GetActiveChannelsForStudent(int studentPersonId)
		{
			GetActiveChannelsForStudentReq getActiveChannelsForStudentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetActiveChannelsForStudentReq>();
			getActiveChannelsForStudentReq.StudentPersonId = studentPersonId;
			GetActiveChannelsForStudentResp activeChannelsForStudent = ClientServiceFactory.GetClientInstance<IAppointmentBookingStudent>().GetActiveChannelsForStudent(getActiveChannelsForStudentReq);
			return (activeChannelsForStudent != null) ? activeChannelsForStudent.ActiveChannels : null;
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00018E30 File Offset: 0x00017030
		public IList<ChannelCalendarWithAvailabilityDTO> LoadAvailabilityForChannel(int studentPersonId, string channelId, string optionalCalendarName, DateTime startDate, int numDays)
		{
			LoadAvailabilityForChannelReq loadAvailabilityForChannelReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAvailabilityForChannelReq>();
			loadAvailabilityForChannelReq.StudentPersonId = studentPersonId;
			loadAvailabilityForChannelReq.ChannelId = channelId;
			loadAvailabilityForChannelReq.OptionalCalendarName = optionalCalendarName;
			loadAvailabilityForChannelReq.StartDate = startDate;
			loadAvailabilityForChannelReq.NumDays = numDays;
			LoadAvailabilityForChannelResp loadAvailabilityForChannelResp = ClientServiceFactory.GetClientInstance<IAppointmentBookingStudent>().LoadAvailabilityForChannel(loadAvailabilityForChannelReq);
			return (loadAvailabilityForChannelResp != null) ? loadAvailabilityForChannelResp.ChannelCalendarsWithAvailabilities : null;
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00018E90 File Offset: 0x00017090
		public AppointmentBookingResDTO TryToBookStudentAppointment(int studentPersonId, string channelId, int availabilityGroupId, string calendarTitle, DateTime start, DateTime end)
		{
			TryToBookStudentAppointmentReq tryToBookStudentAppointmentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<TryToBookStudentAppointmentReq>();
			tryToBookStudentAppointmentReq.StudentPersonId = studentPersonId;
			tryToBookStudentAppointmentReq.ChannelId = channelId;
			tryToBookStudentAppointmentReq.AvailabilityGroupId = availabilityGroupId;
			tryToBookStudentAppointmentReq.CalendarTitle = calendarTitle;
			tryToBookStudentAppointmentReq.Start = start;
			tryToBookStudentAppointmentReq.End = end;
			TryToBookStudentAppointmentResp tryToBookStudentAppointmentResp = ClientServiceFactory.GetClientInstance<IAppointmentBookingStudent>().TryToBookStudentAppointment(tryToBookStudentAppointmentReq);
			return (tryToBookStudentAppointmentResp != null) ? tryToBookStudentAppointmentResp.BookingResult : null;
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00018EF8 File Offset: 0x000170F8
		public AppointmentBookingResDTO ValidateBookStudentAppointment(int studentPersonId, DateTime? date, TimeSpan? startTime, TimeSpan? endTime)
		{
			ValidateBookStudentAppointmentReq validateBookStudentAppointmentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ValidateBookStudentAppointmentReq>();
			validateBookStudentAppointmentReq.StudentPersonId = studentPersonId;
			validateBookStudentAppointmentReq.Date = date;
			validateBookStudentAppointmentReq.StartTime = startTime;
			validateBookStudentAppointmentReq.EndTime = endTime;
			ValidateBookStudentAppointmentResp validateBookStudentAppointmentResp = ClientServiceFactory.GetClientInstance<IAppointmentBookingStudent>().ValidateBookStudentAppointment(validateBookStudentAppointmentReq);
			return (validateBookStudentAppointmentResp != null) ? validateBookStudentAppointmentResp.BookingResult : null;
		}
	}
}

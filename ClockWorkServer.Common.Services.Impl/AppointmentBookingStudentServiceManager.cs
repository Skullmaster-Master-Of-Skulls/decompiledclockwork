using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.StudentAppointmentBooking;
using TechnoPro.Common.Core.AppointmentsCalendar;
using TechnoPro.Common.Core.Mappers.AppointmentBookingStudent;
using TechnoPro.Common.Core.Mappers.AppointmentBookingStudent.BookingRequest;
using TechnoPro.Common.Core.Mappers.AppointmentsCalendar.StudentAppointmentBooking;
using TechnoPro.Common.ICore.AppointmentsCalendar;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent.BookingRequest;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar.StudentAppointmentBooking;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200000D RID: 13
	public class AppointmentBookingStudentServiceManager : IAppointmentBookingStudent, IService
	{
		// Token: 0x0600009F RID: 159 RVA: 0x00004440 File Offset: 0x00002640
		public GetActiveChannelsForStudentResp GetActiveChannelsForStudent(GetActiveChannelsForStudentReq Request)
		{
			IAppointmentBookingStudentManager appointmentBookingStudentManager = new AppointmentBookingStudentManager(Request.GetOperationContext());
			GetActiveChannelsForStudentResp getActiveChannelsForStudentResp = new GetActiveChannelsForStudentResp();
			IList<Channel> activeChannelsForStudent = appointmentBookingStudentManager.GetActiveChannelsForStudent(Request.StudentPersonId);
			IList<ChannelDTO> activeChannels;
			if (activeChannelsForStudent == null)
			{
				activeChannels = null;
			}
			else
			{
				activeChannels = (from g in activeChannelsForStudent
				select g.ToDTO()).ToList<ChannelDTO>();
			}
			getActiveChannelsForStudentResp.ActiveChannels = activeChannels;
			return getActiveChannelsForStudentResp;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000044A8 File Offset: 0x000026A8
		public IsStudentBannedFromOnlineAppointmentBookingResp IsStudentBannedFromOnlineAppointmentBooking(IsStudentBannedFromOnlineAppointmentBookingReq Request)
		{
			IAppointmentBookingStudentManager appointmentBookingStudentManager = new AppointmentBookingStudentManager(Request.GetOperationContext());
			return new IsStudentBannedFromOnlineAppointmentBookingResp
			{
				StudentIsBanned = appointmentBookingStudentManager.IsStudentBannedFromOnlineAppointmentBooking(Request.PersonId)
			};
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000044E0 File Offset: 0x000026E0
		public LoadAvailabilityForChannelResp LoadAvailabilityForChannel(LoadAvailabilityForChannelReq Request)
		{
			IAppointmentBookingStudentManager appointmentBookingStudentManager = new AppointmentBookingStudentManager(Request.GetOperationContext());
			LoadAvailabilityForChannelResp loadAvailabilityForChannelResp = new LoadAvailabilityForChannelResp();
			IList<ChannelCalendarWithAvailability> list = appointmentBookingStudentManager.LoadAvailabilityForChannel(Request.StudentPersonId, Request.ChannelId, Request.OptionalCalendarName, Request.StartDate, Request.NumDays);
			IList<ChannelCalendarWithAvailabilityDTO> channelCalendarsWithAvailabilities;
			if (list == null)
			{
				channelCalendarsWithAvailabilities = null;
			}
			else
			{
				channelCalendarsWithAvailabilities = (from g in list
				select g.ToDTO()).ToList<ChannelCalendarWithAvailabilityDTO>();
			}
			loadAvailabilityForChannelResp.ChannelCalendarsWithAvailabilities = channelCalendarsWithAvailabilities;
			return loadAvailabilityForChannelResp;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004560 File Offset: 0x00002760
		public MarkStudentBannedFromOnlineAppointmentBookingResp MarkStudentBannedFromOnlineAppointmentBooking(MarkStudentBannedFromOnlineAppointmentBookingReq Request)
		{
			IAppointmentBookingStudentManager appointmentBookingStudentManager = new AppointmentBookingStudentManager(Request.GetOperationContext());
			return new MarkStudentBannedFromOnlineAppointmentBookingResp
			{
				DateStudentWasBannedUntil = appointmentBookingStudentManager.MarkStudentBannedFromOnlineAppointmentBooking(Request.PersonId)
			};
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004598 File Offset: 0x00002798
		public TryToBookStudentAppointmentResp TryToBookStudentAppointment(TryToBookStudentAppointmentReq Request)
		{
			IAppointmentBookingStudentManager appointmentBookingStudentManager = new AppointmentBookingStudentManager(Request.GetOperationContext());
			TryToBookStudentAppointmentResp tryToBookStudentAppointmentResp = new TryToBookStudentAppointmentResp();
			AppointmentBookingRes appointmentBookingRes = appointmentBookingStudentManager.TryToBookStudentAppointment(Request.StudentPersonId, Request.ChannelId, Request.AvailabilityGroupId, Request.CalendarTitle, Request.Start, Request.End);
			tryToBookStudentAppointmentResp.BookingResult = ((appointmentBookingRes != null) ? appointmentBookingRes.ToDTO() : null);
			return tryToBookStudentAppointmentResp;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000045F8 File Offset: 0x000027F8
		public ValidateBookStudentAppointmentResp ValidateBookStudentAppointment(ValidateBookStudentAppointmentReq Request)
		{
			IAppointmentBookingStudentManager appointmentBookingStudentManager = new AppointmentBookingStudentManager(Request.GetOperationContext());
			ValidateBookStudentAppointmentResp validateBookStudentAppointmentResp = new ValidateBookStudentAppointmentResp();
			AppointmentBookingRes appointmentBookingRes = appointmentBookingStudentManager.ValidateBookStudentAppointment(Request.StudentPersonId, Request.Date, Request.StartTime, Request.EndTime);
			validateBookStudentAppointmentResp.BookingResult = ((appointmentBookingRes != null) ? appointmentBookingRes.ToDTO() : null);
			return validateBookStudentAppointmentResp;
		}
	}
}

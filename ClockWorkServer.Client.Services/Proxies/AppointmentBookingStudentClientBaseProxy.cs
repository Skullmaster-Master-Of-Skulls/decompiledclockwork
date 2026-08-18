using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200001B RID: 27
	internal class AppointmentBookingStudentClientBaseProxy : ClientBase<IAppointmentBookingStudent>, IAppointmentBookingStudent, IService
	{
		// Token: 0x06000168 RID: 360 RVA: 0x00005B28 File Offset: 0x00003D28
		public AppointmentBookingStudentClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00005B33 File Offset: 0x00003D33
		public AppointmentBookingStudentClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00005B40 File Offset: 0x00003D40
		public GetActiveChannelsForStudentResp GetActiveChannelsForStudent(GetActiveChannelsForStudentReq Request)
		{
			return base.Channel.GetActiveChannelsForStudent(Request);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00005B60 File Offset: 0x00003D60
		public IsStudentBannedFromOnlineAppointmentBookingResp IsStudentBannedFromOnlineAppointmentBooking(IsStudentBannedFromOnlineAppointmentBookingReq Request)
		{
			return base.Channel.IsStudentBannedFromOnlineAppointmentBooking(Request);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00005B80 File Offset: 0x00003D80
		public LoadAvailabilityForChannelResp LoadAvailabilityForChannel(LoadAvailabilityForChannelReq Request)
		{
			return base.Channel.LoadAvailabilityForChannel(Request);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00005BA0 File Offset: 0x00003DA0
		public MarkStudentBannedFromOnlineAppointmentBookingResp MarkStudentBannedFromOnlineAppointmentBooking(MarkStudentBannedFromOnlineAppointmentBookingReq Request)
		{
			return base.Channel.MarkStudentBannedFromOnlineAppointmentBooking(Request);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00005BC0 File Offset: 0x00003DC0
		public TryToBookStudentAppointmentResp TryToBookStudentAppointment(TryToBookStudentAppointmentReq Request)
		{
			return base.Channel.TryToBookStudentAppointment(Request);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00005BE0 File Offset: 0x00003DE0
		public ValidateBookStudentAppointmentResp ValidateBookStudentAppointment(ValidateBookStudentAppointmentReq Request)
		{
			return base.Channel.ValidateBookStudentAppointment(Request);
		}
	}
}

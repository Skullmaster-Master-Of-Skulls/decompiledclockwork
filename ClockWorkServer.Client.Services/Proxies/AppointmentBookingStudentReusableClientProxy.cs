using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200001A RID: 26
	public class AppointmentBookingStudentReusableClientProxy : WCFTokenBasedReusableClientProxy<IAppointmentBookingStudent>, IAppointmentBookingStudent, IService
	{
		// Token: 0x06000160 RID: 352 RVA: 0x000059C0 File Offset: 0x00003BC0
		public AppointmentBookingStudentReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000059CB File Offset: 0x00003BCB
		public AppointmentBookingStudentReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000059D8 File Offset: 0x00003BD8
		public GetActiveChannelsForStudentResp GetActiveChannelsForStudent(GetActiveChannelsForStudentReq Request)
		{
			return this.WrapServiceMethod<GetActiveChannelsForStudentResp>(() => this.Proxy.GetActiveChannelsForStudent(Request));
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00005A10 File Offset: 0x00003C10
		public IsStudentBannedFromOnlineAppointmentBookingResp IsStudentBannedFromOnlineAppointmentBooking(IsStudentBannedFromOnlineAppointmentBookingReq Request)
		{
			return this.WrapServiceMethod<IsStudentBannedFromOnlineAppointmentBookingResp>(() => this.Proxy.IsStudentBannedFromOnlineAppointmentBooking(Request));
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00005A48 File Offset: 0x00003C48
		public LoadAvailabilityForChannelResp LoadAvailabilityForChannel(LoadAvailabilityForChannelReq Request)
		{
			return this.WrapServiceMethod<LoadAvailabilityForChannelResp>(() => this.Proxy.LoadAvailabilityForChannel(Request));
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00005A80 File Offset: 0x00003C80
		public MarkStudentBannedFromOnlineAppointmentBookingResp MarkStudentBannedFromOnlineAppointmentBooking(MarkStudentBannedFromOnlineAppointmentBookingReq Request)
		{
			return this.WrapServiceMethod<MarkStudentBannedFromOnlineAppointmentBookingResp>(() => this.Proxy.MarkStudentBannedFromOnlineAppointmentBooking(Request));
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00005AB8 File Offset: 0x00003CB8
		public TryToBookStudentAppointmentResp TryToBookStudentAppointment(TryToBookStudentAppointmentReq Request)
		{
			return this.WrapServiceMethod<TryToBookStudentAppointmentResp>(() => this.Proxy.TryToBookStudentAppointment(Request));
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00005AF0 File Offset: 0x00003CF0
		public ValidateBookStudentAppointmentResp ValidateBookStudentAppointment(ValidateBookStudentAppointmentReq Request)
		{
			return this.WrapServiceMethod<ValidateBookStudentAppointmentResp>(() => this.Proxy.ValidateBookStudentAppointment(Request));
		}
	}
}

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000047 RID: 71
	internal class AppointmentCancelReasonClientBaseProxy : ClientBase<IAppointmentCancelReason>, IAppointmentCancelReason, IService
	{
		// Token: 0x06000384 RID: 900 RVA: 0x0000A965 File Offset: 0x00008B65
		public AppointmentCancelReasonClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000A970 File Offset: 0x00008B70
		public AppointmentCancelReasonClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000A97C File Offset: 0x00008B7C
		public CreateCancelReasonResp CreateCancelReason(CreateCancelReasonReq Request)
		{
			return base.Channel.CreateCancelReason(Request);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000A99A File Offset: 0x00008B9A
		public void DeleteCancelReason(DeleteCancelReasonReq Request)
		{
			base.Channel.DeleteCancelReason(Request);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000A9AC File Offset: 0x00008BAC
		public LoadAllCancelReasonsResp LoadAllCancelReasons(LoadAllCancelReasonsReq Request)
		{
			return base.Channel.LoadAllCancelReasons(Request);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000A9CC File Offset: 0x00008BCC
		public LoadCancelReasonByIdResp LoadCancelReasonById(LoadCancelReasonByIdReq Request)
		{
			return base.Channel.LoadCancelReasonById(Request);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000A9EC File Offset: 0x00008BEC
		public LoadCancelReasonsResp LoadCancelReasons(LoadCancelReasonsReq Request)
		{
			return base.Channel.LoadCancelReasons(Request);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000AA0A File Offset: 0x00008C0A
		public void UpdateCancelReason(UpdateCancelReasonReq Request)
		{
			base.Channel.UpdateCancelReason(Request);
		}
	}
}

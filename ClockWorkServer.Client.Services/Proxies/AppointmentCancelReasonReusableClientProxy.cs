using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000046 RID: 70
	public class AppointmentCancelReasonReusableClientProxy : WCFTokenBasedReusableClientProxy<IAppointmentCancelReason>, IAppointmentCancelReason, IService
	{
		// Token: 0x0600037C RID: 892 RVA: 0x0000A7FE File Offset: 0x000089FE
		public AppointmentCancelReasonReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000A809 File Offset: 0x00008A09
		public AppointmentCancelReasonReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000A818 File Offset: 0x00008A18
		public CreateCancelReasonResp CreateCancelReason(CreateCancelReasonReq Request)
		{
			return this.WrapServiceMethod<CreateCancelReasonResp>(() => this.Proxy.CreateCancelReason(Request));
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000A850 File Offset: 0x00008A50
		public void DeleteCancelReason(DeleteCancelReasonReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteCancelReason(Request);
			});
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000A888 File Offset: 0x00008A88
		public LoadAllCancelReasonsResp LoadAllCancelReasons(LoadAllCancelReasonsReq Request)
		{
			return this.WrapServiceMethod<LoadAllCancelReasonsResp>(() => this.Proxy.LoadAllCancelReasons(Request));
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000A8C0 File Offset: 0x00008AC0
		public LoadCancelReasonByIdResp LoadCancelReasonById(LoadCancelReasonByIdReq Request)
		{
			return this.WrapServiceMethod<LoadCancelReasonByIdResp>(() => this.Proxy.LoadCancelReasonById(Request));
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000A8F8 File Offset: 0x00008AF8
		public LoadCancelReasonsResp LoadCancelReasons(LoadCancelReasonsReq Request)
		{
			return this.WrapServiceMethod<LoadCancelReasonsResp>(() => this.Proxy.LoadCancelReasons(Request));
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000A930 File Offset: 0x00008B30
		public void UpdateCancelReason(UpdateCancelReasonReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateCancelReason(Request);
			});
		}
	}
}

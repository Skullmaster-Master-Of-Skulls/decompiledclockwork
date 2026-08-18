using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200016C RID: 364
	public class AppointmentCancelInfoReusableClientProxy : WCFTokenBasedReusableClientProxy<IAppointmentCancelInfo>, IAppointmentCancelInfo, IService
	{
		// Token: 0x06000E32 RID: 3634 RVA: 0x00024DE9 File Offset: 0x00022FE9
		public AppointmentCancelInfoReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x00024DF4 File Offset: 0x00022FF4
		public AppointmentCancelInfoReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x00024E00 File Offset: 0x00023000
		public void DeleteCancelInfo(DeleteCancelInfoReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteCancelInfo(Request);
			});
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x00024E38 File Offset: 0x00023038
		public void InsertOrUpdateAppointmentCancelInfo(InsertOrUpdateAppointmentCancelInfoReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.InsertOrUpdateAppointmentCancelInfo(Request);
			});
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x00024E70 File Offset: 0x00023070
		public LoadCancelInfoByAppointmentIdResp LoadCancelInfoByAppointmentId(LoadCancelInfoByAppointmentIdReq Request)
		{
			return this.WrapServiceMethod<LoadCancelInfoByAppointmentIdResp>(() => this.Proxy.LoadCancelInfoByAppointmentId(Request));
		}
	}
}

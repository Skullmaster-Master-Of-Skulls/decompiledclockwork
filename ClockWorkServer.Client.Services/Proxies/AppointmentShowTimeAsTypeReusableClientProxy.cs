using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000048 RID: 72
	public class AppointmentShowTimeAsTypeReusableClientProxy : WCFTokenBasedReusableClientProxy<IAppointmentShowTimeAsType>, IAppointmentShowTimeAsType, IService
	{
		// Token: 0x0600038C RID: 908 RVA: 0x0000AA1A File Offset: 0x00008C1A
		public AppointmentShowTimeAsTypeReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000AA25 File Offset: 0x00008C25
		public AppointmentShowTimeAsTypeReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000AA34 File Offset: 0x00008C34
		public CreateShowTimeAsTypeResp CreateShowTimeAsType(CreateShowTimeAsTypeReq Request)
		{
			return this.WrapServiceMethod<CreateShowTimeAsTypeResp>(() => this.Proxy.CreateShowTimeAsType(Request));
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000AA6C File Offset: 0x00008C6C
		public void DeleteShowTimeAsTypeByAppCode(DeleteShowTimeAsTypeByAppCodeReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteShowTimeAsTypeByAppCode(Request);
			});
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000AAA4 File Offset: 0x00008CA4
		public void DeleteShowTimeAsTypeById(DeleteShowTimeAsTypeByIdReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteShowTimeAsTypeById(Request);
			});
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000AADC File Offset: 0x00008CDC
		public LoadAllShowTimeAsTypesResp LoadAllShowTimeAsTypes(LoadAllShowTimeAsTypesReq Request)
		{
			return this.WrapServiceMethod<LoadAllShowTimeAsTypesResp>(() => this.Proxy.LoadAllShowTimeAsTypes(Request));
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000AB14 File Offset: 0x00008D14
		public LoadShowTimeAsTypeByIdResp LoadShowTimeAsTypeById(LoadShowTimeAsTypeByIdReq Request)
		{
			return this.WrapServiceMethod<LoadShowTimeAsTypeByIdResp>(() => this.Proxy.LoadShowTimeAsTypeById(Request));
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000AB4C File Offset: 0x00008D4C
		public LoadShowTimeAsTypeByAppCodeResp LoadShowTimeAsTypeByAppCode(LoadShowTimeAsTypeByAppCodeReq Request)
		{
			return this.WrapServiceMethod<LoadShowTimeAsTypeByAppCodeResp>(() => this.Proxy.LoadShowTimeAsTypeByAppCode(Request));
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000AB84 File Offset: 0x00008D84
		public void UpdateShowTimeAsType(UpdateShowTimeAsTypeReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateShowTimeAsType(Request);
			});
		}
	}
}

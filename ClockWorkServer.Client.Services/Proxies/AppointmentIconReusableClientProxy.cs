using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200016E RID: 366
	public class AppointmentIconReusableClientProxy : WCFTokenBasedReusableClientProxy<IAppointmentIcon>, IAppointmentIcon, IService
	{
		// Token: 0x06000E3C RID: 3644 RVA: 0x00024EFE File Offset: 0x000230FE
		public AppointmentIconReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x00024F09 File Offset: 0x00023109
		public AppointmentIconReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x00024F18 File Offset: 0x00023118
		public void DeleteAppointmentIconsNotInList(DeleteAppointmentIconsNotInListReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteAppointmentIconsNotInList(Request);
			});
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x00024F50 File Offset: 0x00023150
		public InsertOrUpdateAppointmentIconResp InsertOrUpdateAppointmentIcon(InsertOrUpdateAppointmentIconReq Request)
		{
			return this.WrapServiceMethod<InsertOrUpdateAppointmentIconResp>(() => this.Proxy.InsertOrUpdateAppointmentIcon(Request));
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x00024F88 File Offset: 0x00023188
		public LoadAppointmentIconResp LoadAppointmentIcon(LoadAppointmentIconReq Request)
		{
			return this.WrapServiceMethod<LoadAppointmentIconResp>(() => this.Proxy.LoadAppointmentIcon(Request));
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x00024FC0 File Offset: 0x000231C0
		public LoadAppointmentIconByIconInfoIdResp LoadAppointmentIconByIconInfoId(LoadAppointmentIconByIconInfoIdReq Request)
		{
			return this.WrapServiceMethod<LoadAppointmentIconByIconInfoIdResp>(() => this.Proxy.LoadAppointmentIconByIconInfoId(Request));
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x00024FF8 File Offset: 0x000231F8
		public LoadAppointmentIconsByAppointmentResp LoadAppointmentIconsByAppointment(LoadAppointmentIconsByAppointmentReq Request)
		{
			return this.WrapServiceMethod<LoadAppointmentIconsByAppointmentResp>(() => this.Proxy.LoadAppointmentIconsByAppointment(Request));
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x00025030 File Offset: 0x00023230
		public void DeleteAppointmentIcon(DeleteAppointmentIconReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteAppointmentIcon(Request);
			});
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x00025068 File Offset: 0x00023268
		public LoadAllIconInfosResp LoadAllIconInfos(LoadAllIconInfosReq Request)
		{
			return this.WrapServiceMethod<LoadAllIconInfosResp>(() => this.Proxy.LoadAllIconInfos(Request));
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x000250A0 File Offset: 0x000232A0
		public LoadAppointmentIconByIconNumResp LoadAppointmentIconByIconNum(LoadAppointmentIconByIconNumReq Request)
		{
			return this.WrapServiceMethod<LoadAppointmentIconByIconNumResp>(() => this.Proxy.LoadAppointmentIconByIconNum(Request));
		}
	}
}

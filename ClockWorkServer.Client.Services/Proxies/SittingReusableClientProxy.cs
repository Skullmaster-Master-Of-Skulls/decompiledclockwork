using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000030 RID: 48
	public class SittingReusableClientProxy : WCFTokenBasedReusableClientProxy<ISitting>, ISitting, IService
	{
		// Token: 0x06000288 RID: 648 RVA: 0x00008546 File Offset: 0x00006746
		public SittingReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00008551 File Offset: 0x00006751
		public SittingReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00008560 File Offset: 0x00006760
		public CreateSittingResp CreateSitting(CreateSittingReq Request)
		{
			return this.WrapServiceMethod<CreateSittingResp>(() => this.Proxy.CreateSitting(Request));
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00008598 File Offset: 0x00006798
		public GetSittingEffectiveTimeRangeResp GetSittingEffectiveTimeRange(GetSittingEffectiveTimeRangeReq request)
		{
			return this.WrapServiceMethod<GetSittingEffectiveTimeRangeResp>(() => this.Proxy.GetSittingEffectiveTimeRange(request));
		}

		// Token: 0x0600028C RID: 652 RVA: 0x000085D0 File Offset: 0x000067D0
		public LoadSittingByIdResp LoadSittingById(LoadSittingByIdReq request)
		{
			return this.WrapServiceMethod<LoadSittingByIdResp>(() => this.Proxy.LoadSittingById(request));
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00008608 File Offset: 0x00006808
		public LoadSittingTestsResp LoadSittingTests(LoadSittingTestsReq request)
		{
			return this.WrapServiceMethod<LoadSittingTestsResp>(() => this.Proxy.LoadSittingTests(request));
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00008640 File Offset: 0x00006840
		public LoadSittingsResp LoadSittings(LoadSittingsReq request)
		{
			return this.WrapServiceMethod<LoadSittingsResp>(() => this.Proxy.LoadSittings(request));
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00008678 File Offset: 0x00006878
		public void UpdateSitting(UpdateSittingReq request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateSitting(request);
			});
		}

		// Token: 0x06000290 RID: 656 RVA: 0x000086B0 File Offset: 0x000068B0
		public LoadSittingsByDateRangeResp LoadSittingsByDateRange(LoadSittingsByDateRangeReq Request)
		{
			return this.WrapServiceMethod<LoadSittingsByDateRangeResp>(() => this.Proxy.LoadSittingsByDateRange(Request));
		}

		// Token: 0x06000291 RID: 657 RVA: 0x000086E8 File Offset: 0x000068E8
		public void ClearSittingOnAppointment(ClearSittingOnAppointmentReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.ClearSittingOnAppointment(Request);
			});
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00008720 File Offset: 0x00006920
		public void SetSittingOnAppointment(SetSittingOnAppointmentReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.SetSittingOnAppointment(Request);
			});
		}
	}
}

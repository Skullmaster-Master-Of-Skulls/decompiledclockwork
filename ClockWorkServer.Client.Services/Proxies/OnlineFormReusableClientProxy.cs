using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000FB RID: 251
	public class OnlineFormReusableClientProxy : WCFTokenBasedReusableClientProxy<IOnlineForm>, IOnlineForm, IService
	{
		// Token: 0x060009C1 RID: 2497 RVA: 0x00018F32 File Offset: 0x00017132
		public OnlineFormReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00018F3D File Offset: 0x0001713D
		public OnlineFormReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00018F4C File Offset: 0x0001714C
		public GetAllOnlineFormsResp GetAllOnlineForms(GetAllOnlineFormsReq Request)
		{
			return this.WrapServiceMethod<GetAllOnlineFormsResp>(() => this.Proxy.GetAllOnlineForms(Request));
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x00018F84 File Offset: 0x00017184
		public GetOnlineFormResp GetOnlineForm(GetOnlineFormReq Request)
		{
			return this.WrapServiceMethod<GetOnlineFormResp>(() => this.Proxy.GetOnlineForm(Request));
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x00018FBC File Offset: 0x000171BC
		public void DeleteOnlineForm(DeleteOnlineFormReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteOnlineForm(Request);
			});
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00018FF4 File Offset: 0x000171F4
		public void UpdateOnlineForm(UpdateOnlineFormReq request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateOnlineForm(request);
			});
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0001902C File Offset: 0x0001722C
		public CreateNewOnlineFormResp CreateNewOnlineForm(CreateNewOnlineFormReq request)
		{
			return this.WrapServiceMethod<CreateNewOnlineFormResp>(() => this.Proxy.CreateNewOnlineForm(request));
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x00019064 File Offset: 0x00017264
		public void DisableOnlineForm(DisableOnlineFormReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DisableOnlineForm(Request);
			});
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0001909C File Offset: 0x0001729C
		public void EnableOnlineForm(EnableOnlineFormReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.EnableOnlineForm(Request);
			});
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x000190D4 File Offset: 0x000172D4
		public GetActiveOnlineFormsResp GetActiveOnlineForms(GetActiveOnlineFormsReq request)
		{
			return this.WrapServiceMethod<GetActiveOnlineFormsResp>(() => this.Proxy.GetActiveOnlineForms(request));
		}
	}
}

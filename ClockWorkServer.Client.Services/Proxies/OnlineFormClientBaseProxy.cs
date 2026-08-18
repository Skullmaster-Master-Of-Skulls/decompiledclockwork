using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000FC RID: 252
	internal class OnlineFormClientBaseProxy : ClientBase<IOnlineForm>, IOnlineForm, IService
	{
		// Token: 0x060009CB RID: 2507 RVA: 0x0001910C File Offset: 0x0001730C
		public OnlineFormClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00019117 File Offset: 0x00017317
		public OnlineFormClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x00019124 File Offset: 0x00017324
		public GetAllOnlineFormsResp GetAllOnlineForms(GetAllOnlineFormsReq Request)
		{
			return base.Channel.GetAllOnlineForms(Request);
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00019144 File Offset: 0x00017344
		public GetOnlineFormResp GetOnlineForm(GetOnlineFormReq Request)
		{
			return base.Channel.GetOnlineForm(Request);
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00019162 File Offset: 0x00017362
		public void DeleteOnlineForm(DeleteOnlineFormReq Request)
		{
			base.Channel.DeleteOnlineForm(Request);
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00019172 File Offset: 0x00017372
		public void UpdateOnlineForm(UpdateOnlineFormReq request)
		{
			base.Channel.UpdateOnlineForm(request);
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00019184 File Offset: 0x00017384
		public CreateNewOnlineFormResp CreateNewOnlineForm(CreateNewOnlineFormReq request)
		{
			return base.Channel.CreateNewOnlineForm(request);
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x000191A2 File Offset: 0x000173A2
		public void DisableOnlineForm(DisableOnlineFormReq Request)
		{
			base.Channel.DisableOnlineForm(Request);
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x000191B2 File Offset: 0x000173B2
		public void EnableOnlineForm(EnableOnlineFormReq Request)
		{
			base.Channel.EnableOnlineForm(Request);
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x000191C4 File Offset: 0x000173C4
		public GetActiveOnlineFormsResp GetActiveOnlineForms(GetActiveOnlineFormsReq request)
		{
			return base.Channel.GetActiveOnlineForms(request);
		}
	}
}

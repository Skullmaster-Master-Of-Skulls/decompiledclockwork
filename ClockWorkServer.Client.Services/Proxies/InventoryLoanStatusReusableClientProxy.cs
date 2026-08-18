using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000B7 RID: 183
	public class InventoryLoanStatusReusableClientProxy : WCFTokenBasedReusableClientProxy<IInventoryLoanStatus>, IInventoryLoanStatus, IService
	{
		// Token: 0x06000749 RID: 1865 RVA: 0x00013612 File Offset: 0x00011812
		public InventoryLoanStatusReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0001361D File Offset: 0x0001181D
		public InventoryLoanStatusReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0001362C File Offset: 0x0001182C
		public CreateLoanStatusResp CreateLoanStatus(CreateLoanStatusReq request)
		{
			return this.WrapServiceMethod<CreateLoanStatusResp>(() => this.Proxy.CreateLoanStatus(request));
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x00013664 File Offset: 0x00011864
		public GetLoanStatusByIdResp GetLoanStatusById(GetLoanStatusByIdReq request)
		{
			return this.WrapServiceMethod<GetLoanStatusByIdResp>(() => this.Proxy.GetLoanStatusById(request));
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0001369C File Offset: 0x0001189C
		public GetLoanStatusListResp GetLoanStatusList(GetLoanStatusListReq request)
		{
			return this.WrapServiceMethod<GetLoanStatusListResp>(() => this.Proxy.GetLoanStatusList(request));
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x000136D4 File Offset: 0x000118D4
		public UpdateLoanStatusResp UpdateLoanStatus(UpdateLoanStatusReq request)
		{
			return this.WrapServiceMethod<UpdateLoanStatusResp>(() => this.Proxy.UpdateLoanStatus(request));
		}
	}
}

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000BD RID: 189
	public class InventoryProductStatusReusableClientProxy : WCFTokenBasedReusableClientProxy<IInventoryProductStatus>, IInventoryProductStatus, IService
	{
		// Token: 0x06000797 RID: 1943 RVA: 0x000141FA File Offset: 0x000123FA
		public InventoryProductStatusReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00014205 File Offset: 0x00012405
		public InventoryProductStatusReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00014214 File Offset: 0x00012414
		public CreateProductStatusResp CreateProductStatus(CreateProductStatusReq request)
		{
			return this.WrapServiceMethod<CreateProductStatusResp>(() => this.Proxy.CreateProductStatus(request));
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0001424C File Offset: 0x0001244C
		public UpdateProductStatusResp UpdateProductStatus(UpdateProductStatusReq request)
		{
			return this.WrapServiceMethod<UpdateProductStatusResp>(() => this.Proxy.UpdateProductStatus(request));
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00014284 File Offset: 0x00012484
		public GetProductStatusByIdResp GetProductStatusById(GetProductStatusByIdReq request)
		{
			return this.WrapServiceMethod<GetProductStatusByIdResp>(() => this.Proxy.GetProductStatusById(request));
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x000142BC File Offset: 0x000124BC
		public GetProductStatusListResp GetProductStatusList(GetProductStatusListReq request)
		{
			return this.WrapServiceMethod<GetProductStatusListResp>(() => this.Proxy.GetProductStatusList(request));
		}
	}
}

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000BE RID: 190
	internal class InventoryProductStatusClientBaseProxy : ClientBase<IInventoryProductStatus>, IInventoryProductStatus, IService
	{
		// Token: 0x0600079D RID: 1949 RVA: 0x000142F4 File Offset: 0x000124F4
		public InventoryProductStatusClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x000142FF File Offset: 0x000124FF
		public InventoryProductStatusClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0001430C File Offset: 0x0001250C
		public CreateProductStatusResp CreateProductStatus(CreateProductStatusReq request)
		{
			return base.Channel.CreateProductStatus(request);
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0001432C File Offset: 0x0001252C
		public UpdateProductStatusResp UpdateProductStatus(UpdateProductStatusReq request)
		{
			return base.Channel.UpdateProductStatus(request);
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0001434C File Offset: 0x0001254C
		public GetProductStatusByIdResp GetProductStatusById(GetProductStatusByIdReq request)
		{
			return base.Channel.GetProductStatusById(request);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0001436C File Offset: 0x0001256C
		public GetProductStatusListResp GetProductStatusList(GetProductStatusListReq request)
		{
			return base.Channel.GetProductStatusList(request);
		}
	}
}

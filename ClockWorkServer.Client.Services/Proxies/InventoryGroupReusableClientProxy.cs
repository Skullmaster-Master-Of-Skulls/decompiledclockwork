using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000B3 RID: 179
	public class InventoryGroupReusableClientProxy : WCFTokenBasedReusableClientProxy<IInventoryGroup>, IInventoryGroup, IService
	{
		// Token: 0x06000711 RID: 1809 RVA: 0x00012D72 File Offset: 0x00010F72
		public InventoryGroupReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00012D7D File Offset: 0x00010F7D
		public InventoryGroupReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00012D8C File Offset: 0x00010F8C
		public CreateProductGroupResp CreateProductGroup(CreateProductGroupReq request)
		{
			return this.WrapServiceMethod<CreateProductGroupResp>(() => this.Proxy.CreateProductGroup(request));
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00012DC4 File Offset: 0x00010FC4
		public UpdateProductGroupResp UpdateProductGroup(UpdateProductGroupReq request)
		{
			return this.WrapServiceMethod<UpdateProductGroupResp>(() => this.Proxy.UpdateProductGroup(request));
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x00012DFC File Offset: 0x00010FFC
		public DeleteEmptyProductGroupResp DeleteEmptyProductGroup(DeleteEmptyProductGroupReq request)
		{
			return this.WrapServiceMethod<DeleteEmptyProductGroupResp>(() => this.Proxy.DeleteEmptyProductGroup(request));
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00012E34 File Offset: 0x00011034
		public GetGroupByIdResp GetGroupById(GetGroupByIdReq request)
		{
			return this.WrapServiceMethod<GetGroupByIdResp>(() => this.Proxy.GetGroupById(request));
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00012E6C File Offset: 0x0001106C
		public GetGroupsResp GetGroups(GetGroupsReq request)
		{
			return this.WrapServiceMethod<GetGroupsResp>(() => this.Proxy.GetGroups(request));
		}
	}
}

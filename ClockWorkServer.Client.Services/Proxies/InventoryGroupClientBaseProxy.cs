using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000B4 RID: 180
	internal class InventoryGroupClientBaseProxy : ClientBase<IInventoryGroup>, IInventoryGroup, IService
	{
		// Token: 0x06000718 RID: 1816 RVA: 0x00012EA4 File Offset: 0x000110A4
		public InventoryGroupClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00012EAF File Offset: 0x000110AF
		public InventoryGroupClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00012EBC File Offset: 0x000110BC
		public CreateProductGroupResp CreateProductGroup(CreateProductGroupReq request)
		{
			return base.Channel.CreateProductGroup(request);
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00012EDC File Offset: 0x000110DC
		public UpdateProductGroupResp UpdateProductGroup(UpdateProductGroupReq request)
		{
			return base.Channel.UpdateProductGroup(request);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00012EFC File Offset: 0x000110FC
		public DeleteEmptyProductGroupResp DeleteEmptyProductGroup(DeleteEmptyProductGroupReq request)
		{
			return base.Channel.DeleteEmptyProductGroup(request);
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00012F1C File Offset: 0x0001111C
		public GetGroupByIdResp GetGroupById(GetGroupByIdReq request)
		{
			return base.Channel.GetGroupById(request);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00012F3C File Offset: 0x0001113C
		public GetGroupsResp GetGroups(GetGroupsReq request)
		{
			return base.Channel.GetGroups(request);
		}
	}
}

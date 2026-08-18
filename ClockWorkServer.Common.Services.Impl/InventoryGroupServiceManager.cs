using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Core.Inventory;
using TechnoPro.Common.Core.Mappers.Inventory;
using TechnoPro.Common.ICore.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000050 RID: 80
	public class InventoryGroupServiceManager : IInventoryGroup, IService
	{
		// Token: 0x060002FA RID: 762 RVA: 0x0000E804 File Offset: 0x0000CA04
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000E818 File Offset: 0x0000CA18
		public CreateProductGroupResp CreateProductGroup(CreateProductGroupReq request)
		{
			IInventoryGroupManager inventoryGroupManager = new InventoryGroupManager(request.GetOperationContext());
			return new CreateProductGroupResp
			{
				GroupId = inventoryGroupManager.CreateProductGroup(request.Group.ToDomainObject())
			};
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000E854 File Offset: 0x0000CA54
		public UpdateProductGroupResp UpdateProductGroup(UpdateProductGroupReq request)
		{
			IInventoryGroupManager inventoryGroupManager = new InventoryGroupManager(request.GetOperationContext());
			inventoryGroupManager.UpdateProductGroup(request.Group.ToDomainObject());
			return new UpdateProductGroupResp();
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000E88C File Offset: 0x0000CA8C
		public DeleteEmptyProductGroupResp DeleteEmptyProductGroup(DeleteEmptyProductGroupReq request)
		{
			IInventoryGroupManager inventoryGroupManager = new InventoryGroupManager(request.GetOperationContext());
			return new DeleteEmptyProductGroupResp
			{
				WasDeleted = inventoryGroupManager.DeleteEmptyProductGroup(request.GroupId)
			};
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000E8C4 File Offset: 0x0000CAC4
		public GetGroupByIdResp GetGroupById(GetGroupByIdReq request)
		{
			IInventoryGroupManager inventoryGroupManager = new InventoryGroupManager(request.GetOperationContext());
			return new GetGroupByIdResp
			{
				Group = inventoryGroupManager.GetGroupById(request.GroupId).ToDTO()
			};
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000E900 File Offset: 0x0000CB00
		public GetGroupsResp GetGroups(GetGroupsReq request)
		{
			IInventoryGroupManager inventoryGroupManager = new InventoryGroupManager(request.GetOperationContext());
			return new GetGroupsResp
			{
				Groups = inventoryGroupManager.GetGroups().ToDTO()
			};
		}
	}
}

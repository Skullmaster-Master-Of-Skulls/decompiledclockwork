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
	// Token: 0x02000055 RID: 85
	public class InventoryProductStatusServiceManager : IInventoryProductStatus, IService
	{
		// Token: 0x0600033D RID: 829 RVA: 0x0000F704 File Offset: 0x0000D904
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000F718 File Offset: 0x0000D918
		public CreateProductStatusResp CreateProductStatus(CreateProductStatusReq request)
		{
			IInventoryProductStatusManager inventoryProductStatusManager = new InventoryProductStatusManager(request.GetOperationContext());
			return new CreateProductStatusResp
			{
				ProductStatusId = inventoryProductStatusManager.CreateProductStatus(request.ProductStatus.ToDomainObject())
			};
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000F754 File Offset: 0x0000D954
		public UpdateProductStatusResp UpdateProductStatus(UpdateProductStatusReq request)
		{
			IInventoryProductStatusManager inventoryProductStatusManager = new InventoryProductStatusManager(request.GetOperationContext());
			inventoryProductStatusManager.UpdateProductStatus(request.ProductStatus.ToDomainObject());
			return new UpdateProductStatusResp();
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000F78C File Offset: 0x0000D98C
		public GetProductStatusByIdResp GetProductStatusById(GetProductStatusByIdReq request)
		{
			IInventoryProductStatusManager inventoryProductStatusManager = new InventoryProductStatusManager(request.GetOperationContext());
			return new GetProductStatusByIdResp
			{
				ProductStatus = inventoryProductStatusManager.GetProductStatusById(request.ProductStatusId).ToDTO()
			};
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000F7C8 File Offset: 0x0000D9C8
		public GetProductStatusListResp GetProductStatusList(GetProductStatusListReq request)
		{
			IInventoryProductStatusManager inventoryProductStatusManager = new InventoryProductStatusManager(request.GetOperationContext());
			return new GetProductStatusListResp
			{
				ProductStatusList = inventoryProductStatusManager.GetProductStatusList().ToDTO()
			};
		}
	}
}

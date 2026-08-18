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
	// Token: 0x0200004F RID: 79
	public class InventoryCategoryServiceManager : IInventoryCategory, IService
	{
		// Token: 0x060002F3 RID: 755 RVA: 0x0000E6C8 File Offset: 0x0000C8C8
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000E6DC File Offset: 0x0000C8DC
		public CreateCategoryResp CreateCategory(CreateCategoryReq request)
		{
			IInventoryCategoryManager inventoryCategoryManager = new InventoryCategoryManager(request.GetOperationContext());
			return new CreateCategoryResp
			{
				WasCreated = inventoryCategoryManager.CreateCategory(request.Category.ToDomainObject())
			};
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000E718 File Offset: 0x0000C918
		public AssignCategoryDynamicFormResp AssignCategoryDynamicForm(AssignCategoryDynamicFormReq request)
		{
			IInventoryCategoryManager inventoryCategoryManager = new InventoryCategoryManager(request.GetOperationContext());
			inventoryCategoryManager.AssignCategoryDynamicForm(request.CategoryName, request.DynamicFormId);
			return new AssignCategoryDynamicFormResp();
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000E750 File Offset: 0x0000C950
		public DeleteEmptyCategoryResp DeleteEmptyCategory(DeleteEmptyCategoryReq request)
		{
			IInventoryCategoryManager inventoryCategoryManager = new InventoryCategoryManager(request.GetOperationContext());
			return new DeleteEmptyCategoryResp
			{
				WasDeleted = inventoryCategoryManager.DeleteEmptyCategory(request.CatalogId, request.CategoryName)
			};
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000E78C File Offset: 0x0000C98C
		public GetCategoryByNameResp GetCategoryByName(GetCategoryByNameReq request)
		{
			IInventoryCategoryManager inventoryCategoryManager = new InventoryCategoryManager(request.GetOperationContext());
			return new GetCategoryByNameResp
			{
				Category = inventoryCategoryManager.GetCategoryByName(request.CategoryName).ToDTO()
			};
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000E7C8 File Offset: 0x0000C9C8
		public GetCategoriesByCatalogResp GetCategoriesByCatalog(GetCategoriesByCatalogReq request)
		{
			IInventoryCategoryManager inventoryCategoryManager = new InventoryCategoryManager(request.GetOperationContext());
			return new GetCategoriesByCatalogResp
			{
				Categories = inventoryCategoryManager.GetCategoriesByCatalog(request.CatalogId).ToDTO()
			};
		}
	}
}

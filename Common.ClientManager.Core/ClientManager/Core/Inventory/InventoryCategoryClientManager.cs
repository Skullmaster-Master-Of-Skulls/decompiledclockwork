using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Inventory
{
	// Token: 0x02000052 RID: 82
	public class InventoryCategoryClientManager : IInventoryCategoryClientManager, IWebService
	{
		// Token: 0x060002CC RID: 716 RVA: 0x0000C818 File Offset: 0x0000AA18
		public bool CreateCategory(InventoryCategoryDTO category)
		{
			CreateCategoryReq createCategoryReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateCategoryReq>();
			createCategoryReq.Category = category;
			return ClientServiceFactory.GetClientInstance<IInventoryCategory>().CreateCategory(createCategoryReq).WasCreated;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000C850 File Offset: 0x0000AA50
		public void AssignCategoryDynamicForm(string categoryName, int dynamicFormId)
		{
			AssignCategoryDynamicFormReq assignCategoryDynamicFormReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AssignCategoryDynamicFormReq>();
			assignCategoryDynamicFormReq.CategoryName = categoryName;
			assignCategoryDynamicFormReq.DynamicFormId = dynamicFormId;
			ClientServiceFactory.GetClientInstance<IInventoryCategory>().AssignCategoryDynamicForm(assignCategoryDynamicFormReq);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000C888 File Offset: 0x0000AA88
		public bool DeleteEmptyCategory(int catalogId, string categoryName)
		{
			DeleteEmptyCategoryReq deleteEmptyCategoryReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteEmptyCategoryReq>();
			deleteEmptyCategoryReq.CategoryName = categoryName;
			deleteEmptyCategoryReq.CatalogId = catalogId;
			return ClientServiceFactory.GetClientInstance<IInventoryCategory>().DeleteEmptyCategory(deleteEmptyCategoryReq).WasDeleted;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000C8C8 File Offset: 0x0000AAC8
		public InventoryCategoryDTO GetCategoryByName(string categoryName)
		{
			GetCategoryByNameReq getCategoryByNameReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetCategoryByNameReq>();
			getCategoryByNameReq.CategoryName = categoryName;
			return ClientServiceFactory.GetClientInstance<IInventoryCategory>().GetCategoryByName(getCategoryByNameReq).Category;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000C900 File Offset: 0x0000AB00
		public IList<InventoryCategoryDTO> GetCategoriesByCatalog(int catalogId)
		{
			GetCategoriesByCatalogReq getCategoriesByCatalogReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetCategoriesByCatalogReq>();
			getCategoriesByCatalogReq.CatalogId = catalogId;
			return ClientServiceFactory.GetClientInstance<IInventoryCategory>().GetCategoriesByCatalog(getCategoriesByCatalogReq).Categories;
		}
	}
}

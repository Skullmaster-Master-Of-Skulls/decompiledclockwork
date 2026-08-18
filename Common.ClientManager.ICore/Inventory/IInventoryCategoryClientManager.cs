using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Inventory
{
	// Token: 0x0200004B RID: 75
	public interface IInventoryCategoryClientManager : IWebService
	{
		// Token: 0x0600020D RID: 525
		bool CreateCategory(InventoryCategoryDTO category);

		// Token: 0x0600020E RID: 526
		void AssignCategoryDynamicForm(string categoryName, int dynamicFormId);

		// Token: 0x0600020F RID: 527
		bool DeleteEmptyCategory(int catalogId, string categoryName);

		// Token: 0x06000210 RID: 528
		InventoryCategoryDTO GetCategoryByName(string categoryName);

		// Token: 0x06000211 RID: 529
		IList<InventoryCategoryDTO> GetCategoriesByCatalog(int catalogId);
	}
}

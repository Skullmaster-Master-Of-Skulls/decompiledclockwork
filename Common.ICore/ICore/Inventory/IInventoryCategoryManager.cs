using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.ICore.Inventory
{
	// Token: 0x02000080 RID: 128
	public interface IInventoryCategoryManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600037B RID: 891
		bool CreateCategory(InventoryCategory category);

		// Token: 0x0600037C RID: 892
		void AssignCategoryDynamicForm(string categoryName, int dynamicFormId);

		// Token: 0x0600037D RID: 893
		bool DeleteEmptyCategory(int catalogId, string categoryName);

		// Token: 0x0600037E RID: 894
		InventoryCategory GetCategoryByName(string categoryName);

		// Token: 0x0600037F RID: 895
		IList<InventoryCategory> GetCategoriesByCatalog(int catalogId);
	}
}

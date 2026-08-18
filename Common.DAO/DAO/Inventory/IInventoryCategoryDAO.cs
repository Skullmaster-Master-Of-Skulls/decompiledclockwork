using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.DAO.Inventory
{
	// Token: 0x02000066 RID: 102
	public interface IInventoryCategoryDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000250 RID: 592
		bool CreateCategory(int catalogId, int dynamicFormId, params string[] categories);

		// Token: 0x06000251 RID: 593
		void AssignCategoryDynamicForm(string categoryName, int dynamicFormId);

		// Token: 0x06000252 RID: 594
		bool DeleteEmptyCategory(int catalogId, string categoryName);

		// Token: 0x06000253 RID: 595
		void DeleteRootCategory(int catalogId);

		// Token: 0x06000254 RID: 596
		InventoryCategory GetCategoryByName(string categoryName);

		// Token: 0x06000255 RID: 597
		IList<InventoryCategory> GetCategoriesByCatalog(int catalogId);
	}
}

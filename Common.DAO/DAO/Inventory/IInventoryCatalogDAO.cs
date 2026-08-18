using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.DAO.Inventory
{
	// Token: 0x02000065 RID: 101
	public interface IInventoryCatalogDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600024A RID: 586
		InventoryCatalog GetCatalogById(int catalogId);

		// Token: 0x0600024B RID: 587
		InventoryCatalog GetCatalogByName(IList<int> allowedCatalogIds, string name);

		// Token: 0x0600024C RID: 588
		IList<InventoryCatalog> GetCatalogs(IList<int> allowedCatalogIds);

		// Token: 0x0600024D RID: 589
		int CreateCatalog(InventoryCatalog catalog);

		// Token: 0x0600024E RID: 590
		void UpdateCatalog(InventoryCatalog catalog);

		// Token: 0x0600024F RID: 591
		bool DeleteEmptyCatalog(int catalogId);
	}
}

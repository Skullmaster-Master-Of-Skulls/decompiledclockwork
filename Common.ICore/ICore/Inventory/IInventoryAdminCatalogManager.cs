using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.ICore.Inventory
{
	// Token: 0x0200007E RID: 126
	public interface IInventoryAdminCatalogManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600036C RID: 876
		IList<InventoryCatalog> GetFullCatalogList();

		// Token: 0x0600036D RID: 877
		int CreateCatalog(InventoryCatalog catalog);

		// Token: 0x0600036E RID: 878
		void UpdateCatalog(InventoryCatalog catalog);

		// Token: 0x0600036F RID: 879
		bool DeleteEmptyCatalog(int catalogId);

		// Token: 0x06000370 RID: 880
		int ImportFromXML(string catalogXmlDoc, string catalogName = null, string catalogDescription = null);

		// Token: 0x06000371 RID: 881
		int ImportFromTemplate(string templatesPath, string templateName, string catalogName = null, string catalogDescription = null);
	}
}

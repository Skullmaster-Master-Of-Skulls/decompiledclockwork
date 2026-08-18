using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.ICore.Inventory
{
	// Token: 0x02000086 RID: 134
	public interface IInventoryCatalogManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060003A3 RID: 931
		InventoryCatalog GetCatalogById(int catalogId);

		// Token: 0x060003A4 RID: 932
		InventoryCatalog GetCatalogByName(string name);

		// Token: 0x060003A5 RID: 933
		IList<InventoryCatalog> GetCatalogs();

		// Token: 0x060003A6 RID: 934
		string ExportToXML(int catalogId);

		// Token: 0x060003A7 RID: 935
		InventoryCatalog GetTemplateCatalogByName(string templatesPath, string name);

		// Token: 0x060003A8 RID: 936
		IList<InventoryCatalog> GetTemplateCatalogs(string templatesPath);
	}
}

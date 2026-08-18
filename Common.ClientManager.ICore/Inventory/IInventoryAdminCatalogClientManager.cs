using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Inventory
{
	// Token: 0x02000048 RID: 72
	public interface IInventoryAdminCatalogClientManager : IWebService
	{
		// Token: 0x060001F8 RID: 504
		IList<InventoryCatalogDTO> GetFullCatalogList();

		// Token: 0x060001F9 RID: 505
		int CreateCatalog(InventoryCatalogDTO catalog);

		// Token: 0x060001FA RID: 506
		void UpdateCatalog(InventoryCatalogDTO catalog);

		// Token: 0x060001FB RID: 507
		bool DeleteEmptyCatalog(int catalogId);

		// Token: 0x060001FC RID: 508
		int ImportFromXML(string catalogXmlDoc, string catalogName = null, string catalogDescription = null);

		// Token: 0x060001FD RID: 509
		int ImportFromTemplate(string templateName, string catalogName = null, string catalogDescription = null);
	}
}

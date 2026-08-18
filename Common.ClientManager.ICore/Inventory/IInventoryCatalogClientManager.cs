using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Inventory
{
	// Token: 0x0200004A RID: 74
	public interface IInventoryCatalogClientManager : IWebService
	{
		// Token: 0x06000207 RID: 519
		InventoryCatalogDTO GetCatalogById(int catalogId);

		// Token: 0x06000208 RID: 520
		InventoryCatalogDTO GetCatalogByName(string name);

		// Token: 0x06000209 RID: 521
		IList<InventoryCatalogDTO> GetCatalogs();

		// Token: 0x0600020A RID: 522
		string ExportToXML(int catalogId);

		// Token: 0x0600020B RID: 523
		InventoryCatalogDTO GetTemplateCatalogByName(string name);

		// Token: 0x0600020C RID: 524
		IList<InventoryCatalogDTO> GetTemplateCatalogs();
	}
}

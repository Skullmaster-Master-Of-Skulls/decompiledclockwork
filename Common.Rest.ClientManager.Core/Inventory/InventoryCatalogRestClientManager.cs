using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.ClientManager.ICore.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Inventory
{
	// Token: 0x02000041 RID: 65
	public class InventoryCatalogRestClientManager : BearerTokenRestProxy<IInventoryCatalogClientManager>, IInventoryCatalogClientManager, IWebService
	{
		// Token: 0x06000251 RID: 593 RVA: 0x00007682 File Offset: 0x00005882
		public InventoryCatalogRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000768C File Offset: 0x0000588C
		public InventoryCatalogRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00007697 File Offset: 0x00005897
		public InventoryCatalogDTO GetCatalogById(int catalogId)
		{
			return base.Get<InventoryCatalogDTO>(string.Format("inventorycatalog/catalogid/{0}", catalogId), true);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x000076B0 File Offset: 0x000058B0
		public InventoryCatalogDTO GetCatalogByName(string name)
		{
			return base.Get<InventoryCatalogDTO>(string.Format("inventorycatalog/catalogname/{0}", name), true);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x000076C4 File Offset: 0x000058C4
		public IList<InventoryCatalogDTO> GetCatalogs()
		{
			return base.GetMany<InventoryCatalogDTO>("inventorycatalog", true);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x000076D2 File Offset: 0x000058D2
		public string ExportToXML(int catalogId)
		{
			return base.Get<string>(string.Format("inventorycatalog/exporttoxml/catalogid/{0}", catalogId), true);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x000076EB File Offset: 0x000058EB
		public InventoryCatalogDTO GetTemplateCatalogByName(string name)
		{
			return base.Get<InventoryCatalogDTO>(string.Format("inventorycatalog/templatename/{0}", name), true);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x000076FF File Offset: 0x000058FF
		public IList<InventoryCatalogDTO> GetTemplateCatalogs()
		{
			return base.GetMany<InventoryCatalogDTO>("inventorycatalog/templates", true);
		}
	}
}

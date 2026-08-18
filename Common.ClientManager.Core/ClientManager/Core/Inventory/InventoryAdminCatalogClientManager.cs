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
	// Token: 0x0200004F RID: 79
	public class InventoryAdminCatalogClientManager : IInventoryAdminCatalogClientManager, IWebService
	{
		// Token: 0x060002B4 RID: 692 RVA: 0x0000C360 File Offset: 0x0000A560
		public IList<InventoryCatalogDTO> GetFullCatalogList()
		{
			GetFullCatalogListReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetFullCatalogListReq>();
			return ClientServiceFactory.GetClientInstance<IInventoryAdminCatalog>().GetFullCatalogList(request).Catalogs;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000C390 File Offset: 0x0000A590
		public int CreateCatalog(InventoryCatalogDTO catalog)
		{
			CreateCatalogReq createCatalogReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateCatalogReq>();
			createCatalogReq.Catalog = catalog;
			return ClientServiceFactory.GetClientInstance<IInventoryAdminCatalog>().CreateCatalog(createCatalogReq).CatalogId;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000C3C8 File Offset: 0x0000A5C8
		public void UpdateCatalog(InventoryCatalogDTO catalog)
		{
			UpdateCatalogReq updateCatalogReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateCatalogReq>();
			updateCatalogReq.Catalog = catalog;
			ClientServiceFactory.GetClientInstance<IInventoryAdminCatalog>().UpdateCatalog(updateCatalogReq);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000C3F8 File Offset: 0x0000A5F8
		public bool DeleteEmptyCatalog(int catalogId)
		{
			DeleteEmptyCatalogReq deleteEmptyCatalogReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteEmptyCatalogReq>();
			deleteEmptyCatalogReq.CatalogId = catalogId;
			return ClientServiceFactory.GetClientInstance<IInventoryAdminCatalog>().DeleteEmptyCatalog(deleteEmptyCatalogReq).WasDeleted;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000C430 File Offset: 0x0000A630
		public int ImportFromXML(string catalogXmlDoc, string catalogName = null, string catalogDescription = null)
		{
			ImportFromXMLReq importFromXMLReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ImportFromXMLReq>();
			importFromXMLReq.CatalogXml = catalogXmlDoc;
			importFromXMLReq.CatalogName = catalogName;
			importFromXMLReq.CatalogDescription = catalogDescription;
			return ClientServiceFactory.GetClientInstance<IInventoryAdminCatalog>().ImportFromXML(importFromXMLReq).CatalogId;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000C478 File Offset: 0x0000A678
		public int ImportFromTemplate(string templateName, string catalogName = null, string catalogDescription = null)
		{
			ImportFromTemplateReq importFromTemplateReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ImportFromTemplateReq>();
			importFromTemplateReq.TemplateName = templateName;
			importFromTemplateReq.CatalogName = catalogName;
			importFromTemplateReq.CatalogDescription = catalogDescription;
			return ClientServiceFactory.GetClientInstance<IInventoryAdminCatalog>().ImportFromTemplate(importFromTemplateReq).CatalogId;
		}
	}
}

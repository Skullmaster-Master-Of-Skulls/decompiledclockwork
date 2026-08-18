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
	// Token: 0x02000051 RID: 81
	public class InventoryCatalogClientManager : IInventoryCatalogClientManager, IWebService
	{
		// Token: 0x060002C5 RID: 709 RVA: 0x0000C6D8 File Offset: 0x0000A8D8
		public InventoryCatalogDTO GetCatalogById(int catalogId)
		{
			GetCatalogByIdReq getCatalogByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetCatalogByIdReq>();
			getCatalogByIdReq.CatalogId = catalogId;
			return ClientServiceFactory.GetClientInstance<IInventoryCatalog>().GetCatalogById(getCatalogByIdReq).Catalog;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000C710 File Offset: 0x0000A910
		public InventoryCatalogDTO GetCatalogByName(string name)
		{
			GetCatalogByNameReq getCatalogByNameReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetCatalogByNameReq>();
			getCatalogByNameReq.CatalogName = name;
			return ClientServiceFactory.GetClientInstance<IInventoryCatalog>().GetCatalogByName(getCatalogByNameReq).Catalog;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000C748 File Offset: 0x0000A948
		public IList<InventoryCatalogDTO> GetCatalogs()
		{
			GetCatalogsReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetCatalogsReq>();
			return ClientServiceFactory.GetClientInstance<IInventoryCatalog>().GetCatalogs(request).Catalogs;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000C778 File Offset: 0x0000A978
		public string ExportToXML(int catalogId)
		{
			ExportToXMLReq exportToXMLReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ExportToXMLReq>();
			exportToXMLReq.CatalogId = catalogId;
			return ClientServiceFactory.GetClientInstance<IInventoryCatalog>().ExportToXML(exportToXMLReq).CatalogXml;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000C7B0 File Offset: 0x0000A9B0
		public InventoryCatalogDTO GetTemplateCatalogByName(string templateName)
		{
			GetTemplateCatalogByNameReq getTemplateCatalogByNameReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetTemplateCatalogByNameReq>();
			getTemplateCatalogByNameReq.TemplateName = templateName;
			return ClientServiceFactory.GetClientInstance<IInventoryCatalog>().GetTemplateCatalogByName(getTemplateCatalogByNameReq).Catalog;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000C7E8 File Offset: 0x0000A9E8
		public IList<InventoryCatalogDTO> GetTemplateCatalogs()
		{
			GetTemplateCatalogsReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetTemplateCatalogsReq>();
			return ClientServiceFactory.GetClientInstance<IInventoryCatalog>().GetTemplateCatalogs(request).TemplateCatalogs;
		}
	}
}

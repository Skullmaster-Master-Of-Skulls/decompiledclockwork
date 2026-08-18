using System;
using System.IO;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.ClockWorkServer.Core.Impl;
using TechnoPro.Common.Core.Inventory;
using TechnoPro.Common.Core.Mappers.Inventory;
using TechnoPro.Common.ICore.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200004E RID: 78
	public class InventoryCatalogServiceManager : IInventoryCatalog, IService
	{
		// Token: 0x060002EB RID: 747 RVA: 0x0000E514 File Offset: 0x0000C714
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000E528 File Offset: 0x0000C728
		public GetCatalogByIdResp GetCatalogById(GetCatalogByIdReq request)
		{
			IInventoryCatalogManager inventoryCatalogManager = new InventoryCatalogManager(request.GetOperationContext());
			return new GetCatalogByIdResp
			{
				Catalog = inventoryCatalogManager.GetCatalogById(request.CatalogId).ToDTO()
			};
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000E564 File Offset: 0x0000C764
		public GetCatalogByNameResp GetCatalogByName(GetCatalogByNameReq request)
		{
			IInventoryCatalogManager inventoryCatalogManager = new InventoryCatalogManager(request.GetOperationContext());
			return new GetCatalogByNameResp
			{
				Catalog = inventoryCatalogManager.GetCatalogByName(request.CatalogName).ToDTO()
			};
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000E5A0 File Offset: 0x0000C7A0
		public GetCatalogsResp GetCatalogs(GetCatalogsReq request)
		{
			IInventoryCatalogManager inventoryCatalogManager = new InventoryCatalogManager(request.GetOperationContext());
			return new GetCatalogsResp
			{
				Catalogs = inventoryCatalogManager.GetCatalogs().ToDTO()
			};
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000E5D8 File Offset: 0x0000C7D8
		public ExportToXMLResp ExportToXML(ExportToXMLReq request)
		{
			IInventoryCatalogManager inventoryCatalogManager = new InventoryCatalogManager(request.GetOperationContext());
			return new ExportToXMLResp
			{
				CatalogXml = inventoryCatalogManager.ExportToXML(request.CatalogId)
			};
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000E610 File Offset: 0x0000C810
		public GetTemplateCatalogByNameResp GetTemplateCatalogByName(GetTemplateCatalogByNameReq request)
		{
			ServerExecutingContext serverExecutingContext = ObjectFactory.Resolve<ServerExecutingContext>();
			string text = (serverExecutingContext != null) ? serverExecutingContext.ServerResourcesPath : null;
			IInventoryCatalogManager inventoryCatalogManager = new InventoryCatalogManager(request.GetOperationContext());
			return new GetTemplateCatalogByNameResp
			{
				Catalog = ((text != null) ? inventoryCatalogManager.GetTemplateCatalogByName(Path.Combine(text, "InventoryCatalogs"), request.TemplateName).ToDTO() : null)
			};
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000E670 File Offset: 0x0000C870
		public GetTemplateCatalogsResp GetTemplateCatalogs(GetTemplateCatalogsReq request)
		{
			ServerExecutingContext serverExecutingContext = ObjectFactory.Resolve<ServerExecutingContext>();
			string text = (serverExecutingContext != null) ? serverExecutingContext.ServerResourcesPath : null;
			IInventoryCatalogManager inventoryCatalogManager = new InventoryCatalogManager(request.GetOperationContext());
			return new GetTemplateCatalogsResp
			{
				TemplateCatalogs = ((text != null) ? inventoryCatalogManager.GetTemplateCatalogs(Path.Combine(text, "InventoryCatalogs")).ToDTO() : null)
			};
		}
	}
}

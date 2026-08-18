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
	// Token: 0x0200004C RID: 76
	public class InventoryAdminCatalogServiceManager : IInventoryAdminCatalog, IService
	{
		// Token: 0x060002D9 RID: 729 RVA: 0x0000E174 File Offset: 0x0000C374
		public GetFullCatalogListResp GetFullCatalogList(GetFullCatalogListReq request)
		{
			IInventoryAdminCatalogManager inventoryAdminCatalogManager = new InventoryAdminCatalogManager(request.GetOperationContext());
			return new GetFullCatalogListResp
			{
				Catalogs = inventoryAdminCatalogManager.GetFullCatalogList().ToDTO()
			};
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000E1AC File Offset: 0x0000C3AC
		public CreateCatalogResp CreateCatalog(CreateCatalogReq request)
		{
			IInventoryAdminCatalogManager inventoryAdminCatalogManager = new InventoryAdminCatalogManager(request.GetOperationContext());
			return new CreateCatalogResp
			{
				CatalogId = inventoryAdminCatalogManager.CreateCatalog(request.Catalog.ToDomainObject())
			};
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000E1E8 File Offset: 0x0000C3E8
		public UpdateCatalogResp UpdateCatalog(UpdateCatalogReq request)
		{
			IInventoryAdminCatalogManager inventoryAdminCatalogManager = new InventoryAdminCatalogManager(request.GetOperationContext());
			inventoryAdminCatalogManager.UpdateCatalog(request.Catalog.ToDomainObject());
			return new UpdateCatalogResp();
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000E220 File Offset: 0x0000C420
		public DeleteEmptyCatalogResp DeleteEmptyCatalog(DeleteEmptyCatalogReq request)
		{
			IInventoryAdminCatalogManager inventoryAdminCatalogManager = new InventoryAdminCatalogManager(request.GetOperationContext());
			return new DeleteEmptyCatalogResp
			{
				WasDeleted = inventoryAdminCatalogManager.DeleteEmptyCatalog(request.CatalogId)
			};
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000E258 File Offset: 0x0000C458
		public ImportFromXMLResp ImportFromXML(ImportFromXMLReq request)
		{
			IInventoryAdminCatalogManager inventoryAdminCatalogManager = new InventoryAdminCatalogManager(request.GetOperationContext());
			return new ImportFromXMLResp
			{
				CatalogId = inventoryAdminCatalogManager.ImportFromXML(request.CatalogXml, request.CatalogName, request.CatalogDescription)
			};
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000E29C File Offset: 0x0000C49C
		public ImportFromTemplateResp ImportFromTemplate(ImportFromTemplateReq request)
		{
			IInventoryAdminCatalogManager inventoryAdminCatalogManager = new InventoryAdminCatalogManager(request.GetOperationContext());
			return new ImportFromTemplateResp
			{
				CatalogId = inventoryAdminCatalogManager.ImportFromTemplate(Path.Combine(ObjectFactory.Resolve<ServerExecutingContext>().ServerResourcesPath, "InventoryCatalogs"), request.TemplateName, request.CatalogName, request.CatalogDescription)
			};
		}
	}
}

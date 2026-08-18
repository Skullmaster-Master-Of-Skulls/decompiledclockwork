using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000AB RID: 171
	public class InventoryAdminCatalogReusableClientProxy : WCFTokenBasedReusableClientProxy<IInventoryAdminCatalog>, IInventoryAdminCatalog, IService
	{
		// Token: 0x060006CD RID: 1741 RVA: 0x000123C4 File Offset: 0x000105C4
		public InventoryAdminCatalogReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x000123CF File Offset: 0x000105CF
		public InventoryAdminCatalogReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x000123DC File Offset: 0x000105DC
		public GetFullCatalogListResp GetFullCatalogList(GetFullCatalogListReq request)
		{
			return this.WrapServiceMethod<GetFullCatalogListResp>(() => this.Proxy.GetFullCatalogList(request));
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00012414 File Offset: 0x00010614
		public CreateCatalogResp CreateCatalog(CreateCatalogReq request)
		{
			return this.WrapServiceMethod<CreateCatalogResp>(() => this.Proxy.CreateCatalog(request));
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001244C File Offset: 0x0001064C
		public UpdateCatalogResp UpdateCatalog(UpdateCatalogReq request)
		{
			return this.WrapServiceMethod<UpdateCatalogResp>(() => this.Proxy.UpdateCatalog(request));
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00012484 File Offset: 0x00010684
		public DeleteEmptyCatalogResp DeleteEmptyCatalog(DeleteEmptyCatalogReq request)
		{
			return this.WrapServiceMethod<DeleteEmptyCatalogResp>(() => this.Proxy.DeleteEmptyCatalog(request));
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x000124BC File Offset: 0x000106BC
		public ImportFromXMLResp ImportFromXML(ImportFromXMLReq request)
		{
			return this.WrapServiceMethod<ImportFromXMLResp>(() => this.Proxy.ImportFromXML(request));
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x000124F4 File Offset: 0x000106F4
		public ImportFromTemplateResp ImportFromTemplate(ImportFromTemplateReq request)
		{
			return this.WrapServiceMethod<ImportFromTemplateResp>(() => this.Proxy.ImportFromTemplate(request));
		}
	}
}

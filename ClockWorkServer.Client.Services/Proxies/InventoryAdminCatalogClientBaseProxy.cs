using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000AC RID: 172
	internal class InventoryAdminCatalogClientBaseProxy : ClientBase<IInventoryAdminCatalog>, IInventoryAdminCatalog, IService
	{
		// Token: 0x060006D5 RID: 1749 RVA: 0x0001252C File Offset: 0x0001072C
		public InventoryAdminCatalogClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00012537 File Offset: 0x00010737
		public InventoryAdminCatalogClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00012544 File Offset: 0x00010744
		public GetFullCatalogListResp GetFullCatalogList(GetFullCatalogListReq request)
		{
			return base.Channel.GetFullCatalogList(request);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00012564 File Offset: 0x00010764
		public CreateCatalogResp CreateCatalog(CreateCatalogReq request)
		{
			return base.Channel.CreateCatalog(request);
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00012584 File Offset: 0x00010784
		public UpdateCatalogResp UpdateCatalog(UpdateCatalogReq request)
		{
			return base.Channel.UpdateCatalog(request);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x000125A4 File Offset: 0x000107A4
		public DeleteEmptyCatalogResp DeleteEmptyCatalog(DeleteEmptyCatalogReq request)
		{
			return base.Channel.DeleteEmptyCatalog(request);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x000125C4 File Offset: 0x000107C4
		public ImportFromXMLResp ImportFromXML(ImportFromXMLReq request)
		{
			return base.Channel.ImportFromXML(request);
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x000125E4 File Offset: 0x000107E4
		public ImportFromTemplateResp ImportFromTemplate(ImportFromTemplateReq request)
		{
			return base.Channel.ImportFromTemplate(request);
		}
	}
}

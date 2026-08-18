using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000B0 RID: 176
	internal class InventoryCatalogClientBaseProxy : ClientBase<IInventoryCatalog>, IInventoryCatalog, IService
	{
		// Token: 0x060006FB RID: 1787 RVA: 0x00012AB4 File Offset: 0x00010CB4
		public InventoryCatalogClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00012ABF File Offset: 0x00010CBF
		public InventoryCatalogClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00012ACC File Offset: 0x00010CCC
		public GetCatalogByIdResp GetCatalogById(GetCatalogByIdReq request)
		{
			return base.Channel.GetCatalogById(request);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00012AEC File Offset: 0x00010CEC
		public GetCatalogByNameResp GetCatalogByName(GetCatalogByNameReq request)
		{
			return base.Channel.GetCatalogByName(request);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00012B0C File Offset: 0x00010D0C
		public GetCatalogsResp GetCatalogs(GetCatalogsReq request)
		{
			return base.Channel.GetCatalogs(request);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00012B2C File Offset: 0x00010D2C
		public ExportToXMLResp ExportToXML(ExportToXMLReq request)
		{
			return base.Channel.ExportToXML(request);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00012B4C File Offset: 0x00010D4C
		public GetTemplateCatalogByNameResp GetTemplateCatalogByName(GetTemplateCatalogByNameReq request)
		{
			return base.Channel.GetTemplateCatalogByName(request);
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00012B6C File Offset: 0x00010D6C
		public GetTemplateCatalogsResp GetTemplateCatalogs(GetTemplateCatalogsReq request)
		{
			return base.Channel.GetTemplateCatalogs(request);
		}
	}
}

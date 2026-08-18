using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000AF RID: 175
	public class InventoryCatalogReusableClientProxy : WCFTokenBasedReusableClientProxy<IInventoryCatalog>, IInventoryCatalog, IService
	{
		// Token: 0x060006F3 RID: 1779 RVA: 0x0001294A File Offset: 0x00010B4A
		public InventoryCatalogReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00012955 File Offset: 0x00010B55
		public InventoryCatalogReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00012964 File Offset: 0x00010B64
		public GetCatalogByIdResp GetCatalogById(GetCatalogByIdReq request)
		{
			return this.WrapServiceMethod<GetCatalogByIdResp>(() => this.Proxy.GetCatalogById(request));
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0001299C File Offset: 0x00010B9C
		public GetCatalogByNameResp GetCatalogByName(GetCatalogByNameReq request)
		{
			return this.WrapServiceMethod<GetCatalogByNameResp>(() => this.Proxy.GetCatalogByName(request));
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x000129D4 File Offset: 0x00010BD4
		public GetCatalogsResp GetCatalogs(GetCatalogsReq request)
		{
			return this.WrapServiceMethod<GetCatalogsResp>(() => this.Proxy.GetCatalogs(request));
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00012A0C File Offset: 0x00010C0C
		public ExportToXMLResp ExportToXML(ExportToXMLReq request)
		{
			return this.WrapServiceMethod<ExportToXMLResp>(() => this.Proxy.ExportToXML(request));
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00012A44 File Offset: 0x00010C44
		public GetTemplateCatalogByNameResp GetTemplateCatalogByName(GetTemplateCatalogByNameReq request)
		{
			return this.WrapServiceMethod<GetTemplateCatalogByNameResp>(() => this.Proxy.GetTemplateCatalogByName(request));
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00012A7C File Offset: 0x00010C7C
		public GetTemplateCatalogsResp GetTemplateCatalogs(GetTemplateCatalogsReq request)
		{
			return this.WrapServiceMethod<GetTemplateCatalogsResp>(() => this.Proxy.GetTemplateCatalogs(request));
		}
	}
}

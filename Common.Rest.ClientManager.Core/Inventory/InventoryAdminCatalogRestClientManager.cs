using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Inventory
{
	// Token: 0x0200003F RID: 63
	public class InventoryAdminCatalogRestClientManager : BearerTokenRestProxy<IInventoryAdminCatalogClientManager>, IInventoryAdminCatalogClientManager, IWebService
	{
		// Token: 0x0600023E RID: 574 RVA: 0x0000745A File Offset: 0x0000565A
		public InventoryAdminCatalogRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00007464 File Offset: 0x00005664
		public InventoryAdminCatalogRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000746F File Offset: 0x0000566F
		public IList<InventoryCatalogDTO> GetFullCatalogList()
		{
			return base.GetMany<InventoryCatalogDTO>("inventoryadmincatalog", true);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000747D File Offset: 0x0000567D
		public int CreateCatalog(InventoryCatalogDTO catalog)
		{
			return base.Post<InventoryCatalogDTO, int>(catalog, "inventoryadmincatalog");
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000748B File Offset: 0x0000568B
		public void UpdateCatalog(InventoryCatalogDTO catalog)
		{
			base.Put<InventoryCatalogDTO>(catalog, "inventoryadmincatalog");
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00007499 File Offset: 0x00005699
		public bool DeleteEmptyCatalog(int catalogId)
		{
			base.Delete(string.Format("inventoryadmincatalog/catalogid/{0}", catalogId));
			return true;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000074B4 File Offset: 0x000056B4
		public int ImportFromXML(string catalogXmlDoc, string catalogName = null, string catalogDescription = null)
		{
			ImportFromXMLReq importFromXMLReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ImportFromXMLReq>();
			importFromXMLReq.CatalogXml = catalogXmlDoc;
			importFromXMLReq.CatalogName = catalogName;
			importFromXMLReq.CatalogDescription = catalogDescription;
			return base.Post<ImportFromXMLReq, int>(importFromXMLReq, "inventoryadmincatalog/importfromxml");
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000074F0 File Offset: 0x000056F0
		public int ImportFromTemplate(string templateName, string catalogName = null, string catalogDescription = null)
		{
			ImportFromTemplateReq importFromTemplateReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ImportFromTemplateReq>();
			importFromTemplateReq.TemplateName = templateName;
			importFromTemplateReq.CatalogName = catalogName;
			importFromTemplateReq.CatalogDescription = catalogDescription;
			return base.Post<ImportFromTemplateReq, int>(importFromTemplateReq, "inventoryadmincatalog/importfromtemplate");
		}
	}
}

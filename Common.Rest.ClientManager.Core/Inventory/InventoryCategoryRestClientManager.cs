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
	// Token: 0x02000042 RID: 66
	public class InventoryCategoryRestClientManager : BearerTokenRestProxy<IInventoryCategoryClientManager>, IInventoryCategoryClientManager, IWebService
	{
		// Token: 0x06000259 RID: 601 RVA: 0x0000770D File Offset: 0x0000590D
		public InventoryCategoryRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00007717 File Offset: 0x00005917
		public InventoryCategoryRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00007722 File Offset: 0x00005922
		public bool CreateCategory(InventoryCategoryDTO category)
		{
			return base.Post<InventoryCategoryDTO, bool>(category, "inventorycategory");
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00007730 File Offset: 0x00005930
		public void AssignCategoryDynamicForm(string categoryName, int dynamicFormId)
		{
			AssignCategoryDynamicFormReq assignCategoryDynamicFormReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AssignCategoryDynamicFormReq>();
			assignCategoryDynamicFormReq.CategoryName = categoryName;
			assignCategoryDynamicFormReq.DynamicFormId = dynamicFormId;
			base.Post<AssignCategoryDynamicFormReq>(assignCategoryDynamicFormReq, "inventorycategory/assigndynamicform");
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00007762 File Offset: 0x00005962
		public bool DeleteEmptyCategory(int catalogId, string categoryName)
		{
			base.Delete(string.Format("inventorycategory/emptycategory/catalogid/{0}/categoryname/{1}", catalogId, categoryName));
			return true;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000777C File Offset: 0x0000597C
		public InventoryCategoryDTO GetCategoryByName(string categoryName)
		{
			return base.Get<InventoryCategoryDTO>(string.Format("inventorycategory/categoryname/{0}", categoryName), true);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00007790 File Offset: 0x00005990
		public IList<InventoryCategoryDTO> GetCategoriesByCatalog(int catalogId)
		{
			return base.GetMany<InventoryCategoryDTO>(string.Format("inventorycategory/catalogid/{0}", catalogId), true);
		}
	}
}

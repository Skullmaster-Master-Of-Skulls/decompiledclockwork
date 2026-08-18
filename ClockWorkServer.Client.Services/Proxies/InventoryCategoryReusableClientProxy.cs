using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000B1 RID: 177
	public class InventoryCategoryReusableClientProxy : WCFTokenBasedReusableClientProxy<IInventoryCategory>, IInventoryCategory, IService
	{
		// Token: 0x06000703 RID: 1795 RVA: 0x00012B8A File Offset: 0x00010D8A
		public InventoryCategoryReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00012B95 File Offset: 0x00010D95
		public InventoryCategoryReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00012BA4 File Offset: 0x00010DA4
		public CreateCategoryResp CreateCategory(CreateCategoryReq request)
		{
			return this.WrapServiceMethod<CreateCategoryResp>(() => this.Proxy.CreateCategory(request));
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00012BDC File Offset: 0x00010DDC
		public AssignCategoryDynamicFormResp AssignCategoryDynamicForm(AssignCategoryDynamicFormReq request)
		{
			return this.WrapServiceMethod<AssignCategoryDynamicFormResp>(() => this.Proxy.AssignCategoryDynamicForm(request));
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00012C14 File Offset: 0x00010E14
		public DeleteEmptyCategoryResp DeleteEmptyCategory(DeleteEmptyCategoryReq request)
		{
			return this.WrapServiceMethod<DeleteEmptyCategoryResp>(() => this.Proxy.DeleteEmptyCategory(request));
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00012C4C File Offset: 0x00010E4C
		public GetCategoryByNameResp GetCategoryByName(GetCategoryByNameReq request)
		{
			return this.WrapServiceMethod<GetCategoryByNameResp>(() => this.Proxy.GetCategoryByName(request));
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00012C84 File Offset: 0x00010E84
		public GetCategoriesByCatalogResp GetCategoriesByCatalog(GetCategoriesByCatalogReq request)
		{
			return this.WrapServiceMethod<GetCategoriesByCatalogResp>(() => this.Proxy.GetCategoriesByCatalog(request));
		}
	}
}

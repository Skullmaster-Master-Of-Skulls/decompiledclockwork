using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000B2 RID: 178
	internal class InventoryCategoryClientBaseProxy : ClientBase<IInventoryCategory>, IInventoryCategory, IService
	{
		// Token: 0x0600070A RID: 1802 RVA: 0x00012CBC File Offset: 0x00010EBC
		public InventoryCategoryClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00012CC7 File Offset: 0x00010EC7
		public InventoryCategoryClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00012CD4 File Offset: 0x00010ED4
		public CreateCategoryResp CreateCategory(CreateCategoryReq request)
		{
			return base.Channel.CreateCategory(request);
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00012CF4 File Offset: 0x00010EF4
		public AssignCategoryDynamicFormResp AssignCategoryDynamicForm(AssignCategoryDynamicFormReq request)
		{
			return base.Channel.AssignCategoryDynamicForm(request);
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00012D14 File Offset: 0x00010F14
		public DeleteEmptyCategoryResp DeleteEmptyCategory(DeleteEmptyCategoryReq request)
		{
			return base.Channel.DeleteEmptyCategory(request);
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x00012D34 File Offset: 0x00010F34
		public GetCategoryByNameResp GetCategoryByName(GetCategoryByNameReq request)
		{
			return base.Channel.GetCategoryByName(request);
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x00012D54 File Offset: 0x00010F54
		public GetCategoriesByCatalogResp GetCategoriesByCatalog(GetCategoriesByCatalogReq request)
		{
			return base.Channel.GetCategoriesByCatalog(request);
		}
	}
}

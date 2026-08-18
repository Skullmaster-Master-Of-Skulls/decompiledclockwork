using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Inventory
{
	// Token: 0x02000051 RID: 81
	public interface IInventoryProductStatusClientManager : IWebService
	{
		// Token: 0x0600024E RID: 590
		int CreateProductStatus(InventoryProductStatusDTO productStatus);

		// Token: 0x0600024F RID: 591
		void UpdateProductStatus(InventoryProductStatusDTO productStatus);

		// Token: 0x06000250 RID: 592
		InventoryProductStatusDTO GetProductStatusById(int pStatusId);

		// Token: 0x06000251 RID: 593
		IList<InventoryProductStatusDTO> GetProductStatusList();
	}
}

using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.DAO.Inventory
{
	// Token: 0x0200006C RID: 108
	public interface IInventoryProductStatusDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000295 RID: 661
		int CreateProductStatus(InventoryProductStatus productStatus);

		// Token: 0x06000296 RID: 662
		void UpdateProductStatus(InventoryProductStatus productStatus);

		// Token: 0x06000297 RID: 663
		InventoryProductStatus GetProductStatusById(int pStatusId);

		// Token: 0x06000298 RID: 664
		IList<InventoryProductStatus> GetProductStatusList();
	}
}

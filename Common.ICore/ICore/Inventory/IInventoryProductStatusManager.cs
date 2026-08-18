using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.ICore.Inventory
{
	// Token: 0x02000085 RID: 133
	public interface IInventoryProductStatusManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600039F RID: 927
		int CreateProductStatus(InventoryProductStatus productStatus);

		// Token: 0x060003A0 RID: 928
		void UpdateProductStatus(InventoryProductStatus productStatus);

		// Token: 0x060003A1 RID: 929
		InventoryProductStatus GetProductStatusById(int pStatusId);

		// Token: 0x060003A2 RID: 930
		IList<InventoryProductStatus> GetProductStatusList();
	}
}

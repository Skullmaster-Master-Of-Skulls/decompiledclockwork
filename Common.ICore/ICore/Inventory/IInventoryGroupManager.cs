using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.ICore.Inventory
{
	// Token: 0x02000081 RID: 129
	public interface IInventoryGroupManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000380 RID: 896
		int CreateProductGroup(InventoryGroup pGroup);

		// Token: 0x06000381 RID: 897
		void UpdateProductGroup(InventoryGroup pGroup);

		// Token: 0x06000382 RID: 898
		bool DeleteEmptyProductGroup(int pGroupId);

		// Token: 0x06000383 RID: 899
		InventoryGroup GetGroupById(int pGroupId);

		// Token: 0x06000384 RID: 900
		IList<InventoryGroup> GetGroups();
	}
}

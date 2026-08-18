using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.DAO.Inventory
{
	// Token: 0x02000067 RID: 103
	public interface IInventoryGroupDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000256 RID: 598
		int CreateProductGroup(InventoryGroup pGroup);

		// Token: 0x06000257 RID: 599
		void UpdateProductGroup(InventoryGroup pGroup);

		// Token: 0x06000258 RID: 600
		bool DeleteEmptyProductGroup(int pGroupId);

		// Token: 0x06000259 RID: 601
		InventoryGroup GetGroupById(int pGroupId);

		// Token: 0x0600025A RID: 602
		IList<InventoryGroup> GetGroups();
	}
}

using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.DAO.Inventory
{
	// Token: 0x0200006B RID: 107
	public interface IInventoryLocationDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600028E RID: 654
		int CreateLocation(InventoryLocation location);

		// Token: 0x0600028F RID: 655
		InventoryLocation GetLocationById(int locationId);

		// Token: 0x06000290 RID: 656
		IList<InventoryLocation> GetAllLocations();

		// Token: 0x06000291 RID: 657
		IList<InventoryLocation> GetLocations(string includingText);

		// Token: 0x06000292 RID: 658
		bool LocationInUse(int locationId);

		// Token: 0x06000293 RID: 659
		bool DeleteLocation(int locationId);

		// Token: 0x06000294 RID: 660
		void UpdateLocation(InventoryLocation location);
	}
}

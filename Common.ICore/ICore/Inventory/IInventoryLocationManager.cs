using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.ICore.Inventory
{
	// Token: 0x02000083 RID: 131
	public interface IInventoryLocationManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000389 RID: 905
		int CreateLocation(InventoryLocation location);

		// Token: 0x0600038A RID: 906
		InventoryLocation GetLocationById(int locationId);

		// Token: 0x0600038B RID: 907
		IList<InventoryLocation> GetAllLocations();

		// Token: 0x0600038C RID: 908
		IList<InventoryLocation> GetLocations(string includingText);

		// Token: 0x0600038D RID: 909
		bool LocationInUse(int locationId);

		// Token: 0x0600038E RID: 910
		bool DeleteLocation(int locationId);

		// Token: 0x0600038F RID: 911
		void UpdateLocation(InventoryLocation location);
	}
}

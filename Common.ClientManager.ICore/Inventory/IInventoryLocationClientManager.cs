using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Inventory
{
	// Token: 0x0200004F RID: 79
	public interface IInventoryLocationClientManager : IWebService
	{
		// Token: 0x06000231 RID: 561
		int CreateLocation(InventoryLocationDTO location);

		// Token: 0x06000232 RID: 562
		InventoryLocationDTO GetLocationById(int locationId);

		// Token: 0x06000233 RID: 563
		IList<InventoryLocationDTO> GetAllLocations();

		// Token: 0x06000234 RID: 564
		IList<InventoryLocationDTO> GetLocations(string includingText);

		// Token: 0x06000235 RID: 565
		bool LocationInUse(int locationId);

		// Token: 0x06000236 RID: 566
		bool DeleteLocation(int locationId);

		// Token: 0x06000237 RID: 567
		void UpdateLocation(InventoryLocationDTO location);
	}
}

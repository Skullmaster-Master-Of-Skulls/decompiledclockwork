using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Inventory
{
	// Token: 0x0200004C RID: 76
	public interface IInventoryGroupClientManager : IWebService
	{
		// Token: 0x06000212 RID: 530
		int CreateProductGroup(InventoryGroupDTO pGroup);

		// Token: 0x06000213 RID: 531
		void UpdateProductGroup(InventoryGroupDTO pGroup);

		// Token: 0x06000214 RID: 532
		bool DeleteEmptyProductGroup(int pGroupId);

		// Token: 0x06000215 RID: 533
		InventoryGroupDTO GetGroupById(int pGroupId);

		// Token: 0x06000216 RID: 534
		IList<InventoryGroupDTO> GetGroups();
	}
}

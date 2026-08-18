using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Inventory
{
	// Token: 0x0200004E RID: 78
	public interface IInventoryLoanStatusClientManager : IWebService
	{
		// Token: 0x0600022C RID: 556
		int CreateLoanStatus(InventoryLoanStatusDTO loanStatus);

		// Token: 0x0600022D RID: 557
		void UpdateLoanStatus(InventoryLoanStatusDTO loanStatus);

		// Token: 0x0600022E RID: 558
		InventoryLoanStatusDTO GetLoanStatusById(int lStatusId);

		// Token: 0x0600022F RID: 559
		InventoryLoanStatusDTO GetLoanStatusByName(string loanStatusName);

		// Token: 0x06000230 RID: 560
		IList<InventoryLoanStatusDTO> GetLoanStatusList();
	}
}

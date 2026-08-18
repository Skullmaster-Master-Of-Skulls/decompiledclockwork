using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.DAO.Inventory
{
	// Token: 0x02000068 RID: 104
	public interface IInventoryLoanStatusDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600025B RID: 603
		int CreateLoanStatus(InventoryLoanStatus loanStatus);

		// Token: 0x0600025C RID: 604
		void UpdateLoanStatus(InventoryLoanStatus loanStatus);

		// Token: 0x0600025D RID: 605
		InventoryLoanStatus GetLoanStatusById(int lStatusId);

		// Token: 0x0600025E RID: 606
		IList<InventoryLoanStatus> GetLoanStatusList();
	}
}

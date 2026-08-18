using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.ICore.Inventory
{
	// Token: 0x02000082 RID: 130
	public interface IInventoryLoanStatusManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000385 RID: 901
		int CreateLoanStatus(InventoryLoanStatus loanStatus);

		// Token: 0x06000386 RID: 902
		void UpdateLoanStatus(InventoryLoanStatus loanStatus);

		// Token: 0x06000387 RID: 903
		InventoryLoanStatus GetLoanStatusById(int lStatusId);

		// Token: 0x06000388 RID: 904
		IList<InventoryLoanStatus> GetLoanStatusList();
	}
}

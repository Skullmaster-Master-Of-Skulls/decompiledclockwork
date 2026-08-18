using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.DAO.Inventory
{
	// Token: 0x0200006A RID: 106
	public interface IInventoryLoanDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600027A RID: 634
		IList<InventoryLoan> GetActiveLoans();

		// Token: 0x0600027B RID: 635
		InventoryLoan GetActiveLoanById(int loanID);

		// Token: 0x0600027C RID: 636
		InventoryLoan GetActiveLoanByProduct(Guid productUniqueID);

		// Token: 0x0600027D RID: 637
		InventoryLoan GetActiveLoanByProduct(int productId);

		// Token: 0x0600027E RID: 638
		IList<InventoryLoan> GetActiveLoansByPersonLoanedTo(int personId);

		// Token: 0x0600027F RID: 639
		IList<InventoryLoan> GetActiveLoansByPersonLoanedTo(int personId, DateTime startDate, DateTime endDate);

		// Token: 0x06000280 RID: 640
		IList<InventoryLoan> GetActiveLoansByDueDateInLessThan(TimeSpan dueDateIn);

		// Token: 0x06000281 RID: 641
		IList<InventoryLoan> GetOverDueDateActiveLoans();

		// Token: 0x06000282 RID: 642
		int MakeLoan(InventoryLoanGroup loan, params Guid[] loanedProductUniqueIds);

		// Token: 0x06000283 RID: 643
		int UpdateLoan(InventoryLoan loan);

		// Token: 0x06000284 RID: 644
		void UpdateLoanGroup(InventoryLoanGroup loanGroup);

		// Token: 0x06000285 RID: 645
		IList<InventoryLoan> GetLoansByLoanGroupId(int loanGroupId);

		// Token: 0x06000286 RID: 646
		void ReturnLoan(InventoryReturnedLoan returnedLoan);

		// Token: 0x06000287 RID: 647
		InventoryArchivedLoan GetReturnedLoanById(int loanID);

		// Token: 0x06000288 RID: 648
		IList<InventoryArchivedLoan> GetReturnedLoans();

		// Token: 0x06000289 RID: 649
		IList<InventoryArchivedLoan> GetReturnedLoansByProduct(Guid productUniqueID);

		// Token: 0x0600028A RID: 650
		IList<InventoryArchivedLoan> GetReturnedLoansByProduct(Guid productUniqueID, DateTime startDate, DateTime endDate);

		// Token: 0x0600028B RID: 651
		IList<InventoryArchivedLoan> GetReturnedLoansByProduct(int productId, DateTime startDate, DateTime endDate);

		// Token: 0x0600028C RID: 652
		IList<InventoryArchivedLoan> GetReturnedLoansByPersonLoanedTo(int personId);

		// Token: 0x0600028D RID: 653
		IList<InventoryArchivedLoan> GetReturnedLoansByPersonLoanedTo(int personId, DateTime startDate, DateTime endDate);
	}
}

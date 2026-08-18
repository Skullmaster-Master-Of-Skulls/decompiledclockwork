using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Inventory
{
	// Token: 0x0200004D RID: 77
	public interface IInventoryLoanClientManager : IWebService
	{
		// Token: 0x06000217 RID: 535
		IList<InventoryLoanDTO> GetActiveLoans();

		// Token: 0x06000218 RID: 536
		InventoryLoanDTO GetActiveLoanById(int loanID);

		// Token: 0x06000219 RID: 537
		InventoryLoanDTO GetActiveLoanByProduct(Guid productUniqueID);

		// Token: 0x0600021A RID: 538
		InventoryLoanDTO GetActiveLoanByProduct(int productId);

		// Token: 0x0600021B RID: 539
		IList<InventoryLoanDTO> GetActiveLoansByPersonLoanedTo(int personId);

		// Token: 0x0600021C RID: 540
		IList<InventoryLoanDTO> GetActiveLoansByPersonLoanedTo(int personId, DateTime startDate, DateTime endDate);

		// Token: 0x0600021D RID: 541
		IList<InventoryLoanDTO> GetActiveLoansByDueDateInLessThan(TimeSpan dueDateIn);

		// Token: 0x0600021E RID: 542
		IList<InventoryLoanDTO> GetOverDueDateActiveLoans();

		// Token: 0x0600021F RID: 543
		int MakeLoan(InventoryLoanGroupDTO loan, params Guid[] loanedProductUniqueIds);

		// Token: 0x06000220 RID: 544
		int UpdateLoan(InventoryLoanDTO loan);

		// Token: 0x06000221 RID: 545
		void UpdateLoanGroup(InventoryLoanGroupDTO loanGroup);

		// Token: 0x06000222 RID: 546
		IList<InventoryLoanDTO> GetLoansByLoanGroupId(int loanGroupId);

		// Token: 0x06000223 RID: 547
		void ReturnLoan(InventoryReturnedLoanDTO returnedLoan);

		// Token: 0x06000224 RID: 548
		void ReturnLoan(IList<InventoryReturnedLoanDTO> returnedLoan);

		// Token: 0x06000225 RID: 549
		InventoryArchivedLoanDTO GetReturnedLoanById(int loanID);

		// Token: 0x06000226 RID: 550
		IList<InventoryArchivedLoanDTO> GetReturnedLoans();

		// Token: 0x06000227 RID: 551
		IList<InventoryArchivedLoanDTO> GetReturnedLoansByProduct(Guid productUniqueID);

		// Token: 0x06000228 RID: 552
		IList<InventoryArchivedLoanDTO> GetReturnedLoansByProduct(Guid productUniqueID, DateTime startDate, DateTime endDate);

		// Token: 0x06000229 RID: 553
		IList<InventoryArchivedLoanDTO> GetReturnedLoansByProduct(int productId, DateTime startDate, DateTime endDate);

		// Token: 0x0600022A RID: 554
		IList<InventoryArchivedLoanDTO> GetReturnedLoansByPersonLoanedTo(int personId);

		// Token: 0x0600022B RID: 555
		IList<InventoryArchivedLoanDTO> GetReturnedLoansByPersonLoanedTo(int personId, DateTime startDate, DateTime endDate);
	}
}

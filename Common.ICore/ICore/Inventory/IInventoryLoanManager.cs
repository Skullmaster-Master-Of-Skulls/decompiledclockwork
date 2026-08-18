using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.ICore.Inventory
{
	// Token: 0x02000088 RID: 136
	public interface IInventoryLoanManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060003BF RID: 959
		IList<InventoryLoan> GetActiveLoans();

		// Token: 0x060003C0 RID: 960
		InventoryLoan GetActiveLoanById(int loanID);

		// Token: 0x060003C1 RID: 961
		InventoryLoan GetActiveLoanByProduct(Guid productUniqueID);

		// Token: 0x060003C2 RID: 962
		InventoryLoan GetActiveLoanByProduct(int productId);

		// Token: 0x060003C3 RID: 963
		IList<InventoryLoan> GetActiveLoansByPersonLoanedTo(int personId);

		// Token: 0x060003C4 RID: 964
		IList<InventoryLoan> GetActiveLoansByPersonLoanedTo(int personId, DateTime startDate, DateTime endDate);

		// Token: 0x060003C5 RID: 965
		IList<InventoryLoan> GetActiveLoansByDueDateInLessThan(TimeSpan dueDateIn);

		// Token: 0x060003C6 RID: 966
		IList<InventoryLoan> GetOverDueDateActiveLoans();

		// Token: 0x060003C7 RID: 967
		int MakeLoan(InventoryLoanGroup loan, params Guid[] loanedProductUniqueIds);

		// Token: 0x060003C8 RID: 968
		int UpdateLoan(InventoryLoan loan);

		// Token: 0x060003C9 RID: 969
		void UpdateLoanGroup(InventoryLoanGroup loanGroup);

		// Token: 0x060003CA RID: 970
		IList<InventoryLoan> GetLoansByLoanGroupId(int loanGroupId);

		// Token: 0x060003CB RID: 971
		void ReturnLoan(InventoryReturnedLoan returnedLoan);

		// Token: 0x060003CC RID: 972
		void ReturnLoan(params InventoryReturnedLoan[] returnedLoan);

		// Token: 0x060003CD RID: 973
		InventoryArchivedLoan GetReturnedLoanById(int loanID);

		// Token: 0x060003CE RID: 974
		IList<InventoryArchivedLoan> GetReturnedLoans();

		// Token: 0x060003CF RID: 975
		IList<InventoryArchivedLoan> GetReturnedLoansByProduct(Guid productUniqueID);

		// Token: 0x060003D0 RID: 976
		IList<InventoryArchivedLoan> GetReturnedLoansByProduct(Guid productUniqueID, DateTime startDate, DateTime endDate);

		// Token: 0x060003D1 RID: 977
		IList<InventoryArchivedLoan> GetReturnedLoansByProduct(int productId, DateTime startDate, DateTime endDate);

		// Token: 0x060003D2 RID: 978
		IList<InventoryArchivedLoan> GetReturnedLoansByPersonLoanedTo(int personId);

		// Token: 0x060003D3 RID: 979
		IList<InventoryArchivedLoan> GetReturnedLoansByPersonLoanedTo(int personId, DateTime startDate, DateTime endDate);
	}
}

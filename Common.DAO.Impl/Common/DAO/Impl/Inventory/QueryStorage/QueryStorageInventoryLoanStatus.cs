using System;

namespace TechnoPro.Common.DAO.Impl.Inventory.QueryStorage
{
	// Token: 0x020000C0 RID: 192
	internal static class QueryStorageInventoryLoanStatus
	{
		// Token: 0x04000295 RID: 661
		internal const string SQ_GET_LOAN_STATUS_BY_ID = "Select * from InventoryV2_LoanStatus where LoanStatusID=@loanstatusid";

		// Token: 0x04000296 RID: 662
		internal const string SQ_GET_LOAN_STATUS_LIST = "Select * from InventoryV2_LoanStatus";

		// Token: 0x04000297 RID: 663
		internal const string IQ_CREATE_LOAN_STATUS = "INSERT INTO [InventoryV2_LoanStatus]\r\n                       ([LoanStatusName]\r\n                       ,[LoanStatusDescription])\r\n              VALUES\r\n                       (@loanstatusname\r\n                       ,@loanstatusdescription)\r\n\r\n            SET @loanstatusid = scope_identity()";

		// Token: 0x04000298 RID: 664
		internal const string UQ_UPDATE_LOAN_STATUS = "UPDATE [InventoryV2_LoanStatus]\r\n                SET [LoanStatusName] = @loanstatusname\r\n                    ,[LoanStatusDescription] = @loanstatusdescription\r\n                WHERE LoanStatusID=@loanstatusid";
	}
}

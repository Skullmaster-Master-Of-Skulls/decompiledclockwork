using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000788 RID: 1928
	internal static class TransactionFlowDefaults
	{
		// Token: 0x04002E3E RID: 11838
		internal const TransactionFlowOption IssuedTokens = TransactionFlowOption.NotAllowed;

		// Token: 0x04002E3F RID: 11839
		internal const bool Transactions = false;

		// Token: 0x04002E40 RID: 11840
		internal static TransactionProtocol TransactionProtocol = TransactionProtocol.OleTransactions;

		// Token: 0x04002E41 RID: 11841
		internal const string TransactionProtocolString = "OleTransactions";
	}
}

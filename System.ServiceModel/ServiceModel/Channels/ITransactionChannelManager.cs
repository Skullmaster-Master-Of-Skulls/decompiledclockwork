using System;
using System.Collections.Generic;
using System.ServiceModel.Description;
using System.ServiceModel.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A5A RID: 2650
	internal interface ITransactionChannelManager
	{
		// Token: 0x17001905 RID: 6405
		// (get) Token: 0x060068B0 RID: 26800
		// (set) Token: 0x060068B1 RID: 26801
		TransactionProtocol TransactionProtocol { get; set; }

		// Token: 0x17001906 RID: 6406
		// (get) Token: 0x060068B2 RID: 26802
		// (set) Token: 0x060068B3 RID: 26803
		TransactionFlowOption FlowIssuedTokens { get; set; }

		// Token: 0x17001907 RID: 6407
		// (get) Token: 0x060068B4 RID: 26804
		IDictionary<DirectionalAction, TransactionFlowOption> Dictionary { get; }

		// Token: 0x060068B5 RID: 26805
		TransactionFlowOption GetTransaction(MessageDirection direction, string action);

		// Token: 0x17001908 RID: 6408
		// (get) Token: 0x060068B6 RID: 26806
		SecurityStandardsManager StandardsManager { get; }
	}
}

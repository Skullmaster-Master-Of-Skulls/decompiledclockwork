using System;
using System.ServiceModel.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000794 RID: 1940
	internal static class NetTcpDefaults
	{
		// Token: 0x17001291 RID: 4753
		// (get) Token: 0x060049B0 RID: 18864 RVA: 0x0010ECDB File Offset: 0x0010CEDB
		internal static TransactionProtocol TransactionProtocol
		{
			get
			{
				return TransactionProtocol.Default;
			}
		}

		// Token: 0x17001292 RID: 4754
		// (get) Token: 0x060049B1 RID: 18865 RVA: 0x0010ECE2 File Offset: 0x0010CEE2
		internal static SecurityAlgorithmSuite MessageSecurityAlgorithmSuite
		{
			get
			{
				return SecurityAlgorithmSuite.Default;
			}
		}

		// Token: 0x04002EA7 RID: 11943
		internal const MessageCredentialType MessageSecurityClientCredentialType = MessageCredentialType.Windows;

		// Token: 0x04002EA8 RID: 11944
		internal const bool TransactionsEnabled = false;
	}
}

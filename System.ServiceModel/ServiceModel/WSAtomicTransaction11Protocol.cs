using System;

namespace System.ServiceModel
{
	// Token: 0x0200017B RID: 379
	internal class WSAtomicTransaction11Protocol : TransactionProtocol
	{
		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x00028CDE File Offset: 0x00026EDE
		internal static TransactionProtocol Instance
		{
			get
			{
				return WSAtomicTransaction11Protocol.instance;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x00028CE5 File Offset: 0x00026EE5
		internal override string Name
		{
			get
			{
				return "WSAtomicTransaction11";
			}
		}

		// Token: 0x04000BF4 RID: 3060
		private static TransactionProtocol instance = new WSAtomicTransaction11Protocol();
	}
}

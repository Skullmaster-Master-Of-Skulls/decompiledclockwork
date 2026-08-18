using System;
using System.Transactions;

namespace OracleInternal.MTS
{
	// Token: 0x02000136 RID: 310
	internal class OnAbortEventArgs : OnCommitEventArgs
	{
		// Token: 0x06000C8B RID: 3211 RVA: 0x0008BE78 File Offset: 0x0008A078
		internal OnAbortEventArgs(Enlistment enlistment) : base(enlistment)
		{
		}
	}
}

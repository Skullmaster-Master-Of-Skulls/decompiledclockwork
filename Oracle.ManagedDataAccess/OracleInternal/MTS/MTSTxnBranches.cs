using System;
using OracleInternal.Common;

namespace OracleInternal.MTS
{
	// Token: 0x02000120 RID: 288
	internal class MTSTxnBranches : SyncQueueList<MTSTxnBranch>
	{
		// Token: 0x06000C5A RID: 3162 RVA: 0x0008A8A4 File Offset: 0x00088AA4
		internal void ClearBranches()
		{
			base.Clear();
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x0008A8AC File Offset: 0x00088AAC
		public MTSTxnBranches() : base(int.MaxValue)
		{
		}
	}
}

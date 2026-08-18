using System;
using OracleInternal.Common;

namespace OracleInternal.MTS
{
	// Token: 0x0200012E RID: 302
	internal class MTSTxnRMCache
	{
		// Token: 0x06000C79 RID: 3193 RVA: 0x0008BA74 File Offset: 0x00089C74
		internal static MTSTxnRM GetRM(bool bIsCCP)
		{
			MTSTxnRM result = null;
			if (!MTSTxnRMCache.m_CCPTxnRMs.Dequeue(out result))
			{
				result = new CCPMTSTxnRM();
			}
			return result;
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x0008BA98 File Offset: 0x00089C98
		internal static void PutRM(MTSTxnRM txnRM)
		{
			MTSTxnRMCache.m_CCPTxnRMs.AddIfNotExist(txnRM);
		}

		// Token: 0x04000D99 RID: 3481
		private static SyncQueueList<MTSTxnRM> m_CCPTxnRMs = new SyncQueueList<MTSTxnRM>(int.MaxValue);
	}
}

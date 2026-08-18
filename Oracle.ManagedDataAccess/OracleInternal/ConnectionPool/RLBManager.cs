using System;
using OracleInternal.Common;

namespace OracleInternal.ConnectionPool
{
	// Token: 0x020000E0 RID: 224
	internal static class RLBManager
	{
		// Token: 0x060008BC RID: 2236 RVA: 0x0005E16C File Offset: 0x0005C36C
		public static RLB Put(string message)
		{
			RLB rlb = new RLB(message);
			RLB result = null;
			if (rlb.m_bStatus)
			{
				RLB rlb2 = RLBManager.m_rlbData[rlb.m_id];
				if (rlb2 == null || rlb2.m_dateTime.CompareTo(rlb.m_dateTime) <= 0)
				{
					RLBManager.m_rlbData[rlb.m_id] = rlb;
					result = rlb;
				}
			}
			return result;
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0005E1C8 File Offset: 0x0005C3C8
		public static RLB Get(string id)
		{
			return RLBManager.m_rlbData[id];
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0005E1D8 File Offset: 0x0005C3D8
		public static void InvalidateRLBData(string id)
		{
			RLBManager.m_rlbData[id] = null;
		}

		// Token: 0x04000BB7 RID: 2999
		private static SyncDictionary<string, RLB> m_rlbData = new SyncDictionary<string, RLB>();
	}
}

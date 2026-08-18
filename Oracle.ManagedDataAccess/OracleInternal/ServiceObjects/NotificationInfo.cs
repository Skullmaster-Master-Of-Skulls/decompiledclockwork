using System;

namespace OracleInternal.ServiceObjects
{
	// Token: 0x020001A9 RID: 425
	internal class NotificationInfo
	{
		// Token: 0x06000FE5 RID: 4069 RVA: 0x000A473C File Offset: 0x000A293C
		internal NotificationInfo(short csId, int numOfRegistrations, int[] regId, bool bTimeout, byte[] notifInfo)
		{
			this.m_csId = csId;
			this.m_numOfRegistrations = numOfRegistrations;
			this.m_regId = regId;
			this.m_bTimeoutEvent = bTimeout;
			this.m_notifInfo = notifInfo;
		}

		// Token: 0x0400129A RID: 4762
		internal short m_csId;

		// Token: 0x0400129B RID: 4763
		internal int[] m_regId;

		// Token: 0x0400129C RID: 4764
		internal bool m_bTimeoutEvent;

		// Token: 0x0400129D RID: 4765
		internal byte[] m_notifInfo;

		// Token: 0x0400129E RID: 4766
		internal int m_numOfRegistrations;
	}
}

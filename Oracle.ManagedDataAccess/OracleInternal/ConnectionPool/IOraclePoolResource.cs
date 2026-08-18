using System;

namespace OracleInternal.ConnectionPool
{
	// Token: 0x020000CC RID: 204
	internal interface IOraclePoolResource
	{
		// Token: 0x060007FD RID: 2045
		void Connect(ConnectionString cs, bool bOpenEndUserSession, CriteriaCtx criteriaCtx, string instanceName = null);

		// Token: 0x060007FE RID: 2046
		void AttachServerProcess(long sessionFlags, bool bUseDRCPMultiTag, ref long s2cSessionFlags);

		// Token: 0x060007FF RID: 2047
		void DetachServerProcess(string drcpTagName, bool bUseDRCPMultiTag);

		// Token: 0x06000800 RID: 2048
		void DisConnect(CriteriaCtx criteriaCtx);
	}
}

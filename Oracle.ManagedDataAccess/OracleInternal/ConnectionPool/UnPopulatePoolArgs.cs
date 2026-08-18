using System;

namespace OracleInternal.ConnectionPool
{
	// Token: 0x020000D8 RID: 216
	internal class UnPopulatePoolArgs
	{
		// Token: 0x060008A6 RID: 2214 RVA: 0x0005C7B0 File Offset: 0x0005A9B0
		public UnPopulatePoolArgs(string serviceName, string instanceName, int decrementCount)
		{
			this.m_serviceName = serviceName;
			this.m_instanceName = instanceName;
			this.m_decrementCount = decrementCount;
		}

		// Token: 0x04000B8F RID: 2959
		internal string m_instanceName;

		// Token: 0x04000B90 RID: 2960
		internal int m_decrementCount;

		// Token: 0x04000B91 RID: 2961
		internal string m_serviceName;
	}
}

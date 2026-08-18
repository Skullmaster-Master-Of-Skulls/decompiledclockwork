using System;

namespace System.Data.ProviderBase
{
	// Token: 0x020001DE RID: 478
	internal class DbConnectionPoolGroupProviderInfo
	{
		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06001AAB RID: 6827 RVA: 0x0025F058 File Offset: 0x0025E458
		// (set) Token: 0x06001AAC RID: 6828 RVA: 0x0025F078 File Offset: 0x0025E478
		internal DbConnectionPoolGroup PoolGroup
		{
			get
			{
				return this._poolGroup;
			}
			set
			{
				this._poolGroup = value;
			}
		}

		// Token: 0x04000FC5 RID: 4037
		private DbConnectionPoolGroup _poolGroup;
	}
}

using System;

namespace System.Data.ProviderBase
{
	// Token: 0x0200009D RID: 157
	internal class DbConnectionPoolGroupProviderInfo
	{
		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x00078F84 File Offset: 0x00078384
		// (set) Token: 0x06000860 RID: 2144 RVA: 0x00078FA4 File Offset: 0x000783A4
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

		// Token: 0x04000567 RID: 1383
		private DbConnectionPoolGroup _poolGroup;
	}
}

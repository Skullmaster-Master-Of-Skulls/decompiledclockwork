using System;

namespace System.Data.ProviderBase
{
	// Token: 0x020002C7 RID: 711
	internal class DbConnectionPoolGroupProviderInfo
	{
		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06002B07 RID: 11015 RVA: 0x0011AC48 File Offset: 0x0011A048
		// (set) Token: 0x06002B08 RID: 11016 RVA: 0x0011AC5C File Offset: 0x0011A05C
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

		// Token: 0x04001B83 RID: 7043
		private DbConnectionPoolGroup _poolGroup;
	}
}

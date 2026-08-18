using System;

namespace System.Data.ProviderBase
{
	// Token: 0x020002C9 RID: 713
	internal sealed class DbConnectionPoolGroupOptions
	{
		// Token: 0x06002B15 RID: 11029 RVA: 0x0011B018 File Offset: 0x0011A418
		public DbConnectionPoolGroupOptions(bool poolByIdentity, int minPoolSize, int maxPoolSize, int creationTimeout, int loadBalanceTimeout, bool hasTransactionAffinity)
		{
			this._poolByIdentity = poolByIdentity;
			this._minPoolSize = minPoolSize;
			this._maxPoolSize = maxPoolSize;
			this._creationTimeout = creationTimeout;
			if (loadBalanceTimeout != 0)
			{
				this._loadBalanceTimeout = new TimeSpan(0, 0, loadBalanceTimeout);
				this._useLoadBalancing = true;
			}
			this._hasTransactionAffinity = hasTransactionAffinity;
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06002B16 RID: 11030 RVA: 0x0011B06C File Offset: 0x0011A46C
		public int CreationTimeout
		{
			get
			{
				return this._creationTimeout;
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06002B17 RID: 11031 RVA: 0x0011B080 File Offset: 0x0011A480
		public bool HasTransactionAffinity
		{
			get
			{
				return this._hasTransactionAffinity;
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06002B18 RID: 11032 RVA: 0x0011B094 File Offset: 0x0011A494
		public TimeSpan LoadBalanceTimeout
		{
			get
			{
				return this._loadBalanceTimeout;
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06002B19 RID: 11033 RVA: 0x0011B0A8 File Offset: 0x0011A4A8
		public int MaxPoolSize
		{
			get
			{
				return this._maxPoolSize;
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06002B1A RID: 11034 RVA: 0x0011B0BC File Offset: 0x0011A4BC
		public int MinPoolSize
		{
			get
			{
				return this._minPoolSize;
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06002B1B RID: 11035 RVA: 0x0011B0D0 File Offset: 0x0011A4D0
		public bool PoolByIdentity
		{
			get
			{
				return this._poolByIdentity;
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06002B1C RID: 11036 RVA: 0x0011B0E4 File Offset: 0x0011A4E4
		public bool UseLoadBalancing
		{
			get
			{
				return this._useLoadBalancing;
			}
		}

		// Token: 0x04001B91 RID: 7057
		private readonly bool _poolByIdentity;

		// Token: 0x04001B92 RID: 7058
		private readonly int _minPoolSize;

		// Token: 0x04001B93 RID: 7059
		private readonly int _maxPoolSize;

		// Token: 0x04001B94 RID: 7060
		private readonly int _creationTimeout;

		// Token: 0x04001B95 RID: 7061
		private readonly TimeSpan _loadBalanceTimeout;

		// Token: 0x04001B96 RID: 7062
		private readonly bool _hasTransactionAffinity;

		// Token: 0x04001B97 RID: 7063
		private readonly bool _useLoadBalancing;
	}
}

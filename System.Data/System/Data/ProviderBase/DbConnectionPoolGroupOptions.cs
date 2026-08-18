using System;

namespace System.Data.ProviderBase
{
	// Token: 0x02000279 RID: 633
	internal sealed class DbConnectionPoolGroupOptions
	{
		// Token: 0x06002164 RID: 8548 RVA: 0x00285978 File Offset: 0x00284D78
		public DbConnectionPoolGroupOptions(bool poolByIdentity, int minPoolSize, int maxPoolSize, int creationTimeout, int loadBalanceTimeout, bool hasTransactionAffinity, bool useDeactivateQueue)
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
			this._useDeactivateQueue = useDeactivateQueue;
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06002165 RID: 8549 RVA: 0x002859D8 File Offset: 0x00284DD8
		public int CreationTimeout
		{
			get
			{
				return this._creationTimeout;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06002166 RID: 8550 RVA: 0x002859F8 File Offset: 0x00284DF8
		public bool HasTransactionAffinity
		{
			get
			{
				return this._hasTransactionAffinity;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06002167 RID: 8551 RVA: 0x00285A18 File Offset: 0x00284E18
		public TimeSpan LoadBalanceTimeout
		{
			get
			{
				return this._loadBalanceTimeout;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06002168 RID: 8552 RVA: 0x00285A38 File Offset: 0x00284E38
		public int MaxPoolSize
		{
			get
			{
				return this._maxPoolSize;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06002169 RID: 8553 RVA: 0x00285A58 File Offset: 0x00284E58
		public int MinPoolSize
		{
			get
			{
				return this._minPoolSize;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x0600216A RID: 8554 RVA: 0x00285A78 File Offset: 0x00284E78
		public bool PoolByIdentity
		{
			get
			{
				return this._poolByIdentity;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x0600216B RID: 8555 RVA: 0x00285A98 File Offset: 0x00284E98
		public bool UseDeactivateQueue
		{
			get
			{
				return this._useDeactivateQueue;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x0600216C RID: 8556 RVA: 0x00285AB8 File Offset: 0x00284EB8
		public bool UseLoadBalancing
		{
			get
			{
				return this._useLoadBalancing;
			}
		}

		// Token: 0x040015C3 RID: 5571
		private readonly bool _poolByIdentity;

		// Token: 0x040015C4 RID: 5572
		private readonly int _minPoolSize;

		// Token: 0x040015C5 RID: 5573
		private readonly int _maxPoolSize;

		// Token: 0x040015C6 RID: 5574
		private readonly int _creationTimeout;

		// Token: 0x040015C7 RID: 5575
		private readonly TimeSpan _loadBalanceTimeout;

		// Token: 0x040015C8 RID: 5576
		private readonly bool _hasTransactionAffinity;

		// Token: 0x040015C9 RID: 5577
		private readonly bool _useDeactivateQueue;

		// Token: 0x040015CA RID: 5578
		private readonly bool _useLoadBalancing;
	}
}

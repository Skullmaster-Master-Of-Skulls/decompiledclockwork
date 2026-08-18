using System;

namespace System.Runtime.Collections
{
	// Token: 0x02000052 RID: 82
	internal class ObjectCacheSettings
	{
		// Token: 0x06000330 RID: 816 RVA: 0x0001102C File Offset: 0x0000F22C
		public ObjectCacheSettings()
		{
			this.CacheLimit = 64;
			this.IdleTimeout = ObjectCacheSettings.DefaultIdleTimeout;
			this.LeaseTimeout = ObjectCacheSettings.DefaultLeaseTimeout;
			this.PurgeFrequency = 32;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0001105A File Offset: 0x0000F25A
		private ObjectCacheSettings(ObjectCacheSettings other)
		{
			this.CacheLimit = other.CacheLimit;
			this.IdleTimeout = other.IdleTimeout;
			this.LeaseTimeout = other.LeaseTimeout;
			this.PurgeFrequency = other.PurgeFrequency;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00011092 File Offset: 0x0000F292
		internal ObjectCacheSettings Clone()
		{
			return new ObjectCacheSettings(this);
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0001109A File Offset: 0x0000F29A
		// (set) Token: 0x06000334 RID: 820 RVA: 0x000110A2 File Offset: 0x0000F2A2
		public int CacheLimit
		{
			get
			{
				return this.cacheLimit;
			}
			set
			{
				this.cacheLimit = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000335 RID: 821 RVA: 0x000110AB File Offset: 0x0000F2AB
		// (set) Token: 0x06000336 RID: 822 RVA: 0x000110B3 File Offset: 0x0000F2B3
		public TimeSpan IdleTimeout
		{
			get
			{
				return this.idleTimeout;
			}
			set
			{
				this.idleTimeout = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000337 RID: 823 RVA: 0x000110BC File Offset: 0x0000F2BC
		// (set) Token: 0x06000338 RID: 824 RVA: 0x000110C4 File Offset: 0x0000F2C4
		public TimeSpan LeaseTimeout
		{
			get
			{
				return this.leaseTimeout;
			}
			set
			{
				this.leaseTimeout = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000339 RID: 825 RVA: 0x000110CD File Offset: 0x0000F2CD
		// (set) Token: 0x0600033A RID: 826 RVA: 0x000110D5 File Offset: 0x0000F2D5
		public int PurgeFrequency
		{
			get
			{
				return this.purgeFrequency;
			}
			set
			{
				this.purgeFrequency = value;
			}
		}

		// Token: 0x040001B8 RID: 440
		private int cacheLimit;

		// Token: 0x040001B9 RID: 441
		private TimeSpan idleTimeout;

		// Token: 0x040001BA RID: 442
		private TimeSpan leaseTimeout;

		// Token: 0x040001BB RID: 443
		private int purgeFrequency;

		// Token: 0x040001BC RID: 444
		private const int DefaultCacheLimit = 64;

		// Token: 0x040001BD RID: 445
		private const int DefaultPurgeFrequency = 32;

		// Token: 0x040001BE RID: 446
		private static TimeSpan DefaultIdleTimeout = TimeSpan.FromMinutes(2.0);

		// Token: 0x040001BF RID: 447
		private static TimeSpan DefaultLeaseTimeout = TimeSpan.FromMinutes(5.0);
	}
}

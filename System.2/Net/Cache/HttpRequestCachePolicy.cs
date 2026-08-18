using System;
using System.Globalization;

namespace System.Net.Cache
{
	// Token: 0x02000315 RID: 789
	public class HttpRequestCachePolicy : RequestCachePolicy
	{
		// Token: 0x06001C1D RID: 7197 RVA: 0x00085EF1 File Offset: 0x000840F1
		public HttpRequestCachePolicy() : this(HttpRequestCacheLevel.Default)
		{
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x00085EFC File Offset: 0x000840FC
		public HttpRequestCachePolicy(HttpRequestCacheLevel level) : base(HttpRequestCachePolicy.MapLevel(level))
		{
			this.m_Level = level;
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x00085F48 File Offset: 0x00084148
		public HttpRequestCachePolicy(HttpCacheAgeControl cacheAgeControl, TimeSpan ageOrFreshOrStale) : this(HttpRequestCacheLevel.Default)
		{
			switch (cacheAgeControl)
			{
			case HttpCacheAgeControl.MinFresh:
				this.m_MinFresh = ageOrFreshOrStale;
				return;
			case HttpCacheAgeControl.MaxAge:
				this.m_MaxAge = ageOrFreshOrStale;
				return;
			case HttpCacheAgeControl.MaxStale:
				this.m_MaxStale = ageOrFreshOrStale;
				return;
			}
			throw new ArgumentException(SR.GetString("net_invalid_enum", new object[]
			{
				"HttpCacheAgeControl"
			}), "cacheAgeControl");
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x00085FB0 File Offset: 0x000841B0
		public HttpRequestCachePolicy(HttpCacheAgeControl cacheAgeControl, TimeSpan maxAge, TimeSpan freshOrStale) : this(HttpRequestCacheLevel.Default)
		{
			switch (cacheAgeControl)
			{
			case HttpCacheAgeControl.MinFresh:
				this.m_MinFresh = freshOrStale;
				return;
			case HttpCacheAgeControl.MaxAge:
				this.m_MaxAge = maxAge;
				return;
			case HttpCacheAgeControl.MaxAgeAndMinFresh:
				this.m_MaxAge = maxAge;
				this.m_MinFresh = freshOrStale;
				return;
			case HttpCacheAgeControl.MaxStale:
				this.m_MaxStale = freshOrStale;
				return;
			case HttpCacheAgeControl.MaxAgeAndMaxStale:
				this.m_MaxAge = maxAge;
				this.m_MaxStale = freshOrStale;
				return;
			}
			throw new ArgumentException(SR.GetString("net_invalid_enum", new object[]
			{
				"HttpCacheAgeControl"
			}), "cacheAgeControl");
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x0008603E File Offset: 0x0008423E
		public HttpRequestCachePolicy(DateTime cacheSyncDate) : this(HttpRequestCacheLevel.Default)
		{
			this.m_LastSyncDateUtc = cacheSyncDate.ToUniversalTime();
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x00086054 File Offset: 0x00084254
		public HttpRequestCachePolicy(HttpCacheAgeControl cacheAgeControl, TimeSpan maxAge, TimeSpan freshOrStale, DateTime cacheSyncDate) : this(cacheAgeControl, maxAge, freshOrStale)
		{
			this.m_LastSyncDateUtc = cacheSyncDate.ToUniversalTime();
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001C23 RID: 7203 RVA: 0x0008606C File Offset: 0x0008426C
		public new HttpRequestCacheLevel Level
		{
			get
			{
				return this.m_Level;
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06001C24 RID: 7204 RVA: 0x00086074 File Offset: 0x00084274
		public DateTime CacheSyncDate
		{
			get
			{
				if (this.m_LastSyncDateUtc == DateTime.MinValue || this.m_LastSyncDateUtc == DateTime.MaxValue)
				{
					return this.m_LastSyncDateUtc;
				}
				return this.m_LastSyncDateUtc.ToLocalTime();
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001C25 RID: 7205 RVA: 0x000860AC File Offset: 0x000842AC
		internal DateTime InternalCacheSyncDateUtc
		{
			get
			{
				return this.m_LastSyncDateUtc;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001C26 RID: 7206 RVA: 0x000860B4 File Offset: 0x000842B4
		public TimeSpan MaxAge
		{
			get
			{
				return this.m_MaxAge;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06001C27 RID: 7207 RVA: 0x000860BC File Offset: 0x000842BC
		public TimeSpan MinFresh
		{
			get
			{
				return this.m_MinFresh;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06001C28 RID: 7208 RVA: 0x000860C4 File Offset: 0x000842C4
		public TimeSpan MaxStale
		{
			get
			{
				return this.m_MaxStale;
			}
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x000860CC File Offset: 0x000842CC
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"Level:",
				this.m_Level.ToString(),
				(this.m_MaxAge == TimeSpan.MaxValue) ? string.Empty : (" MaxAge:" + this.m_MaxAge.ToString()),
				(this.m_MinFresh == TimeSpan.MinValue) ? string.Empty : (" MinFresh:" + this.m_MinFresh.ToString()),
				(this.m_MaxStale == TimeSpan.MinValue) ? string.Empty : (" MaxStale:" + this.m_MaxStale.ToString()),
				(this.CacheSyncDate == DateTime.MinValue) ? string.Empty : (" CacheSyncDate:" + this.CacheSyncDate.ToString(CultureInfo.CurrentCulture))
			});
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x000861DE File Offset: 0x000843DE
		private static RequestCacheLevel MapLevel(HttpRequestCacheLevel level)
		{
			if (level <= HttpRequestCacheLevel.NoCacheNoStore)
			{
				return (RequestCacheLevel)level;
			}
			if (level == HttpRequestCacheLevel.CacheOrNextCacheOnly)
			{
				return RequestCacheLevel.CacheOnly;
			}
			if (level == HttpRequestCacheLevel.Refresh)
			{
				return RequestCacheLevel.Reload;
			}
			throw new ArgumentOutOfRangeException("level");
		}

		// Token: 0x04001B71 RID: 7025
		internal static readonly HttpRequestCachePolicy BypassCache = new HttpRequestCachePolicy(HttpRequestCacheLevel.BypassCache);

		// Token: 0x04001B72 RID: 7026
		private HttpRequestCacheLevel m_Level;

		// Token: 0x04001B73 RID: 7027
		private DateTime m_LastSyncDateUtc = DateTime.MinValue;

		// Token: 0x04001B74 RID: 7028
		private TimeSpan m_MaxAge = TimeSpan.MaxValue;

		// Token: 0x04001B75 RID: 7029
		private TimeSpan m_MinFresh = TimeSpan.MinValue;

		// Token: 0x04001B76 RID: 7030
		private TimeSpan m_MaxStale = TimeSpan.MinValue;
	}
}

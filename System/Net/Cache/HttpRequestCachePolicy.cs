using System;
using System.Globalization;

namespace System.Net.Cache
{
	// Token: 0x0200056F RID: 1391
	public class HttpRequestCachePolicy : RequestCachePolicy
	{
		// Token: 0x06002A9D RID: 10909 RVA: 0x000B5080 File Offset: 0x000B4080
		public HttpRequestCachePolicy() : this(HttpRequestCacheLevel.Default)
		{
		}

		// Token: 0x06002A9E RID: 10910 RVA: 0x000B508C File Offset: 0x000B408C
		public HttpRequestCachePolicy(HttpRequestCacheLevel level) : base(HttpRequestCachePolicy.MapLevel(level))
		{
			this.m_Level = level;
		}

		// Token: 0x06002A9F RID: 10911 RVA: 0x000B50D8 File Offset: 0x000B40D8
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

		// Token: 0x06002AA0 RID: 10912 RVA: 0x000B5144 File Offset: 0x000B4144
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

		// Token: 0x06002AA1 RID: 10913 RVA: 0x000B51D6 File Offset: 0x000B41D6
		public HttpRequestCachePolicy(DateTime cacheSyncDate) : this(HttpRequestCacheLevel.Default)
		{
			this.m_LastSyncDateUtc = cacheSyncDate.ToUniversalTime();
		}

		// Token: 0x06002AA2 RID: 10914 RVA: 0x000B51EC File Offset: 0x000B41EC
		public HttpRequestCachePolicy(HttpCacheAgeControl cacheAgeControl, TimeSpan maxAge, TimeSpan freshOrStale, DateTime cacheSyncDate) : this(cacheAgeControl, maxAge, freshOrStale)
		{
			this.m_LastSyncDateUtc = cacheSyncDate.ToUniversalTime();
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06002AA3 RID: 10915 RVA: 0x000B5204 File Offset: 0x000B4204
		public new HttpRequestCacheLevel Level
		{
			get
			{
				return this.m_Level;
			}
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06002AA4 RID: 10916 RVA: 0x000B520C File Offset: 0x000B420C
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

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06002AA5 RID: 10917 RVA: 0x000B5244 File Offset: 0x000B4244
		internal DateTime InternalCacheSyncDateUtc
		{
			get
			{
				return this.m_LastSyncDateUtc;
			}
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06002AA6 RID: 10918 RVA: 0x000B524C File Offset: 0x000B424C
		public TimeSpan MaxAge
		{
			get
			{
				return this.m_MaxAge;
			}
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06002AA7 RID: 10919 RVA: 0x000B5254 File Offset: 0x000B4254
		public TimeSpan MinFresh
		{
			get
			{
				return this.m_MinFresh;
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06002AA8 RID: 10920 RVA: 0x000B525C File Offset: 0x000B425C
		public TimeSpan MaxStale
		{
			get
			{
				return this.m_MaxStale;
			}
		}

		// Token: 0x06002AA9 RID: 10921 RVA: 0x000B5264 File Offset: 0x000B4264
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

		// Token: 0x06002AAA RID: 10922 RVA: 0x000B5377 File Offset: 0x000B4377
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

		// Token: 0x04002932 RID: 10546
		internal static readonly HttpRequestCachePolicy BypassCache = new HttpRequestCachePolicy(HttpRequestCacheLevel.BypassCache);

		// Token: 0x04002933 RID: 10547
		private HttpRequestCacheLevel m_Level;

		// Token: 0x04002934 RID: 10548
		private DateTime m_LastSyncDateUtc = DateTime.MinValue;

		// Token: 0x04002935 RID: 10549
		private TimeSpan m_MaxAge = TimeSpan.MaxValue;

		// Token: 0x04002936 RID: 10550
		private TimeSpan m_MinFresh = TimeSpan.MinValue;

		// Token: 0x04002937 RID: 10551
		private TimeSpan m_MaxStale = TimeSpan.MinValue;
	}
}

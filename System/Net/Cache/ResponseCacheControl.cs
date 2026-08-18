using System;
using System.Text;

namespace System.Net.Cache
{
	// Token: 0x02000562 RID: 1378
	internal class ResponseCacheControl
	{
		// Token: 0x06002A2F RID: 10799 RVA: 0x000B1688 File Offset: 0x000B0688
		internal ResponseCacheControl()
		{
			this.MaxAge = (this.SMaxAge = -1);
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06002A30 RID: 10800 RVA: 0x000B16AC File Offset: 0x000B06AC
		internal bool IsNotEmpty
		{
			get
			{
				return this.Public || this.Private || this.NoCache || this.NoStore || this.MustRevalidate || this.ProxyRevalidate || this.MaxAge != -1 || this.SMaxAge != -1;
			}
		}

		// Token: 0x06002A31 RID: 10801 RVA: 0x000B1700 File Offset: 0x000B0700
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.Public)
			{
				stringBuilder.Append(" public");
			}
			if (this.Private)
			{
				stringBuilder.Append(" private");
				if (this.PrivateHeaders != null)
				{
					stringBuilder.Append('=');
					for (int i = 0; i < this.PrivateHeaders.Length - 1; i++)
					{
						stringBuilder.Append(this.PrivateHeaders[i]).Append(',');
					}
					stringBuilder.Append(this.PrivateHeaders[this.PrivateHeaders.Length - 1]);
				}
			}
			if (this.NoCache)
			{
				stringBuilder.Append(" no-cache");
				if (this.NoCacheHeaders != null)
				{
					stringBuilder.Append('=');
					for (int j = 0; j < this.NoCacheHeaders.Length - 1; j++)
					{
						stringBuilder.Append(this.NoCacheHeaders[j]).Append(',');
					}
					stringBuilder.Append(this.NoCacheHeaders[this.NoCacheHeaders.Length - 1]);
				}
			}
			if (this.NoStore)
			{
				stringBuilder.Append(" no-store");
			}
			if (this.MustRevalidate)
			{
				stringBuilder.Append(" must-revalidate");
			}
			if (this.ProxyRevalidate)
			{
				stringBuilder.Append(" proxy-revalidate");
			}
			if (this.MaxAge != -1)
			{
				stringBuilder.Append(" max-age=").Append(this.MaxAge);
			}
			if (this.SMaxAge != -1)
			{
				stringBuilder.Append(" s-maxage=").Append(this.SMaxAge);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040028DE RID: 10462
		internal bool Public;

		// Token: 0x040028DF RID: 10463
		internal bool Private;

		// Token: 0x040028E0 RID: 10464
		internal string[] PrivateHeaders;

		// Token: 0x040028E1 RID: 10465
		internal bool NoCache;

		// Token: 0x040028E2 RID: 10466
		internal string[] NoCacheHeaders;

		// Token: 0x040028E3 RID: 10467
		internal bool NoStore;

		// Token: 0x040028E4 RID: 10468
		internal bool MustRevalidate;

		// Token: 0x040028E5 RID: 10469
		internal bool ProxyRevalidate;

		// Token: 0x040028E6 RID: 10470
		internal int MaxAge;

		// Token: 0x040028E7 RID: 10471
		internal int SMaxAge;
	}
}

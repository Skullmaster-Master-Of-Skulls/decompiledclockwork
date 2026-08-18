using System;
using System.Text;

namespace System.Net.Cache
{
	// Token: 0x0200030B RID: 779
	internal class ResponseCacheControl
	{
		// Token: 0x06001BD6 RID: 7126 RVA: 0x00084624 File Offset: 0x00082824
		internal ResponseCacheControl()
		{
			this.MaxAge = (this.SMaxAge = -1);
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001BD7 RID: 7127 RVA: 0x00084648 File Offset: 0x00082848
		internal bool IsNotEmpty
		{
			get
			{
				return this.Public || this.Private || this.NoCache || this.NoStore || this.MustRevalidate || this.ProxyRevalidate || this.MaxAge != -1 || this.SMaxAge != -1;
			}
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x0008469C File Offset: 0x0008289C
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

		// Token: 0x04001B34 RID: 6964
		internal bool Public;

		// Token: 0x04001B35 RID: 6965
		internal bool Private;

		// Token: 0x04001B36 RID: 6966
		internal string[] PrivateHeaders;

		// Token: 0x04001B37 RID: 6967
		internal bool NoCache;

		// Token: 0x04001B38 RID: 6968
		internal string[] NoCacheHeaders;

		// Token: 0x04001B39 RID: 6969
		internal bool NoStore;

		// Token: 0x04001B3A RID: 6970
		internal bool MustRevalidate;

		// Token: 0x04001B3B RID: 6971
		internal bool ProxyRevalidate;

		// Token: 0x04001B3C RID: 6972
		internal int MaxAge;

		// Token: 0x04001B3D RID: 6973
		internal int SMaxAge;
	}
}

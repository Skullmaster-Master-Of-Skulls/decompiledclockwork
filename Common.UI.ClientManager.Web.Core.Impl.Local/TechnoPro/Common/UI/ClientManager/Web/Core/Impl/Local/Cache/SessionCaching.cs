using System;
using System.Web;
using System.Web.SessionState;
using TechnoPro.Common.UI.ClientManager.Web.Core.Caching;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Cache
{
	// Token: 0x0200001F RID: 31
	public class SessionCaching : ISessionCaching
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x000071B6 File Offset: 0x000053B6
		protected SessionCaching()
		{
			this.cache = HttpContext.Current.Session;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x000071D0 File Offset: 0x000053D0
		public static SessionCaching CurrentInstance
		{
			get
			{
				return new SessionCaching();
			}
		}

		// Token: 0x1700001A RID: 26
		public object this[string key]
		{
			get
			{
				return this.cache[key];
			}
			set
			{
				this.cache[key] = value;
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00007217 File Offset: 0x00005417
		public void Insert(string key, object value)
		{
			this.cache.Add(key, value);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00007217 File Offset: 0x00005417
		public void Insert(string key, object value, int numSecondsUntilExpires)
		{
			this.cache.Add(key, value);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00007217 File Offset: 0x00005417
		public void Insert(string key, object value, TimeSpan timeSpanUntilExpires)
		{
			this.cache.Add(key, value);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00007228 File Offset: 0x00005428
		public void Clear(string key)
		{
			bool flag = this.cache[key] != null;
			if (flag)
			{
				this.cache.Remove(key);
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00007256 File Offset: 0x00005456
		public void Remove(string key)
		{
			this.cache.Remove(key);
		}

		// Token: 0x0400001D RID: 29
		private HttpSessionState cache;
	}
}

using System;
using System.Web;

namespace Telerik.Web.UI
{
	// Token: 0x02000E28 RID: 3624
	internal class SessionPersistentMedia : IPersistentMediaExtended, IPersistentMedia
	{
		// Token: 0x06008961 RID: 35169 RVA: 0x001F5B84 File Offset: 0x001F3D84
		public T Get<T>(string key) where T : class
		{
			if (this.CurrentContext != null && this.CurrentContext.Session != null)
			{
				return this.CurrentContext.Session[key] as T;
			}
			return default(T);
		}

		// Token: 0x06008962 RID: 35170 RVA: 0x001F5BCB File Offset: 0x001F3DCB
		public void Add<T>(string key, T item) where T : class
		{
			if (this.CurrentContext != null && this.CurrentContext.Session != null)
			{
				this.CurrentContext.Session.Add(key, item);
			}
		}

		// Token: 0x06008963 RID: 35171 RVA: 0x001F5BF9 File Offset: 0x001F3DF9
		public void Remove(string key)
		{
			this.CurrentContext.Session.Remove(key);
		}

		// Token: 0x17002B78 RID: 11128
		// (get) Token: 0x06008964 RID: 35172 RVA: 0x001F5C0C File Offset: 0x001F3E0C
		private HttpContext CurrentContext
		{
			get
			{
				return HttpContext.Current;
			}
		}
	}
}

using System;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Caching
{
	// Token: 0x02000015 RID: 21
	public interface ISessionCaching
	{
		// Token: 0x17000002 RID: 2
		object this[string key]
		{
			get;
			set;
		}

		// Token: 0x0600004B RID: 75
		void Insert(string key, object value);

		// Token: 0x0600004C RID: 76
		void Insert(string key, object value, int numSecondsUntilExpires);

		// Token: 0x0600004D RID: 77
		void Insert(string key, object value, TimeSpan timeSpanUntilExpires);

		// Token: 0x0600004E RID: 78
		void Clear(string key);

		// Token: 0x0600004F RID: 79
		void Remove(string key);
	}
}

using System;

namespace AjaxControlToolkit.Bundling
{
	// Token: 0x0200005E RID: 94
	public interface ICache
	{
		// Token: 0x0600032D RID: 813
		T Get<T>(string key) where T : class;

		// Token: 0x0600032E RID: 814
		void Set(string key, object value);

		// Token: 0x0600032F RID: 815
		void Set(string key, object value, string fileCacheDependencyName);

		// Token: 0x06000330 RID: 816
		void Remove(string key);
	}
}

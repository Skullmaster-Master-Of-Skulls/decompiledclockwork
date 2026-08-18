using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration.Provider;

namespace System.Web.Caching
{
	// Token: 0x02000876 RID: 2166
	public abstract class CacheStoreProvider : ProviderBase, IDisposable
	{
		// Token: 0x17001C95 RID: 7317
		// (get) Token: 0x06006604 RID: 26116
		public abstract long ItemCount { get; }

		// Token: 0x17001C96 RID: 7318
		// (get) Token: 0x06006605 RID: 26117
		public abstract long SizeInBytes { get; }

		// Token: 0x06006606 RID: 26118
		public new abstract void Initialize(string name, NameValueCollection config);

		// Token: 0x06006607 RID: 26119
		public abstract object Add(string key, object item, CacheInsertOptions options);

		// Token: 0x06006608 RID: 26120
		public abstract object Get(string key);

		// Token: 0x06006609 RID: 26121
		public abstract void Insert(string key, object item, CacheInsertOptions options);

		// Token: 0x0600660A RID: 26122
		public abstract object Remove(string key);

		// Token: 0x0600660B RID: 26123
		public abstract object Remove(string key, CacheItemRemovedReason reason);

		// Token: 0x0600660C RID: 26124
		public abstract long Trim(int percent);

		// Token: 0x0600660D RID: 26125
		public abstract bool AddDependent(string key, CacheDependency dependency, out DateTime utcLastUpdated);

		// Token: 0x0600660E RID: 26126
		public abstract void RemoveDependent(string key, CacheDependency dependency);

		// Token: 0x0600660F RID: 26127
		public abstract void Dispose();

		// Token: 0x06006610 RID: 26128
		public abstract IDictionaryEnumerator GetEnumerator();
	}
}

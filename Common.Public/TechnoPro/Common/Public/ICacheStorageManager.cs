using System;
using TechnoPro.Common.Public.Entities.Caching;

namespace TechnoPro.Common.Public
{
	// Token: 0x020000B9 RID: 185
	public interface ICacheStorageManager
	{
		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060004AF RID: 1199
		int CountItems { get; }

		// Token: 0x170001BB RID: 443
		object this[object key]
		{
			get;
			set;
		}

		// Token: 0x060004B2 RID: 1202
		void Insert(object key, object value);

		// Token: 0x060004B3 RID: 1203
		void Insert(object key, object value, DateTime expirationDate);

		// Token: 0x060004B4 RID: 1204
		void Insert(object key, object value, TimeSpan expirationTime);

		// Token: 0x060004B5 RID: 1205
		void Insert(object key, object value, TimeSpan expirationTime, bool slidingExpiration);

		// Token: 0x060004B6 RID: 1206
		void Insert(object key, object value, DateTime expirationDate, TimeSpan slidingExpirationTime);

		// Token: 0x060004B7 RID: 1207
		void Remove(object key);

		// Token: 0x060004B8 RID: 1208
		void Remove(Predicate<object> pKey);

		// Token: 0x060004B9 RID: 1209
		void RemoveAllSubItems(eServerCacheItemType key);

		// Token: 0x060004BA RID: 1210
		void ClearCache();

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060004BB RID: 1211
		object[] Keys { get; }
	}
}

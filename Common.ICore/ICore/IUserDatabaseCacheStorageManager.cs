using System;
using System.Collections.Generic;

namespace TechnoPro.Common.ICore
{
	// Token: 0x02000005 RID: 5
	public interface IUserDatabaseCacheStorageManager
	{
		// Token: 0x17000005 RID: 5
		object this[int userID, string key]
		{
			get;
			set;
		}

		// Token: 0x0600001B RID: 27
		void Insert(int userID, string key, object value);

		// Token: 0x0600001C RID: 28
		void Remove(int userID, string key);

		// Token: 0x0600001D RID: 29
		void Remove(params string[] keys);

		// Token: 0x0600001E RID: 30
		void Insert(int userID, IDictionary<string, object> keyvalues);

		// Token: 0x0600001F RID: 31
		void Clear(int userID);

		// Token: 0x06000020 RID: 32
		IDictionary<string, object> GetValues(int userID, IList<string> keys);

		// Token: 0x17000006 RID: 6
		object this[int userID, Enum key]
		{
			get;
			set;
		}

		// Token: 0x06000023 RID: 35
		void Insert(int userID, Enum key, object value);

		// Token: 0x06000024 RID: 36
		void Remove(int userID, Enum key);

		// Token: 0x06000025 RID: 37
		void Remove(params Enum[] keys);

		// Token: 0x06000026 RID: 38
		void Insert(int userID, IDictionary<Enum, object> keyvalues);

		// Token: 0x06000027 RID: 39
		IDictionary<string, object> GetValues(int userID, IList<Enum> keys);

		// Token: 0x06000028 RID: 40
		void Clear(Enum key);

		// Token: 0x06000029 RID: 41
		void Insert(int userID, IDictionary<string, object> keyvalues, TimeSpan expiryTime);

		// Token: 0x0600002A RID: 42
		void Insert(int userID, string key, object value, TimeSpan expiryTime);
	}
}

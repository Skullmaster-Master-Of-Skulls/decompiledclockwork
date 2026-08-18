using System;
using System.Collections.Generic;

namespace TechnoPro.Common.DAO.Cache
{
	// Token: 0x0200009F RID: 159
	public interface IUserDatabaseCacheDAO
	{
		// Token: 0x17000003 RID: 3
		object this[int userID, string key]
		{
			get;
			set;
		}

		// Token: 0x0600041E RID: 1054
		void Remove(int userID, string key);

		// Token: 0x0600041F RID: 1055
		void Remove(params string[] keys);

		// Token: 0x06000420 RID: 1056
		void Insert(int userID, IDictionary<string, object> keyvalues);

		// Token: 0x06000421 RID: 1057
		void Insert(int userID, IDictionary<string, object> keyvalues, DateTime? expiryDate);

		// Token: 0x06000422 RID: 1058
		void Clear(int userID);

		// Token: 0x06000423 RID: 1059
		IDictionary<string, object> GetValues(int userID, IList<string> keys);

		// Token: 0x06000424 RID: 1060
		void Clear(string key);
	}
}

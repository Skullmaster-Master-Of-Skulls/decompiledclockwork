using System;
using System.Collections.Generic;

namespace TechnoPro.Common.DAO
{
	// Token: 0x0200000E RID: 14
	public interface IMiscSafeDAO
	{
		// Token: 0x0600001D RID: 29
		void Save(string key, string value);

		// Token: 0x0600001E RID: 30
		string GetValue(string key);

		// Token: 0x0600001F RID: 31
		IList<string> GetKeys(string value);
	}
}

using System;
using System.Collections.Generic;

namespace TechnoPro.Common.ICore
{
	// Token: 0x0200000A RID: 10
	public interface IMiscSafeManager
	{
		// Token: 0x06000047 RID: 71
		void Save(string key, string value);

		// Token: 0x06000048 RID: 72
		string GetValue(string key);

		// Token: 0x06000049 RID: 73
		IList<string> GetKeys(string value);
	}
}

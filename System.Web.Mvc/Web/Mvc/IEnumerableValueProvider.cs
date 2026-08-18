using System;
using System.Collections.Generic;

namespace System.Web.Mvc
{
	// Token: 0x02000037 RID: 55
	public interface IEnumerableValueProvider : IValueProvider
	{
		// Token: 0x0600010F RID: 271
		IDictionary<string, string> GetKeysFromPrefix(string prefix);
	}
}

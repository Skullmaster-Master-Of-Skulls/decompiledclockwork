using System;
using System.Collections.Generic;

namespace System.Web.Http.ValueProviders
{
	// Token: 0x0200019B RID: 411
	public interface IEnumerableValueProvider : IValueProvider
	{
		// Token: 0x06000A6F RID: 2671
		IDictionary<string, string> GetKeysFromPrefix(string prefix);
	}
}

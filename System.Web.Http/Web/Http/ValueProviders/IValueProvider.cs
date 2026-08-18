using System;

namespace System.Web.Http.ValueProviders
{
	// Token: 0x0200019A RID: 410
	public interface IValueProvider
	{
		// Token: 0x06000A6D RID: 2669
		bool ContainsPrefix(string prefix);

		// Token: 0x06000A6E RID: 2670
		ValueProviderResult GetValue(string key);
	}
}

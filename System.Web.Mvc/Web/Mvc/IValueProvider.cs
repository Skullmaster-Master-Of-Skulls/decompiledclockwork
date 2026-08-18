using System;

namespace System.Web.Mvc
{
	// Token: 0x02000035 RID: 53
	public interface IValueProvider
	{
		// Token: 0x0600010C RID: 268
		bool ContainsPrefix(string prefix);

		// Token: 0x0600010D RID: 269
		ValueProviderResult GetValue(string key);
	}
}

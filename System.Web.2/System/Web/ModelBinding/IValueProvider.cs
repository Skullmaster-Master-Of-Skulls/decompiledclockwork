using System;

namespace System.Web.ModelBinding
{
	// Token: 0x02000654 RID: 1620
	public interface IValueProvider
	{
		// Token: 0x06004FA6 RID: 20390
		bool ContainsPrefix(string prefix);

		// Token: 0x06004FA7 RID: 20391
		ValueProviderResult GetValue(string key);
	}
}

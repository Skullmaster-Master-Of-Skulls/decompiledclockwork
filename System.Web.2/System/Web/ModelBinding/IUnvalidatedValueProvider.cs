using System;

namespace System.Web.ModelBinding
{
	// Token: 0x02000653 RID: 1619
	public interface IUnvalidatedValueProvider : IValueProvider
	{
		// Token: 0x06004FA5 RID: 20389
		ValueProviderResult GetValue(string key, bool skipValidation);
	}
}

using System;

namespace System.Web.Mvc
{
	// Token: 0x02000036 RID: 54
	public interface IUnvalidatedValueProvider : IValueProvider
	{
		// Token: 0x0600010E RID: 270
		ValueProviderResult GetValue(string key, bool skipValidation);
	}
}

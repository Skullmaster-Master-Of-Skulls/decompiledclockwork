using System;
using System.Collections.Generic;
using System.Web.Http.ValueProviders;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x020000D5 RID: 213
	public interface IValueProviderParameterBinding
	{
		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000534 RID: 1332
		IEnumerable<ValueProviderFactory> ValueProviderFactories { get; }
	}
}

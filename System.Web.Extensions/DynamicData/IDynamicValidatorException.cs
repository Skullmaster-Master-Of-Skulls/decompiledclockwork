using System;
using System.Collections.Generic;

namespace System.Web.DynamicData
{
	// Token: 0x02000109 RID: 265
	public interface IDynamicValidatorException
	{
		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06000DF1 RID: 3569
		IDictionary<string, Exception> InnerExceptions { get; }
	}
}

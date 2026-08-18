using System;
using System.Collections.Specialized;
using System.Web;

namespace Microsoft.Web.Infrastructure.DynamicValidationHelper
{
	// Token: 0x0200000F RID: 15
	internal static class DynamicValidationShim
	{
		// Token: 0x06000056 RID: 86 RVA: 0x000030D3 File Offset: 0x000012D3
		internal static void EnableDynamicValidation(HttpContext context)
		{
			context.Request.EnableGranularRequestValidation();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000030E0 File Offset: 0x000012E0
		internal static bool IsValidationEnabled(HttpContext context)
		{
			return context.Request.ValidateInputWasCalled;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000030F0 File Offset: 0x000012F0
		internal static void GetUnvalidatedCollections(HttpContext context, out Func<NameValueCollection> formGetter, out Func<NameValueCollection> queryStringGetter)
		{
			UnvalidatedRequestValues unvalidated = context.Request.Unvalidated;
			formGetter = (() => unvalidated.Form);
			queryStringGetter = (() => unvalidated.QueryString);
		}
	}
}

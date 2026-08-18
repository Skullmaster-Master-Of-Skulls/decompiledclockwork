using System;

namespace System.Web.Helpers
{
	// Token: 0x02000065 RID: 101
	[Obsolete("Use System.Web.HttpRequest.Unvalidated instead.")]
	public static class Validation
	{
		// Token: 0x06000278 RID: 632 RVA: 0x00009BA6 File Offset: 0x00007DA6
		public static UnvalidatedRequestValues Unvalidated(this HttpRequestBase request)
		{
			return Validation.Unvalidated((HttpRequest)null);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00009BB0 File Offset: 0x00007DB0
		public static UnvalidatedRequestValues Unvalidated(this HttpRequest request)
		{
			HttpContext httpContext = HttpContext.Current;
			return new UnvalidatedRequestValues(new HttpRequestWrapper(httpContext.Request));
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00009BD3 File Offset: 0x00007DD3
		public static string Unvalidated(this HttpRequestBase request, string key)
		{
			return request.Unvalidated()[key];
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00009BE1 File Offset: 0x00007DE1
		public static string Unvalidated(this HttpRequest request, string key)
		{
			return request.Unvalidated()[key];
		}
	}
}

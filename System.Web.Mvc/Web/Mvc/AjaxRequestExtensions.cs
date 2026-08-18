using System;

namespace System.Web.Mvc
{
	// Token: 0x02000182 RID: 386
	public static class AjaxRequestExtensions
	{
		// Token: 0x06000A8A RID: 2698 RVA: 0x0001CE48 File Offset: 0x0001B048
		public static bool IsAjaxRequest(this HttpRequestBase request)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			return request["X-Requested-With"] == "XMLHttpRequest" || (request.Headers != null && request.Headers["X-Requested-With"] == "XMLHttpRequest");
		}
	}
}

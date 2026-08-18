using System;

namespace System.Web.Mvc
{
	// Token: 0x0200014D RID: 333
	public static class HttpRequestExtensions
	{
		// Token: 0x06000891 RID: 2193 RVA: 0x00017A20 File Offset: 0x00015C20
		public static string GetHttpMethodOverride(this HttpRequestBase request)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			string text = request.HttpMethod;
			if (!string.Equals(text, "POST", StringComparison.OrdinalIgnoreCase))
			{
				return text;
			}
			string text2 = null;
			string text3 = request.Headers["X-HTTP-Method-Override"];
			if (!string.IsNullOrEmpty(text3))
			{
				text2 = text3;
			}
			else
			{
				string text4 = request.Form["X-HTTP-Method-Override"];
				if (!string.IsNullOrEmpty(text4))
				{
					text2 = text4;
				}
				else
				{
					string text5 = request.QueryString["X-HTTP-Method-Override"];
					if (!string.IsNullOrEmpty(text5))
					{
						text2 = text5;
					}
				}
			}
			if (text2 != null && !string.Equals(text2, "GET", StringComparison.OrdinalIgnoreCase) && !string.Equals(text2, "POST", StringComparison.OrdinalIgnoreCase))
			{
				text = text2;
			}
			return text;
		}

		// Token: 0x0400026C RID: 620
		internal const string XHttpMethodOverrideKey = "X-HTTP-Method-Override";
	}
}

using System;

namespace System.Net
{
	// Token: 0x020001AA RID: 426
	internal static class CookieModule
	{
		// Token: 0x060010CE RID: 4302 RVA: 0x0005A094 File Offset: 0x00058294
		internal static void OnSendingHeaders(HttpWebRequest httpWebRequest)
		{
			try
			{
				if (httpWebRequest.CookieContainer != null)
				{
					httpWebRequest.Headers.RemoveInternal("Cookie");
					string text;
					string cookieHeader = httpWebRequest.CookieContainer.GetCookieHeader(httpWebRequest.GetRemoteResourceUri(), out text);
					if (cookieHeader.Length > 0)
					{
						httpWebRequest.Headers["Cookie"] = cookieHeader;
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x0005A100 File Offset: 0x00058300
		internal static void OnReceivedHeaders(HttpWebRequest httpWebRequest)
		{
			try
			{
				if (httpWebRequest.CookieContainer != null)
				{
					HttpWebResponse httpResponse = httpWebRequest._HttpResponse;
					if (httpResponse != null)
					{
						CookieCollection cookieCollection = null;
						try
						{
							string setCookie = httpResponse.Headers.SetCookie;
							if (setCookie != null && setCookie.Length > 0)
							{
								cookieCollection = httpWebRequest.CookieContainer.CookieCutter(httpResponse.ResponseUri, "Set-Cookie", setCookie, false);
							}
						}
						catch
						{
						}
						try
						{
							string setCookie2 = httpResponse.Headers.SetCookie2;
							if (setCookie2 != null && setCookie2.Length > 0)
							{
								CookieCollection cookieCollection2 = httpWebRequest.CookieContainer.CookieCutter(httpResponse.ResponseUri, "Set-Cookie2", setCookie2, false);
								if (cookieCollection != null && cookieCollection.Count != 0)
								{
									cookieCollection.Add(cookieCollection2);
								}
								else
								{
									cookieCollection = cookieCollection2;
								}
							}
						}
						catch
						{
						}
						if (cookieCollection != null)
						{
							httpResponse.Cookies = cookieCollection;
						}
					}
				}
			}
			catch
			{
			}
		}
	}
}

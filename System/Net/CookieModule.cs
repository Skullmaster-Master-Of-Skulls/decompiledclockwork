using System;

namespace System.Net
{
	// Token: 0x020004D3 RID: 1235
	internal static class CookieModule
	{
		// Token: 0x06002672 RID: 9842 RVA: 0x0009C44C File Offset: 0x0009B44C
		internal static void OnSendingHeaders(HttpWebRequest httpWebRequest)
		{
			try
			{
				if (httpWebRequest.CookieContainer != null)
				{
					httpWebRequest.Headers.RemoveInternal("Cookie");
					string text;
					string cookieHeader = httpWebRequest.CookieContainer.GetCookieHeader(httpWebRequest.Address, out text);
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

		// Token: 0x06002673 RID: 9843 RVA: 0x0009C4B8 File Offset: 0x0009B4B8
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

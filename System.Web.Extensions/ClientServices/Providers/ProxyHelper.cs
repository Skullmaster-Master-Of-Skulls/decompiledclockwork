using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security;
using System.Text;
using System.Threading;
using System.Web.Resources;
using System.Web.Script.Serialization;
using System.Web.Util;

namespace System.Web.ClientServices.Providers
{
	// Token: 0x02000114 RID: 276
	internal static class ProxyHelper
	{
		// Token: 0x06000E98 RID: 3736 RVA: 0x0003449C File Offset: 0x0003269C
		internal static object CreateWebRequestAndGetResponse(string serverUri, ref CookieContainer cookies, string username, string connectionString, string connectionStringProvider, string[] paramNames, object[] paramValues, Type returnType)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(serverUri);
			httpWebRequest.UseDefaultCredentials = true;
			httpWebRequest.ContentType = "application/json; charset=utf-8";
			httpWebRequest.AllowAutoRedirect = true;
			httpWebRequest.Method = "POST";
			if (cookies == null)
			{
				cookies = ProxyHelper.ConstructCookieContainer(serverUri, username, connectionString, connectionStringProvider);
			}
			if (cookies != null)
			{
				httpWebRequest.CookieContainer = cookies;
			}
			if (paramNames != null && paramNames.Length != 0)
			{
				byte[] serializedParameters = ProxyHelper.GetSerializedParameters(paramNames, paramValues);
				httpWebRequest.ContentLength = (long)serializedParameters.Length;
				using (Stream requestStream = httpWebRequest.GetRequestStream())
				{
					requestStream.Write(serializedParameters, 0, serializedParameters.Length);
					goto IL_8F;
				}
			}
			httpWebRequest.ContentLength = 0L;
			IL_8F:
			object result;
			try
			{
				using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
				{
					if (httpWebResponse == null)
					{
						throw new WebException(AtlasWeb.ClientService_BadJsonResponse);
					}
					ProxyHelper.GetCookiesFromResponse(httpWebResponse, cookies, serverUri, username, connectionString, connectionStringProvider);
					if (returnType == null)
					{
						result = null;
					}
					else
					{
						JavaScriptTypeResolver resolver = AppSettings.UseLegacyClientServicesJsonHandling ? new SimpleTypeResolver() : new DictionaryTypeResolver();
						JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer(resolver);
						string responseString = ProxyHelper.GetResponseString(httpWebResponse);
						Dictionary<string, object> dictionary = javaScriptSerializer.DeserializeObject(responseString) as Dictionary<string, object>;
						if (dictionary == null || !dictionary.ContainsKey("d"))
						{
							throw new WebException(AtlasWeb.ClientService_BadJsonResponse);
						}
						result = ObjectConverter.ConvertObjectToType(dictionary["d"], returnType, javaScriptSerializer);
					}
				}
			}
			catch (WebException ex)
			{
				HttpWebResponse httpWebResponse2 = (HttpWebResponse)ex.Response;
				if (httpWebResponse2 == null)
				{
					throw;
				}
				throw new WebException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.ProxyHelper_BadStatusCode, new object[]
				{
					httpWebResponse2.StatusCode.ToString(),
					ProxyHelper.GetResponseString(httpWebResponse2)
				}), ex);
			}
			return result;
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x00034668 File Offset: 0x00032868
		private static void GetCookiesFromResponse(HttpWebResponse response, CookieContainer cookies, string serverUri, string username, string connectionString, string connectionStringProvider)
		{
			foreach (object obj in response.Cookies)
			{
				Cookie cookie = (Cookie)obj;
				cookies.Add(cookie);
			}
			int count = response.Headers.Count;
			for (int i = 0; i < count; i++)
			{
				string key = response.Headers.GetKey(i);
				if (key != null && key == "Set-Cookie")
				{
					string cookieHeaders = response.Headers.Get(i);
					ProxyHelper.StoreCookie(serverUri, cookieHeaders, username, connectionString, connectionStringProvider);
				}
			}
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0003471C File Offset: 0x0003291C
		private static byte[] GetSerializedParameters(string[] paramNames, object[] paramValues)
		{
			int num = paramNames.Length;
			if (num != paramValues.Length)
			{
				throw new ArgumentException(null, "paramValues");
			}
			if (num < 1)
			{
				return new byte[0];
			}
			StringBuilder stringBuilder = new StringBuilder(40 * num);
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			stringBuilder.Append("{" + javaScriptSerializer.Serialize(paramNames[0]) + ":" + javaScriptSerializer.Serialize(paramValues[0]));
			for (int i = 1; i < num; i++)
			{
				stringBuilder.Append("," + javaScriptSerializer.Serialize(paramNames[i]) + ":" + javaScriptSerializer.Serialize(paramValues[i]));
			}
			stringBuilder.Append("}");
			return Encoding.UTF8.GetBytes(stringBuilder.ToString());
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x000347D4 File Offset: 0x000329D4
		private static string GetResponseString(HttpWebResponse response)
		{
			string result;
			using (Stream responseStream = response.GetResponseStream())
			{
				using (StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8))
				{
					int num = 1024;
					if (responseStream.CanSeek && responseStream.Length > (long)num)
					{
						num = (int)responseStream.Length;
					}
					char[] array = new char[num];
					StringBuilder stringBuilder = new StringBuilder(num);
					for (int i = streamReader.Read(array, 0, num); i > 0; i = streamReader.Read(array, 0, num))
					{
						stringBuilder.Append(new string(array, 0, i));
					}
					result = stringBuilder.ToString();
				}
			}
			return result;
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x00034890 File Offset: 0x00032A90
		internal static CookieContainer ConstructCookieContainer(string serverUri, string username, string connectionString, string connectionStringProvider)
		{
			if (username == null)
			{
				if (Thread.CurrentPrincipal != null)
				{
					username = Thread.CurrentPrincipal.Identity.Name;
				}
				else
				{
					username = string.Empty;
				}
			}
			string[] cookiesFromIECache = ProxyHelper.GetCookiesFromIECache(serverUri, username, connectionString, connectionStringProvider);
			if (cookiesFromIECache == null || cookiesFromIECache.Length < 1)
			{
				return new CookieContainer();
			}
			CookieContainer cookieContainer = new CookieContainer(cookiesFromIECache.Length + 10, cookiesFromIECache.Length + 10, 4096);
			Uri uri = new Uri(serverUri);
			for (int i = 0; i < cookiesFromIECache.Length; i++)
			{
				if (!string.IsNullOrEmpty(cookiesFromIECache[i]))
				{
					int num = cookiesFromIECache[i].IndexOf('=');
					string text;
					string text2;
					if (num < 0)
					{
						text = cookiesFromIECache[i];
						text2 = string.Empty;
					}
					else
					{
						text = cookiesFromIECache[i].Substring(0, num);
						text2 = cookiesFromIECache[i].Substring(num + 1);
					}
					text = text.Trim();
					text2 = text2.Trim();
					if (text.Length != 32 || !(text2 == "Q"))
					{
						cookieContainer.Add(new Cookie(text, text2, "/", uri.Host));
					}
				}
			}
			return cookieContainer;
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x00034998 File Offset: 0x00032B98
		internal static bool DoAnyCookiesExist(string serverUri, string username, string connectionString, string connectionStringProvider)
		{
			string[] cookiesFromIECache = ProxyHelper.GetCookiesFromIECache(serverUri, username, connectionString, connectionStringProvider);
			if (cookiesFromIECache == null || cookiesFromIECache.Length < 1)
			{
				return false;
			}
			foreach (string text in cookiesFromIECache)
			{
				if (text != null && text.Trim().Length > 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x000349E4 File Offset: 0x00032BE4
		[SecuritySafeCritical]
		private static void StoreCookie(string serverUri, string cookieHeaders, string username, string connectionString, string connectionStringProvider)
		{
			if (string.IsNullOrEmpty(cookieHeaders))
			{
				return;
			}
			string[] array = cookieHeaders.Split(new char[]
			{
				','
			});
			int i = 0;
			while (i < array.Length)
			{
				StringBuilder stringBuilder = new StringBuilder(array[i++]);
				while (i < array.Length)
				{
					int num = array[i].IndexOf('=');
					int num2 = array[i].IndexOf(';');
					if (num > 0 && (num2 < 0 || num2 > num))
					{
						break;
					}
					stringBuilder.Append(",");
					stringBuilder.Append(array[i++]);
				}
				string text = stringBuilder.ToString();
				int num3 = text.IndexOf('=');
				string str = ((num3 < 0) ? text : text.Substring(0, num3)).Trim();
				string text2 = ((num3 < 0) ? string.Empty : text.Substring(num3 + 1)).Trim();
				if (text2.Length > 0)
				{
					ProxyHelper.ChangeCookieAndStoreInDB(ref str, ref text2, username, connectionString, connectionStringProvider);
				}
				UnsafeNativeMethods.InternetSetCookieW(serverUri, null, str + " = " + text2);
			}
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x00034AE8 File Offset: 0x00032CE8
		[SecuritySafeCritical]
		private static string[] GetCookiesFromIECache(string uri, string username, string connectionString, string connectionStringProvider)
		{
			int num = 0;
			if (UnsafeNativeMethods.InternetGetCookieW(uri, null, null, ref num) == 0 || num < 1)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(num);
			if (UnsafeNativeMethods.InternetGetCookieW(uri, null, stringBuilder, ref num) == 0)
			{
				return null;
			}
			string[] array = stringBuilder.ToString().Split(new char[]
			{
				';'
			}, StringSplitOptions.RemoveEmptyEntries);
			if (connectionString != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = ProxyHelper.GetCookieFromDB(array[i], username, connectionString, connectionStringProvider);
				}
			}
			return array;
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x00034B56 File Offset: 0x00032D56
		private static string GetCookieFromDB(string cookieHeader, string username, string connectionString, string connectionStringProvider)
		{
			cookieHeader = cookieHeader.Trim();
			if (cookieHeader.Length != 34 || cookieHeader[33] != 'Q' || cookieHeader.IndexOf('=') != 32)
			{
				return cookieHeader;
			}
			return SqlHelper.GetCookieFromDB(cookieHeader.Substring(0, 32), username, connectionString, connectionStringProvider);
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x00034B98 File Offset: 0x00032D98
		private static void ChangeCookieAndStoreInDB(ref string cookieName, ref string cookieValue, string username, string connectionString, string connectionStringProvider)
		{
			string[] array = cookieValue.Split(new char[]
			{
				';'
			});
			if (array.Length < 1)
			{
				return;
			}
			string text = array[0];
			bool flag = false;
			StringBuilder stringBuilder = new StringBuilder((connectionString == null) ? text : "Q", cookieValue.Length);
			for (int i = 1; i < array.Length; i++)
			{
				if (string.Compare(array[i].Trim(), "HttpOnly", StringComparison.OrdinalIgnoreCase) == 0)
				{
					flag = true;
				}
				else
				{
					stringBuilder.Append(";" + array[i]);
				}
			}
			if (!flag)
			{
				return;
			}
			if (connectionString != null)
			{
				string text2 = SqlHelper.StoreCookieInDB(cookieName, text, username, connectionString, connectionStringProvider);
				if (string.IsNullOrEmpty(text2))
				{
					return;
				}
				cookieName = text2;
			}
			cookieName = cookieName.Trim();
			if (text.Length < 1)
			{
				cookieValue = ";" + stringBuilder.ToString().Substring((connectionString == null) ? 0 : 1);
				return;
			}
			cookieValue = stringBuilder.ToString().Trim();
		}
	}
}

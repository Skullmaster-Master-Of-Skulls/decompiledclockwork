using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System.Web.WebPages
{
	// Token: 0x02000058 RID: 88
	internal static class UrlUtil
	{
		// Token: 0x06000216 RID: 534 RVA: 0x00008930 File Offset: 0x00006B30
		public static string GenerateClientUrl(HttpContextBase httpContext, string contentPath)
		{
			if (string.IsNullOrEmpty(contentPath))
			{
				return contentPath;
			}
			string text;
			contentPath = UrlUtil.StripQuery(contentPath, out text);
			if (string.IsNullOrEmpty(text))
			{
				return UrlUtil.GenerateClientUrlInternal(httpContext, contentPath);
			}
			return UrlUtil.GenerateClientUrlInternal(httpContext, contentPath) + text;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00008970 File Offset: 0x00006B70
		public static string GenerateClientUrl(HttpContextBase httpContext, string basePath, string path, params object[] pathParts)
		{
			if (string.IsNullOrEmpty(path))
			{
				return path;
			}
			if (pathParts != null)
			{
				for (int i = 0; i < pathParts.Length; i++)
				{
					if (pathParts[i] == null)
					{
						throw new ArgumentNullException("pathParts");
					}
				}
			}
			if (basePath != null)
			{
				path = VirtualPathUtility.Combine(basePath, path);
			}
			string text;
			string contentPath = UrlUtil.BuildUrl(path, out text, pathParts);
			if (string.IsNullOrEmpty(text))
			{
				return UrlUtil.GenerateClientUrlInternal(httpContext, contentPath);
			}
			return UrlUtil.GenerateClientUrlInternal(httpContext, contentPath) + text;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x000089DC File Offset: 0x00006BDC
		private static string GenerateClientUrlInternal(HttpContextBase httpContext, string contentPath)
		{
			if (string.IsNullOrEmpty(contentPath))
			{
				return contentPath;
			}
			bool flag = contentPath[0] == '~';
			if (flag)
			{
				string contentPath2 = VirtualPathUtility.ToAbsolute(contentPath, httpContext.Request.ApplicationPath);
				return UrlUtil.GenerateClientUrlInternal(httpContext, contentPath2);
			}
			if (!UrlUtil._urlRewriterHelper.WasRequestRewritten(httpContext))
			{
				return contentPath;
			}
			string relativePath = UrlUtil.MakeRelative(httpContext.Request.Path, contentPath);
			return UrlUtil.MakeAbsolute(httpContext.Request.RawUrl, relativePath);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00008A54 File Offset: 0x00006C54
		public static string MakeAbsolute(string basePath, string relativePath)
		{
			string text;
			basePath = UrlUtil.StripQuery(basePath, out text);
			return VirtualPathUtility.Combine(basePath, relativePath);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00008A74 File Offset: 0x00006C74
		public static string MakeRelative(string fromPath, string toPath)
		{
			string text = VirtualPathUtility.MakeRelative(fromPath, toPath);
			if (string.IsNullOrEmpty(text) || text[0] == '?')
			{
				text = "./" + text;
			}
			return text;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00008AAC File Offset: 0x00006CAC
		private static string StripQuery(string path, out string query)
		{
			int num = path.IndexOf('?');
			if (num >= 0)
			{
				query = path.Substring(num);
				return path.Substring(0, num);
			}
			query = null;
			return path;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00008ADC File Offset: 0x00006CDC
		internal static void ResetUrlRewriterHelper()
		{
			UrlUtil._urlRewriterHelper = new UrlRewriterHelper();
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00008AE8 File Offset: 0x00006CE8
		internal static string BuildUrl(string path, out string query, params object[] pathParts)
		{
			if (pathParts == null || pathParts.Length == 0)
			{
				query = string.Empty;
				return HttpUtility.UrlPathEncode(path);
			}
			if (pathParts.Length != 1)
			{
				StringBuilder stringBuilder = new StringBuilder(path);
				StringBuilder stringBuilder2 = new StringBuilder();
				foreach (object obj in pathParts)
				{
					if (UrlUtil.IsDisplayableType(obj.GetType()))
					{
						string value = Convert.ToString(obj, CultureInfo.InvariantCulture);
						stringBuilder.Append('/');
						stringBuilder.Append(value);
					}
					else
					{
						UrlUtil.AppendToQueryString(stringBuilder2, obj);
					}
				}
				query = stringBuilder2.ToString();
				return HttpUtility.UrlPathEncode(stringBuilder.ToString());
			}
			object obj2 = pathParts[0];
			if (UrlUtil.IsDisplayableType(obj2.GetType()))
			{
				string str = Convert.ToString(obj2, CultureInfo.InvariantCulture);
				path = path + "/" + str;
				query = string.Empty;
				return HttpUtility.UrlPathEncode(path);
			}
			StringBuilder stringBuilder3 = new StringBuilder();
			UrlUtil.AppendToQueryString(stringBuilder3, obj2);
			query = stringBuilder3.ToString();
			return HttpUtility.UrlPathEncode(path);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00008BDC File Offset: 0x00006DDC
		private static void AppendToQueryString(StringBuilder queryString, object obj)
		{
			IDictionary<string, object> dictionary = TypeHelper.ObjectToDictionary(obj);
			foreach (KeyValuePair<string, object> keyValuePair in dictionary)
			{
				if (queryString.Length == 0)
				{
					queryString.Append('?');
				}
				else
				{
					queryString.Append('&');
				}
				string str = Convert.ToString(keyValuePair.Value, CultureInfo.InvariantCulture);
				queryString.Append(HttpUtility.UrlEncode(keyValuePair.Key)).Append('=').Append(HttpUtility.UrlEncode(str));
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00008C78 File Offset: 0x00006E78
		private static bool IsDisplayableType(Type t)
		{
			return t.GetInterfaces().Length > 0;
		}

		// Token: 0x040000B2 RID: 178
		private static UrlRewriterHelper _urlRewriterHelper = new UrlRewriterHelper();
	}
}

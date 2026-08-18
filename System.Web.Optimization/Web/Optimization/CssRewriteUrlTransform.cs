using System;
using System.Text.RegularExpressions;

namespace System.Web.Optimization
{
	// Token: 0x02000015 RID: 21
	public class CssRewriteUrlTransform : IItemTransform
	{
		// Token: 0x060000CB RID: 203 RVA: 0x00003EEC File Offset: 0x000020EC
		internal static string RebaseUrlToAbsolute(string baseUrl, string url)
		{
			if (string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(baseUrl) || url.StartsWith("/", StringComparison.OrdinalIgnoreCase))
			{
				return url;
			}
			if (!baseUrl.EndsWith("/", StringComparison.OrdinalIgnoreCase))
			{
				baseUrl += "/";
			}
			return VirtualPathUtility.ToAbsolute(baseUrl + url);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003F7C File Offset: 0x0000217C
		internal static string ConvertUrlsToAbsolute(string baseUrl, string content)
		{
			if (string.IsNullOrWhiteSpace(content))
			{
				return content;
			}
			Regex regex = new Regex("url\\(['\"]?(?<url>[^)]+?)['\"]?\\)");
			return regex.Replace(content, (Match match) => "url(" + CssRewriteUrlTransform.RebaseUrlToAbsolute(baseUrl, match.Groups["url"].Value) + ")");
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003FC0 File Offset: 0x000021C0
		public string Process(string includedVirtualPath, string input)
		{
			if (includedVirtualPath == null)
			{
				throw new ArgumentNullException("includedVirtualPath");
			}
			string directory = VirtualPathUtility.GetDirectory(includedVirtualPath.Substring(1));
			return CssRewriteUrlTransform.ConvertUrlsToAbsolute(directory, input);
		}
	}
}

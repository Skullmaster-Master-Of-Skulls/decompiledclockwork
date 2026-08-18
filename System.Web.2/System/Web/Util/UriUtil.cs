using System;
using System.Text;

namespace System.Web.Util
{
	// Token: 0x020001D7 RID: 471
	internal static class UriUtil
	{
		// Token: 0x0600178B RID: 6027 RVA: 0x00049BE9 File Offset: 0x00047DE9
		internal static Uri BuildUri(string scheme, string serverName, string port, string path, string queryString)
		{
			return UriUtil.BuildUriImpl(scheme, serverName, port, path, queryString, AppSettings.UseLegacyRequestUrlGeneration);
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x00049BFC File Offset: 0x00047DFC
		internal static Uri BuildUriImpl(string scheme, string serverName, string port, string path, string queryString, bool useLegacyRequestUrlGeneration)
		{
			if (!useLegacyRequestUrlGeneration)
			{
				if (path != null)
				{
					path = UriUtil.EscapeForPath(path);
				}
				if (queryString != null)
				{
					string text = queryString.Replace("#", "%23");
					queryString = text;
				}
			}
			if (port != null)
			{
				port = ":" + port;
			}
			string uriString = string.Concat(new string[]
			{
				scheme,
				"://",
				serverName,
				port,
				path,
				queryString
			});
			return new Uri(uriString);
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x00049C70 File Offset: 0x00047E70
		private static string EscapeForPath(string unescaped)
		{
			if (string.IsNullOrEmpty(unescaped) || UriUtil.ContainsOnlyPathSafeCharacters(unescaped))
			{
				return unescaped;
			}
			string text = Uri.EscapeDataString(unescaped);
			if (string.Equals(text, unescaped, StringComparison.Ordinal))
			{
				return unescaped;
			}
			StringBuilder stringBuilder = new StringBuilder(text);
			stringBuilder.Replace("%21", "!");
			stringBuilder.Replace("%24", "$");
			stringBuilder.Replace("%26", "&");
			stringBuilder.Replace("%27", "'");
			stringBuilder.Replace("%28", "(");
			stringBuilder.Replace("%29", ")");
			stringBuilder.Replace("%2A", "*");
			stringBuilder.Replace("%2B", "+");
			stringBuilder.Replace("%2C", ",");
			stringBuilder.Replace("%2F", "/");
			stringBuilder.Replace("%3A", ":");
			stringBuilder.Replace("%3B", ";");
			stringBuilder.Replace("%3D", "=");
			stringBuilder.Replace("%40", "@");
			return stringBuilder.ToString();
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x00049DA0 File Offset: 0x00047FA0
		private static bool ContainsOnlyPathSafeCharacters(string input)
		{
			foreach (char c in input)
			{
				if (('a' > c || c > 'z') && ('A' > c || c > 'Z') && ('0' > c || c > '9'))
				{
					if (c <= '@')
					{
						switch (c)
						{
						case '!':
						case '$':
						case '&':
						case '\'':
						case '(':
						case ')':
						case '*':
						case '+':
						case ',':
						case '-':
						case '.':
						case '/':
						case ':':
						case ';':
						case '=':
							goto IL_CE;
						case '"':
						case '#':
						case '%':
						case '0':
						case '1':
						case '2':
						case '3':
						case '4':
						case '5':
						case '6':
						case '7':
						case '8':
						case '9':
						case '<':
							break;
						default:
							if (c == '@')
							{
								goto IL_CE;
							}
							break;
						}
					}
					else if (c == '_' || c == '~')
					{
						goto IL_CE;
					}
					return false;
				}
				IL_CE:;
			}
			return true;
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x00049E8C File Offset: 0x0004808C
		internal static void ExtractQueryAndFragment(string input, out string path, out string queryAndFragment)
		{
			int num = input.IndexOfAny(UriUtil._queryFragmentSeparators);
			if (num != -1)
			{
				path = input.Substring(0, num);
				queryAndFragment = input.Substring(num);
				return;
			}
			path = input;
			queryAndFragment = null;
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x00049EC4 File Offset: 0x000480C4
		internal static bool IsSafeScheme(string url)
		{
			return url.IndexOf(":", StringComparison.Ordinal) == -1 || url.StartsWith("http:", StringComparison.OrdinalIgnoreCase) || url.StartsWith("https:", StringComparison.OrdinalIgnoreCase) || url.StartsWith("ftp:", StringComparison.OrdinalIgnoreCase) || url.StartsWith("file:", StringComparison.OrdinalIgnoreCase) || url.StartsWith("news:", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x00049F28 File Offset: 0x00048128
		internal static bool TrySplitUriForPathEncode(string input, out string schemeAndAuthority, out string path, out string queryAndFragment, bool checkScheme)
		{
			string text;
			UriUtil.ExtractQueryAndFragment(input, out text, out queryAndFragment);
			bool flag = !checkScheme || UriUtil.IsSafeScheme(text);
			Uri uri;
			if (flag && Uri.TryCreate(text, UriKind.Absolute, out uri))
			{
				string authority = uri.Authority;
				if (!string.IsNullOrEmpty(authority))
				{
					int num = text.IndexOf(authority, StringComparison.OrdinalIgnoreCase);
					if (num != -1)
					{
						int num2 = num + authority.Length;
						schemeAndAuthority = text.Substring(0, num2);
						path = text.Substring(num2);
						return true;
					}
				}
			}
			schemeAndAuthority = null;
			path = null;
			queryAndFragment = null;
			return false;
		}

		// Token: 0x0400171A RID: 5914
		private static readonly char[] _queryFragmentSeparators = new char[]
		{
			'?',
			'#'
		};
	}
}

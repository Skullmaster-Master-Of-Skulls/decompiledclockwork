using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020000B9 RID: 185
	public sealed class HttpUtility
	{
		// Token: 0x06000CDC RID: 3292 RVA: 0x0002404C File Offset: 0x0002224C
		public static string HtmlDecode(string s)
		{
			return HttpEncoder.Current.HtmlDecode(s);
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x00024059 File Offset: 0x00022259
		public static void HtmlDecode(string s, TextWriter output)
		{
			HttpEncoder.Current.HtmlDecode(s, output);
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x00024067 File Offset: 0x00022267
		public static string HtmlEncode(string s)
		{
			return HttpEncoder.Current.HtmlEncode(s);
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x00024074 File Offset: 0x00022274
		public static string HtmlEncode(object value)
		{
			if (value == null)
			{
				return null;
			}
			IHtmlString htmlString = value as IHtmlString;
			if (htmlString != null)
			{
				return htmlString.ToHtmlString();
			}
			return HttpUtility.HtmlEncode(Convert.ToString(value, CultureInfo.CurrentCulture));
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x000240A7 File Offset: 0x000222A7
		public static void HtmlEncode(string s, TextWriter output)
		{
			HttpEncoder.Current.HtmlEncode(s, output);
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x000240B5 File Offset: 0x000222B5
		public static string HtmlAttributeEncode(string s)
		{
			return HttpEncoder.Current.HtmlAttributeEncode(s);
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x000240C2 File Offset: 0x000222C2
		public static void HtmlAttributeEncode(string s, TextWriter output)
		{
			HttpEncoder.Current.HtmlAttributeEncode(s, output);
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x000240D0 File Offset: 0x000222D0
		internal static string FormatPlainTextSpacesAsHtml(string s)
		{
			if (s == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			StringWriter stringWriter = new StringWriter(stringBuilder);
			int length = s.Length;
			for (int i = 0; i < length; i++)
			{
				char c = s[i];
				if (c == ' ')
				{
					stringWriter.Write("&nbsp;");
				}
				else
				{
					stringWriter.Write(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0002412C File Offset: 0x0002232C
		internal static string FormatPlainTextAsHtml(string s)
		{
			if (s == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			StringWriter output = new StringWriter(stringBuilder);
			HttpUtility.FormatPlainTextAsHtml(s, output);
			return stringBuilder.ToString();
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x00024158 File Offset: 0x00022358
		internal static void FormatPlainTextAsHtml(string s, TextWriter output)
		{
			if (s == null)
			{
				return;
			}
			int length = s.Length;
			char c = '\0';
			int i = 0;
			while (i < length)
			{
				char c2 = s[i];
				if (c2 <= ' ')
				{
					if (c2 != '\n')
					{
						if (c2 != '\r')
						{
							if (c2 != ' ')
							{
								goto IL_B7;
							}
							if (c == ' ')
							{
								output.Write("&nbsp;");
							}
							else
							{
								output.Write(c2);
							}
						}
					}
					else
					{
						output.Write("<br>");
					}
				}
				else if (c2 <= '&')
				{
					if (c2 != '"')
					{
						if (c2 != '&')
						{
							goto IL_B7;
						}
						output.Write("&amp;");
					}
					else
					{
						output.Write("&quot;");
					}
				}
				else if (c2 != '<')
				{
					if (c2 != '>')
					{
						goto IL_B7;
					}
					output.Write("&gt;");
				}
				else
				{
					output.Write("&lt;");
				}
				IL_F8:
				c = c2;
				i++;
				continue;
				IL_B7:
				if (c2 >= '\u00a0' && c2 < 'Ā')
				{
					output.Write("&#");
					int num = (int)c2;
					output.Write(num.ToString(NumberFormatInfo.InvariantInfo));
					output.Write(';');
					goto IL_F8;
				}
				output.Write(c2);
				goto IL_F8;
			}
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0002426A File Offset: 0x0002246A
		public static NameValueCollection ParseQueryString(string query)
		{
			return HttpUtility.ParseQueryString(query, Encoding.UTF8);
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00024278 File Offset: 0x00022478
		public static NameValueCollection ParseQueryString(string query, Encoding encoding)
		{
			if (query == null)
			{
				throw new ArgumentNullException("query");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (query.Length > 0 && query[0] == '?')
			{
				query = query.Substring(1);
			}
			return new HttpValueCollection(query, false, true, encoding);
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x000242C7 File Offset: 0x000224C7
		public static string UrlEncode(string str)
		{
			if (str == null)
			{
				return null;
			}
			return HttpUtility.UrlEncode(str, Encoding.UTF8);
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x000242D9 File Offset: 0x000224D9
		public static string UrlPathEncode(string str)
		{
			return HttpEncoder.Current.UrlPathEncode(str);
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x000242E8 File Offset: 0x000224E8
		internal static string AspCompatUrlEncode(string s)
		{
			s = HttpUtility.UrlEncode(s);
			s = s.Replace("!", "%21");
			s = s.Replace("*", "%2A");
			s = s.Replace("(", "%28");
			s = s.Replace(")", "%29");
			s = s.Replace("-", "%2D");
			s = s.Replace(".", "%2E");
			s = s.Replace("_", "%5F");
			s = s.Replace("\\", "%5C");
			return s;
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x0002438E File Offset: 0x0002258E
		public static string UrlEncode(string str, Encoding e)
		{
			if (str == null)
			{
				return null;
			}
			return Encoding.ASCII.GetString(HttpUtility.UrlEncodeToBytes(str, e));
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x000243A6 File Offset: 0x000225A6
		internal static string UrlEncodeNonAscii(string str, Encoding e)
		{
			return HttpEncoder.Current.UrlEncodeNonAscii(str, e);
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x000243B4 File Offset: 0x000225B4
		public static string UrlEncode(byte[] bytes)
		{
			if (bytes == null)
			{
				return null;
			}
			return Encoding.ASCII.GetString(HttpUtility.UrlEncodeToBytes(bytes));
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x000243CB File Offset: 0x000225CB
		public static string UrlEncode(byte[] bytes, int offset, int count)
		{
			if (bytes == null)
			{
				return null;
			}
			return Encoding.ASCII.GetString(HttpUtility.UrlEncodeToBytes(bytes, offset, count));
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x000243E4 File Offset: 0x000225E4
		public static byte[] UrlEncodeToBytes(string str)
		{
			if (str == null)
			{
				return null;
			}
			return HttpUtility.UrlEncodeToBytes(str, Encoding.UTF8);
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x000243F8 File Offset: 0x000225F8
		public static byte[] UrlEncodeToBytes(string str, Encoding e)
		{
			if (str == null)
			{
				return null;
			}
			byte[] bytes = e.GetBytes(str);
			return HttpEncoder.Current.UrlEncode(bytes, 0, bytes.Length, false);
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x00024422 File Offset: 0x00022622
		public static byte[] UrlEncodeToBytes(byte[] bytes)
		{
			if (bytes == null)
			{
				return null;
			}
			return HttpUtility.UrlEncodeToBytes(bytes, 0, bytes.Length);
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x00024433 File Offset: 0x00022633
		public static byte[] UrlEncodeToBytes(byte[] bytes, int offset, int count)
		{
			return HttpEncoder.Current.UrlEncode(bytes, offset, count, true);
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x00024443 File Offset: 0x00022643
		[Obsolete("This method produces non-standards-compliant output and has interoperability issues. The preferred alternative is UrlEncode(String).")]
		public static string UrlEncodeUnicode(string str)
		{
			return HttpEncoder.Current.UrlEncodeUnicode(str, false);
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x00024451 File Offset: 0x00022651
		[Obsolete("This method produces non-standards-compliant output and has interoperability issues. The preferred alternative is UrlEncodeToBytes(String).")]
		public static byte[] UrlEncodeUnicodeToBytes(string str)
		{
			if (str == null)
			{
				return null;
			}
			return Encoding.ASCII.GetBytes(HttpUtility.UrlEncodeUnicode(str));
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x00024468 File Offset: 0x00022668
		public static string UrlDecode(string str)
		{
			if (str == null)
			{
				return null;
			}
			return HttpUtility.UrlDecode(str, Encoding.UTF8);
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x0002447A File Offset: 0x0002267A
		public static string UrlDecode(string str, Encoding e)
		{
			return HttpEncoder.Current.UrlDecode(str, e);
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x00024488 File Offset: 0x00022688
		public static string UrlDecode(byte[] bytes, Encoding e)
		{
			if (bytes == null)
			{
				return null;
			}
			return HttpUtility.UrlDecode(bytes, 0, bytes.Length, e);
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x0002449A File Offset: 0x0002269A
		public static string UrlDecode(byte[] bytes, int offset, int count, Encoding e)
		{
			return HttpEncoder.Current.UrlDecode(bytes, offset, count, e);
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x000244AA File Offset: 0x000226AA
		public static byte[] UrlDecodeToBytes(string str)
		{
			if (str == null)
			{
				return null;
			}
			return HttpUtility.UrlDecodeToBytes(str, Encoding.UTF8);
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x000244BC File Offset: 0x000226BC
		public static byte[] UrlDecodeToBytes(string str, Encoding e)
		{
			if (str == null)
			{
				return null;
			}
			return HttpUtility.UrlDecodeToBytes(e.GetBytes(str));
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x000244CF File Offset: 0x000226CF
		public static byte[] UrlDecodeToBytes(byte[] bytes)
		{
			if (bytes == null)
			{
				return null;
			}
			return HttpUtility.UrlDecodeToBytes(bytes, 0, (bytes != null) ? bytes.Length : 0);
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x000244E6 File Offset: 0x000226E6
		public static byte[] UrlDecodeToBytes(byte[] bytes, int offset, int count)
		{
			return HttpEncoder.Current.UrlDecode(bytes, offset, count);
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x000244F8 File Offset: 0x000226F8
		internal static string FormatHttpDateTime(DateTime dt)
		{
			if (dt < DateTime.MaxValue.AddDays(-1.0) && dt > DateTime.MinValue.AddDays(1.0))
			{
				dt = dt.ToUniversalTime();
			}
			return dt.ToString("R", DateTimeFormatInfo.InvariantInfo);
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x0002455B File Offset: 0x0002275B
		internal static string FormatHttpDateTimeUtc(DateTime dt)
		{
			return dt.ToString("R", DateTimeFormatInfo.InvariantInfo);
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x00024570 File Offset: 0x00022770
		internal static string FormatHttpCookieDateTime(DateTime dt)
		{
			if (dt < DateTime.MaxValue.AddDays(-1.0) && dt > DateTime.MinValue.AddDays(1.0))
			{
				dt = dt.ToUniversalTime();
			}
			return dt.ToString("ddd, dd-MMM-yyyy HH':'mm':'ss 'GMT'", DateTimeFormatInfo.InvariantInfo);
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x000245D3 File Offset: 0x000227D3
		public static string JavaScriptStringEncode(string value)
		{
			return HttpUtility.JavaScriptStringEncode(value, false);
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x000245DC File Offset: 0x000227DC
		public static string JavaScriptStringEncode(string value, bool addDoubleQuotes)
		{
			string text = HttpEncoder.Current.JavaScriptStringEncode(value);
			if (!addDoubleQuotes)
			{
				return text;
			}
			return "\"" + text + "\"";
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x0002460C File Offset: 0x0002280C
		internal static bool TryParseCoordinates(string value, out double doubleValue)
		{
			NumberStyles style = NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint;
			return double.TryParse(value, style, CultureInfo.InvariantCulture, out doubleValue);
		}
	}
}

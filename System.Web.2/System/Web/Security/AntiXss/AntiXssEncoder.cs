using System;
using System.IO;
using System.Text;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web.Security.AntiXss
{
	// Token: 0x0200061D RID: 1565
	public class AntiXssEncoder : HttpEncoder
	{
		// Token: 0x06004E19 RID: 19993 RVA: 0x00110ED1 File Offset: 0x0010F0D1
		protected internal override void HtmlAttributeEncode(string value, TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			output.Write(UnicodeCharacterEncoder.HtmlAttributeEncode(value));
		}

		// Token: 0x06004E1A RID: 19994 RVA: 0x00110EED File Offset: 0x0010F0ED
		protected internal override void HtmlEncode(string value, TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			output.Write(AntiXssEncoder.HtmlEncode(value, false));
		}

		// Token: 0x06004E1B RID: 19995 RVA: 0x00110F0C File Offset: 0x0010F10C
		protected internal override byte[] UrlEncode(byte[] bytes, int offset, int count)
		{
			if (!HttpEncoder.ValidateUrlEncodingParameters(bytes, offset, count))
			{
				return null;
			}
			string @string = Encoding.UTF8.GetString(bytes, offset, count);
			string s = AntiXssEncoder.UrlEncode(@string, Encoding.UTF8);
			return Encoding.UTF8.GetBytes(s);
		}

		// Token: 0x06004E1C RID: 19996 RVA: 0x00110F4C File Offset: 0x0010F14C
		protected internal override string UrlPathEncode(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}
			string str;
			string s;
			string str2;
			if (!UriUtil.TrySplitUriForPathEncode(value, out str, out s, out str2, false))
			{
				str = null;
				UriUtil.ExtractQueryAndFragment(value, out s, out str2);
			}
			return str + HtmlParameterEncoder.UrlPathEncode(s, Encoding.UTF8) + str2;
		}

		// Token: 0x06004E1D RID: 19997 RVA: 0x00110F92 File Offset: 0x0010F192
		public static void MarkAsSafe(LowerCodeCharts lowerCodeCharts, LowerMidCodeCharts lowerMidCodeCharts, MidCodeCharts midCodeCharts, UpperMidCodeCharts upperMidCodeCharts, UpperCodeCharts upperCodeCharts)
		{
			if (HostingEnvironment.IsHosted)
			{
				HttpApplicationFactory.ThrowIfApplicationOnStartCalled();
			}
			UnicodeCharacterEncoder.MarkAsSafe(lowerCodeCharts, lowerMidCodeCharts, midCodeCharts, upperMidCodeCharts, upperCodeCharts);
		}

		// Token: 0x06004E1E RID: 19998 RVA: 0x00110FAB File Offset: 0x0010F1AB
		public static string CssEncode(string input)
		{
			return CssEncoder.Encode(input);
		}

		// Token: 0x06004E1F RID: 19999 RVA: 0x00110FB3 File Offset: 0x0010F1B3
		public static string HtmlEncode(string input, bool useNamedEntities)
		{
			return UnicodeCharacterEncoder.HtmlEncode(input, useNamedEntities);
		}

		// Token: 0x06004E20 RID: 20000 RVA: 0x00110FBC File Offset: 0x0010F1BC
		public static string UrlEncode(string input)
		{
			return AntiXssEncoder.UrlEncode(input, Encoding.UTF8);
		}

		// Token: 0x06004E21 RID: 20001 RVA: 0x00110FC9 File Offset: 0x0010F1C9
		public static string HtmlFormUrlEncode(string input)
		{
			return AntiXssEncoder.HtmlFormUrlEncode(input, Encoding.UTF8);
		}

		// Token: 0x06004E22 RID: 20002 RVA: 0x00110FD6 File Offset: 0x0010F1D6
		public static string UrlEncode(string input, int codePage)
		{
			return AntiXssEncoder.UrlEncode(input, Encoding.GetEncoding(codePage));
		}

		// Token: 0x06004E23 RID: 20003 RVA: 0x00110FE4 File Offset: 0x0010F1E4
		public static string HtmlFormUrlEncode(string input, int codePage)
		{
			return AntiXssEncoder.HtmlFormUrlEncode(input, Encoding.GetEncoding(codePage));
		}

		// Token: 0x06004E24 RID: 20004 RVA: 0x00110FF2 File Offset: 0x0010F1F2
		public static string UrlEncode(string input, Encoding inputEncoding)
		{
			if (inputEncoding == null)
			{
				inputEncoding = Encoding.UTF8;
			}
			return HtmlParameterEncoder.QueryStringParameterEncode(input, inputEncoding);
		}

		// Token: 0x06004E25 RID: 20005 RVA: 0x00111005 File Offset: 0x0010F205
		public static string HtmlFormUrlEncode(string input, Encoding inputEncoding)
		{
			if (inputEncoding == null)
			{
				inputEncoding = Encoding.UTF8;
			}
			return HtmlParameterEncoder.FormStringParameterEncode(input, inputEncoding);
		}

		// Token: 0x06004E26 RID: 20006 RVA: 0x00111018 File Offset: 0x0010F218
		public static string XmlEncode(string input)
		{
			return UnicodeCharacterEncoder.XmlEncode(input);
		}

		// Token: 0x06004E27 RID: 20007 RVA: 0x00111020 File Offset: 0x0010F220
		public static string XmlAttributeEncode(string input)
		{
			return UnicodeCharacterEncoder.XmlAttributeEncode(input);
		}
	}
}

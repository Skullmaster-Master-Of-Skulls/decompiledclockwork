using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web.Configuration;

namespace System.Web.Util
{
	// Token: 0x02000205 RID: 517
	public class HttpEncoder
	{
		// Token: 0x0600195C RID: 6492 RVA: 0x0004E97C File Offset: 0x0004CB7C
		public HttpEncoder()
		{
			this._isDefaultEncoder = (base.GetType() == typeof(HttpEncoder));
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x0600195D RID: 6493 RVA: 0x0004E9A0 File Offset: 0x0004CBA0
		// (set) Token: 0x0600195E RID: 6494 RVA: 0x0004E9DF File Offset: 0x0004CBDF
		public static HttpEncoder Current
		{
			get
			{
				HttpContext httpContext = HttpContext.Current;
				if (httpContext != null && httpContext.DisableCustomHttpEncoder)
				{
					return HttpEncoder._defaultEncoder;
				}
				if (HttpEncoder._customEncoder == null)
				{
					HttpEncoder._customEncoder = HttpEncoder._customEncoderResolver.Value;
				}
				return HttpEncoder._customEncoder;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				HttpEncoder._customEncoder = value;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x0600195F RID: 6495 RVA: 0x0004E9F5 File Offset: 0x0004CBF5
		public static HttpEncoder Default
		{
			get
			{
				return HttpEncoder._defaultEncoder;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06001960 RID: 6496 RVA: 0x0004E9FC File Offset: 0x0004CBFC
		internal virtual bool JavaScriptEncodeAmpersand
		{
			get
			{
				return !AppSettings.JavaScriptDoNotEncodeAmpersand;
			}
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x0004EA08 File Offset: 0x0004CC08
		private static void AppendCharAsUnicodeJavaScript(StringBuilder builder, char c)
		{
			builder.Append("\\u");
			int num = (int)c;
			builder.Append(num.ToString("x4", CultureInfo.InvariantCulture));
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x0004EA3C File Offset: 0x0004CC3C
		private bool CharRequiresJavaScriptEncoding(char c)
		{
			return c < ' ' || c == '"' || c == '\\' || c == '\'' || c == '<' || c == '>' || (c == '&' && this.JavaScriptEncodeAmpersand) || c == '\u0085' || c == '\u2028' || c == '\u2029';
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x0004EA90 File Offset: 0x0004CC90
		internal static string CollapsePercentUFromStringInternal(string s, Encoding e)
		{
			int length = s.Length;
			HttpEncoder.UrlDecoder urlDecoder = new HttpEncoder.UrlDecoder(length, e);
			int num = s.IndexOf("%u", StringComparison.Ordinal);
			if (num == -1)
			{
				return s;
			}
			int i = 0;
			while (i < length)
			{
				char c = s[i];
				if (c != '%' || i >= length - 5 || s[i + 1] != 'u')
				{
					goto IL_C8;
				}
				int num2 = HttpEncoderUtility.HexToInt(s[i + 2]);
				int num3 = HttpEncoderUtility.HexToInt(s[i + 3]);
				int num4 = HttpEncoderUtility.HexToInt(s[i + 4]);
				int num5 = HttpEncoderUtility.HexToInt(s[i + 5]);
				if (num2 < 0 || num3 < 0 || num4 < 0 || num5 < 0)
				{
					goto IL_C8;
				}
				c = (char)(num2 << 12 | num3 << 8 | num4 << 4 | num5);
				i += 5;
				urlDecoder.AddChar(c);
				IL_E5:
				i++;
				continue;
				IL_C8:
				if ((c & 'ﾀ') == '\0')
				{
					urlDecoder.AddByte((byte)c);
					goto IL_E5;
				}
				urlDecoder.AddChar(c);
				goto IL_E5;
			}
			return Utf16StringValidator.ValidateString(urlDecoder.GetString());
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x0004EB98 File Offset: 0x0004CD98
		private static HttpEncoder GetCustomEncoderFromConfig()
		{
			RuntimeConfig appConfig = RuntimeConfig.GetAppConfig();
			HttpRuntimeSection httpRuntime = appConfig.HttpRuntime;
			string encoderType = httpRuntime.EncoderType;
			Type type = ConfigUtil.GetType(encoderType, "encoderType", httpRuntime);
			ConfigUtil.CheckBaseType(typeof(HttpEncoder), type, "encoderType", httpRuntime);
			return (HttpEncoder)HttpRuntime.CreatePublicInstanceByWebObjectActivator(type);
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x0004EBEC File Offset: 0x0004CDEC
		private static string HeaderEncodeInternal(string value)
		{
			string result = value;
			if (HttpEncoder.HeaderValueNeedsEncoding(value))
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (char c in value)
				{
					if (c < ' ' && c != '\t')
					{
						stringBuilder.Append(HttpEncoder._headerEncodingTable[(int)c]);
					}
					else if (c == '\u007f')
					{
						stringBuilder.Append("%7f");
					}
					else
					{
						stringBuilder.Append(c);
					}
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x0004EC67 File Offset: 0x0004CE67
		protected internal virtual void HeaderNameValueEncode(string headerName, string headerValue, out string encodedHeaderName, out string encodedHeaderValue)
		{
			encodedHeaderName = (string.IsNullOrEmpty(headerName) ? headerName : HttpEncoder.HeaderEncodeInternal(headerName));
			encodedHeaderValue = (string.IsNullOrEmpty(headerValue) ? headerValue : HttpEncoder.HeaderEncodeInternal(headerValue));
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x0004EC90 File Offset: 0x0004CE90
		private static bool HeaderValueNeedsEncoding(string value)
		{
			foreach (char c in value)
			{
				if ((c < ' ' && c != '\t') || c == '\u007f')
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x0004ECCC File Offset: 0x0004CECC
		internal string HtmlAttributeEncode(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}
			if (this._isDefaultEncoder)
			{
				int num = HttpEncoder.IndexOfHtmlAttributeEncodingChars(value, 0);
				if (num == -1)
				{
					return value;
				}
			}
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			this.HtmlAttributeEncode(value, stringWriter);
			return stringWriter.ToString();
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x0004ED14 File Offset: 0x0004CF14
		protected internal virtual void HtmlAttributeEncode(string value, TextWriter output)
		{
			if (value == null)
			{
				return;
			}
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			HttpWriter httpWriter = output as HttpWriter;
			if (httpWriter != null)
			{
				HttpEncoder.HtmlAttributeEncodeInternal(value, httpWriter);
				return;
			}
			HttpEncoder.HtmlAttributeEncodeInternal(value, output);
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x0004ED4C File Offset: 0x0004CF4C
		private static void HtmlAttributeEncodeInternal(string value, HttpWriter writer)
		{
			int num = HttpEncoder.IndexOfHtmlAttributeEncodingChars(value, 0);
			if (num == -1)
			{
				writer.Write(value);
				return;
			}
			int length = value.Length;
			int num2 = 0;
			do
			{
				if (num > num2)
				{
					writer.WriteString(value, num2, num - num2);
				}
				char c = value[num];
				if (c <= '&')
				{
					if (c != '"')
					{
						if (c == '&')
						{
							writer.Write("&amp;");
						}
					}
					else
					{
						writer.Write("&quot;");
					}
				}
				else if (c != '\'')
				{
					if (c == '<')
					{
						writer.Write("&lt;");
					}
				}
				else
				{
					writer.Write("&#39;");
				}
				num2 = num + 1;
				if (num2 >= length)
				{
					return;
				}
				num = HttpEncoder.IndexOfHtmlAttributeEncodingChars(value, num2);
			}
			while (num != -1);
			writer.WriteString(value, num2, length - num2);
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x0004EDFC File Offset: 0x0004CFFC
		private unsafe static void HtmlAttributeEncodeInternal(string s, TextWriter output)
		{
			int num = HttpEncoder.IndexOfHtmlAttributeEncodingChars(s, 0);
			if (num == -1)
			{
				output.Write(s);
				return;
			}
			int num2 = s.Length - num;
			fixed (string text = s)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* ptr2 = ptr;
				while (num-- > 0)
				{
					output.Write(*(ptr2++));
				}
				while (num2-- > 0)
				{
					char c = *(ptr2++);
					if (c <= '<')
					{
						if (c <= '&')
						{
							if (c == '"')
							{
								output.Write("&quot;");
								continue;
							}
							if (c == '&')
							{
								output.Write("&amp;");
								continue;
							}
						}
						else
						{
							if (c == '\'')
							{
								output.Write("&#39;");
								continue;
							}
							if (c == '<')
							{
								output.Write("&lt;");
								continue;
							}
						}
						output.Write(c);
					}
					else
					{
						output.Write(c);
					}
				}
			}
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x0004EED4 File Offset: 0x0004D0D4
		internal string HtmlDecode(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}
			if (this._isDefaultEncoder)
			{
				return WebUtility.HtmlDecode(value);
			}
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			this.HtmlDecode(value, stringWriter);
			return stringWriter.ToString();
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x0004EF13 File Offset: 0x0004D113
		protected internal virtual void HtmlDecode(string value, TextWriter output)
		{
			WebUtility.HtmlDecode(value, output);
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x0004EF1C File Offset: 0x0004D11C
		internal string HtmlEncode(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}
			if (this._isDefaultEncoder)
			{
				return WebUtility.HtmlEncode(value);
			}
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			this.HtmlEncode(value, stringWriter);
			return stringWriter.ToString();
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x0004EF5B File Offset: 0x0004D15B
		protected internal virtual void HtmlEncode(string value, TextWriter output)
		{
			WebUtility.HtmlEncode(value, output);
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x0004EF64 File Offset: 0x0004D164
		private unsafe static int IndexOfHtmlAttributeEncodingChars(string s, int startPos)
		{
			int i = s.Length - startPos;
			fixed (string text = s)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* ptr2 = ptr + startPos;
				while (i > 0)
				{
					char c = *ptr2;
					if (c <= '<')
					{
						if (c <= '&')
						{
							if (c != '"' && c != '&')
							{
								goto IL_56;
							}
						}
						else if (c != '\'' && c != '<')
						{
							goto IL_56;
						}
						return s.Length - i;
					}
					IL_56:
					ptr2++;
					i--;
				}
			}
			return -1;
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x0004EFD8 File Offset: 0x0004D1D8
		internal static void InitializeOnFirstRequest()
		{
			HttpEncoder value = HttpEncoder._customEncoderResolver.Value;
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x0004EFF0 File Offset: 0x0004D1F0
		private static bool IsNonAsciiByte(byte b)
		{
			return b >= 127 || b < 32;
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x0004F000 File Offset: 0x0004D200
		protected internal virtual string JavaScriptStringEncode(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = null;
			int startIndex = 0;
			int num = 0;
			int i = 0;
			while (i < value.Length)
			{
				char c = value[i];
				if (this.CharRequiresJavaScriptEncoding(c))
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(value.Length + 5);
					}
					if (num > 0)
					{
						stringBuilder.Append(value, startIndex, num);
					}
					startIndex = i + 1;
					num = 0;
				}
				switch (c)
				{
				case '\b':
					stringBuilder.Append("\\b");
					break;
				case '\t':
					stringBuilder.Append("\\t");
					break;
				case '\n':
					stringBuilder.Append("\\n");
					break;
				case '\v':
					goto IL_E4;
				case '\f':
					stringBuilder.Append("\\f");
					break;
				case '\r':
					stringBuilder.Append("\\r");
					break;
				default:
					if (c != '"')
					{
						if (c != '\\')
						{
							goto IL_E4;
						}
						stringBuilder.Append("\\\\");
					}
					else
					{
						stringBuilder.Append("\\\"");
					}
					break;
				}
				IL_FC:
				i++;
				continue;
				IL_E4:
				if (this.CharRequiresJavaScriptEncoding(c))
				{
					HttpEncoder.AppendCharAsUnicodeJavaScript(stringBuilder, c);
					goto IL_FC;
				}
				num++;
				goto IL_FC;
			}
			if (stringBuilder == null)
			{
				return value;
			}
			if (num > 0)
			{
				stringBuilder.Append(value, startIndex, num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x0004F134 File Offset: 0x0004D334
		internal byte[] UrlDecode(byte[] bytes, int offset, int count)
		{
			if (!HttpEncoder.ValidateUrlEncodingParameters(bytes, offset, count))
			{
				return null;
			}
			int num = 0;
			byte[] array = new byte[count];
			for (int i = 0; i < count; i++)
			{
				int num2 = offset + i;
				byte b = bytes[num2];
				if (b == 43)
				{
					b = 32;
				}
				else if (b == 37 && i < count - 2)
				{
					int num3 = HttpEncoderUtility.HexToInt((char)bytes[num2 + 1]);
					int num4 = HttpEncoderUtility.HexToInt((char)bytes[num2 + 2]);
					if (num3 >= 0 && num4 >= 0)
					{
						b = (byte)(num3 << 4 | num4);
						i += 2;
					}
				}
				array[num++] = b;
			}
			if (num < array.Length)
			{
				byte[] array2 = new byte[num];
				Array.Copy(array, array2, num);
				array = array2;
			}
			return array;
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x0004F1D8 File Offset: 0x0004D3D8
		internal string UrlDecode(byte[] bytes, int offset, int count, Encoding encoding)
		{
			if (!HttpEncoder.ValidateUrlEncodingParameters(bytes, offset, count))
			{
				return null;
			}
			HttpEncoder.UrlDecoder urlDecoder = new HttpEncoder.UrlDecoder(count, encoding);
			int i = 0;
			while (i < count)
			{
				int num = offset + i;
				byte b = bytes[num];
				if (b == 43)
				{
					b = 32;
					goto IL_E7;
				}
				if (b != 37 || i >= count - 2)
				{
					goto IL_E7;
				}
				if (bytes[num + 1] == 117 && i < count - 5)
				{
					int num2 = HttpEncoderUtility.HexToInt((char)bytes[num + 2]);
					int num3 = HttpEncoderUtility.HexToInt((char)bytes[num + 3]);
					int num4 = HttpEncoderUtility.HexToInt((char)bytes[num + 4]);
					int num5 = HttpEncoderUtility.HexToInt((char)bytes[num + 5]);
					if (num2 < 0 || num3 < 0 || num4 < 0 || num5 < 0)
					{
						goto IL_E7;
					}
					char ch = (char)(num2 << 12 | num3 << 8 | num4 << 4 | num5);
					i += 5;
					urlDecoder.AddChar(ch);
				}
				else
				{
					int num6 = HttpEncoderUtility.HexToInt((char)bytes[num + 1]);
					int num7 = HttpEncoderUtility.HexToInt((char)bytes[num + 2]);
					if (num6 >= 0 && num7 >= 0)
					{
						b = (byte)(num6 << 4 | num7);
						i += 2;
						goto IL_E7;
					}
					goto IL_E7;
				}
				IL_EE:
				i++;
				continue;
				IL_E7:
				urlDecoder.AddByte(b);
				goto IL_EE;
			}
			return Utf16StringValidator.ValidateString(urlDecoder.GetString());
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x0004F2EC File Offset: 0x0004D4EC
		internal string UrlDecode(string value, Encoding encoding)
		{
			if (value == null)
			{
				return null;
			}
			int length = value.Length;
			HttpEncoder.UrlDecoder urlDecoder = new HttpEncoder.UrlDecoder(length, encoding);
			int i = 0;
			while (i < length)
			{
				char c = value[i];
				if (c == '+')
				{
					c = ' ';
					goto IL_10B;
				}
				if (c != '%' || i >= length - 2)
				{
					goto IL_10B;
				}
				if (value[i + 1] == 'u' && i < length - 5)
				{
					int num = HttpEncoderUtility.HexToInt(value[i + 2]);
					int num2 = HttpEncoderUtility.HexToInt(value[i + 3]);
					int num3 = HttpEncoderUtility.HexToInt(value[i + 4]);
					int num4 = HttpEncoderUtility.HexToInt(value[i + 5]);
					if (num < 0 || num2 < 0 || num3 < 0 || num4 < 0)
					{
						goto IL_10B;
					}
					c = (char)(num << 12 | num2 << 8 | num3 << 4 | num4);
					i += 5;
					urlDecoder.AddChar(c);
				}
				else
				{
					int num5 = HttpEncoderUtility.HexToInt(value[i + 1]);
					int num6 = HttpEncoderUtility.HexToInt(value[i + 2]);
					if (num5 < 0 || num6 < 0)
					{
						goto IL_10B;
					}
					byte b = (byte)(num5 << 4 | num6);
					i += 2;
					urlDecoder.AddByte(b);
				}
				IL_125:
				i++;
				continue;
				IL_10B:
				if ((c & 'ﾀ') == '\0')
				{
					urlDecoder.AddByte((byte)c);
					goto IL_125;
				}
				urlDecoder.AddChar(c);
				goto IL_125;
			}
			return Utf16StringValidator.ValidateString(urlDecoder.GetString());
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x0004F434 File Offset: 0x0004D634
		internal byte[] UrlEncode(byte[] bytes, int offset, int count, bool alwaysCreateNewReturnValue)
		{
			byte[] array = this.UrlEncode(bytes, offset, count);
			if (!alwaysCreateNewReturnValue || array == null || array != bytes)
			{
				return array;
			}
			return (byte[])array.Clone();
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x0004F464 File Offset: 0x0004D664
		protected internal virtual byte[] UrlEncode(byte[] bytes, int offset, int count)
		{
			if (!HttpEncoder.ValidateUrlEncodingParameters(bytes, offset, count))
			{
				return null;
			}
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < count; i++)
			{
				char c = (char)bytes[offset + i];
				if (c == ' ')
				{
					num++;
				}
				else if (!HttpEncoderUtility.IsUrlSafeChar(c))
				{
					num2++;
				}
			}
			if (num != 0 || num2 != 0)
			{
				byte[] array = new byte[count + num2 * 2];
				int num3 = 0;
				for (int j = 0; j < count; j++)
				{
					byte b = bytes[offset + j];
					char c2 = (char)b;
					if (HttpEncoderUtility.IsUrlSafeChar(c2))
					{
						array[num3++] = b;
					}
					else if (c2 == ' ')
					{
						array[num3++] = 43;
					}
					else
					{
						array[num3++] = 37;
						array[num3++] = (byte)HttpEncoderUtility.IntToHex(b >> 4 & 15);
						array[num3++] = (byte)HttpEncoderUtility.IntToHex((int)(b & 15));
					}
				}
				return array;
			}
			if (offset == 0 && bytes.Length == count)
			{
				return bytes;
			}
			byte[] array2 = new byte[count];
			Buffer.BlockCopy(bytes, offset, array2, 0, count);
			return array2;
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x0004F558 File Offset: 0x0004D758
		internal string UrlEncodeNonAscii(string str, Encoding e)
		{
			if (string.IsNullOrEmpty(str))
			{
				return str;
			}
			if (e == null)
			{
				e = Encoding.UTF8;
			}
			byte[] bytes = e.GetBytes(str);
			byte[] bytes2 = this.UrlEncodeNonAscii(bytes, 0, bytes.Length, false);
			return Encoding.ASCII.GetString(bytes2);
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x0004F59C File Offset: 0x0004D79C
		internal byte[] UrlEncodeNonAscii(byte[] bytes, int offset, int count, bool alwaysCreateNewReturnValue)
		{
			if (!HttpEncoder.ValidateUrlEncodingParameters(bytes, offset, count))
			{
				return null;
			}
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				if (HttpEncoder.IsNonAsciiByte(bytes[offset + i]))
				{
					num++;
				}
			}
			if (!alwaysCreateNewReturnValue && num == 0)
			{
				return bytes;
			}
			byte[] array = new byte[count + num * 2];
			int num2 = 0;
			for (int j = 0; j < count; j++)
			{
				byte b = bytes[offset + j];
				if (HttpEncoder.IsNonAsciiByte(b))
				{
					array[num2++] = 37;
					array[num2++] = (byte)HttpEncoderUtility.IntToHex(b >> 4 & 15);
					array[num2++] = (byte)HttpEncoderUtility.IntToHex((int)(b & 15));
				}
				else
				{
					array[num2++] = b;
				}
			}
			return array;
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x0004F648 File Offset: 0x0004D848
		[Obsolete("This method produces non-standards-compliant output and has interoperability issues. The preferred alternative is UrlEncode(*).")]
		internal string UrlEncodeUnicode(string value, bool ignoreAscii)
		{
			if (value == null)
			{
				return null;
			}
			int length = value.Length;
			StringBuilder stringBuilder = new StringBuilder(length);
			for (int i = 0; i < length; i++)
			{
				char c = value[i];
				if ((c & 'ﾀ') == '\0')
				{
					if (ignoreAscii || HttpEncoderUtility.IsUrlSafeChar(c))
					{
						stringBuilder.Append(c);
					}
					else if (c == ' ')
					{
						stringBuilder.Append('+');
					}
					else
					{
						stringBuilder.Append('%');
						stringBuilder.Append(HttpEncoderUtility.IntToHex((int)(c >> 4 & '\u000f')));
						stringBuilder.Append(HttpEncoderUtility.IntToHex((int)(c & '\u000f')));
					}
				}
				else
				{
					stringBuilder.Append("%u");
					stringBuilder.Append(HttpEncoderUtility.IntToHex((int)(c >> 12 & '\u000f')));
					stringBuilder.Append(HttpEncoderUtility.IntToHex((int)(c >> 8 & '\u000f')));
					stringBuilder.Append(HttpEncoderUtility.IntToHex((int)(c >> 4 & '\u000f')));
					stringBuilder.Append(HttpEncoderUtility.IntToHex((int)(c & '\u000f')));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x0004F73C File Offset: 0x0004D93C
		protected internal virtual string UrlPathEncode(string value)
		{
			if (!BinaryCompatibility.Current.TargetsAtLeastFramework46)
			{
				return this.UrlPathEncodeImpl(value);
			}
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}
			string str;
			string value2;
			string str2;
			if (!UriUtil.TrySplitUriForPathEncode(value, out str, out value2, out str2, false))
			{
				str = null;
				value2 = value;
				str2 = null;
			}
			return str + this.UrlPathEncodeImpl(value2) + str2;
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x0004F78C File Offset: 0x0004D98C
		private string UrlPathEncodeImpl(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}
			int num = value.IndexOf('?');
			if (num >= 0)
			{
				return this.UrlPathEncodeImpl(value.Substring(0, num)) + value.Substring(num);
			}
			return HttpEncoderUtility.UrlEncodeSpaces(this.UrlEncodeNonAscii(value, Encoding.UTF8));
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x0004F7DC File Offset: 0x0004D9DC
		internal byte[] UrlTokenDecode(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			int length = input.Length;
			if (length < 1)
			{
				return new byte[0];
			}
			int num = (int)(input[length - 1] - '0');
			if (num < 0 || num > 10)
			{
				return null;
			}
			char[] array = new char[length - 1 + num];
			for (int i = 0; i < length - 1; i++)
			{
				char c = input[i];
				if (c != '-')
				{
					if (c != '_')
					{
						array[i] = c;
					}
					else
					{
						array[i] = '/';
					}
				}
				else
				{
					array[i] = '+';
				}
			}
			for (int j = length - 1; j < array.Length; j++)
			{
				array[j] = '=';
			}
			return Convert.FromBase64CharArray(array, 0, array.Length);
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x0004F888 File Offset: 0x0004DA88
		internal string UrlTokenEncode(byte[] input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (input.Length < 1)
			{
				return string.Empty;
			}
			string text = Convert.ToBase64String(input);
			if (text == null)
			{
				return null;
			}
			int num = text.Length;
			while (num > 0 && text[num - 1] == '=')
			{
				num--;
			}
			char[] array = new char[num + 1];
			array[num] = (char)(48 + text.Length - num);
			for (int i = 0; i < num; i++)
			{
				char c = text[i];
				if (c != '+')
				{
					if (c != '/')
					{
						if (c != '=')
						{
							array[i] = c;
						}
						else
						{
							array[i] = c;
						}
					}
					else
					{
						array[i] = '_';
					}
				}
				else
				{
					array[i] = '-';
				}
			}
			return new string(array);
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x0004F940 File Offset: 0x0004DB40
		internal static bool ValidateUrlEncodingParameters(byte[] bytes, int offset, int count)
		{
			if (bytes == null && count == 0)
			{
				return false;
			}
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (offset < 0 || offset > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || offset + count > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			return true;
		}

		// Token: 0x040017CD RID: 6093
		private static HttpEncoder _customEncoder;

		// Token: 0x040017CE RID: 6094
		private readonly bool _isDefaultEncoder;

		// Token: 0x040017CF RID: 6095
		private static readonly Lazy<HttpEncoder> _customEncoderResolver = new Lazy<HttpEncoder>(new Func<HttpEncoder>(HttpEncoder.GetCustomEncoderFromConfig));

		// Token: 0x040017D0 RID: 6096
		private static readonly HttpEncoder _defaultEncoder = new HttpEncoder();

		// Token: 0x040017D1 RID: 6097
		private static readonly string[] _headerEncodingTable = new string[]
		{
			"%00",
			"%01",
			"%02",
			"%03",
			"%04",
			"%05",
			"%06",
			"%07",
			"%08",
			"%09",
			"%0a",
			"%0b",
			"%0c",
			"%0d",
			"%0e",
			"%0f",
			"%10",
			"%11",
			"%12",
			"%13",
			"%14",
			"%15",
			"%16",
			"%17",
			"%18",
			"%19",
			"%1a",
			"%1b",
			"%1c",
			"%1d",
			"%1e",
			"%1f"
		};

		// Token: 0x02000948 RID: 2376
		private class UrlDecoder
		{
			// Token: 0x06006987 RID: 27015 RVA: 0x00177758 File Offset: 0x00175958
			private void FlushBytes()
			{
				if (this._numBytes > 0)
				{
					this._numChars += this._encoding.GetChars(this._byteBuffer, 0, this._numBytes, this._charBuffer, this._numChars);
					this._numBytes = 0;
				}
			}

			// Token: 0x06006988 RID: 27016 RVA: 0x001777A6 File Offset: 0x001759A6
			internal UrlDecoder(int bufferSize, Encoding encoding)
			{
				this._bufferSize = bufferSize;
				this._encoding = encoding;
				this._charBuffer = new char[bufferSize];
			}

			// Token: 0x06006989 RID: 27017 RVA: 0x001777C8 File Offset: 0x001759C8
			internal void AddChar(char ch)
			{
				if (this._numBytes > 0)
				{
					this.FlushBytes();
				}
				char[] charBuffer = this._charBuffer;
				int numChars = this._numChars;
				this._numChars = numChars + 1;
				charBuffer[numChars] = ch;
			}

			// Token: 0x0600698A RID: 27018 RVA: 0x00177800 File Offset: 0x00175A00
			internal void AddByte(byte b)
			{
				if (this._byteBuffer == null)
				{
					this._byteBuffer = new byte[this._bufferSize];
				}
				byte[] byteBuffer = this._byteBuffer;
				int numBytes = this._numBytes;
				this._numBytes = numBytes + 1;
				byteBuffer[numBytes] = b;
			}

			// Token: 0x0600698B RID: 27019 RVA: 0x0017783F File Offset: 0x00175A3F
			internal string GetString()
			{
				if (this._numBytes > 0)
				{
					this.FlushBytes();
				}
				if (this._numChars > 0)
				{
					return new string(this._charBuffer, 0, this._numChars);
				}
				return string.Empty;
			}

			// Token: 0x040037C9 RID: 14281
			private int _bufferSize;

			// Token: 0x040037CA RID: 14282
			private int _numChars;

			// Token: 0x040037CB RID: 14283
			private char[] _charBuffer;

			// Token: 0x040037CC RID: 14284
			private int _numBytes;

			// Token: 0x040037CD RID: 14285
			private byte[] _byteBuffer;

			// Token: 0x040037CE RID: 14286
			private Encoding _encoding;
		}
	}
}

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.Text;

namespace System.Runtime
{
	// Token: 0x02000036 RID: 54
	internal static class UrlUtility
	{
		// Token: 0x060001B0 RID: 432 RVA: 0x00007A64 File Offset: 0x00005C64
		public static NameValueCollection ParseQueryString(string query)
		{
			return UrlUtility.ParseQueryString(query, Encoding.UTF8);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00007A74 File Offset: 0x00005C74
		public static NameValueCollection ParseQueryString(string query, Encoding encoding)
		{
			if (query == null)
			{
				throw Fx.Exception.ArgumentNull("query");
			}
			if (encoding == null)
			{
				throw Fx.Exception.ArgumentNull("encoding");
			}
			if (query.Length > 0 && query[0] == '?')
			{
				query = query.Substring(1);
			}
			return new UrlUtility.HttpValueCollection(query, encoding);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00007ACB File Offset: 0x00005CCB
		public static string UrlEncode(string str)
		{
			if (str == null)
			{
				return null;
			}
			return UrlUtility.UrlEncode(str, Encoding.UTF8);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00007AE0 File Offset: 0x00005CE0
		public static string UrlPathEncode(string str)
		{
			if (str == null)
			{
				return null;
			}
			int num = str.IndexOf('?');
			if (num >= 0)
			{
				return UrlUtility.UrlPathEncode(str.Substring(0, num)) + str.Substring(num);
			}
			return UrlUtility.UrlEncodeSpaces(UrlUtility.UrlEncodeNonAscii(str, Encoding.UTF8));
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00007B29 File Offset: 0x00005D29
		public static string UrlEncode(string str, Encoding encoding)
		{
			if (str == null)
			{
				return null;
			}
			return Encoding.ASCII.GetString(UrlUtility.UrlEncodeToBytes(str, encoding));
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00007B41 File Offset: 0x00005D41
		public static string UrlEncodeUnicode(string str)
		{
			if (str == null)
			{
				return null;
			}
			return UrlUtility.UrlEncodeUnicodeStringToStringInternal(str, false);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00007B50 File Offset: 0x00005D50
		private static string UrlEncodeUnicodeStringToStringInternal(string s, bool ignoreAscii)
		{
			int length = s.Length;
			StringBuilder stringBuilder = new StringBuilder(length);
			for (int i = 0; i < length; i++)
			{
				char c = s[i];
				if ((c & 'ﾀ') == '\0')
				{
					if (ignoreAscii || UrlUtility.IsSafe(c))
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
						stringBuilder.Append(UrlUtility.IntToHex((int)(c >> 4 & '\u000f')));
						stringBuilder.Append(UrlUtility.IntToHex((int)(c & '\u000f')));
					}
				}
				else
				{
					stringBuilder.Append("%u");
					stringBuilder.Append(UrlUtility.IntToHex((int)(c >> 12 & '\u000f')));
					stringBuilder.Append(UrlUtility.IntToHex((int)(c >> 8 & '\u000f')));
					stringBuilder.Append(UrlUtility.IntToHex((int)(c >> 4 & '\u000f')));
					stringBuilder.Append(UrlUtility.IntToHex((int)(c & '\u000f')));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00007C40 File Offset: 0x00005E40
		private static string UrlEncodeNonAscii(string str, Encoding e)
		{
			if (string.IsNullOrEmpty(str))
			{
				return str;
			}
			if (e == null)
			{
				e = Encoding.UTF8;
			}
			byte[] array = e.GetBytes(str);
			array = UrlUtility.UrlEncodeBytesToBytesInternalNonAscii(array, 0, array.Length, false);
			return Encoding.ASCII.GetString(array);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00007C80 File Offset: 0x00005E80
		private static string UrlEncodeSpaces(string str)
		{
			if (str != null && str.IndexOf(' ') >= 0)
			{
				str = str.Replace(" ", "%20");
			}
			return str;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00007CA4 File Offset: 0x00005EA4
		public static byte[] UrlEncodeToBytes(string str, Encoding e)
		{
			if (str == null)
			{
				return null;
			}
			byte[] bytes = e.GetBytes(str);
			return UrlUtility.UrlEncodeBytesToBytesInternal(bytes, 0, bytes.Length, false);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00007CC9 File Offset: 0x00005EC9
		public static string UrlDecode(string str, Encoding e)
		{
			if (str == null)
			{
				return null;
			}
			return UrlUtility.UrlDecodeStringFromStringInternal(str, e);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00007CD8 File Offset: 0x00005ED8
		private static byte[] UrlEncodeBytesToBytesInternal(byte[] bytes, int offset, int count, bool alwaysCreateReturnValue)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < count; i++)
			{
				char c = (char)bytes[offset + i];
				if (c == ' ')
				{
					num++;
				}
				else if (!UrlUtility.IsSafe(c))
				{
					num2++;
				}
			}
			if (!alwaysCreateReturnValue && num == 0 && num2 == 0)
			{
				return bytes;
			}
			byte[] array = new byte[count + num2 * 2];
			int num3 = 0;
			for (int j = 0; j < count; j++)
			{
				byte b = bytes[offset + j];
				char c2 = (char)b;
				if (UrlUtility.IsSafe(c2))
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
					array[num3++] = (byte)UrlUtility.IntToHex(b >> 4 & 15);
					array[num3++] = (byte)UrlUtility.IntToHex((int)(b & 15));
				}
			}
			return array;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00007DA3 File Offset: 0x00005FA3
		private static bool IsNonAsciiByte(byte b)
		{
			return b >= 127 || b < 32;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00007DB4 File Offset: 0x00005FB4
		private static byte[] UrlEncodeBytesToBytesInternalNonAscii(byte[] bytes, int offset, int count, bool alwaysCreateReturnValue)
		{
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				if (UrlUtility.IsNonAsciiByte(bytes[offset + i]))
				{
					num++;
				}
			}
			if (!alwaysCreateReturnValue && num == 0)
			{
				return bytes;
			}
			byte[] array = new byte[count + num * 2];
			int num2 = 0;
			for (int j = 0; j < count; j++)
			{
				byte b = bytes[offset + j];
				if (UrlUtility.IsNonAsciiByte(b))
				{
					array[num2++] = 37;
					array[num2++] = (byte)UrlUtility.IntToHex(b >> 4 & 15);
					array[num2++] = (byte)UrlUtility.IntToHex((int)(b & 15));
				}
				else
				{
					array[num2++] = b;
				}
			}
			return array;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00007E50 File Offset: 0x00006050
		private static string UrlDecodeStringFromStringInternal(string s, Encoding e)
		{
			int length = s.Length;
			UrlUtility.UrlDecoder urlDecoder = new UrlUtility.UrlDecoder(length, e);
			int i = 0;
			while (i < length)
			{
				char c = s[i];
				if (c == '+')
				{
					c = ' ';
					goto IL_106;
				}
				if (c != '%' || i >= length - 2)
				{
					goto IL_106;
				}
				if (s[i + 1] == 'u' && i < length - 5)
				{
					int num = UrlUtility.HexToInt(s[i + 2]);
					int num2 = UrlUtility.HexToInt(s[i + 3]);
					int num3 = UrlUtility.HexToInt(s[i + 4]);
					int num4 = UrlUtility.HexToInt(s[i + 5]);
					if (num < 0 || num2 < 0 || num3 < 0 || num4 < 0)
					{
						goto IL_106;
					}
					c = (char)(num << 12 | num2 << 8 | num3 << 4 | num4);
					i += 5;
					urlDecoder.AddChar(c);
				}
				else
				{
					int num5 = UrlUtility.HexToInt(s[i + 1]);
					int num6 = UrlUtility.HexToInt(s[i + 2]);
					if (num5 < 0 || num6 < 0)
					{
						goto IL_106;
					}
					byte b = (byte)(num5 << 4 | num6);
					i += 2;
					urlDecoder.AddByte(b);
				}
				IL_120:
				i++;
				continue;
				IL_106:
				if ((c & 'ﾀ') == '\0')
				{
					urlDecoder.AddByte((byte)c);
					goto IL_120;
				}
				urlDecoder.AddChar(c);
				goto IL_120;
			}
			return urlDecoder.GetString();
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00007F8E File Offset: 0x0000618E
		private static int HexToInt(char h)
		{
			if (h >= '0' && h <= '9')
			{
				return (int)(h - '0');
			}
			if (h >= 'a' && h <= 'f')
			{
				return (int)(h - 'a' + '\n');
			}
			if (h < 'A' || h > 'F')
			{
				return -1;
			}
			return (int)(h - 'A' + '\n');
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00007FC4 File Offset: 0x000061C4
		private static char IntToHex(int n)
		{
			if (n <= 9)
			{
				return (char)(n + 48);
			}
			return (char)(n - 10 + 97);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00007FDC File Offset: 0x000061DC
		internal static bool IsSafe(char ch)
		{
			if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || (ch >= '0' && ch <= '9'))
			{
				return true;
			}
			if (ch != '!')
			{
				switch (ch)
				{
				case '\'':
				case '(':
				case ')':
				case '*':
				case '-':
				case '.':
					return true;
				case '+':
				case ',':
					break;
				default:
					if (ch == '_')
					{
						return true;
					}
					break;
				}
				return false;
			}
			return true;
		}

		// Token: 0x02000089 RID: 137
		private class UrlDecoder
		{
			// Token: 0x06000426 RID: 1062 RVA: 0x0001368C File Offset: 0x0001188C
			private void FlushBytes()
			{
				if (this._numBytes > 0)
				{
					this._numChars += this._encoding.GetChars(this._byteBuffer, 0, this._numBytes, this._charBuffer, this._numChars);
					this._numBytes = 0;
				}
			}

			// Token: 0x06000427 RID: 1063 RVA: 0x000136DA File Offset: 0x000118DA
			internal UrlDecoder(int bufferSize, Encoding encoding)
			{
				this._bufferSize = bufferSize;
				this._encoding = encoding;
				this._charBuffer = new char[bufferSize];
			}

			// Token: 0x06000428 RID: 1064 RVA: 0x000136FC File Offset: 0x000118FC
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

			// Token: 0x06000429 RID: 1065 RVA: 0x00013734 File Offset: 0x00011934
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

			// Token: 0x0600042A RID: 1066 RVA: 0x00013773 File Offset: 0x00011973
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

			// Token: 0x04000294 RID: 660
			private int _bufferSize;

			// Token: 0x04000295 RID: 661
			private int _numChars;

			// Token: 0x04000296 RID: 662
			private char[] _charBuffer;

			// Token: 0x04000297 RID: 663
			private int _numBytes;

			// Token: 0x04000298 RID: 664
			private byte[] _byteBuffer;

			// Token: 0x04000299 RID: 665
			private Encoding _encoding;
		}

		// Token: 0x0200008A RID: 138
		[Serializable]
		private class HttpValueCollection : NameValueCollection
		{
			// Token: 0x0600042B RID: 1067 RVA: 0x000137A5 File Offset: 0x000119A5
			internal HttpValueCollection(string str, Encoding encoding) : base(StringComparer.OrdinalIgnoreCase)
			{
				if (!string.IsNullOrEmpty(str))
				{
					this.FillFromString(str, true, encoding);
				}
				base.IsReadOnly = false;
			}

			// Token: 0x0600042C RID: 1068 RVA: 0x000137CA File Offset: 0x000119CA
			protected HttpValueCollection(SerializationInfo info, StreamingContext context) : base(info, context)
			{
			}

			// Token: 0x0600042D RID: 1069 RVA: 0x000137D4 File Offset: 0x000119D4
			internal void FillFromString(string s, bool urlencoded, Encoding encoding)
			{
				int num = (s != null) ? s.Length : 0;
				for (int i = 0; i < num; i++)
				{
					int num2 = i;
					int num3 = -1;
					while (i < num)
					{
						char c = s[i];
						if (c == '=')
						{
							if (num3 < 0)
							{
								num3 = i;
							}
						}
						else if (c == '&')
						{
							break;
						}
						i++;
					}
					string text = null;
					string text2;
					if (num3 >= 0)
					{
						text = s.Substring(num2, num3 - num2);
						text2 = s.Substring(num3 + 1, i - num3 - 1);
					}
					else
					{
						text2 = s.Substring(num2, i - num2);
					}
					if (urlencoded)
					{
						base.Add(UrlUtility.UrlDecode(text, encoding), UrlUtility.UrlDecode(text2, encoding));
					}
					else
					{
						base.Add(text, text2);
					}
					if (i == num - 1 && s[i] == '&')
					{
						base.Add(null, string.Empty);
					}
				}
			}

			// Token: 0x0600042E RID: 1070 RVA: 0x000138A1 File Offset: 0x00011AA1
			public override string ToString()
			{
				return this.ToString(true, null);
			}

			// Token: 0x0600042F RID: 1071 RVA: 0x000138AC File Offset: 0x00011AAC
			private string ToString(bool urlencoded, IDictionary excludeKeys)
			{
				int count = this.Count;
				if (count == 0)
				{
					return string.Empty;
				}
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < count; i++)
				{
					string text = this.GetKey(i);
					if (excludeKeys == null || text == null || excludeKeys[text] == null)
					{
						if (urlencoded)
						{
							text = UrlUtility.UrlEncodeUnicode(text);
						}
						string value = (!string.IsNullOrEmpty(text)) ? (text + "=") : string.Empty;
						ArrayList arrayList = (ArrayList)base.BaseGet(i);
						int num = (arrayList != null) ? arrayList.Count : 0;
						if (stringBuilder.Length > 0)
						{
							stringBuilder.Append('&');
						}
						if (num == 1)
						{
							stringBuilder.Append(value);
							string text2 = (string)arrayList[0];
							if (urlencoded)
							{
								text2 = UrlUtility.UrlEncodeUnicode(text2);
							}
							stringBuilder.Append(text2);
						}
						else if (num == 0)
						{
							stringBuilder.Append(value);
						}
						else
						{
							for (int j = 0; j < num; j++)
							{
								if (j > 0)
								{
									stringBuilder.Append('&');
								}
								stringBuilder.Append(value);
								string text2 = (string)arrayList[j];
								if (urlencoded)
								{
									text2 = UrlUtility.UrlEncodeUnicode(text2);
								}
								stringBuilder.Append(text2);
							}
						}
					}
				}
				return stringBuilder.ToString();
			}
		}
	}
}

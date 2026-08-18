using System;
using System.Text;

namespace System.Web.Http
{
	// Token: 0x02000009 RID: 9
	internal static class UriQueryUtility
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00002914 File Offset: 0x00000B14
		public static string UrlEncode(string str)
		{
			if (str == null)
			{
				return null;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(str);
			return Encoding.ASCII.GetString(UriQueryUtility.UrlEncode(bytes, 0, bytes.Length, false));
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002947 File Offset: 0x00000B47
		public static string UrlDecode(string str)
		{
			if (str == null)
			{
				return null;
			}
			return UriQueryUtility.UrlDecodeInternal(str, Encoding.UTF8);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000295C File Offset: 0x00000B5C
		private static byte[] UrlEncode(byte[] bytes, int offset, int count, bool alwaysCreateNewReturnValue)
		{
			byte[] array = UriQueryUtility.UrlEncode(bytes, offset, count);
			if (!alwaysCreateNewReturnValue || array == null || array != bytes)
			{
				return array;
			}
			return (byte[])array.Clone();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000298C File Offset: 0x00000B8C
		private static byte[] UrlEncode(byte[] bytes, int offset, int count)
		{
			if (!UriQueryUtility.ValidateUrlEncodingParameters(bytes, offset, count))
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
				else if (!UriQueryUtility.IsUrlSafeChar(c))
				{
					num2++;
				}
			}
			if (num == 0 && num2 == 0)
			{
				return bytes;
			}
			byte[] array = new byte[count + num2 * 2];
			int num3 = 0;
			for (int j = 0; j < count; j++)
			{
				byte b = bytes[offset + j];
				char c2 = (char)b;
				if (UriQueryUtility.IsUrlSafeChar(c2))
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
					array[num3++] = (byte)UriQueryUtility.IntToHex(b >> 4 & 15);
					array[num3++] = (byte)UriQueryUtility.IntToHex((int)(b & 15));
				}
			}
			return array;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002A6C File Offset: 0x00000C6C
		private static string UrlDecodeInternal(string value, Encoding encoding)
		{
			if (value == null)
			{
				return null;
			}
			int length = value.Length;
			UriQueryUtility.UrlDecoder urlDecoder = new UriQueryUtility.UrlDecoder(length, encoding);
			int i = 0;
			while (i < length)
			{
				char c = value[i];
				if (c == '+')
				{
					c = ' ';
					goto IL_77;
				}
				if (c != '%' || i >= length - 2)
				{
					goto IL_77;
				}
				int num = UriQueryUtility.HexToInt(value[i + 1]);
				int num2 = UriQueryUtility.HexToInt(value[i + 2]);
				if (num < 0 || num2 < 0)
				{
					goto IL_77;
				}
				byte b = (byte)(num << 4 | num2);
				i += 2;
				urlDecoder.AddByte(b);
				IL_91:
				i++;
				continue;
				IL_77:
				if ((c & 'ﾀ') == '\0')
				{
					urlDecoder.AddByte((byte)c);
					goto IL_91;
				}
				urlDecoder.AddChar(c);
				goto IL_91;
			}
			return urlDecoder.GetString();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002B1B File Offset: 0x00000D1B
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

		// Token: 0x0600003A RID: 58 RVA: 0x00002B51 File Offset: 0x00000D51
		private static char IntToHex(int n)
		{
			if (n <= 9)
			{
				return (char)(n + 48);
			}
			return (char)(n - 10 + 97);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002B68 File Offset: 0x00000D68
		private static bool IsUrlSafeChar(char ch)
		{
			if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || (ch >= '0' && ch <= '9'))
			{
				return true;
			}
			if (ch != '!')
			{
				switch (ch)
				{
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

		// Token: 0x0600003C RID: 60 RVA: 0x00002BCC File Offset: 0x00000DCC
		private static bool ValidateUrlEncodingParameters(byte[] bytes, int offset, int count)
		{
			if (bytes == null && count == 0)
			{
				return false;
			}
			if (bytes == null)
			{
				throw Error.ArgumentNull("bytes");
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

		// Token: 0x0200000A RID: 10
		private class UrlDecoder
		{
			// Token: 0x0600003D RID: 61 RVA: 0x00002C1C File Offset: 0x00000E1C
			private void FlushBytes()
			{
				if (this._numBytes > 0)
				{
					this._numChars += this._encoding.GetChars(this._byteBuffer, 0, this._numBytes, this._charBuffer, this._numChars);
					this._numBytes = 0;
				}
			}

			// Token: 0x0600003E RID: 62 RVA: 0x00002C6A File Offset: 0x00000E6A
			internal UrlDecoder(int bufferSize, Encoding encoding)
			{
				this._bufferSize = bufferSize;
				this._encoding = encoding;
				this._charBuffer = new char[bufferSize];
			}

			// Token: 0x0600003F RID: 63 RVA: 0x00002C8C File Offset: 0x00000E8C
			internal void AddChar(char ch)
			{
				if (this._numBytes > 0)
				{
					this.FlushBytes();
				}
				this._charBuffer[this._numChars++] = ch;
			}

			// Token: 0x06000040 RID: 64 RVA: 0x00002CC4 File Offset: 0x00000EC4
			internal void AddByte(byte b)
			{
				if (this._byteBuffer == null)
				{
					this._byteBuffer = new byte[this._bufferSize];
				}
				this._byteBuffer[this._numBytes++] = b;
			}

			// Token: 0x06000041 RID: 65 RVA: 0x00002D03 File Offset: 0x00000F03
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

			// Token: 0x04000007 RID: 7
			private int _bufferSize;

			// Token: 0x04000008 RID: 8
			private int _numChars;

			// Token: 0x04000009 RID: 9
			private char[] _charBuffer;

			// Token: 0x0400000A RID: 10
			private int _numBytes;

			// Token: 0x0400000B RID: 11
			private byte[] _byteBuffer;

			// Token: 0x0400000C RID: 12
			private Encoding _encoding;
		}
	}
}

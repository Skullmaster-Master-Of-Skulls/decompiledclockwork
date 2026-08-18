using System;
using System.Text;

namespace System.Web.Http
{
	// Token: 0x0200001D RID: 29
	internal static class UriQueryUtility
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x000046A4 File Offset: 0x000028A4
		public static string UrlEncode(string str)
		{
			if (str == null)
			{
				return null;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(str);
			return Encoding.ASCII.GetString(UriQueryUtility.UrlEncode(bytes, 0, bytes.Length, false));
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000046D7 File Offset: 0x000028D7
		public static string UrlDecode(string str)
		{
			if (str == null)
			{
				return null;
			}
			return UriQueryUtility.UrlDecodeInternal(str, Encoding.UTF8);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000046EC File Offset: 0x000028EC
		private static byte[] UrlEncode(byte[] bytes, int offset, int count, bool alwaysCreateNewReturnValue)
		{
			byte[] array = UriQueryUtility.UrlEncode(bytes, offset, count);
			if (!alwaysCreateNewReturnValue || array == null || array != bytes)
			{
				return array;
			}
			return (byte[])array.Clone();
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000471C File Offset: 0x0000291C
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

		// Token: 0x060000B5 RID: 181 RVA: 0x000047FC File Offset: 0x000029FC
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

		// Token: 0x060000B6 RID: 182 RVA: 0x000048AB File Offset: 0x00002AAB
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

		// Token: 0x060000B7 RID: 183 RVA: 0x000048E1 File Offset: 0x00002AE1
		private static char IntToHex(int n)
		{
			if (n <= 9)
			{
				return (char)(n + 48);
			}
			return (char)(n - 10 + 97);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000048F8 File Offset: 0x00002AF8
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

		// Token: 0x060000B9 RID: 185 RVA: 0x0000495C File Offset: 0x00002B5C
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

		// Token: 0x0200001E RID: 30
		private class UrlDecoder
		{
			// Token: 0x060000BA RID: 186 RVA: 0x000049AC File Offset: 0x00002BAC
			private void FlushBytes()
			{
				if (this._numBytes > 0)
				{
					this._numChars += this._encoding.GetChars(this._byteBuffer, 0, this._numBytes, this._charBuffer, this._numChars);
					this._numBytes = 0;
				}
			}

			// Token: 0x060000BB RID: 187 RVA: 0x000049FA File Offset: 0x00002BFA
			internal UrlDecoder(int bufferSize, Encoding encoding)
			{
				this._bufferSize = bufferSize;
				this._encoding = encoding;
				this._charBuffer = new char[bufferSize];
			}

			// Token: 0x060000BC RID: 188 RVA: 0x00004A1C File Offset: 0x00002C1C
			internal void AddChar(char ch)
			{
				if (this._numBytes > 0)
				{
					this.FlushBytes();
				}
				this._charBuffer[this._numChars++] = ch;
			}

			// Token: 0x060000BD RID: 189 RVA: 0x00004A54 File Offset: 0x00002C54
			internal void AddByte(byte b)
			{
				if (this._byteBuffer == null)
				{
					this._byteBuffer = new byte[this._bufferSize];
				}
				this._byteBuffer[this._numBytes++] = b;
			}

			// Token: 0x060000BE RID: 190 RVA: 0x00004A93 File Offset: 0x00002C93
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

			// Token: 0x04000031 RID: 49
			private int _bufferSize;

			// Token: 0x04000032 RID: 50
			private int _numChars;

			// Token: 0x04000033 RID: 51
			private char[] _charBuffer;

			// Token: 0x04000034 RID: 52
			private int _numBytes;

			// Token: 0x04000035 RID: 53
			private byte[] _byteBuffer;

			// Token: 0x04000036 RID: 54
			private Encoding _encoding;
		}
	}
}

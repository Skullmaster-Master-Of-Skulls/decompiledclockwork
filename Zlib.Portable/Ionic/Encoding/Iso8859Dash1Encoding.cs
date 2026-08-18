using System;
using System.Text;

namespace Ionic.Encoding
{
	// Token: 0x02000003 RID: 3
	public class Iso8859Dash1Encoding : Encoding
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000021EA File Offset: 0x000003EA
		public override string WebName
		{
			get
			{
				return "iso-8859-1";
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021F4 File Offset: 0x000003F4
		public override int GetBytes(char[] chars, int start, int count, byte[] bytes, int byteIndex)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", "null array");
			}
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", "null array");
			}
			if (start < 0)
			{
				throw new ArgumentOutOfRangeException("start");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("charCount");
			}
			if (chars.Length - start < count)
			{
				throw new ArgumentOutOfRangeException("chars");
			}
			if (byteIndex < 0 || byteIndex > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("byteIndex");
			}
			for (int i = 0; i < count; i++)
			{
				char c = chars[start + i];
				if (c >= 'ÿ')
				{
					bytes[byteIndex + i] = 63;
				}
				else
				{
					bytes[byteIndex + i] = (byte)c;
				}
			}
			return count;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000022A0 File Offset: 0x000004A0
		public override int GetChars(byte[] bytes, int start, int count, char[] chars, int charIndex)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", "null array");
			}
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", "null array");
			}
			if (start < 0)
			{
				throw new ArgumentOutOfRangeException("start");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("charCount");
			}
			if (bytes.Length - start < count)
			{
				throw new ArgumentOutOfRangeException("bytes");
			}
			if (charIndex < 0 || charIndex > chars.Length)
			{
				throw new ArgumentOutOfRangeException("charIndex");
			}
			for (int i = 0; i < count; i++)
			{
				chars[charIndex + i] = (char)bytes[i + start];
			}
			return count;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002336 File Offset: 0x00000536
		public override int GetByteCount(char[] chars, int index, int count)
		{
			return count;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002336 File Offset: 0x00000536
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return count;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000233C File Offset: 0x0000053C
		public override int GetMaxByteCount(int charCount)
		{
			return charCount;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000233C File Offset: 0x0000053C
		public override int GetMaxCharCount(int byteCount)
		{
			return byteCount;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002342 File Offset: 0x00000542
		public static int CharacterCount
		{
			get
			{
				return 256;
			}
		}
	}
}

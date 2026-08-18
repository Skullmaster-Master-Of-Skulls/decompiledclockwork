using System;
using System.IO;
using System.Text;

namespace System.IdentityModel
{
	// Token: 0x02000029 RID: 41
	internal abstract class CanonicalFormWriter
	{
		// Token: 0x06000131 RID: 305 RVA: 0x00005E18 File Offset: 0x00004018
		protected static void Base64EncodeAndWrite(Stream stream, byte[] workBuffer, char[] base64WorkBuffer, byte[] data)
		{
			if (data.Length / 3 * 4 + 4 > base64WorkBuffer.Length)
			{
				CanonicalFormWriter.EncodeAndWrite(stream, Convert.ToBase64String(data));
				return;
			}
			int count = Convert.ToBase64CharArray(data, 0, data.Length, base64WorkBuffer, 0, Base64FormattingOptions.None);
			CanonicalFormWriter.EncodeAndWrite(stream, workBuffer, base64WorkBuffer, count);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00005E58 File Offset: 0x00004058
		protected static void EncodeAndWrite(Stream stream, byte[] workBuffer, string s)
		{
			if (s.Length > workBuffer.Length)
			{
				CanonicalFormWriter.EncodeAndWrite(stream, s);
				return;
			}
			for (int i = 0; i < s.Length; i++)
			{
				char c = s[i];
				if (c >= '\u007f')
				{
					CanonicalFormWriter.EncodeAndWrite(stream, s);
					return;
				}
				workBuffer[i] = (byte)c;
			}
			stream.Write(workBuffer, 0, s.Length);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00005EB3 File Offset: 0x000040B3
		protected static void EncodeAndWrite(Stream stream, byte[] workBuffer, char[] chars)
		{
			CanonicalFormWriter.EncodeAndWrite(stream, workBuffer, chars, chars.Length);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00005EC0 File Offset: 0x000040C0
		protected static void EncodeAndWrite(Stream stream, byte[] workBuffer, char[] chars, int count)
		{
			if (count > workBuffer.Length)
			{
				CanonicalFormWriter.EncodeAndWrite(stream, chars, count);
				return;
			}
			for (int i = 0; i < count; i++)
			{
				char c = chars[i];
				if (c >= '\u007f')
				{
					CanonicalFormWriter.EncodeAndWrite(stream, chars, count);
					return;
				}
				workBuffer[i] = (byte)c;
			}
			stream.Write(workBuffer, 0, count);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005F0C File Offset: 0x0000410C
		private static void EncodeAndWrite(Stream stream, string s)
		{
			byte[] bytes = CanonicalFormWriter.Utf8WithoutPreamble.GetBytes(s);
			stream.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00005F30 File Offset: 0x00004130
		private static void EncodeAndWrite(Stream stream, char[] chars, int count)
		{
			byte[] bytes = CanonicalFormWriter.Utf8WithoutPreamble.GetBytes(chars, 0, count);
			stream.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x040000E6 RID: 230
		internal static readonly UTF8Encoding Utf8WithoutPreamble = new UTF8Encoding(false);
	}
}
